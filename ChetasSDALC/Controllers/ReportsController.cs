using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChetasSDALC.Controllers
{
    public class ReportsController : Controller
    {
        DemoEntities db = new DemoEntities();
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            var tb = db.Tb_Population.ToList();
            return View(tb);
        }
        public ActionResult Reports()
        {
            List<Tb_Population> population = new List<Tb_Population>();
            population = db.Tb_Population.ToList();
            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            ReportParameter param = new ReportParameter();
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Rptpopulation.rdlc";
            ReportDataSource rptdatasource = new ReportDataSource("ds", population);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View();
        }

        [HttpPost]
        public ActionResult Reports(string name)
        {
            List<Tb_Population> population = new List<Tb_Population>();
            population = (from p in db.Tb_Population where p.StateName == name select p).ToList();
            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Rptpopulation.rdlc";
            //ReportParameter[] param = new ReportParameter[1];
            //param[0] = new ReportParameter("statename", name);
            //rptviewer.LocalReport.SetParameters(param);
            ReportDataSource rptdatasource = new ReportDataSource("ds", population);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            rptviewer.SizeToReportContent = true;
            ViewBag.ReportViewer = rptviewer;
            return View();
        }
    }
}
}
