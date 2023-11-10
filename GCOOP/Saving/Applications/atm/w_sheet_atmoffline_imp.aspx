<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_atmoffline_imp.aspx.cs"
    Inherits="Saving.w_sheet_atmoffline_imp" Title="Untitled Page" %>
<%@ Register tagprefix="CustomControls" namespace="CoreSavingLibrary" Assembly="CoreSavingLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsUploadFile%>
    <%=jsSaveData%>
    <%=jsPreviewFile%>
    <%=jsDownloadFile%>
    <%=jsCancelFile%>
    <%=jsPostFile%>
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
        function UploadFile() {
           if(confirm("ยืนยันการบันทึกรายการ"))
            jsUploadFile();
        }
        function DownloadFile() {
            jsDownloadFile();
        }
        function PreviewFile() {
            jsPreviewFile();
        }
        function SaveData() {
            if (confirm("ยืนยันการบันทึกรายการ"))
            jsSaveData();
        }
        function CancelFile() {
            if (confirm("ยืนยันการบันทึกรายการ"))
            jsCancelFile();
        }
        function PostFile() {
            if (confirm("ยืนยันการบันทึกรายการ"))
            jsPostFile();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<div>
<asp:Label ID="Label1" runat="server" Text="FILE_FORMAT_CODE : "></asp:Label>
<asp:DropDownList ID="DpFileFormatCode" runat="server"  Width="400px"></asp:DropDownList><br />
<asp:FileUpload ID="fiUpload" runat="server" />
<asp:Label ID="Label2" runat="server" Text="Lists Files : "></asp:Label>
<asp:DropDownList ID="DpFileLists" runat="server"  Width="250px" ></asp:DropDownList>
<input type="button" value="PreviewFile" onclick="PreviewFile();" />
<input type="button" value="DownloadFile" onclick="DownloadFile();" /><br />
<input type="button" value="0.ดึงรายการรอ Post" onclick="New_Toolbar();" />
<input type="button" value="1.Upload File" onclick="UploadFile();" />
<input type="button" value="2.นำเข้าข้อมูลรอ Post" onclick="SaveData();" />
<input type="button" value="3.ยกเลิกรายการรอ Post" onclick="CancelFile();" />
<input type="button" value="4.Post รายการ" onclick="PostFile();" /><br />
<%=output%>
    <asp:HiddenField ID="HdFilename" runat="server" Value="" />
    <div style="overflow:scroll;width:750px;height:550px;"><div style="width:950px">
        <customcontrols:dynamicallygridview ID="TableGrid" AllowSorting="true" 
        TableName="ATMTMPTRANSDATA" PageSize="20" TableSortColumns="operate_date asc"
        TableQuery="select * from ATMTMPTRANSDATA where export_status=-9 and post_status=0 and item_status<>-9 "
        TableKeyColumns=" COOP_ID,MEMBER_NO, OPERATE_DATE,BANK_CODE,ATM_NO,ATM_SEQNO,SAVING_ACC"
        TableShowColumns="MEMBER_NO,REF_ACCNO,SAVING_ACC,OPERATE_DATE,SYSTEM_CODE,OPERATE_CODE,BANK_CODE,ATM_NO,ATM_SEQNO,ITEM_AMT,ITEM_STATUS,POST_STATUS"
        TableShowTitleColumns="MEMBER_NO=ทะเบียน,REF_ACCNO=เลขอ้างอิง,SAVING_ACC=บัญชี,OPERATE_DATE=วันทำการ,SYSTEM_CODE=ระบบ,OPERATE_CODE=ประเภท,BANK_CODE=ธนาคาร,ATM_NO=รหัสตู้,ATM_SEQNO=ลำดับตู้,ITEM_AMT=ยอดเงิน,ITEM_STATUS=สถานะ,POST_STATUS=สถานะ post"
        TableUpdateColumns="OPERATE_DATE,BANK_CODE,ATM_NO,ATM_SEQNO,ITEM_AMT,ITEM_STATUS,POST_STATUS"
        ShowSQL="True" runat="server" ></customcontrols:dynamicallygridview>
        <% /*<asp:GridView ID="TableGridView" runat="server"  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ></asp:GridView> */ %>
        <%=TableGrid.MsgTextHtml%>
		</div></div>
</div>
</asp:Content>
