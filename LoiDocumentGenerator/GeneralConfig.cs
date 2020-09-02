using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCL.easyPDF7.Interop.EasyPDFLoader;
using BCL.easyPDF7.Interop.EasyPDFPrinter;
using System.IO;
using System.Configuration;

namespace LoiDocumentGenerator
{
    public static class GeneralConfig
    {
        //public static string Filename_ApprovalSheet = "approvalsheet-template.htm";
        //public static string Filename_PunchlistSheet = "punchlistsheet-template.htm";
        //public static string Filename_LIDecSheet = "lidec-template.htm";
        //public static int DocId_SSV4G_1800 = int.Parse(ConfigurationManager.AppSettings["docid_ssvl04G_1800"]);
        //public static int DocId_SSV4G = int.Parse(ConfigurationManager.AppSettings["docid_ssvl04G"]);
        //public static int DocId_SSV3G = int.Parse(ConfigurationManager.AppSettings["docid_ssvl03G"]);
        //public static int DocId_L04G_1800 = int.Parse(ConfigurationManager.AppSettings["docid_kpil04G_1800"]);
        //public static int DocId_L04G = int.Parse(ConfigurationManager.AppSettings["docid_kpil04G"]);
        //public static int DocId_L03G = int.Parse(ConfigurationManager.AppSettings["docid_kpil03G"]);
        //public static int DocId_LIDec = int.Parse(ConfigurationManager.AppSettings["docid_lidec"]);
        public static string ConvertAnyFormatToPDFHtmlNew(string FileNamePath, string strPath, string filename)
        {
            bool isSucceed = true;
            string ReFileName = filename;
            ReFileName = ReFileName + ".pdf";
            string strReturn;
            Loader oLoader;
            Printer oPrinter;
            PrintJob oPrintJob;
            Type type = System.Type.GetTypeFromProgID("easyPDF.Loader.7");
            oLoader = (Loader)Activator.CreateInstance(type);
            oPrinter = (Printer)oLoader.LoadObject("easyPDF.Printer.7");
            oPrinter.DefaultPrinter = "easyPDF SDK 7";
            oPrintJob = (PrintJob)oPrinter.IEPrintJob;
            oPrintJob.InitializationTimeout = 0 * 60000;
            oPrintJob.PageConversionTimeout = 0 * 60000;
            oPrintJob.FileConversionTimeout = 0 * 60000;
            IESetting oIESetting;
            PrinterMonitor oPrintMonitor;
            oPrintMonitor = oPrinter.PrinterMonitor;
            try
            {
                if (!System.IO.Directory.Exists(Path.Combine(strPath, ReFileName)))
                    oPrintJob.PrintOut(FileNamePath, Path.Combine(strPath, ReFileName));
                else
                {
                    System.IO.File.Delete(strPath + ReFileName);
                    oPrintJob.PrintOut(FileNamePath, Path.Combine(strPath, ReFileName));
                }
                strReturn = ReFileName;
            }
            catch (Exception ex)
            {
                strReturn = ex.Message.ToString();
                if (oPrintJob != null)
                {
                    strReturn = oPrintJob.ConversionResultMessage;
                    prnConversionResult result = oPrintJob.ConversionResult;
                    if ((result == prnConversionResult.PRN_CR_CONVERSION | result == prnConversionResult.PRN_CR_CONVERSION_INIT | result == prnConversionResult.PRN_CR_CONVERSION_PRINT))
                        strReturn = oPrintJob.PrinterResultMessage;
                }
            }
            finally
            {
                oPrintJob = null;
                oPrinter = null;
            }
            //if (isSucceed == false)
            //    EBOQ_Lib_New.DAL.DAL_AppLog.ErrLogInsert("EBASTFileUpload:ConvertAnyFormatToPDFHtmlNew", strReturn, "NON-SP");

            return strReturn;
        }
    }

}
