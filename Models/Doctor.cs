using System.ComponentModel.DataAnnotations;

namespace BigBangDoctorPatient.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }
        public string? Doctor_Name { get; set; }
        public string? Specialization { get; set; }
        public string? Doctor_Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Doctor_PhNo { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public byte[]? ImageData { get; set; }
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

    }
}
