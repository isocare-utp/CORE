<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="wd_ass_set_calculator.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_ass_set_calculator_ctrl.wd_ass_set_calculator" %>
<%@ Register Src="DsMaster.ascx" TagName="DsMaster" TagPrefix="uc1" %>
<%@ Register Src="DsData.ascx" TagName="DsData" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMaster = new DataSourceTool;
        var dsData = new DataSourceTool;


        function B_select_Click() {
            PostSave();
            window.close();
        }

        function SaveData() {
            var isConfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
            if (isConfirm) {
                PostSaveData();
            }
        }
        function OnDsDataClicked(s, r, c, v) {
            //alert(c);
            if (c == "b_del") {
                Postdel();
            }
        }
        function OnCloseDialog() {
            parent.RemoveIFrame();
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 621px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<center>
<table style="width:100%" >

    
    <tr>
    <td >
    <asp:Panel ID="Panel4" runat="server" Width="600px" BorderStyle="Ridge" Height="450px"  >
    <uc2:DsData ID="dsData" runat="server" style="width:600px"/>
        </asp:Panel>
    </td>
    <td >
    <asp:Panel ID="Panel1" runat="server" Width="600px" BorderStyle="Ridge" Height="450px" > 
    <uc1:DsMaster ID="dsMaster" runat="server" style="width:600px"/>
     </asp:Panel>
   
    </td>
    </tr>
    <tr>
    
                <td valign="top" class="style10">
              <div style="overflow: hidden;"> </div>
                <br />
                </td>
                <td valign="top" class="style1">
                <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <input id="b_add" type="button" value="&lt; &lt;" onclick="B_select_Click()" style="width:60px; height:30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="b_save" type="button" value="ตกลง" onclick="SaveData()"  style="width:60px; height:30px"/>&nbsp;
                    <input id="b_cancel" type="button" value="ยกเลิก/ปิดหน้าจอ" onclick="OnCloseDialog()" style="width:100px; height:30px" />
                    </td>
            </tr>
  
    <asp:HiddenField ID="HdIndex" runat="server" />
            <asp:HiddenField ID="HdRow" runat="server" />
               <asp:HiddenField ID="HdTotal" runat="server" />
               </table>
               </center>
                <asp:HiddenField ID="Hd_datadesc" runat="server" />
               <asp:HiddenField ID="HD_sheetseq" runat="server" />
               <asp:HiddenField ID="HD_sheetcode" runat="server" />
               <asp:HiddenField ID="Hd_seq" runat="server" />
</asp:Content>


