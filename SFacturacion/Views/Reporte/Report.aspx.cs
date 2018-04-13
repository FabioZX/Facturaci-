using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UltimoIntento.Views
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection oCon = new SqlConnection("Data Source=.;Initial Catalog=SPfacturacion;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
                string sSql = "select * from Facturacion ";
                SqlDataAdapter oDa = new SqlDataAdapter(sSql, oCon);
                DataTable oTabla = new DataTable();
                oDa.Fill(oTabla);
                ReportDataSource rds = new ReportDataSource();
                rds.Value = oTabla;
                rds.Name = "DataSet1";
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.ReportEmbeddedResource = "Report1.rdlc";
                ReportViewer1.LocalReport.ReportPath = @"Report1.rdlc";
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}