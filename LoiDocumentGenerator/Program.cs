using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.IO;
//using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
//using BCL.easyPDF7.Interop.EasyPDFLoader;
//using BCL.easyPDF7.Interop.EasyPDFPrinter;
using System.Configuration;
using System.Web.UI;
using LoiDocumentGenerator.App_Code.Controller;
using System.Data;
using System.Data.SqlClient;

namespace LoiDocumentGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.GenerateLoiDoc();
        }

        private void GenerateLoiDoc()
        {
            GeneratorController gc = new GeneratorController();

            DataTable dt = gc.LOI_generateDocument_getdata();
            Console.WriteLine("Total LOI Generated: " + dt.Rows.Count.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                string LoiDocPath = Path.Combine(Environment.CurrentDirectory, "Template"); 
                string Templatefile = ConfigurationManager.AppSettings["Templatefile"];                
                string FullTemplate = Path.Combine(LoiDocPath, Templatefile);

                string DirectoryResultPath = Path.Combine(ConfigurationManager.AppSettings["DirectoryResultPath"], dr["LOI_Code"].ToString());
                if (!Directory.Exists(DirectoryResultPath))
                    Directory.CreateDirectory(DirectoryResultPath);

                string filenameSavedOri = string.Concat("LOIDoc_", dr["LOI_Code"].ToString());
                string resultfile = Path.Combine(DirectoryResultPath, filenameSavedOri + "_final.pdf");
                string resultfilehtml = Path.Combine(DirectoryResultPath, filenameSavedOri + ".htm");

                string ResultPathUrl = string.Concat(ConfigurationManager.AppSettings["ResultPathUrl"], dr["LOI_Code"].ToString(), "/", filenameSavedOri, ".pdf");

                string TextTemplate = ReadApprovalTemplate(FullTemplate);

                StringBuilder sb = new StringBuilder();
                sb.Append(TextTemplate);
                sb.Replace("[nokia_logo]", "<img src='https://eboqh3i.nsnebast.com/Images/nokia-logo.png' height='36px' width='100px'/>");
                sb.Replace("[ttd]", "<img src='data:image/png;base64,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' height='36px' width='100px'/>");
                sb.Replace("[subcon_name]", dr["Subcone_Name"].ToString());
                sb.Replace("[subcon_address]", dr["SCon_Addr"].ToString());
                sb.Replace("[subcon_address_city]", "");
                sb.Replace("[subcon_phone]", dr["SCon_Phone"].ToString());
                sb.Replace("[subcon_fax]", dr["SCon_Fax"].ToString());
                sb.Replace("[loi_reference]", dr["LOI_Reference"].ToString());
                sb.Replace("[loi_approved]", "Jakarta, " + Convert.ToDateTime(dr["MU_Approve_Date"]).ToString("dd MMMM yyyy"));
                string cpo = dr["CPO"].ToString();
                sb.Replace("[cpo]", cpo.Remove(cpo.Length - 1));
                sb.Replace("[epm_id]", dr["EPM_Vendor_ID"].ToString());
                sb.Replace("[po_no]", cpo.Remove(cpo.Length - 1));
                string sow = dr["ScopeOfWork"].ToString();
                sb.Replace("[sow_detail]", sow.Remove(sow.Length - 1));
                sb.Replace("[Approver_cpm]", dr["CPM_Approver_Id"].ToString());
                sb.Replace("[Approver_cpm_date]", Convert.ToDateTime(dr["CPM_Approve_Date"]).ToString("dd MMMM yyyy HH:mm:ss"));
                sb.Replace("[Approver_pbm]", dr["PBM_Approver_Id"].ToString());
                sb.Replace("[Approver_pbm_date]", Convert.ToDateTime(dr["PBM_Approve_Date"]).ToString("dd MMMM yyyy HH:mm:ss")); 
                sb.Replace("[Approver_cdm]", dr["CDM_Approver_Id"].ToString());
                sb.Replace("[Approver_cdm_date]", Convert.ToDateTime(dr["CDM_Approve_Date"]).ToString("dd MMMM yyyy HH:mm:ss")); 
                sb.Replace("[Approver_mb]", dr["MB_Approver_Id"].ToString());
                sb.Replace("[Approver_mb_date]", Convert.ToDateTime(dr["MB_Approve_Date"]).ToString("dd MMMM yyyy HH:mm:ss"));
                sb.Replace("[Approver_mu]", dr["MU_Approver_Id"].ToString());
                sb.Replace("[Approver_mu_date]", Convert.ToDateTime(dr["MU_Approve_Date"]).ToString("dd MMMM yyyy HH:mm:ss"));
                sb.Replace("[CTName]", dr["ProjectName"].ToString());
                Console.WriteLine("Preparing to generate document and Merge");
                try
                {

                    HtmlTextWriter sw = new HtmlTextWriter(new StreamWriter(resultfilehtml, false, UnicodeEncoding.UTF8));
                    sw.BeginRender();
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                    sw.Dispose();
                    string result = GeneralConfig.ConvertAnyFormatToPDFHtmlNew(resultfilehtml, DirectoryResultPath, filenameSavedOri);
                    int RequestId = Convert.ToInt32(dr["RequestId"]);
                    string LoiDetailPdfPath = GenerateDetailLoiDoc(gc, RequestId, dr["LOI_Code"].ToString(), DirectoryResultPath);
                    Console.WriteLine("Merge in Progress");
                    using (FileStream stream = new FileStream(resultfile, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4);
                        PdfCopy pdf = new PdfCopy(pdfDoc, stream);
                        pdfDoc.Open();
                        pdf.AddDocument(new PdfReader(result));
                        pdf.AddDocument(new PdfReader(LoiDetailPdfPath));

                        if (pdfDoc != null)
                            pdfDoc.Close();
                    }
                    Console.WriteLine("Merge Completed");
                    Console.WriteLine("Record Result of Merge");
                    gc.LOI_Update_Document_GeneratedLink(ResultPathUrl, RequestId);
                    Console.WriteLine("Record Result of Merge Completed");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Merge error: " + ex.Message.ToString());
                    EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("Program.cs:GenerateLoiDoc", ex.Message, "NON-SP");
                    Console.Read();
                }
            }            
        }

        private string ReadApprovalTemplate(string filename)
        {
            string filetemplatePath = filename.Replace("\\", "/"); //(ConfigurationManager.AppSettings["Template_fpath"] + filename).Replace("\\", "/");
            string content = string.Empty;
            if (File.Exists(@filetemplatePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filetemplatePath, System.Text.Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    content = string.Empty;
                    EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("ReadApprovalTemplate", ex.Message.ToString(), "NON-SP");
                }
            }
            return content;
        }

        private string GenerateDetailLoiDoc(GeneratorController gc, int reqid, string loicode, string DirectoryResultPath)
        {
            string resulturl = "";
            DataTable dt = gc.generate_loi_detail_getdata(reqid);
            string LoiDocPath = Path.Combine(Environment.CurrentDirectory, "Template");
            string Templatedetailfile = ConfigurationManager.AppSettings["Templatedetailfile"];
            string FullTemplateDetail = Path.Combine(LoiDocPath, Templatedetailfile);
            string filenameSavedOri = string.Concat("LOIDoc_Detail_", loicode);
            string resultfile = Path.Combine(DirectoryResultPath, filenameSavedOri, ".pdf");
            string resultfilehtml = Path.Combine(DirectoryResultPath, filenameSavedOri + ".htm");

            string TextTemplateDetail = ReadApprovalTemplate(FullTemplateDetail);

            int no = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(TextTemplateDetail);
            foreach (DataRow dr in dt.Rows)
            {
                no += 1;
                sb.Append("<tr style='text-align:center'>");
                sb.Append("<td>"+ no + "</td>");
                sb.Append("<td>"+ dr["workpackageid"].ToString() + "</td>");
                sb.Append("<td>"+ dr["Customer_PO"].ToString() + "</td>");
                sb.Append("<td>"+ Convert.ToDateTime(dr["Customer_PO_Date"]).ToString("dd/MM/yyyy") + "</td>");
                sb.Append("<td>"+ dr["Region"].ToString() + "</td>");
                sb.Append("<td>"+ dr["Site_ID"].ToString() + "</td>");
                sb.Append("<td>"+ dr["Site_ID"].ToString() + "</td>");
                sb.Append("<td>"+ dr["ScopeOfWork"].ToString() + "</td>");
                sb.Append("<td>"+ dr["Subcone_Name"].ToString() + "</td>");
                sb.Append("<td>"+ dr["Site_Model"].ToString() + "</td>");
                sb.Append("<td>"+ String.Format("{0:n0}", dr["Unit_Price"]) + "</td>");
                sb.Append("<td>"+ dr["Qty"].ToString() + "</td>");
                sb.Append("<td>" + String.Format("{0:n0}", dr["Total_Price"]) + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</body>");
            try
            {

                HtmlTextWriter sw = new HtmlTextWriter(new StreamWriter(resultfilehtml, false, UnicodeEncoding.UTF8));
                sw.BeginRender();
                sw.WriteLine(sb.ToString());
                sw.Close();
                sw.Dispose();
                resulturl = GeneralConfig.ConvertAnyFormatToPDFHtmlNew(resultfilehtml, DirectoryResultPath, filenameSavedOri);
            }

            catch (Exception ex)
            {
                EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("Program.cs:GenerateDetailLoiDoc", ex.Message, "NON-SP");
            }
            return resulturl;
        }

        //static void Main(string[] args)
        //{
        //    //Loader oLoader;
        //    //Printer oPrinter;
        //    //PrintJob oPrintJob;
        //    //Type type = System.Type.GetTypeFromProgID("easyPDF.Loader.7");
        //    //oLoader = (Loader)Activator.CreateInstance(type);
        //    //oPrinter = (Printer)oLoader.LoadObject("easyPDF.Printer.7");
        //    //oPrinter.DefaultPrinter = "easyPDF SDK 7";
        //    //oPrintJob = (PrintJob)oPrinter.IEPrintJob;
        //    //oPrintJob.InitializationTimeout = 0 * 60000;
        //    //oPrintJob.PageConversionTimeout = 0 * 60000;
        //    //oPrintJob.FileConversionTimeout = 0 * 60000;
        //    //IESetting oIESetting;
        //    //PrinterMonitor oPrintMonitor;
        //    //oPrintMonitor = oPrinter.PrinterMonitor;
        //    //string FileNamePath = Directory.GetCurrentDirectory() + "/template_file/LOI Document template.doc";
        //    //string ssssss = @"D:\Test2.pdf";
        //    //oPrintJob.PrintOut(FileNamePath, ssssss);

        //    //string oldFile = Directory.GetCurrentDirectory() + "/template_file/LOI Document template.pdf"; ;
        //    //string newFile = @"D:\Test2.pdf";

        //    //// open the reader
        //    //PdfReader reader = new PdfReader(oldFile);
        //    //Rectangle size = reader.GetPageSizeWithRotation(1);
        //    //Document document = new Document(size);

        //    //// open the writer
        //    //FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
        //    //PdfWriter writer = PdfWriter.GetInstance(document, fs);
        //    //document.Open();

        //    //// the pdf content
        //    //PdfContentByte cb = writer.DirectContent;

        //    //// select the font properties
        //    //BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //    //cb.SetColorFill(BaseColor.DARK_GRAY);
        //    //cb.SetFontAndSize(bf, 8);

        //    //// write the text in the pdf content
        //    //cb.BeginText();
        //    //string text = "Some random blablablabla...";
        //    //// put the alignment and coordinates here
        //    //cb.ShowTextAligned(1, text, 520, 640, 0);
        //    //cb.ShowText("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        //    //cb.EndText();
        //    //cb.BeginText();
        //    //text = "Other random blabla...";
        //    //// put the alignment and coordinates here
        //    //cb.ShowTextAligned(2, text, 100, 200, 0);
        //    //cb.EndText();

        //    //// create the new page and add it to the pdf
        //    //PdfImportedPage page = writer.GetImportedPage(reader, 1);
        //    //cb.AddTemplate(page, 0, 0);

        //    //// close the streams and voilá the file should be changed :)
        //    //document.Close();
        //    //fs.Close();
        //    //writer.Close();
        //    //reader.Close();

        //    //try
        //    //{
        //    //    //HtmlTextWriter sw = new HtmlTextWriter(new StreamWriter(strDocPathSavedHtm, false, UnicodeEncoding.UTF8));
        //    //    //sw.BeginRender();
        //    //    //sw.WriteLine(sb.ToString());
        //    //    //sw.Close();
        //    //    //sw.Dispose();
        //    //    //Console.WriteLine(strDocPathSavedHtm);
        //    //    //Console.WriteLine(docpath);
        //    //    //Console.WriteLine(filenameSavedOri);
        //    //    //string result = Controller.GeneralConfig.ConvertAnyFormatToPDFHtmlNew(strDocPathSavedHtm, docpath, filenameSavedOri);
        //    //    //string outputfile = Path.Combine(docpath, filenameSavedPdfFinal);
        //    //    using (FileStream stream = new FileStream("D:\\Test2.pdf", FileMode.Create))
        //    //    {
        //    //        Document pdfDoc = new Document(PageSize.A4);
        //    //        PdfCopy pdf = new PdfCopy(pdfDoc, stream);
        //    //        pdfDoc.Open();
        //    //        pdf.AddDocument(new PdfReader(docpathCert));
        //    //        if (pdfDoc != null)
        //    //            pdfDoc.Close();

        //    //    }
        //    //}

        //    //catch (Exception ex)
        //    //{
        //    //}

        //    string path = Directory.GetCurrentDirectory() + "/eda8f646-992e-11ea-8b25-0cc47a792c0a_id_eda8f646-992e-11ea-8b25-0cc47a792c0a.html";
        //    string readText = File.ReadAllText(path);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(readText);
        //    StringReader sr = new StringReader(readText);

        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //        pdfDoc.Open();

        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();

        //        FileStream fs = new FileStream("D:\\SourceCode\\eLoi\\Web\\tempdocument\\TestFile1.pdf", FileMode.Create, FileAccess.Write);
        //        fs.Write(bytes, 0, bytes.Length);
        //    }
        //    //string path = Directory.GetCurrentDirectory() + "/template_file/LOI Document template.pdf";
        //    //PdfDocument PDFDoc = PdfReader.Open(path, PdfDocumentOpenMode.Import);
        //    //PdfDocument PDFNewDoc = new PdfDocument();
        //    //for (int Pg = 0; Pg < PDFDoc.Pages.Count; Pg++)
        //    //{
        //    //    PDFNewDoc.AddPage(PDFDoc.Pages[Pg]);
        //    //    //string test = PDFNewDoc.Pages[1].Contents;
        //    //    string test = PDFNewDoc.Pages[0].Contents.ToString();
        //    //}

        //    ////for (int index = 0; index < PDFNewDoc.Pages[1].Contents.Elements.Count; index++)
        //    ////{
        //    ////    PdfDictionary.PdfStream stream = PDFNewDoc.Pages[1].Contents.Elements.GetDictionary(index).Stream;
        //    ////    outputText += new PDFParser().ExtractTextFromPDFBytes(stream.Value);
        //    ////}

        //    //PDFNewDoc.Save(@"D:\Test2.pdf");
        //}


    }
}
