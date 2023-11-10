<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=PostMemberNo%>
   <script type="text/javascript">
       function OnDsMainItemChanged(s, r, c, v) {
          if (c == "deptgrp_code1") {                    
               if (dsMain.GetItem(0, "deptgrp_code1") == "0" ) {
                   dsMain.SetItem(0, "deptaccount_no", "");
                   dsMain.GetElement(0, 'deptaccount_no').disabled = true;
                   dsMain.GetElement(0, 'deptaccount_no').style.background = '#CCCCCC';
               } else {
                   dsMain.GetElement(0, 'deptaccount_no').disabled = false;
                   dsMain.GetElement(0, 'deptaccount_no').style.background = '#FFFFFF';
//                   var gcode = "";
//                   if (dsMain.GetItem(0, "deptgrp_code1") == 1) { gcode = "'00'"; }
//                   if (dsMain.GetItem(0, "deptgrp_code2") == 1) { gcode = "'02'"; }
//                   if (dsMain.GetItem(0, "deptgrp_code3") == 1) { gcode = "'01'"; }

//                   var Hdgroup =Gcoop.GetEl("HdDeptgroup").value;
//                   if (Gcoop.GetEl("HdDeptgroup").value != "") {
//                       Gcoop.GetEl("HdDeptgroup").value = Gcoop.GetEl("HdDeptgroup").value + "," + gcode;
//                   } else {
//                       Gcoop.GetEl("HdDeptgroup").value = gcode;
//                   }                                      
               }
           }
           if (c == "memno") {
               PostMemberNo();              
           }

           if (c == "language") {
               PostName();      
           }

       }
       function init_memname(order) {          
           if (order == '1') {            
           dsMain.SetItem(0, "seconder", "");
           dsMain.GetElement(0, 'seconder').disabled = true;
           dsMain.GetElement(0, 'seconder').style.background = '#CCCCCC';
       } else {        
           dsMain.GetElement(0, 'seconder').disabled = false;
           dsMain.GetElement(0, 'seconder').style.background = '#FFFFFF';
           }
       }

       function OnDsMainClicked(s, r, c) {
           if (c == "b_deptno") {
               var memno = dsMain.GetItem(0, "memno");
               var deptgrp_code1 = dsMain.GetItem(0, "deptgrp_code1");
               if (memno == null || deptgrp_code1 == "0"){
                   alert("กรูณาเลือกรับรองเงินฝาก หรือ กรอกเลขสมาชิกให้ถูกต้อง"); return;
               } else {
                   var groupcode = "'00','01','02'";
                    Gcoop.OpenIFrame("800", "500", "../../../CriteriaIReport/u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user/w_dlg_deptno_ctrl/w_dlg_deptno.aspx", "?memno=" + memno + "&groupcode=" + groupcode);
                   //Gcoop.OpenDlg("800", "500", "STK/GCOOP/Saving/CriteriaIReport/u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user/w_dlg_deptno_ctrl/w_dlg_deptno.aspx", "?memno=" + memno + "&groupcode=" + groupcode);
               }
            }
            
        }
        //function OnDsMainItemChanged(s, r, c, v) {
            
          //  if (c == "seconder") {
           //     alert(c); //return;
             //   PostName();
         //   }
       // }
       function GetDepnoFromDlg(deptno) {
           dsMain.SetItem(0, "deptaccount_no", deptno.trim());
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <uc1:DsMain ID="dsMain" runat="server" />
    <asp:HiddenField ID="HdDeptgroup" runat="server" />
</asp:Content>
