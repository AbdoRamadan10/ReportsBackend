using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReportsBackend.Application.Services;
using ReportsBackend.Application.DTOs.Report;
using ReportsBackend.Domain.Helpers;
using System.Text;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Previewer;
using ReportsBackend.Api.Helpers;

namespace ReportsBackend.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    //[Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAll([FromQuery] FindOptions options)
        {
            var reports = await _reportService.GetAllAsync(options);
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> Get(int id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult<ReportDto>> Create([FromBody] ReportCreateDto dto)
        {
            var created = await _reportService.CreateAsync(dto);
            //return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportUpdateDto dto)
        {
            await _reportService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reportService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportReports([FromQuery] FindOptions options)
        {
            var reportsResult = await _reportService.GetAllAsync(options);
            var reports = reportsResult.Items;

            var csv = new StringBuilder();
            // Header
            csv.AppendLine("Id,Name,Description,Path,PrivilegeId,PrivilegeName");

            // Rows
            foreach (var report in reports)
            {
                csv.AppendLine($"{report.Id},\"{report.Name}\",\"{report.Description}\",\"{report.Path}\",{report.PrivilegeId},\"{report.PrivilegeName}\"");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "reports_export.csv");
        }

        [HttpGet("export-file")]
        public async Task<IActionResult> ExportReportsFile([FromQuery] FindOptions options, [FromQuery] string format = "pdf")
        {
            var reportsResult = await _reportService.GetAllAsync(options);
            var reports = reportsResult.Items.ToList();

            var columns = new List<ColumnDefinition<ReportDto>>
{
    new() { Header = "Id", ValueSelector = r => r.Id.ToString() },
    new() { Header = "Name", ValueSelector = r => r.Name },
    new() { Header = "Description", ValueSelector = r => r.Description },
    new() { Header = "Path", ValueSelector = r => r.Path },
    new() { Header = "PrivilegeId", ValueSelector = r => r.PrivilegeId.ToString() },
    new() { Header = "PrivilegeName", ValueSelector = r => r.PrivilegeName }
};

            if (format.ToLower() == "excel" || format.ToLower() == "xlsx")
            {
                var excelBytes = ExcelExportHelper.GenerateTableExcel(reports, columns, "Reports Export");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reports_export.xlsx");
            }
            else // default to PDF
            {
                var pdfBytes = PdfExportHelper.GenerateTablePdf(reports, columns, "Reports Export");
                return File(pdfBytes, "application/pdf", "reports_export.pdf");
            }
        }

    }
}
