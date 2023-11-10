<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="wd_fin_bankaccount.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.wd_fin_bankaccount_ctrl.wd_fin_bankaccount" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../../../../JsCss/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="../../../../js/Gcoop.js" type="text/javascript"></script>
 <script type="text/javascript">
     var dsList = new DataSourceTool();

     function OnDsListClicked(s, r, c) {
         var account_no = dsList.GetItem(r, "account_no"); 
         var bank_code = dsList.GetItem(r, "bank_code");
         var bankbranch_code = dsList.GetItem(r, "bankbranch_code"); 
         try {
             window.opener.GetDataFromDlg(account_no, bank_code, bankbranch_code);
             window.close();
         } catch (err) {
             parent.GetDataFromDlg(account_no, bank_code, bankbranch_code);
             parent.RemoveIFrame();
         }
     }
     $(document).ready(function () {
         for (var i = 0; i < dsList.GetRowCount(); i++) {             
             if (dsList.GetItem(i, "close_status") == "1") {
                 dsList.GetElement(i, "bank_desc").style.background = "#CD5C5C";
                 dsList.GetElement(i, "branch_name").style.background = "#CD5C5C";
                 dsList.GetElement(i, "account_no").style.background = "#CD5C5C";
                 dsList.GetElement(i, "account_name").style.background = "#CD5C5C"; 
                 dsList.GetElement(i, "close_desc").style.background = "#CD5C5C";
             } else {
                 dsList.GetElement(i, "bank_desc").style.background = "#FFFFFF";
                 dsList.GetElement(i, "branch_name").style.background = "#FFFFFF";
                 dsList.GetElement(i, "account_no").style.background = "#FFFFFF";
                 dsList.GetElement(i, "account_name").style.background = "#FFFFFF";
                 dsList.GetElement(i, "close_desc").style.background = "#FFFFFF";
             }
         }
     });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
<table>
        <tr>
            <td>
                <uc1:dsList ID="dsList" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
