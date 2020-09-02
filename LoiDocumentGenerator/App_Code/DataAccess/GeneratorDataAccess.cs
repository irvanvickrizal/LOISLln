using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EBOQ_Lib_New.DAL;

namespace LoiDocumentGenerator.App_Code.DataAccess
{
    public class GeneratorDataAccess : DBConfiguration
    {
        public GeneratorDataAccess()
        {

        }

        public DataTable LOI_generateDocument_getdata()
        {
            Command = new SqlCommand("usp_LOI_generateDocument_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:LOI_generateDocument_getdata", "usp_LOI_generateDocument_getdata");
        }

        public bool LOI_Update_Document_GeneratedLink(string LOI_Document, int RequestId)
        {
            Command = new SqlCommand("usp_LOI_Update_Document_GeneratedLink", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            SqlParameter prm_LOI_Document = new SqlParameter("@LOI_Document", SqlDbType.VarChar,1000);
            prm_LOI_Document.Value = LOI_Document;
            Command.Parameters.Add(prm_LOI_Document);                       

            return CustomExecuteCommand.ExecuteNonQuery(Command, Connection, "LOIDataAccess:LOI_Update_Document_GeneratedLink", "usp_LOI_Update_Document_GeneratedLink");
        }

        public DataTable generate_loi_detail_getdata(int RequestId)
        {
            Command = new SqlCommand("usp_generate_loi_detail_getdata", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_RequestId = new SqlParameter("@RequestId", SqlDbType.BigInt);
            prm_RequestId.Value = RequestId;
            Command.Parameters.Add(prm_RequestId);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:generate_loi_detail_getdata", "usp_generate_loi_detail_getdata");
        }
    }
}
