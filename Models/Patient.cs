using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBangDoctorPatient.Models
{
    public class Patient
    {
        [Key]
        public int Patient_Id { get; set; }
        public string? Patient_Name { get; set; }
        public string? Disease { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Patient_PhNo { get; set; }
        public string? Password { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
