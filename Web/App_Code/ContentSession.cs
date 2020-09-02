using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContentSession
/// </summary>
public abstract class ContentSession
{

    public ContentSession()
    {
    }
    public static int USERID
    {
        get
        {
            if (HttpContext.Current.Session[Resources.Resources.ses_userid] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(HttpContext.Current.Session[Resources.Resources.ses_userid].ToString());
            }
        }
    }

    public static int RoleID
    {
        get
        {
            if (HttpContext.Current.Session[Resources.Resources.ses_roleid] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(HttpContext.Current.Session[Resources.Resources.ses_roleid].ToString());
            }
        }
    }

    public static string FULLNAME
    {
        get
        {
            if (HttpContext.Current.Session[Resources.Resources.ses_fullname] == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(HttpContext.Current.Session[Resources.Resources.ses_fullname]);
            }
        }
    }

    public static string USERGROUP
    {
        get
        {
            if (HttpContext.Current.Session[Resources.Resources.ses_usergroup] == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(HttpContext.Current.Session[Resources.Resources.ses_usergroup]);
            }
        }
    }

    public static string SIGNTITLE
    {
        get
        {
            if (HttpContext.Current.Session[Resources.Resources.ses_signtitle] == null)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(HttpContext.Current.Session[Resources.Resources.ses_signtitle]);
            }
        }
    }

    

    public static string language
    {
        get
        {
            string value = "EN";
            if (HttpContext.Current.Request.Cookies["lang"] != null)
            {
                value = HttpContext.Current.Request.Cookies["lang"].Value;
            }
            else
            {
                HttpCookie cookie = new HttpCookie("lang");
                cookie.Value = "EN";
                cookie.Expires = DateTime.Now.AddHours(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            value = HttpContext.Current.Request.Cookies["lang"].Value;
            return value;
        }
        
    }

    public static Int32 QS_GetID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["id"]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString["id"]);
            }
        }
    }

    public static Int32 QS_GetPOID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_POID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_POID]);
            }
        }
    }

    public static Int32 QS_GetBOQID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQID]);
            }
        }
    }

    public static Int32 QS_GetAttachmentID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_AttachmentID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_AttachmentID]);
            }
        }
    }

    public static Int32 QS_GetBOQSCOPEID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQSCOPEID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQSCOPEID]);
            }
        }
    }

    public static Int32 QS_GetDELEGATIONID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_DELEGATIONID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_DELEGATIONID]);
            }
        }
    }

    public static Int32 QS_GetPOScopeID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_POSCOPEID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_POSCOPEID]);
            }
        }
    }

    public static Int32 QS_GetTranSNO
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_TransSNO]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_TransSNO]);
            }
        }
    }

    public static Int32 QS_GetSubmitID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_SubmitID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_SubmitID]);
            }
        }
    }

    public static Int32 QS_GetCRID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_CRID]))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[GeneralConfig.QS_CRID]);
            }
        }
    }

    public static string QS_GetBOQType
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQType]))
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.QueryString[GeneralConfig.QS_BOQType];
            }
        }
    }

    public static string QS_GetWPID
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_WPID]))
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.QueryString[GeneralConfig.QS_WPID];
            }
        }
    }

    public static string QS_GetDocType
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_DocType]))
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.QueryString[GeneralConfig.QS_DocType];
            }
        }
    }

    public static string QS_GetFrom
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[GeneralConfig.QS_From]))
            {
                return "Def";
            }
            else
            {
                return HttpContext.Current.Request.QueryString[GeneralConfig.QS_From];
            }
        }
    }

    public static string CTName
    {
        get
        {
            if (HttpContext.Current.Session["LoginerCTName"] == null)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Session["LoginerCTName"].ToString();
            }
        }
    }
}