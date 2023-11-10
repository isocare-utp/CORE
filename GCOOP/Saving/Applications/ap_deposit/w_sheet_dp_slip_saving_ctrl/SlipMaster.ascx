<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlipMaster.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_slip_saving_ctrl.SlipMaster" %>
<style type="text/css">
    *
    {
        padding: 0;
        margin: 0;
    }
    input[type="text"]
    {
        border: 1px solid #000000;
        height: 20px;
    }
    .LabelFormView1
    {
        background-color: rgb(211, 231, 255);
        border: 1px solid #000000;
        color: Black;
        text-align: right;
        font-family: Tahoma;
        font-size: 13px;
    }
    #totalAmount
    {
        font-size: 18px;
        font-family: Tahoma;
        color: #00AB44;
    }
</style>
<script type="text/javascript" language="javascript">
    function DeptAccountNoKeyUp(item, e) {
        var charCode;
        if (e && e.which) {
            charCode = e.which;
        } else if (window.event) {
            e = window.event;
            charCode = e.keyCode;
        }
        if (charCode == 13) {
            postDeptAccountNo();
        }
    }

    function ReplaceAll(text, t1, t2) {
        var text2 = text;
        while (text2.indexOf(t1) >= 0) {
            text2 = text2.replace(t1, t2);
        }
        return text2;
    }

    function DeptSlipAmtKeyUp(item, e) {
        var charCode;
        if (e && e.which) {
            charCode = e.which;
        } else if (window.event) {
            e = window.event;
            charCode = e.keyCode;
        }
        if (charCode == 13) {
            try {
                var pd = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_recppaytype_code').value;
                var prnbal = document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_prncbal').value + "";
                var f1 = parseFloat(item.value);
                var f2 = parseFloat(ReplaceAll(prnbal, ",", ""));
                var f3 = 0;
                if (pd == "DEP") {
                    f3 = f2 + f1;
                } else if (pd == "WID") {
                    f3 = f2 - f1;
                } else {
                    Gcoop.GetEl("totalAmount").innerHTML = "";
                    return;
                }
                Gcoop.GetEl("totalAmount").innerHTML = "ยอดฯหลังทำรายการ = " + Gcoop.NumberFormat(f3) + " &nbsp; &nbsp; &nbsp;";
            } catch (err) {
            }
            item.value = Gcoop.NumberFormat(item.value);
            document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptslip_netamt').value = item.value;
        }
    }

    function RecppaytypeCodeKeyUp(item, e) {
        var charCode;
        if (e && e.which) {
            charCode = e.which;
        } else if (window.event) {
            e = window.event;
            charCode = e.keyCode;
        }
        if (charCode == 13) {
            document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptslip_amt').focus();
            document.getElementById('ctl00_ContentPlace_SlipMaster1_FormView1_Fdb_deptslip_amt').select();
        }
    }
