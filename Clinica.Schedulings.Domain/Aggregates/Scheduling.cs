using Clinica.Base.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinica.Schedulings.Domain.Aggregates
{
    public sealed class Scheduling : AggregateRoot
    {
        public Guid PatientId { get; private set; }

        public Guid DoctorId { get; private set; }

        public DateTime DateScheduling { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public string? Observation { get; private set; }

        private Scheduling()
        { }

        private Scheduling(Guid patientId, Guid doctorId, DateTime dateScheduling)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            DateScheduling = dateScheduling;
            Status = AppointmentStatus.Scheduled;
        }

        public static ValueResult<Scheduling> Create(Guid patientId, Guid doctorId, DateTime dateScheduling)
        {
            var errors = new List<ValueErrorDetail>();
            if (patientId == Guid.Empty)
            {
                errors.Add("O paciente é obrigatório.");
            }

            if (doctorId == Guid.Empty)
            {
                errors.Add("O médico é obrigatório.");
            }

            if (dateScheduling < DateTime.Now)
            {
                errors.Add("A data da consulta não pode ser menor que a data atual.");
            }

            var roundedDate = RoundToNearestHalfHour(dateScheduling).Value;

            return errors.Count != 0
                ? ValueResult<Scheduling>.Failure(errors)
                : ValueResult<Scheduling>.Success(new Scheduling(patientId, doctorId, roundedDate));
        }

        public void ChangeObservation(string observation)
        {
            Observation = observation;
        }

        public void ChangeDateScheduling(DateTime dateScheduling)
        {
            var roundedDate = RoundToNearestHalfHour(dateScheduling);
            DateScheduling = roundedDate.Value;
        }

        public ValueResult<Scheduling> Confirm()
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                return ValueResult<Scheduling>.Failure("Você só pode confirmar uma consulta se ela estiver com o status 'Agendada'");
            }

            Status = AppointmentStatus.Confirmed;
            Observation = $"Consulta confirmada com sucesso na data {DateScheduling}";
            return ValueResult<Scheduling>.Success(this);
        }

        public ValueResult<Scheduling> CancelByPatient()
        {
            var validStatuses = new[] { AppointmentStatus.Scheduled, AppointmentStatus.Confirmed };
            if (!validStatuses.Contains(Status))
            {
                return ValueResult<Scheduling>.Failure("Você só pode cancelar uma consulta se ela estiver com o status 'Agendada' ou 'Confirmada'");
            }

            Status = AppointmentStatus.CancelledByPatient;
            Observation = $"Consulta do dia {DateScheduling} cancelada pelo paciente!";
            return ValueResult<Scheduling>.Success(this);
        }

        public ValueResult<Scheduling> CancelByDoctor()
        {
            var validStatuses = new[] { AppointmentStatus.Scheduled, AppointmentStatus.Confirmed };
            if (!validStatuses.Contains(Status))
            {
                return ValueResult<Scheduling>.Failure("Você só pode cancelar uma consulta se ela estiver com o status 'Agendada' ou 'Confirmada'");
            }

            Status = AppointmentStatus.CancelledByDoctor;
            Observation = $"Consulta do dia {DateScheduling} cancelada pelo médico!";
            return ValueResult<Scheduling>.Success(this);
        }

        public ValueResult<Scheduling> Complete()
        {
            if (Status != AppointmentStatus.Confirmed)
            {
                return ValueResult<Scheduling>.Failure("Você só pode realizar uma consulta se ela estiver com o status 'Confirmado'");
            }

            Status = AppointmentStatus.Completed;
            Observation = $"Consulta do dia {DateScheduling} realizada com sucesso!";
            return ValueResult<Scheduling>.Success(this);
        }

        public ValueResult<Scheduling> NoShow()
        {
            if (Status != AppointmentStatus.Confirmed)
            {
                return ValueResult<Scheduling>.Failure("Você só pode realizar uma consulta se ela estiver com o status 'Agendada'");
            }

            Status = AppointmentStatus.NoShow;
            Observation = $"Você compareceu à consulta do dia {DateScheduling}.";
            return ValueResult<Scheduling>.Success(this);
        }

        private static ValueResult<DateTime> RoundToNearestHalfHour(DateTime date)
        {
            if (date.Hour < 8 || date.Hour > 20)
            {
                return ValueResult<DateTime>.Failure("O horário da consulta deve ser entre 8h e 20h.");
            }

            var minute = date.Minute >= 15 && date.Minute < 45 ? 30 : 0;
            var hour = date.Hour + (date.Minute >= 45 ? 1 : 0);

            return ValueResult<DateTime>.Success(new DateTime(date.Year, date.Month, date.Day, hour, minute, 0, DateTimeKind.Utc));
        }
    }
}