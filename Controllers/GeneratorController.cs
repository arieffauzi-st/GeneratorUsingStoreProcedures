using GeneratorUsingStoreProcedures.DTOs;
using GeneratorUsingStoreProcedures.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorUsingStoreProcedures.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public GeneratorController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody] SubmitDataDto submitData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Validate data
                for (int i = 0; i < submitData.Payload.Count; i++)
                {
                    var item = submitData.Payload[i];
                    if ((i + 1) % 100 == 0 && item.HobbyName.ToLower() == "tidur")
                    {
                        return BadRequest($"Terdapat error pada baris {i + 1} tidak menyukai hobi tidur");
                    }
                }

                await _dataRepository.ClearAllDataAsync();
                await _dataRepository.AddPersonalRangeAsync(submitData.Payload);
                return Ok(new { success = true, message = "Data berhasil disimpan" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Terjadi kesalahan: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetData()
        {
            try
            {
                await _dataRepository.ClearAllDataAsync();
                return Ok(new { success = true, message = "Data berhasil direset" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Terjadi kesalahan: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGenderTotals()
        {
            var result = await _dataRepository.GetGenderTotalsAsync();
            return Ok(result);
        }
    }
}
