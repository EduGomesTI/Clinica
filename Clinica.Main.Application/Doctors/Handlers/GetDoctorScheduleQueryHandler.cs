using Clinica.Base.Domain;
using Clinica.Main.Application.Doctors.Queries;
using Clinica.Main.Application.Doctors.Responses;
using Clinica.Main.Domain.Doctors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinica.Main.Application.Doctors.Handlers
{
    internal sealed class GetDoctorScheduleQueryHandler : IRequestHandler<GetDoctorScheduleByDoctorIdQuery, ValueResult<IEnumerable<GetDoctorScheduleResponse>>>
    {
        private readonly IDoctorRepository _repository;
        private readonly ILogger<GetDoctorScheduleQueryHandler> _logger;

        public GetDoctorScheduleQueryHandler(IDoctorRepository repository, ILogger<GetDoctorScheduleQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ValueResult<IEnumerable<GetDoctorScheduleResponse>>> Handle(GetDoctorScheduleByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDoctorScheduleQueryHandler.Handle - Getting doctor schedule by doctor id");
            var schedule = await _repository.GetDoctorScheduler(request.Id, cancellationToken);

            var response = schedule.Select(s => new GetDoctorScheduleResponse(s.Key, s.Value));

            return ValueResult<IEnumerable<GetDoctorScheduleResponse>>.Success(response);
        }
    }
}