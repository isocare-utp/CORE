<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationSelectionPage.aspx.cs"
    Inherits="Saving.ApplicationSelectionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>เลือกระบบ</title>
    <link href="JsCss/FrameLayout.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tableApplication
        {
            /*border: solid 1px #4499BC;*/
            margin-top: 20px;
        }
        .tableApplication td
        {
            /*border: solid 1px #4499BC;*/
            height: 150px;
            width: 150px;
            vertical-align: top;
            text-align: center;
        }
        .imApplication
        {
            margin-top: 1px;
            cursor: pointer;
        }
        .imApplicationDeny
        {
            margin-top: 1px;
            cursor: default;
            filter: alpha(opacity=80);
            opacity: 0.2;
        }
        .imApplicationNone
        {
            margin-top: 1px;
            cursor: auto;
        }
        .lbApplication
        {
            font-family: Tahoma, MS Sans Serif, Serif;
            font-size: 16px;
            color: #990000;
            cursor: pointer;
        }
        .lbApplicationDeny
        {
            font-family: Tahoma, MS Sans Serif, Serif;
            font-size: 16px;
            color: #BB8888;
            cursor: default;
        }
        .lbWorkDateOn
        {
            font-family: Tahoma, MS Sans Serif, Serif;
            font-size: 10px;
            color: blue;
            cursor: pointer;
        }
        .lbWorkDateOnDeny
        {
            font-family: Tahoma, MS Sans Serif, Serif;
            font-size: 10px;
            color: #8888BB;
            cursor: default;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ClickMenu() {
            alert("กรุณาเลือกระบบก่อน");
        }

        function GoLogOff() {
            if (confirm("ยืนยันการออกจากระบบ")) {
                window.location = "./Logout.aspx";
            }
        }

        function OnClickApplication(appName) {
            if (appName != "") {
                window.location = "ApplicationSelectionPage.aspx?setApp=" + appName;
            }
        }
    </script>
</head>
<body style="background-color: rgb(200, 235, 255); background-repeat: repeat-x; background-position: top;">
    <form id="form1" runat="server">
    <div align="center">
        <table width="970" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 30px;">
                </td>
            </tr>
            <tr>
                <td class="marginTopMid">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td valign="middle" style="width: 80px; text-align: right;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/band_black.jpg" Width="60px" />
                            </td>
                            <td valign="top" style="text-align: left;">
                                <div style="margin-left: 5px;">
                                    <div style="margin-top: 6px;">
                                        <asp:Label ID="LbSiteNameThai" runat="server" Text="sso.offline_label_thai" Style="font-family: Tahoma;
                                            font-size: 24px; font-weight: bold"></asp:Label>
                                        <br />
                                    </div>
                                    <div style="margin-top: 3px;">
                                        <asp:Label ID="LbSiteNameEnglish" runat="server" Text="sso.offline_label_eng" Style="font-family: Times New Roman;
                                            font-size: 10px; font-weight: bold"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="menuListAll">
                    <table width="100%">
                        <tr>
                            <td width="50%" align="left" valign="middle">
                                <table border="0" cellpadding="0" cellspacing="0" class="menuSystem">
                                    <tr>
                                        <td>
                                            <span onclick="ClickMenu()">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Image/ico/systemmenu.png" AlternateText="List of Application Menu"
                                                    Width="20px" />
                                                <br />
                                                ListMenu </span>
                                        </td>
                                        <td>
                                            <span onclick="ClickMenu();">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/ico/clipboard.png" AlternateText="New"
                                                    Width="20px" />
                                                <br />
                                                New[F2] </span>
                                        </td>
                                        <td>
                                            <span onclick="ClickMenu();">
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Image/ico/folder.png" AlternateText="Open"
                                                    Width="20px" />
                                                <br />
                                                Open[F8] </span>
                                        </td>
                                        <td>
                                            <span onclick="ClickMenu();">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Image/ico/save.png" AlternateText="Save"
                                                    Width="20px" />
                                                <br />
                                                Save[F9] </span>
                                        </td>
                                        <td style="width: 220px;" align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="50%" align="right" valign="middle">
                                <table border="0" cellpadding="0" cellspacing="0" class="menuExit">
                                    <tr>
                                        <td id="TdReport" runat="server">
                                            <span onclick="ClickMenu()">
                                                <asp:Image ID="ImReport" runat="server" ImageUrl="~/Image/icoReport.png" AlternateText="Report"
                                                    Width="20px" />
                                                <br />
                                                Report </span>
                                        </td>
                                        <td>
                                            <span onclick="ClickMenu()">
                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Image/chgpwd_icon.png" AlternateText="Change Password"
                                                    Width="20px" />
                                                <br />
                                                Password </span>
                                        </td>
                                        <td>
                                            <span onclick="ClickMenu()">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Image/applications_icon.png" AlternateText="Open"
                                                    Width="20px" />
                                                <br />
                                                Apps </span>
                                        </td>
                                        <td>
                                            <span onclick="GoLogOff();">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Image/exit.png" AlternateText="Save"
                                                    Width="20px" />
                                                <br />
                                                LogOff </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="background-color: white; height: 660px; vertical-align: top; text-align: left">
                    <div id="divMenuGroup" class="menubarGroupClose" runat="server">
                        <div align="center">
                        </div>
                    </div>
                    <div>
                    </div>
                    <div align="center">
                        <table id="TableMenuAndContent" width="96%" runat="server">
                            <tr>
                                <td colspan="2" style="text-align: left; height: 40px; vertical-align: middle;">
                                    <asp:Label ID="LbSystemAndPage" runat="server" Text="เลือกระบบ" Font-Bold="True"
                                        Font-Names="Tahoma" Font-Size="20px" ForeColor="#999999"></asp:Label>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td id="tdMenuLeft" colspan="2" style="height: 400px; vertical-align: top; text-align: center;"
                                    runat="server" align="center">
                                    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="tableApplication" cellpadding="0" cellspacing="0" align="center">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Literal ID="LtTrEnd" runat="server" Text='<%#Eval("TrStart")%>'></asp:Literal>
                                                        <td>
                                                            <div onclick="OnClickApplication('<%#Eval("AppEvent")%>')">
                                                                <asp:Image ID="ImApplicationIcon" runat="server" ImageUrl='<%#Eval("Picture")%>'
                                                                    CssClass='<%#Eval("PictureCss")%>' Height="84px" Width="84px" />
                                                                <br />
                                                                <asp:Label ID="LbWorkDateStatus" runat="server" Text='<%#Eval("WorkDate")%>' CssClass='<%#Eval("WorkDateLableCss")%>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="LbApplicationText" runat="server" Text='<%#Eval("Name")%>' CssClass='<%#Eval("AppLableCss")%>'></asp:Label>
                                                            </div>
                                                        </td>
                                                        <asp:Literal ID="Literal1" runat="server" Text='<%#Eval("TrEnd")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="footer" style="font-size: 8px;">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
