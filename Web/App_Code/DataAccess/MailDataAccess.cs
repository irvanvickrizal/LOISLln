using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EBOQ_Lib_New.DAL;


namespace eLoi.DataAccess
{
    public class MailDataAccess : DBConfiguration
    {
        public MailDataAccess()
        {
        }

        public DataTable GetMailConfigurationBaseName(string configname)
        {
            Command = new SqlCommand("uspConfig_GetMailConfigurationBaseName", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlParameter prm_configname = new SqlParameter("@configname", SqlDbType.VarChar);
            prm_configname.Value = configname;
            Command.Parameters.Add(prm_configname);

            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "LOIDataAccess:GetMailConfigurationBaseName", "uspConfig_GetMailConfigurationBaseName");
        }
    }
}
