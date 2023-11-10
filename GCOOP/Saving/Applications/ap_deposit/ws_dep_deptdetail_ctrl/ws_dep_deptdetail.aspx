<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
CodeBehind="ws_dep_deptdetail.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.ws_dep_deptdetail" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsFixed.ascx" TagName="DsFixed" TagPrefix="uc3" %>
<%@ Register Src="DsCodept.ascx" TagName="DsCodept" TagPrefix="uc5" %>
<%@ Register Src="DsPics.ascx" TagName="DsPics" TagPrefix="uc6" %>
<%@ Register Src="DsChgdept.ascx" TagName="DsChgdept" TagPrefix="uc7" %>
<%@ Register Src="DsMasdue.ascx" TagName="DsMasdue" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var DsMain = new DataSourceTool;
    var DsDetail = new DataSourceTool;
    var DsFixed = new DataSourceTool;
    var DsCodept = new DataSourceTool;
    var DsPics = new DataSourceTool;

    function OnDsMainItemChanged(s, r, c) {
        if (c == "deptaccount_no") {
            PostDeptno();

        }
    }
    function OnDsMainClicked(s, r, c) {
        if (c == "b_searchdeptno") {
            Gcoop.OpenIFrame2(735, 720, 'wd_dep_search_deptaccount.aspx', '')
        } else if (c == "b_calaccuint") {
            PostCalaccuint();
        }
    }
    function MenubarOpen() {
        Gcoop.OpenIFrame2(735, 720, 'wd_dep_search_deptaccount.aspx', '')
    }
    function GetDeptNoFromDlg(deptno) {
        dsMain.SetItem(0, "deptaccount_no", deptno);
        PostDeptno();
    }
    function SheetLoadComplete() {
        var deptaccount_no = dsMain.GetItem(0, "deptaccount_no");
        if (deptaccount_no == "" || deptaccount_no == null) {
            dsMain.Focus(0, "deptaccount_no");
        } 
    }    
</script>
<style type="text/css">
        .ui-tabs
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #tabs
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };


        $(function () {

            //  console.log($("#tabs").tabs()); //ชื่อฟิวส์
            encodeURIComponent('\u0e3a')
            var tabIndex = Gcoop.GetEl("hdTabIndex").value; // Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event) {
                    //console.log(event)
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">

    <uc1:DsMain ID="dsMain" runat="server" />
     <div id="tabs">
        <ul>
            <li><a href="#tabs-1">รายการเคลื่อนไหว</a></li>
            <li><a href="#tabs-2">ต้นเงินฝาก</a></li>
            <li><a href="#tabs-3">ผู้ฝากร่วม</a></li>
            <li><a href="#tabs-4">วันครบกำหนด</a></li>
            <li><a href="#tabs-5">เงินฝากรายเดือน</a></li>
        </ul>
        <div id="tabs-1">
             <asp:Panel ID="Panel1" runat="server" Width="700px" Height="400px" ScrollBars="Auto" HorizontalAlign="Center" >
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnRowDataBound="GridView1_RowDataBound"
            OnPageIndexChanging="OnPageIndexChanging" PageSize="20" Style="width:1500px" PagerSettings-Mode="NumericFirstLast"
             PagerSettings-FirstPageText="<<" PagerSettings-LastPageText=">>" PagerSettings-NextPageText=">"
              PagerSettings-PreviousPageText="<" HeaderStyle-BackColor="#D3E7FF">
                <Columns>
                    <asp:BoundField DataField="seq_no" HeaderText="ลำดับ">
                        <ItemStyle HorizontalAlign="Center" Width="4%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="entry_date" HeaderText="วันที่" DataFormatString="{0:dd/MM/yyyy}" >
                        <ItemStyle HorizontalAlign="Center" Width="6%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="cp_book_flag" HeaderText="รายการ">
                        <ItemStyle HorizontalAlign="Center" Width="5%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="operate_date" HeaderText="วันที่คิดดอกเบี้ย" DataFormatString="{0:dd/MM/yyyy}" >
                        <ItemStyle HorizontalAlign="Center" Width="6%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="cp_withdraw" HeaderText="ถอน" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="10%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="cp_deposit" HeaderText="ฝาก" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="10%"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="prncbal" HeaderText="ยอดคงเหลือ" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="11%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="int_amt" HeaderText="Div ครั้งนี้" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="7%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="accuint_amt" HeaderText="Div สะสม" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="8%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="entry_id" HeaderText="ผู้บันทึก">
                        <ItemStyle HorizontalAlign="Left" Width="5%"  />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="deptslip_no" HeaderText="อ้างอิง">
                        <ItemStyle HorizontalAlign="Center" Width="8%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="ref_seq_no" HeaderText="อ้างอิง">
                        <ItemStyle HorizontalAlign="Center" Width="7%"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="prnc_no" HeaderText="ต้นเงิน" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right" Width="9%"  />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="chrg_amt" HeaderText="ค่าปรับ" DataFormatString="{0:#,##0.00}">
                        <ItemStyle HorizontalAlign="Right"  Width="5%"  />
                    </asp:BoundField>
					<asp:BoundField DataField="item_status" HeaderText="สถานะ"  >
                        <ItemStyle  Width="1%"  />
                    </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
        <div id="tabs-2">
            <uc3:DsFixed ID="dsFixed" runat="server" />
        </div>
        <div id="tabs-3">
            <uc5:DsCodept ID="dsCodept" runat="server" />
           
        </div>
        <div id="tabs-4">
            <uc8:DsMasdue ID="dsMasdue" runat="server" />
            
        </div>
        <div id="tabs-5">
            <uc7:DsChgdept ID="dsChgdept" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="HdChangePage"  Value="" runat="server" />
</asp:Content>