</script>
<asp:FormView ID="FormView1" runat="server">
    <InsertItemTemplate>
        <asp:Label ID="LbMainText" runat="server" Text="ฝากถอนเงินออมทรัพย์ (เงินสด)" Font-Bold="True"
            Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
        &nbsp; &nbsp;
        <asp:TextBox ID="Fdb_deptformat" BorderStyle="None" ReadOnly="true" runat="server"
            Width="85px" Text='<%#Bind("Fdb_deptformat")%>' ForeColor="#0099CC" Font-Bold="true"
            Font-Size="14px"></asp:TextBox>
        <br />
        <br />
        <table width="760" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="12%" height="30">
                    <asp:Label ID="LbAccNo" runat="server" Text="เลขที่บัญชี:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="22%">
                    <asp:DropDownList ID="Fdb_deptcoop_id" Enabled="false" Width="70px" BackColor="#FFFFBB"
                        runat="server" DataTextField="coop_id" DataValueField="coop_id" SelectedValue='<%#Bind("Fdb_deptcoop_id")%>'
                        DataSourceID="ObjDdCoopId" AppendDataBoundItems="true" ForeColor="Red" Font-Bold="true">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="Fdb_deptaccount_no" runat="server" Width="85px" BackColor="#FFFFBB"
                        Text='<%#Eval("Fdb_deptaccount_no")%>' onkeyup="DeptAccountNoKeyUp(this, event)"
                        onfocus="this.select()" TabIndex="1" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:Label ID="Label3" runat="server" Text="ทำรายการ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="21%">
                    <asp:DropDownList ID="Fdb_recppaytype_code" Width="122" BackColor="#FFFFBB" runat="server"
                        AppendDataBoundItems="true" TabIndex="2" ForeColor="#0000FF" Font-Bold="true">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="DEP">+ทำรายการฝาก</asp:ListItem>
                        <asp:ListItem Value="WID">-ทำรายการถอน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <asp:Label ID="Label2" runat="server" Text="วันที่ทำการ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="21%">
                    <asp:TextBox ID="Fdb_deptslip_date" runat="server" Text='<%#Bind("Fdb_deptslip_date", "{0:MM/dd/yyyy}")%>'
                        Width="124px" ForeColor="Blue" Font-Bold="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <asp:Label ID="LbPrincBal" runat="server" Text="ยอดคงเหลือ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Fdb_prncbal" runat="server" Width="146px" Text='<%#Bind("Fdb_prncbal", "{0:#,###0.00}")%>'
                        ReadOnly="true" ForeColor="Blue" Font-Bold="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:Label ID="LbWidrawable" runat="server" Text="ชื่อบัญชี" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Fdb_deptaccount_name" runat="server" Text='<%#Bind("Fdb_deptaccount_name")%>'
                        ReadOnly="true" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="Fdb_member_no" runat="server" Text='<%#Bind("Fdb_member_no")%>'
                        ReadOnly="true" Width="118px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <asp:Label ID="Label4" runat="server" Text="เงื่อนไขถอน:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="Fdb_deptaccount_ename" runat="server" Text='<%#Bind("Fdb_deptaccount_ename")%>'
                        ReadOnly="true" Width="630px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="50" colspan="6">
                    <table>
                        <tr>
                            <td width="50%">
                            </td>
                            <td width="50%">
                                <asp:Label ID="Label5" runat="server" Text="ยอดทำรายการ:" CssClass="LabelFormView1"
                                    Height="30px" Width="130px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_deptslip_amt" runat="server" Text='<%#Eval("Fdb_deptslip_amt", "{0:#,##0}")%>'
                                    Width="222px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="16px" Font-Bold="True" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="50" colspan="6">
                    <table style="visibility: hidden">
                        <tr>
                            <td width="50%">
                                <asp:Label ID="Label1" runat="server" Text="ค่าปรับ:" CssClass="LabelFormView1" Height="30px"
                                    Width="120px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_other_amt" runat="server" Text='<%#Bind("Fdb_other_amt", "{0:#,##0}")%>'
                                    Width="200px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="14px" Font-Bold="true" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td width="50%">
                                <asp:Label ID="Label9" runat="server" Text="ยอดรวม:" CssClass="LabelFormView1" Height="30px"
                                    Width="130px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_deptslip_netamt" runat="server" Text='<%#Bind("Fdb_deptslip_netamt", "{0:#,##0}")%>'
                                    Width="222px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="14px" Font-Bold="true" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="Fdb_deptslip_no" Value='<%#Bind("Fdb_deptslip_no")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField1" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptitemtype_code" Value='<%#Bind("Fdb_deptitemtype_code")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField2" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_cash_type" Value='<%#Bind("Fdb_cash_type")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField3" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_withdrawable_amt" Value='<%#Bind("Fdb_withdrawable_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_checkpend_amt" Value='<%#Bind("Fdb_checkpend_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_loangarantee_amt" Value='<%#Bind("Fdb_loangarantee_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_accuint_amt" Value='<%#Bind("Fdb_accuint_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_fee_amt" Value='<%#Bind("Fdb_fee_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preprncbal" Value='<%#Bind("Fdb_preprncbal")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preaccuint_amt" Value='<%#Bind("Fdb_preaccuint_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_entry_id" Value='<%#Bind("Fdb_entry_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_entry_date" Value='<%#Bind("Fdb_entry_date")%>' runat="server" />
        <asp:HiddenField ID="Fdb_intarrear_amt" Value='<%#Bind("Fdb_intarrear_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_dpstm_no" Value='<%#Bind("Fdb_dpstm_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_calint_from" Value='<%#Bind("Fdb_calint_from")%>' runat="server" />
        <asp:HiddenField ID="Fdb_calint_to" Value='<%#Bind("Fdb_calint_to")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_amt1" Value='<%#Bind("Fdb_int_amt1")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_return" Value='<%#Bind("Fdb_int_return")%>' runat="server" />
        <asp:HiddenField ID="Fdb_item_status" Value='<%#Bind("Fdb_item_status")%>' runat="server" />
        <asp:HiddenField ID="Fdb_closeday_status" Value='<%#Bind("Fdb_closeday_status")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField4" Value='<%#Bind("")%>' runat="server" />
        <asp:HiddenField ID="HiddenField5" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_prnc_no" Value='<%#Bind("Fdb_prnc_no")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField6" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptgroup_code" Value='<%#Bind("Fdb_deptgroup_code")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_count_wtd" Value='<%#Bind("Fdb_count_wtd")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField7" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_machine_id" Value='<%#Bind("Fdb_machine_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_nobook_flag" Value='<%#Bind("Fdb_nobook_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_authorize_id" Value='<%#Bind("Fdb_authorize_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_cheque_send_flag" Value='<%#Bind("Fdb_cheque_send_flag")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_remark" Value='<%#Bind("Fdb_remark")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tofrom_accid" Value='<%#Bind("Fdb_tofrom_accid")%>' runat="server" />
        <asp:HiddenField ID="Fdb_refer_slipno" Value='<%#Bind("Fdb_refer_slipno")%>' runat="server" />
        <asp:HiddenField ID="Fdb_refer_app" Value='<%#Bind("Fdb_refer_app")%>' runat="server" />
        <asp:HiddenField ID="Fdb_posttovc_flag" Value='<%#Bind("Fdb_posttovc_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tax_amt" Value='<%#Bind("Fdb_tax_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_bfyear" Value='<%#Bind("Fdb_int_bfyear")%>' runat="server" />
        <asp:HiddenField ID="Fdb_showfor_dept" Value='<%#Bind("Fdb_showfor_dept")%>' runat="server" />
        <asp:HiddenField ID="Fdb_accid_flag" Value='<%#Bind("Fdb_accid_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_transec_no" Value='<%#Bind("Fdb_transec_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preadjdoc_no" Value='<%#Bind("Fdb_preadjdoc_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preadjdoc_date" Value='<%#Bind("Fdb_preadjdoc_date")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_preadjitem_type" Value='<%#Bind("Fdb_preadjitem_type")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_voucher_no" Value='<%#Bind("Fdb_voucher_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_curyear" Value='<%#Bind("Fdb_int_curyear")%>' runat="server" />
        <asp:HiddenField ID="Fdb_genvc_flag" Value='<%#Bind("Fdb_genvc_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_payfee_meth" Value='<%#Bind("Fdb_payfee_meth")%>' runat="server" />
        <asp:HiddenField ID="Fdb_book_balance" Value='<%#Bind("Fdb_book_balance")%>' runat="server" />
        <asp:HiddenField ID="Fdb_peroid_dept" Value='<%#Bind("Fdb_peroid_dept")%>' runat="server" />
        <asp:HiddenField ID="Fdb_payfee_flag" Value='<%#Bind("Fdb_payfee_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_due_flag" Value='<%#Bind("Fdb_due_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_deptamt_other" Value='<%#Bind("Fdb_deptamt_other")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField8" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptwith_flag" Value='<%#Bind("Fdb_deptwith_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_operate_time" Value='<%#Bind("Fdb_operate_time")%>' runat="server" />
        <asp:HiddenField ID="Fdb_teller_flag" Value='<%#Bind("Fdb_teller_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_check_no" Value='<%#Bind("Fdb_check_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_bank_code" Value='<%#Bind("Fdb_bank_code")%>' runat="server" />
        <asp:HiddenField ID="Fdb_bankbranch_code" Value='<%#Bind("Fdb_bankbranch_code")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField9" Value='<%#Bind("")%>' runat="server" />--%>
        <%--<asp:HiddenField ID="Fdb_deptformat" Value='<%#Bind("Fdb_deptformat")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_spint" Value='<%#Bind("Fdb_spint")%>' runat="server" />
        <asp:HiddenField ID="Fdb_spint_status" Value='<%#Bind("Fdb_spint_status")%>' runat="server" />
        <asp:HiddenField ID="Fdb_send_gov" Value='<%#Bind("Fdb_send_gov")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField10" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_coop_id" Value='<%#Bind("Fdb_coop_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_group_itemtpe" Value='<%#Bind("Fdb_group_itemtpe")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tax_return" Value='<%#Bind("Fdb_tax_return")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField11" Value='<%#Bind("")%>' runat="server" />
        <asp:HiddenField ID="HiddenField12" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_depttype_code" Value='<%#Eval("Fdb_depttype_code")%>' runat="server" />
        <asp:ObjectDataSource ID="ObjDdCoopId" runat="server" SelectMethod="DdCoopId" TypeName="Saving.Applications.ap_deposit.w_sheet_dp_slip_saving_ctrl.SlipMaster">
        </asp:ObjectDataSource>
    </InsertItemTemplate>
    <ItemTemplate>
        <asp:Label ID="LbMainText" runat="server" Text="ฝากถอนเงินออมทรัพย์ (เงินสด)" Font-Bold="True"
            Font-Names="Tahoma" Font-Size="14px" Font-Underline="True" ForeColor="#0099CC"></asp:Label>
        &nbsp; &nbsp;
        <asp:TextBox ID="Fdb_deptformat" BorderStyle="None" ReadOnly="true" runat="server"
            Width="285px" Text='<%#Bind("Fdb_deptformat")%>' ForeColor="#0000FF" Font-Bold="true"
            Font-Size="14px"></asp:TextBox>
        <br />
        <br />
        <table width="760" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="12%" height="30">
                    <asp:Label ID="LbAccNo" runat="server" Text="เลขที่บัญชี:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="22%">
                    <asp:DropDownList ID="Fdb_deptcoop_id" Enabled="false" Width="70px" BackColor="#FFFFBB"
                        runat="server" DataTextField="coop_id" DataValueField="coop_id" SelectedValue='<%#Bind("Fdb_deptcoop_id")%>'
                        DataSourceID="ObjDdCoopId" AppendDataBoundItems="true" ForeColor="Red" Font-Bold="true">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="Fdb_deptaccount_no" runat="server" Width="85px" BackColor="#FFFFBB"
                        Text='<%#Eval("Fdb_deptaccount_no")%>' onkeyup="DeptAccountNoKeyUp(this, event)"
                        onfocus="this.select()" TabIndex="1" ForeColor="Red" Font-Bold="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:Label ID="Label3" runat="server" Text="ทำรายการ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="21%">
                    <asp:DropDownList ID="Fdb_recppaytype_code" Width="122" BackColor="#AADEFF" runat="server"
                        AppendDataBoundItems="true" TabIndex="2" ForeColor="#0000FF" Font-Bold="true"
                        onkeyup="RecppaytypeCodeKeyUp(this, event)">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="DEP">+ทำรายการฝาก</asp:ListItem>
                        <asp:ListItem Value="WID">-ทำรายการถอน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <asp:Label ID="Label2" runat="server" Text="วันที่ทำการ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td width="21%">
                    <asp:TextBox ID="Fdb_deptslip_date" runat="server" Text='<%#Bind("Fdb_deptslip_date", "{0:dd/MM/yyyy}")%>'
                        Width="124px" ForeColor="Blue" Font-Bold="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <asp:Label ID="LbPrincBal" runat="server" Text="ยอดคงเหลือ:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Fdb_prncbal" runat="server" Width="146px" Text='<%#Bind("Fdb_prncbal", "{0:#,###0.00}")%>'
                        ReadOnly="true" ForeColor="Blue" Font-Bold="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:Label ID="LbWidrawable" runat="server" Text="ชื่อบัญชี" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="Fdb_deptaccount_name" runat="server" Text='<%#Bind("Fdb_deptaccount_name")%>'
                        ReadOnly="true" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="Fdb_member_no" runat="server" Text='<%#Bind("Fdb_member_no")%>'
                        ReadOnly="true" Width="118px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <asp:Label ID="Label4" runat="server" Text="เงื่อนไขถอน:" CssClass="LabelFormView1"
                        Height="20px" Width="80px"></asp:Label>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="Fdb_deptaccount_ename" runat="server" Text='<%#Bind("Fdb_deptaccount_ename")%>'
                        ReadOnly="true" Width="630px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="50" colspan="6">
                    <table>
                        <tr>
                            <td width="50%">
                            </td>
                            <td width="50%">
                                <asp:Label ID="Label5" runat="server" Text="ยอดทำรายการ:" CssClass="LabelFormView1"
                                    Height="30px" Width="130px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_deptslip_amt" runat="server" Text='<%#Eval("Fdb_deptslip_amt", "{0:#,##0}")%>'
                                    Width="222px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="16px" Font-Bold="True" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="50" colspan="6">
                    <table style="visibility: hidden">
                        <tr>
                            <td width="50%">
                                <asp:Label ID="Label1" runat="server" Text="ค่าปรับ:" CssClass="LabelFormView1" Height="30px"
                                    Width="120px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_other_amt" runat="server" Text='<%#Bind("Fdb_other_amt", "{0:#,##0}")%>'
                                    Width="200px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="14px" Font-Bold="true" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td width="50%">
                                <asp:Label ID="Label9" runat="server" Text="ยอดรวม:" CssClass="LabelFormView1" Height="30px"
                                    Width="130px" Font-Size="18px"></asp:Label>
                                <asp:TextBox ID="Fdb_deptslip_netamt" runat="server" Text='<%#Bind("Fdb_deptslip_netamt", "{0:#,##0}")%>'
                                    Width="222px" Height="30px" ForeColor="#00FF00" BorderColor="#009900" BackColor="black"
                                    Font-Size="14px" Font-Bold="true" onfocus="this.select()" onkeyup="DeptSlipAmtKeyUp(this, event)"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="Fdb_deptslip_no" Value='<%#Bind("Fdb_deptslip_no")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField1" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptitemtype_code" Value='<%#Bind("Fdb_deptitemtype_code")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField2" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_cash_type" Value='<%#Bind("Fdb_cash_type")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField3" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_withdrawable_amt" Value='<%#Bind("Fdb_withdrawable_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_checkpend_amt" Value='<%#Bind("Fdb_checkpend_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_loangarantee_amt" Value='<%#Bind("Fdb_loangarantee_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_accuint_amt" Value='<%#Bind("Fdb_accuint_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_fee_amt" Value='<%#Bind("Fdb_fee_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preprncbal" Value='<%#Bind("Fdb_preprncbal")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preaccuint_amt" Value='<%#Bind("Fdb_preaccuint_amt")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_entry_id" Value='<%#Bind("Fdb_entry_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_entry_date" Value='<%#Bind("Fdb_entry_date")%>' runat="server" />
        <asp:HiddenField ID="Fdb_intarrear_amt" Value='<%#Bind("Fdb_intarrear_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_dpstm_no" Value='<%#Bind("Fdb_dpstm_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_calint_from" Value='<%#Bind("Fdb_calint_from")%>' runat="server" />
        <asp:HiddenField ID="Fdb_calint_to" Value='<%#Bind("Fdb_calint_to")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_amt1" Value='<%#Bind("Fdb_int_amt1")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_return" Value='<%#Bind("Fdb_int_return")%>' runat="server" />
        <asp:HiddenField ID="Fdb_item_status" Value='<%#Bind("Fdb_item_status")%>' runat="server" />
        <asp:HiddenField ID="Fdb_closeday_status" Value='<%#Bind("Fdb_closeday_status")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField4" Value='<%#Bind("")%>' runat="server" />
        <asp:HiddenField ID="HiddenField5" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_prnc_no" Value='<%#Bind("Fdb_prnc_no")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField6" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptgroup_code" Value='<%#Bind("Fdb_deptgroup_code")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_count_wtd" Value='<%#Bind("Fdb_count_wtd")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField7" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_machine_id" Value='<%#Bind("Fdb_machine_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_nobook_flag" Value='<%#Bind("Fdb_nobook_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_authorize_id" Value='<%#Bind("Fdb_authorize_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_cheque_send_flag" Value='<%#Bind("Fdb_cheque_send_flag")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_remark" Value='<%#Bind("Fdb_remark")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tofrom_accid" Value='<%#Bind("Fdb_tofrom_accid")%>' runat="server" />
        <asp:HiddenField ID="Fdb_refer_slipno" Value='<%#Bind("Fdb_refer_slipno")%>' runat="server" />
        <asp:HiddenField ID="Fdb_refer_app" Value='<%#Bind("Fdb_refer_app")%>' runat="server" />
        <asp:HiddenField ID="Fdb_posttovc_flag" Value='<%#Bind("Fdb_posttovc_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tax_amt" Value='<%#Bind("Fdb_tax_amt")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_bfyear" Value='<%#Bind("Fdb_int_bfyear")%>' runat="server" />
        <asp:HiddenField ID="Fdb_showfor_dept" Value='<%#Bind("Fdb_showfor_dept")%>' runat="server" />
        <asp:HiddenField ID="Fdb_accid_flag" Value='<%#Bind("Fdb_accid_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_transec_no" Value='<%#Bind("Fdb_transec_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preadjdoc_no" Value='<%#Bind("Fdb_preadjdoc_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_preadjdoc_date" Value='<%#Bind("Fdb_preadjdoc_date")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_preadjitem_type" Value='<%#Bind("Fdb_preadjitem_type")%>'
            runat="server" />
        <asp:HiddenField ID="Fdb_voucher_no" Value='<%#Bind("Fdb_voucher_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_int_curyear" Value='<%#Bind("Fdb_int_curyear")%>' runat="server" />
        <asp:HiddenField ID="Fdb_genvc_flag" Value='<%#Bind("Fdb_genvc_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_payfee_meth" Value='<%#Bind("Fdb_payfee_meth")%>' runat="server" />
        <asp:HiddenField ID="Fdb_book_balance" Value='<%#Bind("Fdb_book_balance")%>' runat="server" />
        <asp:HiddenField ID="Fdb_peroid_dept" Value='<%#Bind("Fdb_peroid_dept")%>' runat="server" />
        <asp:HiddenField ID="Fdb_payfee_flag" Value='<%#Bind("Fdb_payfee_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_due_flag" Value='<%#Bind("Fdb_due_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_deptamt_other" Value='<%#Bind("Fdb_deptamt_other")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField8" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_deptwith_flag" Value='<%#Bind("Fdb_deptwith_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_operate_time" Value='<%#Bind("Fdb_operate_time")%>' runat="server" />
        <asp:HiddenField ID="Fdb_teller_flag" Value='<%#Bind("Fdb_teller_flag")%>' runat="server" />
        <asp:HiddenField ID="Fdb_check_no" Value='<%#Bind("Fdb_check_no")%>' runat="server" />
        <asp:HiddenField ID="Fdb_bank_code" Value='<%#Bind("Fdb_bank_code")%>' runat="server" />
        <asp:HiddenField ID="Fdb_bankbranch_code" Value='<%#Bind("Fdb_bankbranch_code")%>'
            runat="server" />
        <%--<asp:HiddenField ID="HiddenField9" Value='<%#Bind("")%>' runat="server" />--%>
        <%--<asp:HiddenField ID="Fdb_deptformat" Value='<%#Bind("Fdb_deptformat")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_spint" Value='<%#Bind("Fdb_spint")%>' runat="server" />
        <asp:HiddenField ID="Fdb_spint_status" Value='<%#Bind("Fdb_spint_status")%>' runat="server" />
        <asp:HiddenField ID="Fdb_send_gov" Value='<%#Bind("Fdb_send_gov")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField10" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_coop_id" Value='<%#Bind("Fdb_coop_id")%>' runat="server" />
        <asp:HiddenField ID="Fdb_group_itemtpe" Value='<%#Bind("Fdb_group_itemtpe")%>' runat="server" />
        <asp:HiddenField ID="Fdb_tax_return" Value='<%#Bind("Fdb_tax_return")%>' runat="server" />
        <%--<asp:HiddenField ID="HiddenField11" Value='<%#Bind("")%>' runat="server" />
        <asp:HiddenField ID="HiddenField12" Value='<%#Bind("")%>' runat="server" />--%>
        <asp:HiddenField ID="Fdb_depttype_code" Value='<%#Eval("Fdb_depttype_code")%>' runat="server" />
        <asp:ObjectDataSource ID="ObjDdCoopId" runat="server" SelectMethod="DdCoopId" TypeName="Saving.Applications.ap_deposit.w_sheet_dp_slip_saving_ctrl.SlipMaster">
        </asp:ObjectDataSource>
    </ItemTemplate>
</asp:FormView>
