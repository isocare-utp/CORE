<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_accmaster.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_accmaster" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- protected String postNewClear;
        protected String postShowTreeviewAll;
        protected String postNoShowTreeviewAll;
        protected String postEditData;
        protected String postDeleteAccountId;
        protected String postFindShow;--%>
    <%=postNewClear%>
    <%=postShowTreeviewAll%>    
    <%=postNoShowTreeviewAll%>
    <%=postEditData %>
    <%=postDeleteAccountId%>
    <%=postFindShow%>
    <style type="text/css">
        .style5
        {
            font-size: small;
            font-weight: bold;
        }
        .style6
        {
            font-size: small;
        }
        </style>
    <%--protected String postNewClear;
        protected String postShowTreeviewAll;
        protected String postNoShowTreeviewAll;
        protected String postEditData;
        protected String postDeleteAccountId;--%>

    <script type="text/javascript">
    
    function MenubarOpen()
    {
      //   Gcoop.OpenIFrame(500,500, "w_dlg_search_accmaster.aspx","");
        Gcoop.OpenDlg(600,500, "w_dlg_search_accmaster.aspx","")
    }
    function OnFindShow(acc_id)
    {
        Gcoop.GetEl("HdAccNo").value = acc_id; 
        postFindShow();   
    }
    
    function OnDwMainClick(s,r,c)
    {
        Gcoop.CheckDw(s, r, c, "on_report", 1, 0);
        Gcoop.CheckDw(s, r, c, "active_status", 1, 0);
//        if (c == "on_report"){
//            Gcoop.CheckDw(s, r, c, "on_report", 1, 0);
//        }else if(c == "active_status"){
//            Gcoop.CheckDw(s, r, c, "active_status", 1, 0);
//        }
       // return 0;
    }
    
    function DeleteAccountId()
    {
        var account_id = "";
        account_id = objDw_master.GetItem(1,"account_id");
        if(confirm("ยืนยันการลบข้อมูลรหัสบัญชี : "+ account_id)){
              postDeleteAccountId();
        } 
        return 0;
    }
    
    //Clear หน้าจอ
    function MenubarNew()
    {
       if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
          postNewClear();
       }
  }
    //แก้ไขข้อมูล
    function EditData()
    {
        postEditData();
    }
    // แสดง Tree ทั้งหมด
    function ShowTreeview()
    {
        postShowTreeviewAll();
    }
    // ย่อ Tree ทั้งหมด
    function NoShowTreeview()
    {
        postNoShowTreeviewAll();
    }

     //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        try{
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");
            
            if (!isconfirm ){
                return false;
            }
            
            var alertstr = "";
            var account_id = objDw_master.GetItem(1,"account_id");
            var section_id = objDw_master.GetItem(1,"section_id");
            var account_type_id = objDw_master.GetItem(1,"account_type_id");
            var account_name =  objDw_master.GetItem(1,"account_name");
            var account_group_id = objDw_master.GetItem(1,"account_group_id");
            var account_level = objDw_master.GetItem(1, "account_level");
            var account_control_id = objDw_master.GetItem(1, "account_control_id");
            var account_nature = objDw_master.GetItem(1,"account_nature");
            var account_activity =  objDw_master.GetItem(1,"account_activity");
            var rev_group = objDw_master.GetItem(1, "account_rev_group");

            if (account_id == "" || account_id == null){
                alertstr = alertstr + "_กรุณากรอกรหัสบัญชี\n";
            }
            if (section_id == "" || section_id == null){
                alertstr = alertstr+ "_กรุณากรอกแผนก/ธุรกิจ\n";
            }
             if (account_type_id == "" || account_type_id == null){
                alertstr = alertstr+ "_กรุณากรอกประเภทบัญชี\n";
            }
             if (account_name == "" || account_name == null){
                alertstr = alertstr+ "_กรุณากรอกชื่อบัญชี\n";
            }
            
             if (account_group_id == "" || account_group_id == null){
                alertstr = alertstr+ "_กรุณากรอกหมวดบัญชี\n";
            }
             if (account_level == "" || account_level == null){
                alertstr = alertstr+ "_กรุณากรอกระดับบัญชี\n";
            }
             if (account_nature == "" || account_nature == null){
                alertstr = alertstr+ "_กรุณากรอกฝั่งบัญชี\n";
            }
             if (account_activity == "" || account_activity == null){
                alertstr = alertstr+ "_กรุณากรอกกิจกรรม\n";
            }
            if (rev_group == "" || rev_group == null) {
                alertstr = alertstr+ "_กรุณากรอกหมวดงบการเงิน\n";
            }
            if (account_control_id == null || account_control_id ==""  && account_level > "1") {
                alertstr = alertstr + "_กรุณากรอกรหัสบัญชีคุมยอด\n";
            }
            if (alertstr == "") {
                return true;
                alert(objDw_master.Describe("Datawindow.Data.Xml"));
            }
            else {
                alert(alertstr);
                return false;
            }
        }catch (err){
            alert (err); 
            return false;
        }
    }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width:100%;">
            <tr>
                <td class="style5" colspan="2">
                    รายละเอียดผังบัญชี</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="185px" BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            DataWindowObject="d_acc_accmaster_add_service" LibraryList="~/DataWindow/account/accmaster.pbl"
                            ClientEventClicked="OnDwMainClick" ClientFormatting="True" Height="155px">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                    <asp:Button ID="B_add" runat="server" OnClick="B_add_Click" Text="เพิ่มข้อมูล" Width="70px"
                        UseSubmitBehavior="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel2" runat="server" Width="496px">
                                                            <asp:Button ID="B_edit" runat="server" Text="แก้ไขข้อมูล" Width="70px" OnClick="B_edit_Click"
                            UseSubmitBehavior="False" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <input ID="ButtonDelete" onclick="DeleteAccountId()" type="button" 
                                                                value="ลบข้อมูล" />
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <span class="linkSpan" onclick="ShowTreeview()" style="font-family: Tahoma; font-size: small;
                        float: left; color: #0000CC;">แสดงผังบัญชีทั้งหมด&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                     </span><span class="linkSpan" onclick="NoShowTreeview()" style="font-family: Tahoma;
                        font-size: small; float: left; color: #0000CC;">ย่อผังบัญชีทั้งหมด</span></td>
            </tr>
            <tr>
                <td class="style6">
                    ผังบัญชี :
                    <asp:Label ID="lbl_coopname" runat="server" Text="coopname"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" EnableClientScript="False"
                        ImageSet="Arrows" OnTreeNodePopulate="TreeView1_TreeNodePopulate" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                        OnTreeNodeExpanded="TreeView1_TreeNodeExpanded">
                        <ParentNodeStyle Font-Bold="False" ForeColor="#FF0066" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <RootNodeStyle ForeColor="#0000CC" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <LeafNodeStyle ForeColor="#009900" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hd_aistatus" runat="server" />
                    <asp:HiddenField ID="Hd_checkclear" runat="server" Value="false" />
                    <asp:HiddenField ID="Hd_accid_master" runat="server" />
                    <asp:HiddenField ID="HdAccNo" runat="server" />
  
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
