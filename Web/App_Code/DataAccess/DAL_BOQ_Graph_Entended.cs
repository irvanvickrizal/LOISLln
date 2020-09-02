using EBOQ_Lib_New.DAL;
using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Created by Fauzan
/// 4 Feb 2019
/// Extended function for DAL BOQ Grap
/// </summary>
namespace eLoi.DataAccess
{
    public class DAL_BOQ_Graph_Entended : DAL_BOQ_Graph
    {
        public DataTable DAL_GFRDoneSummary(int userid, bool setIndividual)
        {
            Command = new SqlCommand("uspGFR_GetApprovedSummary", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlParameter prmUserID = new SqlParameter("@userid", SqlDbType.Int);
            SqlParameter prmSetIndividual = new SqlParameter("@setindividual", SqlDbType.Bit);
            prmUserID.Value = userid;
            prmSetIndividual.Value = setIndividual;
            Command.Parameters.Add(prmUserID);
            Command.Parameters.Add(prmSetIndividual);
            return CustomExecuteCommand.ExecuteReaderDT(Command, Connection, "DAL_BOQ:DAL_GFRDoneSummary", "uspGFR_GetApprovedSummary");
        }
    }
}