using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using EBOQ_Lib_New.DAL;
using System.Data;
using System.Configuration;

public static class MailController
{
    public static void SendMail(string email, string subject, string bodymessage, string configtype)
    {
        SendMailUser(email, bodymessage, subject, configtype);
    }

    private static void SendMailUser(string recipientmail, string bodymessage, string subject, string configtype)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        System.Net.Mail.SmtpClient mySMTPClient = new System.Net.Mail.SmtpClient();
        System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
        string recipients = string.Empty;
        myEmail.Bcc.Clear();
        myEmail.Bcc.Add(recipientmail);
        myEmail.Bcc.Add("irvan.vickrizal@nokia.com");
        myEmail.Bcc.Add("agustian.hia.ext@nokia.com");
        myEmail.Bcc.Add("yunita.putri.ext@nokia.com");
        myEmail.BodyEncoding = System.Text.Encoding.UTF8;
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8;
        myEmail.Subject = subject;
        myEmail.Body = bodymessage;
        myEmail.IsBodyHtml = true;
        myEmail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["MailAddressFrom"], ConfigurationManager.AppSettings["MailFrom"]);
        SmtpClientEBAST(myEmail, configtype);
    }

    public static void SendMailAttachement(string email, string subject, string bodymessage, string configtype, string Attachement)
    {
        SendMailUserAttachement(email, bodymessage, subject, configtype, Attachement);
    }

    private static void SendMailUserAttachement(string recipientmail, string bodymessage, string subject, string configtype, string Attachement)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        System.Net.Mail.SmtpClient mySMTPClient = new System.Net.Mail.SmtpClient();
        System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(Attachement);

        string recipients = string.Empty;
        myEmail.Bcc.Clear();
        myEmail.Bcc.Add(recipientmail);
        myEmail.Bcc.Add("irvan.vickrizal@nokia.com");
        myEmail.Bcc.Add("agustian.hia.ext@nokia.com");
        myEmail.Bcc.Add("yunita.putri.ext@nokia.com");
        myEmail.BodyEncoding = System.Text.Encoding.UTF8;
        myEmail.SubjectEncoding = System.Text.Encoding.UTF8;
        myEmail.Subject = subject;
        myEmail.Body = bodymessage;
        myEmail.IsBodyHtml = true;
        myEmail.Attachments.Add(attachment);
        myEmail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["MailAddressFrom"], ConfigurationManager.AppSettings["MailFrom"]);
        SmtpClientEBAST(myEmail, configtype);
    }

    private static SmtpClient SmtpClientEBAST(MailMessage myemail, string configtype)
    {
        eLoi.DataAccess.MailDataAccess mailda = new eLoi.DataAccess.MailDataAccess();
        SmtpClient smtpconfig = new SmtpClient();

        DataTable getMailConfig = mailda.GetMailConfigurationBaseName(configtype);
        string mailUsername = string.Empty;
        string mailPassword = string.Empty;
        string smptName = string.Empty;
        string smtpPort = string.Empty;
        if (getMailConfig.Rows.Count > 0)
        {
            foreach (DataRow drw in getMailConfig.Rows)
            {
                mailUsername = Convert.ToString(drw["Mail_Username"]);
                mailPassword = Convert.ToString(drw["Mail_Password"]);
                smptName = Convert.ToString(drw["SMTP"]);
                smtpPort = Convert.ToString(drw["SMTP_PORT"]);
            }
        }

        smtpconfig.Host = smptName;
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailUsername, mailPassword);
        smtpconfig.Credentials = credentials;
        try
        {
            smtpconfig.Send(myemail);
        }
        catch (Exception ex)
        {
        }
        return smtpconfig;
    }

    private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    



}