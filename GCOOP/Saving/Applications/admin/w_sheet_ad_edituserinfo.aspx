<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ad_edituserinfo.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_edituserinfo" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <%=initJavaScript%>
    <%=jsSearch%>
    <%=jsresetpass%>
    <%=NoUserName%>
    <%=jsDescription%>
    <%=DeleteUserName%>
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }
        function itemChange(s,r,c,v) {
            if (c == "user_name") {
                s.SetItem(1, "user_name", v);
                s.AcceptText();
                jsSearch();
            } else if (c == "full_name") {
                s.SetItem(1, "full_name", v);
                s.AcceptText();
                jsDescription();
            }
        }
        function Clicked(s, r, c, v) {
            switch (c) {
                case "b_1":
                    user_name = objDwUserName.GetItem(1, "user_name");

                    if (user_name == null || user_name == "") {
                        NoUserName();
                    }
                    else {
                        Gcoop.OpenIFrame("350", "200", "w_dlg_ad_editpass.aspx", "?user_name=" + user_name);
                    }
                    break;
                case "b_2":
                    jsresetpass();
                    break;
                case "b_3":
                    user_name = objDwUserName.GetItem(1,"user_name");
                    Gcoop.OpenIFrame("300", "200", "w_dlg_ad_memberof.aspx", "?user_name=" + user_name);
                    break;
                case "b_4":
                    user_name = objDwUserName.GetItem(1, "user_name");

                    if (user_name == null || user_name == "") {
                        NoUserName();
                    }
                    else {
                        DeleteUserName();
                    }
                    break;

            }

        }

        function MenubarOpen() {
            Gcoop.OpenIFrame(700, 350, "w_dlg_ad_users_list.aspx", '')
        }
        function ReceiveUserName(user_name) {
            objDwUserName.SetItem(1, "user_name", user_name);
            jsSearch();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

                <dw:WebDataWindowControl ID="DwUserName" runat="server" DataWindowObject="d_um_edituser"
                    LibraryList="~/DataWindow/admin/ad_user.pbl;"
                    ClientScriptable="True" 
                    ClientEventItemChanged="itemChange" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientEventClicked="Clicked">
                </dw:WebDataWindowControl>
                <asp:HiddenField ID="HdCkDes" runat="server" Value="" />
</asp:Content>
