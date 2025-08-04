using Oracle.ManagedDataAccess.Client;
using ReportsBackend.Domain.AG_Grid;
using ReportsBackend.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Helpers
{
    public class OracleSqlExecutor
    {
        private readonly string _connectionString;

        public OracleSqlExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> GetTotalCountAsync(string sql, params OracleParameter[] parameters)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand($"SELECT COUNT(*) FROM ({sql})", connection))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        // For queries that return results (SELECT)
        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsync(string sql, params OracleParameter[] parameters)
        {
            var results = new List<Dictionary<string, object>>();

            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(sql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            results.Add(row);
                        }
                    }
                }
            }

            return results;
        }

        // For queries that return results (SELECT) Modifed With FindOptions
        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsyncPaginated(
            string sql,
            FindOptions? findOptions = null,
            params OracleParameter[] parameters

            )
        {
            var results = new List<Dictionary<string, object>>();

            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Apply pagination if FindOptions is provided
                string paginatedSql = sql;
                if (findOptions != null)
                {
                    int offset = (findOptions.PageNumber - 1) * findOptions.PageSize;
                    paginatedSql = $@"
                SELECT * FROM (
                    SELECT a.*, ROWNUM rn FROM (
                        {sql}
                    ) a
                    WHERE ROWNUM <= {offset + findOptions.PageSize}
                )
                WHERE rn > {offset}";
                }

                using (var command = new OracleCommand(paginatedSql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            results.Add(row);
                        }
                    }
                }
            }

            return results;
        }


        // For queries that return results (SELECT) Modified With FindOptions and Sorting
        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsyncPaginatedSort(string sql,
            FindOptions? findOptions = null,
            params OracleParameter[] parameters)
        {

            if (!string.IsNullOrEmpty(findOptions.SortBy))
            {
                sql += $" ORDER BY {findOptions.SortBy} {(findOptions.SortDescending ? "DESC" : "ASC")}";
            }

            if (findOptions != null)
            {
                int offset = (findOptions.PageNumber - 1) * findOptions.PageSize;
                sql = $@"
                          {sql}
                    OFFSET {offset} ROWS FETCH NEXT {findOptions.PageSize} ROWS ONLY";
            }


            var results = new List<Dictionary<string, object>>();

            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(sql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            results.Add(row);
                        }
                    }
                }
            }

            return results;
        }

        // For queries that return results (SELECT) Modified With FindOptions and Sorting and filtering
        public async Task<List<Dictionary<string, object>>> ExecuteQueryAsyncPaginatedSortFilter(
    string baseSql,
    FindOptions? findOptions = null,
    params OracleParameter[] parameters)
        {
            var results = new List<Dictionary<string, object>>();
            var combinedParameters = new List<OracleParameter>(parameters);

            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                string finalSql = baseSql;
                string whereClause = "";

                // Build WHERE clause from filters
                if (findOptions?.Filters != null && findOptions.Filters.Any())
                {
                    var filterConditions = new List<string>();
                    int paramIndex = 0;

                    foreach (var filter in findOptions.Filters)
                    {
                        string paramName = $":filter_{paramIndex}";
                        string condition;

                        switch (filter.Operator.ToLower())
                        {
                            case "equals":
                                condition = $"{filter.ColumnName} = {paramName}";
                                combinedParameters.Add(new OracleParameter(paramName, filter.Value));
                                break;

                            case "contains":
                                condition = $"{filter.ColumnName} LIKE '%' || {paramName} || '%'";
                                combinedParameters.Add(new OracleParameter(paramName, filter.Value));
                                break;

                            case "startswith":
                                condition = $"{filter.ColumnName} LIKE {paramName} || '%'";
                                combinedParameters.Add(new OracleParameter(paramName, filter.Value));
                                break;

                            case "endswith":
                                condition = $"{filter.ColumnName} LIKE '%' || {paramName}";
                                combinedParameters.Add(new OracleParameter(paramName, filter.Value));
                                break;

                            default:
                                throw new ArgumentException($"Unsupported operator: {filter.Operator}");
                        }

                        filterConditions.Add(condition);
                        paramIndex++;
                    }

                    whereClause = " WHERE " + string.Join(" AND ", filterConditions);
                }

                // Add sorting (ORDER BY)
                string orderByClause = "";
                if (!string.IsNullOrEmpty(findOptions?.SortBy))
                {
                    orderByClause = $" ORDER BY {findOptions.SortBy} {(findOptions.SortDescending ? "DESC" : "ASC")}";
                }

                // Apply pagination (Oracle ROWNUM)
                string paginatedSql = finalSql + whereClause + orderByClause;
                if (findOptions != null)
                {
                    int offset = (findOptions.PageNumber - 1) * findOptions.PageSize;
                    paginatedSql = $@"
                SELECT * FROM (
                    SELECT a.*, ROWNUM rn FROM (
                        {finalSql + whereClause + orderByClause}
                    ) a
                    WHERE ROWNUM <= {offset + findOptions.PageSize}
                )
                WHERE rn > {offset}";
                }

                // Execute query
                using (var command = new OracleCommand(paginatedSql, connection))
                {
                    if (combinedParameters.Any())
                        command.Parameters.AddRange(combinedParameters.ToArray());

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            results.Add(row);
                        }
                    }
                }
            }

            return results;
        }

        public async Task<GridResponse<Dictionary<string, object>>> ExecuteGridQueryAsync(
    string baseSql,
    GridRequest gridRequest,
    params OracleParameter[] parameters)
        {
            var response = new GridResponse<Dictionary<string, object>>();
            var combinedParameters = new List<OracleParameter>(parameters);
            string whereClause = "";
            string orderByClause = "";

            // 1. Build WHERE clause from FilterModel
            if (gridRequest.FilterModel != null && gridRequest.FilterModel.Any())
            {
                var filterConditions = new List<string>();
                int paramIndex = 0;

                foreach (var filter in gridRequest.FilterModel)
                {
                    string columnName = filter.Key; // Column ID (e.g., "name")
                    FilterModel filterModel = filter.Value;
                    string paramName = $":filter_{paramIndex}";

                    switch (filterModel.FilterType?.ToLower())
                    {
                        case "equals":
                            filterConditions.Add($"{columnName} = {paramName}");
                            combinedParameters.Add(new OracleParameter(paramName, filterModel.Filter));
                            break;

                        case "contains":
                            filterConditions.Add($"{columnName} LIKE '%' || {paramName} || '%'");
                            combinedParameters.Add(new OracleParameter(paramName, filterModel.Filter));
                            break;

                        case "notcontains":
                            filterConditions.Add($"{columnName} NOT LIKE '%' || {paramName} || '%'");
                            combinedParameters.Add(new OracleParameter(paramName, filterModel.Filter));
                            break;

                        case "startswith":
                            filterConditions.Add($"{columnName} LIKE {paramName} || '%'");
                            combinedParameters.Add(new OracleParameter(paramName, filterModel.Filter));
                            break;

                        case "endswith":
                            filterConditions.Add($"{columnName} LIKE '%' || {paramName}");
                            combinedParameters.Add(new OracleParameter(paramName, filterModel.Filter));
                            break;

                        default:
                            throw new NotSupportedException($"Filter type '{filterModel.FilterType}' is not supported.");
                    }

                    paramIndex++;
                }

                whereClause = " WHERE " + string.Join(" AND ", filterConditions);
            }

            // 2. Build ORDER BY clause from SortModel
            if (gridRequest.SortModel != null && gridRequest.SortModel.Any())
            {
                var sortClauses = gridRequest.SortModel
                    .Select(sort => $"{sort.ColId} {sort.Sort.ToUpper()}");
                orderByClause = " ORDER BY " + string.Join(", ", sortClauses);
            }

            // 3. Build paginated SQL (Oracle ROWNUM)
            int pageSize = gridRequest.EndRow - gridRequest.StartRow;
            string paginatedSql = $@"
        SELECT * FROM (
            SELECT a.*, ROWNUM rn FROM (
                {baseSql + whereClause + orderByClause}
            ) a
            WHERE ROWNUM <= {gridRequest.EndRow}
        )
        WHERE rn > {gridRequest.StartRow}";

            // 4. Execute the query
            var rows = new List<Dictionary<string, object>>();
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand(paginatedSql, connection))
                {
                    if (combinedParameters.Any())
                        command.Parameters.AddRange(combinedParameters.ToArray());

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            rows.Add(row);
                        }
                    }
                }

                // 5. Get total row count (for LastRow)
                string countSql = $"SELECT COUNT(*) FROM ({baseSql + whereClause})";
                using (var countCommand = new OracleCommand(countSql, connection))
                {
                    if (combinedParameters.Any())
                        countCommand.Parameters.AddRange(combinedParameters.ToArray());
                    response.LastRow = Convert.ToInt32(await countCommand.ExecuteScalarAsync());
                }
            }

            response.Rows = rows;
            return response;
        }

        public async Task<GridResponse<Dictionary<string, object>>> ExecuteGridQueryAsyncFinal(
    string baseSql,
    GridRequest gridRequest,
    params OracleParameter[] parameters)
        {
            var response = new GridResponse<Dictionary<string, object>>();
            var combinedParameters = new List<OracleParameter>(parameters);

            // 1. Build WHERE clause
            string whereClause = BuildWhereClause(gridRequest, combinedParameters);

            // 2. Build ORDER BY clause
            string orderByClause = BuildOrderByClause(gridRequest);

            // 3. Get total count (for LastRow)
            int totalRows = await GetTotalCountAsync(baseSql, whereClause, combinedParameters);
            response.LastRow = totalRows;

            // 4. Apply pagination and execute query
            string paginatedSql = $@"
        SELECT * FROM (
            SELECT a.*, ROWNUM rn FROM (
                {baseSql}{whereClause}{orderByClause}
            ) a
            WHERE ROWNUM <= {gridRequest.EndRow}
        )
        WHERE rn > {gridRequest.StartRow}";

            response.Rows = await ExecuteQueryAsyncGrid(paginatedSql, combinedParameters);
            return response;
        }

        private string BuildWhereClause(GridRequest request, List<OracleParameter> parameters)
        {
            if (request.FilterModel == null || !request.FilterModel.Any())
                return "";

            var conditions = new List<string>();
            int paramIndex = 0;



            foreach (var filter in request.FilterModel)
            {
                string column = filter.Key;
                var model = filter.Value;
                string paramName = $":filter_{paramIndex}";

                switch (model.Type?.ToLower())
                {
                    case "equals":
                        conditions.Add($"{column} = {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "contains":
                        conditions.Add($"{column} LIKE '%' || {paramName} || '%'");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "notcontains":
                        conditions.Add($"{column} NOT LIKE '%' || {paramName} || '%'");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "startswith":
                        conditions.Add($"{column} LIKE {paramName} || '%'");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "endswith":
                        conditions.Add($"{column} LIKE '%' || {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "greaterthan":
                        conditions.Add($"{column} > {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "lessthan":
                        conditions.Add($"{column} < {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "greaterthanorequals":
                        conditions.Add($"{column} >= {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;

                    case "lessthanorequals":
                        conditions.Add($"{column} <= {paramName}");
                        parameters.Add(new OracleParameter(paramName, model.Filter));
                        break;



                    // Add more filter types as needed
                    default:
                        throw new NotSupportedException($"Filter type '{model.Type}' is not supported.");
                }

                paramIndex++;
            }

            return conditions.Any() ? " WHERE " + string.Join(" AND ", conditions) : "";
        }

        private string BuildOrderByClause(GridRequest request)
        {
            if (request.SortModel == null || !request.SortModel.Any())
                return "";

            var sorts = request.SortModel
                .Select(s => $"{s.ColId} {s.Sort.ToUpper()}");
            return " ORDER BY " + string.Join(", ", sorts);
        }

        private async Task<int> GetTotalCountAsync(string baseSql, string whereClause, List<OracleParameter> parameters)
        {
            string countSql = $"SELECT COUNT(*) FROM ({baseSql}{whereClause})";
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand(countSql, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }


        private async Task<List<Dictionary<string, object>>> ExecuteQueryAsyncGrid(string sql, List<OracleParameter> parameters)
        {
            var results = new List<Dictionary<string, object>>();
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            results.Add(row);
                        }
                    }
                }
            }
            return results;
        }




        // For commands that don't return results (INSERT, UPDATE, DELETE)
        public async Task<int> ExecuteCommandAsync(string sql, params OracleParameter[] parameters)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(sql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        // For scalar queries that return a single value
        public async Task<object> ExecuteScalarAsync(string sql, params OracleParameter[] parameters)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand(sql, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return await command.ExecuteScalarAsync();
                }
            }
        }
    }
}
