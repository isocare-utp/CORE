<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_cm_constant_config.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_cm_constant_config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var timeout = 500;
        var closetimer = 0;
        var ddmenuitem = 0;

        function mopen(id) {
            mcancelclosetime();
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
            ddmenuitem = document.getElementById(id);
            ddmenuitem.style.visibility = 'visible';

        }
        function mclose() {
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
        }

        function mclosetime() {
            closetimer = window.setTimeout(mclose, timeout);
        }

        function mcancelclosetime() {
            if (closetimer) {
                window.clearTimeout(closetimer);
                closetimer = null;
            }
        }

        //document.onclick = mclose; 
    </script>
    <style type="text/css">
        body
        {
            font-family: "trebuchet ms" ,Tahoma;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <ul id="sddm">
        <li><a href="#" onclick="mopen('m1')" onmouseout="mclosetime()">เลือก รายการข้อกำหนด</a>
            <div id="m1" onmouseover="mcancelclosetime()" onmouseout="mclosetime()">
                <asp:Literal ID="ltr_cnstmenu" runat="server"></asp:Literal>
            </div>
        </li>
    </ul>
    <div style="float: left;">
        <div style="float: right;">
            <asp:Label ID="lbl_cnstmenu" runat="server" Text="" CssClass="font14px"></asp:Label>
        </div>
        <br />
        <br />
        <asp:Panel ID="pnl_cnst" runat="server" Width="100%">
        </asp:Panel>
    </div>
</asp:Content>
