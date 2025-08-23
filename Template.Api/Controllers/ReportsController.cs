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
using Oracle.ManagedDataAccess.Client;
using ReportsBackend.Infrastracture.Helpers;
using ReportsBackend.Domain.Exceptions;
using ReportsBackend.Application.DTOs.Role;
using ReportsBackend.Domain.AG_Grid;
using ReportsBackend.Application.DTOs.ReportColumn;
using ReportsBackend.Application.DTOs.ReportParameter;

namespace ReportsBackend.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    //[Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;
        private readonly OracleSqlExecutor _oracleExecutor;


        public ReportsController(ReportService reportService, OracleSqlExecutor oracleExecutor)
        {
            _reportService = reportService;
            _oracleExecutor = oracleExecutor;


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

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<ReportDto>> Create([FromBody] ReportCreateDto dto)
        {
            var created = await _reportService.CreateAsync(dto);
            //return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            return Ok(created);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportUpdateDto dto)
        {
            await _reportService.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reportService.DeleteAsync(id);
            return NoContent();
        }

        //[HttpGet("export")]
        //public async Task<IActionResult> ExportReports([FromQuery] FindOptions options)
        //{
        //    var reportsResult = await _reportService.GetAllAsync(options);
        //    var reports = reportsResult.Items;

        //    var csv = new StringBuilder();
        //    // Header
        //    csv.AppendLine("Id,Name,Description,Path,PrivilegeId,PrivilegeName");

        //    // Rows
        //    foreach (var report in reports)
        //    {
        //        csv.AppendLine($"{report.Id},\"{report.Name}\",\"{report.Description}\",\"{report.Path}\",{report.PrivilegeId},\"{report.PrivilegeName}\"");
        //    }

        //    var bytes = Encoding.UTF8.GetBytes(csv.ToString());
        //    return File(bytes, "text/csv", "reports_export.csv");
        //}

        //[HttpGet("export-file")]
        //public async Task<IActionResult> ExportReportsFile([FromQuery] FindOptions options, [FromQuery] string format = "pdf")
        //{
        //    var reportsResult = await _reportService.GetAllAsync(options);
        //    var reports = reportsResult.Items.ToList();

        //    var columns = new List<ColumnDefinition<ReportDto>>
        //        {
        //            new() { Header = "Id", ValueSelector = r => r.Id.ToString() },
        //            new() { Header = "Name", ValueSelector = r => r.Name },
        //            new() { Header = "Description", ValueSelector = r => r.Description },
        //            new() { Header = "Path", ValueSelector = r => r.Path },
        //            new() { Header = "PrivilegeId", ValueSelector = r => r.PrivilegeId.ToString() },
        //            new() { Header = "PrivilegeName", ValueSelector = r => r.PrivilegeName }
        //        };

        //    if (format.ToLower() == "excel" || format.ToLower() == "xlsx" || format.ToLower() == "csv")
        //    {
        //        var excelBytes = ExcelExportHelper.GenerateTableExcel(reports, columns, "Reports Export");
        //        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reports_export.xlsx");
        //    }
        //    else // default to PDF
        //    {
        //        var pdfBytes = PdfExportHelper.GenerateTablePdf(reports, columns, "Reports Export");
        //        return File(pdfBytes, "application/pdf", "reports_export.pdf");
        //    }
        //}

        [HttpPost("execute-grid-final")]
        public async Task<ActionResult<List<object>>> ExecuteGridFinal(int reportId, GridRequest options)
        {
            var report = await _reportService.GetByIdAsync(reportId);
            if (report == null)
                throw new NotFoundException("Report", reportId.ToString());
            var sql = report.Query;

            var sqlParameters = report.Parameters;
            sqlParameters = sqlParameters.OrderBy(p => p.Sort).ToList(); // Ensure parameters are sorted by name
            var oracleParameters = new List<OracleParameter>();
            int index = 0;

            foreach (var param in sqlParameters)
            {
                //if (param.DataType.ToLower() == "number")


                // Ensure the sort of oracleParameters and add them in the same order as in the query  
                oracleParameters.Add(new OracleParameter(param.Name, param.DataType) { Value = options.SqlParameters[index] ?? param.DefaultValue });
                index++;
            }

            var totalCount = await _oracleExecutor.GetTotalCountAsync(sql, oracleParameters.ToArray());
            var results = await _oracleExecutor.ExecuteGridQueryAsyncFinal(sql, options, oracleParameters.ToArray());

            return Ok(results);
        }


        [HttpGet("{reportId}/columns")]
        public async Task<ActionResult<List<ReportColumnDto>>> GetColumnsByReportId(int reportId)
        {

            var columns = await _reportService.GetColumnsByReportIdAsync(reportId);
            return Ok(columns);
        }


        [HttpPost("{reportId}/columns")]
        public async Task<ActionResult<List<ReportColumnDto>>> CreateColumnsForReport(int reportId, List<ReportColumnCreateDto> columns)
        {

            await _reportService.CreateColumnsAsync(reportId, columns);
            return Ok(columns);

        }

        [HttpPut("{reportId}/columns/{columnId}")]
        public async Task<IActionResult> UpdateColumn(int reportId, int columnId, [FromBody] ReportColumnCreateDto dto)
        {
            await _reportService.UpdateColumnAsync(reportId, columnId, dto);
            return NoContent();
        }


        [HttpPost("{reportId}/export-result")]
        public async Task<IActionResult> ExportReportResult(
            int reportId,
            [FromBody] GridRequest options,
            [FromQuery] string format = "excel")
        {
            options.EndRow = options.EndRow = int.MaxValue; // Set a high limit to get all results
            var report = await _reportService.GetByIdAsync(reportId);
            if (report == null)
                throw new NotFoundException("Report", reportId.ToString());

            var sql = report.Query;
            var sqlParameters = report.Parameters?.OrderBy(p => p.Sort).ToList() ?? new List<ReportParameterDto>();
            var oracleParameters = new List<OracleParameter>();
            int index = 0;

            foreach (var param in sqlParameters)
            {
                var value = (options.SqlParameters != null && options.SqlParameters.Count > index)
                    ? options.SqlParameters[index]
                    : param.DefaultValue;
                oracleParameters.Add(new OracleParameter(param.Name, param.DataType) { Value = value });
                index++;
            }

            var response = await _oracleExecutor.ExecuteGridQueryAsyncFinal(sql, options, oracleParameters.ToArray());

            // If no results, return empty file
            if (response == null || response.Rows == null || response.Rows.Count == 0)
                return NoContent();

            // Get headers from first row
            var headers = response.Rows.First().Keys.ToList();

            if (format.ToLower() == "pdf")
            {
                var pdfBytes = PdfExportHelper.GenerateDynamicTablePdf(response.Rows, headers, $"Report {report.Name} Export");
                return File(pdfBytes, "application/pdf", $"report_{reportId}_result.pdf");
            }
            else // default to Excel
            {
                var excelBytes = ExcelExportHelper.GenerateDynamicTableExcel(response.Rows, headers, $"Report {report.Name} Export");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"report_{reportId}_result.xlsx");
            }
        }







    }
}

