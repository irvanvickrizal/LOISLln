using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


public static class GeneralConfig
{
    public enum MessageType { Success, Error, Info, Warning };
    public static string GetDefURL()
    {
        return ConfigurationManager.AppSettings["defurl"];
    }
    public static string WORKFLOW_DEFINED_STATUS_NY = "notdefined";
    public static string WORKFLOW_DEFINED_STATUS_DRAFT = "draft";
    public static string WORKFLOW_DEFINED_STATUS_DONE = "done";
    public static string ACTIVESTATUS_ALL = "all";
    public static string ACTIVESTATUS_ACTIVECODE = "1";
    public static string ACTIVESTATUS_INACTIVECODE = "0";
    public static string QS_POID = "pid";
    public static string QS_POSCOPEID = "psi";
    public static string QS_BOQID = "bid";
    public static string QS_BOQSCOPEID = "bsi";
    public static string QS_TransSNO = "sno";
    public static string QS_CRID = "crid";
    public static string QS_WPID = "wid";
    public static string QS_AttachmentID = "atid";
    public static string QS_DocType = "dtype";
    public static string QS_From = "frm";
    public static string QS_SubmitID = "sbi";
    public static string QS_DELEGATIONID = "dgi";
    public static string Upload_SiteList = "upsitelist";
    public static string Upload_BOQPO = "upboqlist";
    public static string Def_sheetname = "Sheet1";
    public static string Upload_Draft_Status = "Draft";
    public static string Upload_Completed_Status = "Completed";
    public static string Upload_Cancel_Status = "Canceled";
    public static string Upload_Uploaded_status = "Uploaded";
    public static string read_status_pending = "Pending";
    public static string read_status_wip = "WIP";
    public static string read_status_Fail = "Fail";
    public static string read_status_Completed = "Completed";
    public static string read_status_deleted = "Deleted";
    public static string BOQFLOWTYPE_ASPLAN = "asplan";
    public static string BOQFLOWTYPE_ASPO = "aspo";
    public static string BOQFLOWTYPE_ASBUILT = "asbuilt";
    public static string BOQFLOWTYPE_ASFINAL = "asfinal";
    public static string BOQFLOWTYPE_ATPGFR = "atp";
    public static string MediaToConfirm_MobileApp = "mapp";
    public static string MediaToConfirm_Web = "web";
    public static string WORKFLOW_ACTIVITY_TYPE_ACTUALFLOW = "ActualFlow";
    public static string WORKFLOW_ACTIVITY_TYPE_CRFLOW = "CRFlow";
    public static string Milestone_Status_Draft = "Draft";
    public static string Milestone_Status_Pending = "Pending";
    public static string Milestone_Status_Approval = "Approval";
    public static string Milestone_Status_Completed = "Completed";
    public static string QS_BOQType = "bot";
    public static string QS_AsPO = "po";
    public static string QS_AsPlan = "pln";
    public static string QS_AsBuilt = "blt";
    public static string QS_AsFinal = "fnl";
    public static string QS_DocType_TSSR = "ts";
    public static string QS_DocType_ATP = "at";
    public static string QS_DocType_Other = "ot";
    public static string Update_Status_Rejected = "Rejected";
    public static string Update_Status_TSSRRejected = "TSSRRejected";
    public static string BOQ_Status_Pending = "Pending";
    public static string BOQ_Status_Approval = "Approval";
    public static string BOQ_Status_Submitted = "Submitted";
    public static string BOQ_Status_Approved = "Approved";
    public static string BOQ_Status_Completed = "Completed";
    public static string Submission_Pending = "Pending";
    public static string Submission_Approval = "Approval";
    public static string Submission_Submitted = "Submitted";
    public static string Submission_AtpChecked = "AtpChecked";
    public static string Submission_Rejected = "Rejected";
    public static string Submission_PhotoRejected = "PhotoRejected";

    public static string Treatment_CR_Surplus = "Surplus";
    public static string Treatment_CR_Claim = "Claim";
    public static string Treatment_CR_NotRequired = "NR";

    public static string MailNotif_AsPlan_ReadyCreation = "AsPlan_ReadyCreation";
    public static string MailNotif_AsBuilt_ReadyCreation = "AsBuilt_ReadyCreation";
    public static string MailNotif_AsFinal_ReadyCreation = "AsFinal_ReadyCreation";
    public static string MailNotif_ATPReviewPending = "ATP_ReviewPending";
    public static string MailNotif_ClaimRequest = "Claim_Request";
    public static string MailNotif_TSSRReject = "TSSR_Reject";
    public static string MailNotif_DelegationCanceled = "Delegation_Canceled";
    public static string MailNotif_ATPPhotoReject = "ATP_PhotoReject";
    public static string MailNotif_ATPGFRPhotoReject = "ATPGFR_PhotoReject";
    public static int GroundPart = 1;
    public static int TowerPart = 2;

    public static string GetAttachmentPath()
    {
        return ConfigurationManager.AppSettings["attachment_fdocpath"];
    }

    public static string GetAttachmentVPath()
    {
        return ConfigurationManager.AppSettings["attachment_vdocpath"];
    }

    public static string GetPhotoPath()
    {
        return ConfigurationManager.AppSettings["attachment_fphotopath"];
    }

    public static string GetDataUploadPath()
    {
        return ConfigurationManager.AppSettings["data_uploadpath"];
    }

    public static string GetDataBOQSiteBasedUploadPath()
    {
        return ConfigurationManager.AppSettings["data_uploadpath_boqsitebase"];
    }

    public static string GetDataBOQSiteBasedUploadVPath()
    {
        return ConfigurationManager.AppSettings["urlvdocpath_sitebase"];
    }

    public static string GetDataUploadVPath()
    {
        return ConfigurationManager.AppSettings["data_uploadvpath"];
    }

    public static string GetDefVPath()
    {
        return ConfigurationManager.AppSettings["urlvdocpathdef"];
    }

    public static string MailConfigType()
    {
        return ConfigurationManager.AppSettings["mailconfigtype"];
    }
}