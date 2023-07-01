using BigBangDoctorPatient.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBangDoctorPatient.Context
{
    public class DoctorPatientContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BigBangDoctorPatient.Models.Admin>? Admin { get; set; }

        public DoctorPatientContext(DbContextOptions<DoctorPatientContext> options) : base(options)
        {

        }
    }
}
