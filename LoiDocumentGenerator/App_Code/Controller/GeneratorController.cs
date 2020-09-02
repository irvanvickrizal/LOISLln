using LoiDocumentGenerator.App_Code.DataAccess;
using ClosedXML.Excel;
using EBoqProvider;
using Nokia.Eboq.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Transactions;

namespace LoiDocumentGenerator.App_Code.Controller
{
    public class GeneratorController
    {
        GeneratorDataAccess da;
        public GeneratorController()
        {
            da = new GeneratorDataAccess();
        }

        public DataTable LOI_generateDocument_getdata()
        {
            return da.LOI_generateDocument_getdata();
        }

        public bool LOI_Update_Document_GeneratedLink(string LOI_Document, int RequestId)
        {
            return da.LOI_Update_Document_GeneratedLink(LOI_Document, RequestId);
        }

        public DataTable generate_loi_detail_getdata(int RequestId)
        {
            return da.generate_loi_detail_getdata(RequestId);
        }
    }
}
