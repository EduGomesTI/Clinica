namespace Clinica.Main.Domain.Patients
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync(CancellationToken cancellationToken, bool page = false, int pageStart = 1, int pageSize = 1);

        Task<Patient?> GetPatientByIdAsync(Guid? id, CancellationToken cancellationToken);

        Task<Patient?> GetPatientByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken);
    }
}