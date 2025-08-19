using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PM
{
    internal class SQLite
    {
        private string _connectionString;

        /// <summary>
        /// 初始化 SQLite 数据库帮助类
        /// </summary>
        /// <param name="dbPath">数据库文件路径</param>
        public SQLite(string dbPath)
        {
            _connectionString = $"Data Source={dbPath};Version=3;";

            // 如果数据库文件不存在，则创建
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }
        }

        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <param name="sql">创建表的SQL语句</param>
        public void CreateTable(string sql)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行非查询SQL语句（INSERT, UPDATE, DELETE）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回DataTable
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>包含查询结果的DataTable</returns>
        public DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询并返回第一行第一列的值
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>第一行第一列的值</returns>
        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回字符串结果（第一行第一列）
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>查询结果的字符串表示，如果没有结果则返回null</returns>
        public string ExecuteString(string sql, params SQLiteParameter[] parameters)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    var result = command.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        /// <summary>
        /// 批量插入数据（使用事务提高性能）
        /// </summary>
        /// <param name="sql">插入语句</param>
        /// <param name="parametersList">参数列表</param>
        /// <returns>是否成功</returns>
        public bool BulkInsert(string sql, List<SQLiteParameter[]> parametersList)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SQLiteCommand(sql, connection, transaction))
                        {
                            foreach (var parameters in parametersList)
                            {
                                command.Parameters.Clear();
                                if (parameters != null)
                                {
                                    command.Parameters.AddRange(parameters);
                                }
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行事务操作
        /// </summary>
        /// <param name="sqlCommands">SQL命令和参数列表</param>
        /// <returns>是否成功</returns>
        public bool ExecuteTransaction(List<Tuple<string, SQLiteParameter[]>> sqlCommands)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var cmd in sqlCommands)
                        {
                            using (var command = new SQLiteCommand(cmd.Item1, connection, transaction))
                            {
                                if (cmd.Item2 != null)
                                {
                                    command.Parameters.AddRange(cmd.Item2);
                                }
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否存在</returns>
        public bool TableExists(string tableName)
        {
            string sql = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            var parameter = new SQLiteParameter("@tableName", DbType.String) { Value = tableName };

            long count = (long)ExecuteScalar(sql, parameter);
            return count > 0;
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="backupPath">备份路径</param>
        public void BackupDatabase(string backupPath)
        {
            using (var source = new SQLiteConnection(_connectionString))
            using (var destination = new SQLiteConnection($"Data Source={backupPath};Version=3;"))
            {
                source.Open();
                destination.Open();
                source.BackupDatabase(destination, "main", "main", -1, null, 0);
            }
        }

        /// <summary>
        /// 执行查询并返回 JSON 字符串
        /// </summary>
        /// <param name="sql">SQL 查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>JSON 格式的查询结果</returns>
        public string ExecuteJson(string sql, params SQLiteParameter[] parameters)
        {
            DataTable dataTable = ExecuteDataTable(sql, parameters);
            return JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        }

        /// <summary>
        /// 执行查询并返回对象列表的 JSON 字符串
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="sql">SQL 查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>对象列表的 JSON 表示</returns>
        public string ExecuteJson<T>(string sql, params SQLiteParameter[] parameters) where T : new()
        {
            List<T> list = new List<T>();
            DataTable dataTable = ExecuteDataTable(sql, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                T item = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    var property = typeof(T).GetProperty(column.ColumnName);
                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(item, row[column], null);
                    }
                }
                list.Add(item);
            }

            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }
    }
}
