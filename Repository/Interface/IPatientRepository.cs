using BigBangDoctorPatient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBangDoctorPatient.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
        Task<bool> PatientExists(int id);
    }
}
