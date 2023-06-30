﻿using System.ComponentModel.DataAnnotations;

namespace BigBangDoctorPatient.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        public string? Admin_Name { get; set; }
        public string? Admin_Password { get; set; }
    }
}
