using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EBOQ_Lib_New.DAL
{
    //public class CustomExecuteCommand
    //{
    //    public static bool ExecuteNonQuery(SqlCommand cmd, SqlConnection conn, string erroperation, string errsp)
    //    {
    //        bool isSucceed = true;
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //            isSucceed = false;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (isSucceed == false)
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return isSucceed;
    //    }

    //    public static int ExecuteNonQuery_GetReturnInt(SqlCommand cmd, SqlConnection conn, SqlParameter prmOutput, string erroperation, string errsp)
    //    {
    //        int getValue = 0;
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            cmd.ExecuteNonQuery();
    //            getValue = int.Parse(prmOutput.Value.ToString());
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return getValue;
    //    }

    //    public static Int32 ExecuteNonQuery_GetReturnInt32(SqlCommand cmd, SqlConnection conn, SqlParameter prmOutput, string erroperation, string errsp)
    //    {
    //        Int32 getValue = 0;
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            cmd.ExecuteNonQuery();
    //            getValue = Convert.ToInt32(prmOutput.Value);
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return getValue;
    //    }

    //    public static bool ExecuteNonQuery_GetReturnBoolean(SqlCommand cmd, SqlConnection conn, SqlParameter prmOutput, string erroperation, string errsp)
    //    {
    //        bool getValue = true;
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            cmd.ExecuteNonQuery();
    //            getValue = Convert.ToBoolean(prmOutput.Value);
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return getValue;
    //    }

    //    public static string ExecuteNonQuery_GetReturnString(SqlCommand cmd, SqlConnection conn, SqlParameter prmOutput, string erroperation, string errsp)
    //    {
    //        string getValue = string.Empty;
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            cmd.ExecuteNonQuery();
    //            getValue = Convert.ToString(prmOutput.Value);
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return getValue;
    //    }

    //    public static DataTable ExecuteReaderDT(SqlCommand cmd, SqlConnection conn, string erroperation, string errsp)
    //    {
    //        DataTable dtresult = new DataTable();
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            SqlDataReader sdr = cmd.ExecuteReader();
    //            if (sdr.HasRows)
    //                dtresult.Load(sdr);
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return dtresult;
    //    }

    //    public static DataSet ExecuteReaderDS(SqlCommand cmd, SqlConnection conn, string erroperation, string errsp)
    //    {
    //        DataSet dtresult = new DataSet();
    //        string strErrMessage = string.Empty;
    //        try
    //        {
    //            conn.Open();
    //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //            adapter.Fill(dtresult);
    //        }
    //        catch (Exception ex)
    //        {
    //            strErrMessage = ex.Message.ToString();
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }

    //        if (!string.IsNullOrEmpty(strErrMessage))
    //            DAL_AppLog.ErrLogInsert(erroperation, strErrMessage, errsp);

    //        return dtresult;
    //    }
    //}
}
