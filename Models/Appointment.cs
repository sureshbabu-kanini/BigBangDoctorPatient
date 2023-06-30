using System.ComponentModel.DataAnnotations;

namespace BigBangDoctorPatient.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }
        public string? Appointment_Date { get; set; }

        public int Patient_Id { get; set; }
        public Patient? Patient { get; set; }

        public int Doctor_Id { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
