using BigBangDoctorPatient.Models;
using BigBangDoctorPatient.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BigBangDoctorPatient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _patientRepository.GetPatients();
            return Ok(patients);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientRepository.GetPatient(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // POST: api/Patient

        
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            await _patientRepository.AddPatient(patient);

            return CreatedAtAction("GetPatient", new { id = patient.Patient_Id }, patient);
        }

        // PUT: api/Patient/5

        [Authorize(Roles = "Admin,Patient")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.Patient_Id)
            {
                return BadRequest();
            }

            try
            {
                await _patientRepository.UpdatePatient(patient);
            }
            catch
            {
                if (!await _patientRepository.PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Patient/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatients(string query)
        {
            var patients = await _patientRepository.SearchPatients(query);

            if (patients == null || !patients.Any())
            {
                return NotFound();
            }


            return Ok(patients);
        }

        // GET: api/Patient/searchByName
        [HttpGet("searchByName")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatientsByName(string name)
        {
            var patients = await _patientRepository.SearchPatientsByName(name);

            if (patients == null || patients.Count == 0)
            {
                return NotFound();
            }

            return Ok(patients);
        }

        // DELETE: api/Patient/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientRepository.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientRepository.DeletePatient(patient);

            return NoContent();
        }
    }
}
