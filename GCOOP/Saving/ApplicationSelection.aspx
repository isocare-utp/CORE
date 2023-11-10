<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="ApplicationSelection.aspx.cs" Inherits="Saving.ApplicationSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        function OnClickApplication(appName) {
            if (appName != "") {
                parent.ApplicationSelection(appName);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
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
</asp:Content>