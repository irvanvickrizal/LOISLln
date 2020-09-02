using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LoiDocumentGenerator.App_Code.DataAccess
{
    public class AdditionalExecuteCommand
    {
        public static object ExecuteScalar(SqlCommand cmd, string erroperation, string errsp)
        {
            object data = null;
            string strErrMessage = string.Empty;
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 120;
                    data = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                strErrMessage = ex.Message.ToString();
            }
            finally
            {

            }
            return data;
        }

        public static bool InsertBulkCopy(DataTable dt, SqlConnection connection, List<SqlBulkCopyColumnMapping> ListColumnMapping, string DestinationTableName)
        {
            bool result = false;
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    bulkCopy.DestinationTableName = DestinationTableName;
                    if (ListColumnMapping.Count > 0)
                    {
                        foreach (SqlBulkCopyColumnMapping colMapping in ListColumnMapping)
                        {
                            bulkCopy.ColumnMappings.Add(colMapping);
                        }
                    }
                    bulkCopy.WriteToServer(dt);

                }
                result = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return result;
        }

    }
}
