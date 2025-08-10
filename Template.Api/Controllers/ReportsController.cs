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

            if (format.ToLower() == "excel" || format.ToLower() == "xlsx" || format.ToLower() == "csv")
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

        //[HttpPost("execute")]
        //public async Task<List<object>> GetEmployees(int reportId, Dictionary<string, object> parameters = null,
        //     Dictionary<string, object> filterParameters = null
        //    )
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        new NotFoundException("Report", reportId.ToString());

        //    var sql = report.Query;

        //    var oracleParameters = new List<OracleParameter>();



        //    foreach (var param in report.Parameters)
        //    {
        //        oracleParameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }

        //    if (parameters != null)
        //    {
        //        foreach (var param in oracleParameters)
        //        {

        //            if (parameters.TryGetValue(param.ParameterName, out var value))
        //            {
        //                param.Value = value;
        //            }
        //            else
        //            {
        //                // If the parameter is not provided, use the default value
        //                param.Value = param.Value ?? DBNull.Value;
        //            }
        //        }
        //    }


        //    var results = await _oracleExecutor.ExecuteQueryAsync(sql, oracleParameters.ToArray());


        //    if (filterParameters != null && filterParameters.Any())
        //    {
        //        results.Where(row =>
        //        {
        //            foreach (var filter in filterParameters)
        //            {
        //                if (!row.ContainsKey(filter.Key))
        //                    return false; // Column doesn't exist

        //                var rowValue = row[filter.Key];
        //                var filterValue = filter.Value;

        //                // Handle null comparisons
        //                if (rowValue == null && filterValue == null) continue;
        //                if (rowValue == null || filterValue == null) return false;

        //                // Simple equality comparison (you can enhance this)
        //                if (!rowValue.ToString().Equals(filterValue.ToString(), StringComparison.OrdinalIgnoreCase))
        //                    return false;
        //            }
        //            return true;
        //        }).ToList();
        //    }




        //    var result = new List<object>();
        //    foreach (var row in results)
        //    {
        //        var item = new Dictionary<string, object>();
        //        foreach (var column in row)
        //        {
        //            item[column.Key] = column.Value;
        //        }
        //        result.Add(item);
        //    }



        //    return result;





        //    //    var sql = "select * from \"Reports\" where \"Name\" = :name_id";
        //    //    var parameters = new OracleParameter[]
        //    //    {
        //    //new OracleParameter("name_id", OracleDbType.NVarchar2) { Value = 'a' }
        //    //    };

        //    //    var results = await _oracleExecutor.ExecuteQueryAsync(sql, parameters);

        //    //    return results.Select(row => new ReportDto
        //    //    {
        //    //        Id = Convert.ToInt32(row["Id"]),
        //    //        Name = row["Name"].ToString(),
        //    //        Description = row["Description"].ToString(),
        //    //        Path = row["Path"].ToString(),
        //    //        PrivilegeId = Convert.ToInt32(row["PrivilegeId"]),
        //    //    }).ToList();
        //}












        //[HttpPost("execute")]
        //public async Task<List<object>> GetEmployees(int reportId, [FromBody] ExecuteReportRequest request = null

        //    )
        //{


        //    //var parameters = request?.Parameters;
        //    //var filterParameters = request?.FilterParameters;


        //    // Convert all parameter keys to uppercase
        //    var parameters = request?.Parameters?.ToDictionary(
        //        kvp => kvp.Key.ToUpperInvariant(),
        //        kvp => kvp.Value,
        //        StringComparer.OrdinalIgnoreCase);

        //    // Convert all filter parameter keys to uppercase
        //    var filterParameters = request?.FilterParameters?.ToDictionary(
        //        kvp => kvp.Key.ToUpperInvariant(),
        //        kvp => kvp.Value,
        //        StringComparer.OrdinalIgnoreCase);




        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        new NotFoundException("Report", reportId.ToString());

        //    var sql = report.Query;

        //    var oracleParameters = new List<OracleParameter>();



        //    foreach (var param in report.Parameters)
        //    {
        //        oracleParameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }

        //    if (parameters != null)
        //    {
        //        foreach (var param in oracleParameters)
        //        {

        //            if (parameters.TryGetValue(param.ParameterName, out var value))
        //            {
        //                param.Value = value;
        //            }
        //            else
        //            {
        //                // If the parameter is not provided, use the default value
        //                param.Value = param.Value ?? DBNull.Value;
        //            }
        //        }
        //    }


        //    var results = await _oracleExecutor.ExecuteQueryAsync(sql, oracleParameters.ToArray());


        //    if (filterParameters != null && filterParameters.Any())
        //    {
        //        results = results.Select(row =>
        //        {
        //            foreach (var filter in filterParameters)
        //            {
        //                if (!row.ContainsKey(filter.Key))
        //                    return null; // Column doesn't exist
        //                var rowValue = row[filter.Key];
        //                var filterValue = filter.Value;
        //                // Handle null comparisons
        //                if (rowValue == null && filterValue == null) continue;
        //                if (rowValue == null || filterValue == null) return null;
        //                // Simple equality comparison (you can enhance this)
        //                if (!rowValue.ToString().Equals(filterValue.ToString(), StringComparison.OrdinalIgnoreCase))
        //                    return null;
        //            }
        //            return row;
        //        }).Where(r => r != null).ToList();
        //    }




        //    var result = new List<object>();
        //    foreach (var row in results)
        //    {
        //        var item = new Dictionary<string, object>();
        //        foreach (var column in row)
        //        {
        //            item[column.Key] = column.Value;
        //        }
        //        result.Add(item);
        //    }



        //    return result;





        //    //    var sql = "select * from \"Reports\" where \"Name\" = :name_id";
        //    //    var parameters = new OracleParameter[]
        //    //    {
        //    //new OracleParameter("name_id", OracleDbType.NVarchar2) { Value = 'a' }
        //    //    };

        //    //    var results = await _oracleExecutor.ExecuteQueryAsync(sql, parameters);

        //    //    return results.Select(row => new ReportDto
        //    //    {
        //    //        Id = Convert.ToInt32(row["Id"]),
        //    //        Name = row["Name"].ToString(),
        //    //        Description = row["Description"].ToString(),
        //    //        Path = row["Path"].ToString(),
        //    //        PrivilegeId = Convert.ToInt32(row["PrivilegeId"]),
        //    //    }).ToList();
        //}


        //[HttpPost("execute")]
        //public async Task<ActionResult<List<object>>> Execute(int reportId)
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        throw new NotFoundException("Report", reportId.ToString());
        //    var sql = report.Query;

        //    var parameters = new List<OracleParameter>();

        //    foreach (var param in report.Parameters)
        //    {
        //        parameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }



        //    var results = await _oracleExecutor.ExecuteQueryAsync(sql, parameters.ToArray());

        //    return Ok(results);


        //}

        //[HttpPost("execute-paginated")]
        //public async Task<ActionResult<List<object>>> ExecutePaginated(int reportId, [FromQuery] FindOptions options)
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        throw new NotFoundException("Report", reportId.ToString());
        //    var sql = report.Query;

        //    var parameters = new List<OracleParameter>();

        //    foreach (var param in report.Parameters)
        //    {
        //        parameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }



        //    var results = await _oracleExecutor.ExecuteQueryAsyncPaginated(sql, options, parameters.ToArray());

        //    return Ok(results);


        //}

        //[HttpPost("execute-paginated-sort")]
        //public async Task<PaginatedResult<object>> ExecutePaginatedSort(int reportId, [FromQuery] FindOptions options)
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        throw new NotFoundException("Report", reportId.ToString());
        //    var sql = report.Query;

        //    var parameters = new List<OracleParameter>();

        //    foreach (var param in report.Parameters)
        //    {
        //        parameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }

        //    var totalCount = await _oracleExecutor.GetTotalCountAsync(sql, parameters.ToArray());



        //    var results = await _oracleExecutor.ExecuteQueryAsyncPaginatedSort(sql, options, parameters.ToArray());

        //    return new PaginatedResult<object>
        //    {
        //        Items = results,
        //        PageNumber = options.PageNumber,
        //        PageSize = options.PageSize,
        //        TotalCount = totalCount
        //    };




        //}

        //[HttpPost("execute-paginated-sort-filter")]
        //public async Task<PaginatedResult<object>> ExecutePaginatedSortFilter(int reportId, [FromQuery] FindOptions options)
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        throw new NotFoundException("Report", reportId.ToString());
        //    var sql = report.Query;

        //    var parameters = new List<OracleParameter>();

        //    foreach (var param in report.Parameters)
        //    {
        //        parameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }

        //    var totalCount = await _oracleExecutor.GetTotalCountAsync(sql, parameters.ToArray());



        //    var results = await _oracleExecutor.ExecuteQueryAsyncPaginatedSortFilter(sql, options, parameters.ToArray());

        //    return new PaginatedResult<object>
        //    {
        //        Items = results,
        //        PageNumber = options.PageNumber,
        //        PageSize = options.PageSize,
        //        TotalCount = totalCount
        //    };




        //}

        //[HttpPost("execute-grid")]
        //public async Task<ActionResult<List<object>>> ExecuteGrid(int reportId, GridRequest options)
        //{
        //    var report = await _reportService.GetByIdAsync(reportId);
        //    if (report == null)
        //        throw new NotFoundException("Report", reportId.ToString());
        //    var sql = report.Query;

        //    var parameters = new List<OracleParameter>();

        //    foreach (var param in report.Parameters)
        //    {
        //        parameters.Add(new OracleParameter(param.Name, param.DataType) { Value = param.DefaultValue });
        //    }

        //    var totalCount = await _oracleExecutor.GetTotalCountAsync(sql, parameters.ToArray());



        //    var results = await _oracleExecutor.ExecuteGridQueryAsync(sql, options, parameters.ToArray());

        //    return Ok(results);





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


    }
}

