using BigBangDoctorPatient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBangDoctorPatient.Repository.Interface
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctor(int id);
        Task AddDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
        Task DeleteDoctor(int id);
        Task<bool> DoctorExists(int id);
    }
}
