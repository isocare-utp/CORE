<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="wd_ass_getyear.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_ass_getyear_ctrl.wd_ass_getyear" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool();

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_1") {
                dsMain.SetRowFocus(r);
                var year = dsMain.GetItem(r, "year") - 543;
                try {
                    window.opener.GetValueyear(year);
                    window.close();
                }
                catch (err) {
                    parent.GetValueyear(year);
                    window.close();
                }
            }
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
    <uc2:DsMain ID="dsMain" runat="server" />
    </center>
</asp:Content>
