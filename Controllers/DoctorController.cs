using BigBangDoctorPatient.Models;
using BigBangDoctorPatient.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BigBangDoctorPatient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorRepository.GetDoctor(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // POST: api/Doctor
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctorRequest([FromForm] Doctor doctor, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                return BadRequest("Image file is required.");
            }

            var imageData = await ConvertImageToByteArray(imageFile);
            doctor.ImageData = imageData;

            doctor.Status = "Pending"; // Set the status of the doctor to "Pending"

            await _doctorRepository.AddDoctor(doctor);

            // Send approval request to the admin
            SendApprovalRequestToAdmin(doctor.Doctor_Id);

            return CreatedAtAction("GetDoctor", new { id = doctor.Doctor_Id }, doctor);
        }

        private void SendApprovalRequestToAdmin(int doctorId)
        {
            // Implement the logic to send an approval request to the admin
        }

        // PUT: api/Doctor/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromForm] Doctor doctor)
        {
            if (id != doctor.Doctor_Id)
            {
                return BadRequest();
            }

            try
            {
                var existingDoctor = await _doctorRepository.GetDoctor(id);
                if (existingDoctor == null)
                {
                    return NotFound();
                }

                // Retain the existing image data
                doctor.ImageData = existingDoctor.ImageData;

                await _doctorRepository.UpdateDoctor(doctor);
            }
            catch
            {
                throw; // Rethrow the exception for now, but you can handle it as per your requirements
            }

            return NoContent();
        }


        private async Task<byte[]> ConvertImageToByteArray(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // DELETE: api/Doctor/5
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (!await _doctorRepository.DoctorExists(id))
            {
                return NotFound();
            }

            await _doctorRepository.DeleteDoctor(id);

            return NoContent();
        }

        // GET: api/Doctor/BySpecialization?specialization=xxx
        [HttpGet("BySpecialization")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await _doctorRepository.GetDoctors();
            var filteredDoctors = doctors.Where(d => d.Specialization != null && d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase));
            return Ok(filteredDoctors);
        }


        // GET: api/Doctor/ApprovedDoctors
        [HttpGet("ApprovedDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetApprovedDoctors()
        {
            var approvedDoctors = await _doctorRepository.GetDoctors();
            var filteredDoctors = approvedDoctors.Where(d => d.Status == "Approved");
            return Ok(filteredDoctors);
        }

    }
}
