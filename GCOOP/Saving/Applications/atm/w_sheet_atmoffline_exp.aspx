<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_atmoffline_exp.aspx.cs"
    Inherits="Saving.w_sheet_atmoffline_exp" Title="Untitled Page" %>
<%@ Register tagprefix="CustomControls" namespace="CoreSavingLibrary" Assembly="CoreSavingLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%=jsPreviewFile%>
    <%=jsDownloadFile%>
    <%=jsCancelFile%>
    <%=jsExportFile%>
    <script type="text/javascript">
        $(function () {

            $('input[name="start_tdate_0"]').keyup(function () {
                ActiveChangeFormatDate('input[name="start_tdate_0"]');
            });
            $('input[name="end_tdate_0"]').keyup(function () {
                ActiveChangeFormatDate('input[name="end_tdate_0"]');
            });

        });
        function Validate() {
            //objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกรายการ");
        }
        function DownloadFile() {
            jsDownloadFile();
        }
        function PreviewFile() {
            jsPreviewFile();
        }
        function CancelFile() {
            jsCancelFile();
        }
        function ExportFile() {
            jsExportFile();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<div>
<asp:Label ID="Label1" runat="server" Text="FILE_FORMAT_CODE"></asp:Label>
<asp:DropDownList ID="DpFileFormatCode" runat="server"  Width="400px"></asp:DropDownList><br />
<asp:Label ID="Label2" runat="server" Text="Lists Files : "></asp:Label>
<asp:DropDownList ID="DpFileLists" runat="server"  Width="250px" ></asp:DropDownList><br />
<input type="button" value="1.ExportFile" onclick="ExportFile();" />
<input type="button" value="2.PreviewFile" onclick="PreviewFile();" />
<input type="button" value="3.DownloadFile" onclick="DownloadFile();" />
<input type="button" value="4.CancelFile" onclick="CancelFile();" /><br />
<br />
<%=output%>
    <asp:HiddenField ID="HdFilename" runat="server" Value="" />
    <div style="overflow:scroll;width:750px;height:550px;"><div style="width:950px">
        <customcontrols:dynamicallygridview ID="TableGrid" AllowSorting="true" 
        TableName="ATMTMPTRANSDATA" PageSize="20" TableSortColumns="operate_date asc"
        TableQuery="select * from ATMTMPTRANSDATA where export_status=-9 and post_status=0 and item_status<>-9 "
        TableKeyColumns=" COOP_ID,MEMBER_NO, OPERATE_DATE,BANK_CODE,ATM_NO,ATM_SEQNO,SAVING_ACC"
        TableShowColumns="MEMBER_NO,SAVING_ACC,OPERATE_DATE,SYSTEM_CODE,OPERATE_CODE,BANK_CODE,ATM_NO,ATM_SEQNO,ITEM_AMT,ITEM_STATUS,POST_STATUS"
        TableShowTitleColumns="MEMBER_NO=ทะเบียน,SAVING_ACC=บัญชี,OPERATE_DATE=วันทำการ,SYSTEM_CODE=ระบบ,OPERATE_CODE=ประเภท,BANK_CODE=ธนาคาร,ATM_NO=รหัสตู้,ATM_SEQNO=ลำดับตู้,ITEM_AMT=ยอดเงิน,ITEM_STATUS=สถานะ,POST_STATUS=สถานะ post"
        TableUpdateColumns="OPERATE_DATE,BANK_CODE,ATM_NO,ATM_SEQNO,ITEM_AMT,ITEM_STATUS,POST_STATUS"
        ShowSQL="True" runat="server" ></customcontrols:dynamicallygridview>
        <% /*<asp:GridView ID="TableGridView" runat="server"  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ></asp:GridView> */ %>
        <%=TableGrid.MsgTextHtml%>
		</div></div>
</div>
</asp:Content>
