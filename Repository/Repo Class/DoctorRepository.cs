using BigBangDoctorPatient.Context;
using BigBangDoctorPatient.Models;
using BigBangDoctorPatient.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBangDoctorPatient.Repository.Repo_Class
{
    public class DoctorRepository : IDoctorRepository
    {
        public readonly DoctorPatientContext _context;

        public DoctorRepository(DoctorPatientContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _context.Doctors.Include(x=>x.Patients).Include(y=>y.Appointments).ToListAsync();
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctor.Doctor_Id);
            if (existingDoctor != null)
            {
                // Retain the existing image data and status
                doctor.ImageData = existingDoctor.ImageData;
                doctor.Status = existingDoctor.Status;

                _context.Entry(existingDoctor).CurrentValues.SetValues(doctor);
                await _context.SaveChangesAsync();
            }
        }




        public async Task DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DoctorExists(int id)
        {
            return await _context.Doctors.AnyAsync(e => e.Doctor_Id == id);
        }
    }
}
