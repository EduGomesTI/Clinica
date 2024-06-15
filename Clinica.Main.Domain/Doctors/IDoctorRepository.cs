namespace Clinica.Main.Domain.Doctors
{
    public interface IDoctorRepository
    {
        Task<Dictionary<string, DateTime>> GetDoctorScheduler(Guid? DoctorId, CancellationToken cancellationToken);
    }
}