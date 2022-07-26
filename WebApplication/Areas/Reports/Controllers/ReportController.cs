using System;
using System.Web.Mvc;

namespace WebApplication.Areas.Reports.Controllers
{
    public class ReportController : Controller
    {
        // GET: Reports/Report
        public ActionResult GenerateReport()
        {
            try
            {
                //byte[] bytes = null;
                //Warning[] warnings;
                //string[] streamIds;
                //string mimeType = string.Empty;
                //string encoding = string.Empty;
                //string extension = string.Empty;
                //string fileName = "";
                //string deviceInfo = "";


                //LocalReport localreport = new LocalReport();
                //ReportDataSource ListUser = new ReportDataSource("UserDataSet", new Users().GenerateDataUser());
                //localreport.DataSources.Add(ListUser);
                //localreport.ReportPath = Server.MapPath("~/Areas/Reports/Data/ListDataUser.rdlc");
                //fileName = "ReportUser.xlsx";
                //bytes = localreport.Render("Excel", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

                //return File(bytes, mimeType, fileName);
                return null;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}