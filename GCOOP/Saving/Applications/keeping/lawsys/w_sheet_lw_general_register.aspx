<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_lw_general_register.aspx.cs"
    Inherits="Saving.Applications.lawsys.w_sheet_lw_general_register" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=RetriveMain%>    
    <%=initGenRegNo%>
    <%=DeleteFileName%>
    <%=DeleteRegister%>
    
    <script type="text/javascript">
        function OnDwMainItemChanged(sender, rowNumber, colunmName, newValue){
            if(colunmName == "genreg_no"){
                objDwMain.SetItem(rowNumber, colunmName, newValue);
                objDwMain.AcceptText();
                var genReg = objDwMain.GetItem(rowNumber , colunmName);
                if(genReg.length > 2){
                    initGenRegNo();
                }
                else{
                    alert("เลขที่ทะเบียนไม่ถูกต้อง ควรมีค่าเป็น YYXXXXXX เช่น \n541 = 54000001 \n54001 = 54000001" );
                }
            }
            else if(colunmName == "register_tdate" || colunmName == "return_tdate"){
                objDwMain.SetItem(rowNumber, colunmName, newValue);
                objDwMain.AcceptText();
                return 0;
            }
        }
        function CheckFiledBeforeSave(){
            var title = "";
            var desc = "";
            var regisDate = "";
            var regisFrom = "";
            var returnDate = "";
            var check = false;
            try{
                title = objDwMain.GetItem(1, "register_title");
            }catch(err){ title = "";}
            try{
                desc = objDwMain.GetItem(1, "register_desc");
            }catch(err){ desc = "";}
            try{
                regisDate = objDwMain.GetItem(1, "register_tdate");
            }catch(err){ regisDate = "";}
            try{
                returnDate = objDwMain.GetItem(1, "return_tdate");
            }catch(err){ returnDate = "";}
            try{
                regisFrom = objDwMain.GetItem(1, "register_from");
            }catch(err){ regisFrom = "";}
            if(title != "" && title != null && desc != "" && desc != null &&regisDate != "//      " && regisFrom != "" && regisFrom != null && returnDate != "//      "){
                check = true;
            }else{ check = false; }
            return check;
        }
        function DeleteFile(filename){
            Gcoop.GetEl('HdDeleteFileName').value = filename;
            checkBeforeDelete = confirm("ต้องการลบไฟล์เอกสารใช่หรือไม่?");
            if (checkBeforeDelete)
            {
               DeleteFileName();
            }
        }
        function DeleteRegis(){
            checkBeforeDelete = confirm("ต้องการลบใช่หรือไม่?");
            if (checkBeforeDelete)
            {
               DeleteRegister();
            }
        }
        function Validate(){
            var genRegNo = Gcoop.GetEl("HdGenRegNo").value;
            var regNo = "";
            try{
                regNo = objDwMain.GetItem(1, "genreg_no");
            }catch(err){ regNo = ""; }
            var checkBeforeSave = false;
            if(genRegNo == "true" && regNo != null && regNo != ""){
                var regNo = objDwMain.GetItem(1, "genreg_no");
                checkBeforeSave = CheckFiledBeforeSave();
                if(checkBeforeSave == true){
                    return confirm("ต้องการแก้ไขข้อมูลเลขที่ทะเบียน " + regNo + " ใช่หรือไม่?");
                }
                else{
                    alert("กรุณากรอกข้อมูลให้ครบถ้วน");
                }
            }
            else{
                checkBeforeSave = CheckFiledBeforeSave();
                if(checkBeforeSave == true){
                    return confirm("ต้องการเพิ่มข้อมูลเลขที่ทะเบียน ใช่หรือไม่?");
                }
                else{
                    alert("กรุณากรอกข้อมูลให้ครบถ้วน");
                }
            }
        }
        
        function MenubarOpen(){
            Gcoop.OpenDlg("630px", "550px", "w_dlg_search_generalreg.aspx", "");
        }
        
        function GetGenRegNoFromDlg(genRegNo){
            Gcoop.GetEl("HdGetFromDlg").value = genRegNo;
            RetriveMain();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <asp:Label ID="Label1" runat="server" Text="ลงทะเบียนกฎหมาย" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="16px" Font-Underline="True" />
    <br />
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_lw_general_register"
        LibraryList="~/DataWindow/lawsys/lw_general_register.pbl" 
        ClientFormatting="True" ClientEventButtonClicked="OnDwMainButtonClicked"
        ClientEventItemChanged="OnDwMainItemChanged" ForeColor="Blue">
    </dw:WebDataWindowControl>
    <br />
    <input id="Button1" type="button" value="ลบทั้งทะเบียนนี้" 
        onclick="DeleteRegis()"/><br />
    <br />
&nbsp;<asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;อัพโหลดเอกสาร :" 
        BackColor="#D3E7FF" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Tahoma" Font-Size="10pt" Height="22px" Width="104px"></asp:Label>
    &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="BtnSave" runat="server" Text="อัพโหลด" 
        onclick="Button1_Click" />
&nbsp;&nbsp;<asp:Label ID="lblAlert" runat="server" ForeColor="Red" 
        Text="ไม่ได้ใส่เลขที่ทะเบียน" Visible="False"></asp:Label>
    <br />
&nbsp;<asp:GridView ID="GvShow" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" Width="705px" Height="58px" 
        onselectedindexchanged="GvShow_SelectedIndexChanged" GridLines="None" 
        >
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" Font-Names="tohoma" Font-Size="12pt" />
        <Columns>
            <asp:BoundField DataField="FileName" HeaderText="ชื่อไฟล์" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Download") %>'>ดาวน์โหลด</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                <span onclick="DeleteFile('<%#Eval("FileName")%>')" onmouseover="style.cursor='hand'">
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Underline="True" 
                        ForeColor="Blue">ลบ</asp:HyperLink>
                </span>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#D3E7FF" Font-Bold="True" ForeColor="Black" 
            Font-Names="Tahoma" Font-Size="11pt" Font-Strikeout="False" 
            HorizontalAlign="Center" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:HiddenField ID="HdGenRegNo" runat="server" Value="false" />
    &nbsp;<asp:HiddenField ID="HdGetFromDlg" runat="server" Value="false" />
    <asp:HiddenField ID="HdDeleteFileName" runat="server" />
</asp:Content>
