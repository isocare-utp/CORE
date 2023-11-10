<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_slip_mthkeep.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_slip_mthkeep_ctrl.ws_kp_slip_mthkeep" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "smembgroup_desc") {
                dsMain.SetItem(0, "smembgroup_code", v);
            }
            else if (c == "emembgroup_desc") {
                dsMain.SetItem(0, "emembgroup_code", v);
            }
            else if (c == "smembgroup_code") {
                dsMain.SetItem(0, "smembgroup_desc", v);
            }
            else if (c == "emembgroup_code") {
                dsMain.SetItem(0, "emembgroup_desc", v);
            }
        }

        function PostPrint() {
            PostPrint();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <input type="button" value="ออกใบเสร็จ" id="btnPrint" onclick="PostPrint()" style="margin-left: 550px;" />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
