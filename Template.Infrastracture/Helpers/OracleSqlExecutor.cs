using Oracle.ManagedDataAccess.Client;
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
