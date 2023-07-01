using BigBangDoctorPatient.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    public int Appointment_Id { get; set; }
    public string? Appointment_Date { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
