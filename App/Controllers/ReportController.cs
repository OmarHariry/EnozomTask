using App.DataTransferObject;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{

    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _service;

        public ReportController(ReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetBookWithCopies()
        {
            var report = await _service.GetReport();
            return Ok(report);
        }
    }
}
