<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="UltimoIntento.Views.Report" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      
    </div>
        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="981px">
        </rsweb:ReportViewer>
        
    </form>
</body>
</html>
