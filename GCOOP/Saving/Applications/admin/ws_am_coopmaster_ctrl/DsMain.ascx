<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin.ws_am_coopmaster_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>

                <td>
                    <div>
                        <span>รหัสสหกรณ์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="coop_no" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>ชื่อสหกรณ์:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="coop_name" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ที่อยู่สหกรณ์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox  ID="coop_addr" runat="server" Style="text-align: center; " ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="province_code" runat="server" Style="font-size: 12px;">
                    </asp:DropDownList> 
                                                             
                </td>                                                        
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เขต/อำเภอ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="district_code" runat="server" Style="font-size: 12px;">
                    </asp:DropDownList>
                                                              
                </td>
                <td>
                    <div>
                        <span>แขวง/ตำบล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox  ID="tambol" runat="server" Style="text-align: center; " ></asp:TextBox>
                </td>
                                                    
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสไปรษณีย์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox  ID="postcode" runat="server" Style="text-align: center; " ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>หมายเลขโทรศัพท์:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="coop_tel" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมายเลขแฟกซ์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="coop_fax" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>ประธาน:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="chairman" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้จัดการ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="manager" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>ผู้ช่วยผู้จัดการ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="vicemanager" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้ช่วย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="assistent" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>เจ้าหน้าที่ฝ่ายบัญชี:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="office_account" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เจ้าหน้าที่ฝ่ายการเงิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="office_finance" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>ผู้ตรวจสอบบัญชี:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="auditor" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>                                              
            </tr>
            <tr>
                <td >
                    <div>
                        <span>ที่อยู่ผู้ตรวจสอบบัญชี:</span>
                    </div>
                </td>
                <td colspan="5"> 
                    <asp:TextBox ID="auditor_addr" runat="server" Style="text-align: center; width : 595px;" ></asp:TextBox>
                </td>                                                      
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เกษียณอายุ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="retry_age" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เดือนที่เกษียณอายุ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="retry_month" runat="server" Style="text-align: center;" ></asp:TextBox>
                </td>  
                                                                      
            </tr>
            
        </table>
    </EditItemTemplate>
</asp:FormView>
