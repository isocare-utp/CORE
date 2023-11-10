<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="wd_dep_search_deptaccount.aspx.cs" Inherits="Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl.wd_dep_search_deptaccount" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../../JsCss/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../../../js/Gcoop.js" type="text/javascript"></script>
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        function OnDsMainClicked(s, r, c) {
            if (c == "b_searchdeptacc") {
                dsMain.SetItem(0, "changepage", "flase");
                JsSearchDeptAcc();
            }
        }
        $(document).ready(function () {

            var totalRows = document.getElementById("ctl00_ContentPlace_HdRow").value;
            if (totalRows > 16) {
                $('#ctl00_ContentPlace_GridView1 tbody tr ').not(':last-child').click(function () {
                    var tableData = $(this).children("td").map(function () {
                        return $(this).text();
                    }).get()
                    var deptaccount_no = tableData[0];
                    var deptaccount_no_name = tableData[0] + ' ' + tableData[1];
                    try {
                        var isConfirm = confirm("ต้องการเลือก  " + deptaccount_no_name + "  ใช่หรือไม่");
                        if (isConfirm) {
                            window.opener.GetDeptNoFromDlg(deptaccount_no);
                            window.close();
                        }
                    } catch (err) {
                        parent.GetDeptNoFromDlg(deptaccount_no);
                        window.close()
                    }
                });
            }
            else {
                $('#ctl00_ContentPlace_GridView1 tbody').on('click', 'tr', function () {
                    var tableData = $(this).children("td").map(function () {
                        return $(this).text();
                    }).get()
                    var deptaccount_no = tableData[0];
                    var deptaccount_no_name = tableData[0] + ' ' + tableData[1];
                    try {
                        var isConfirm = confirm("ต้องการเลือก  " + deptaccount_no_name + "  ใช่หรือไม่");
                        if (isConfirm) {
                            window.opener.GetDeptNoFromDlg(deptaccount_no);
                            window.close();
                        }
                    } catch (err) {
                        parent.GetDeptNoFromDlg(deptaccount_no);
                        window.close()
                    }
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div style="margin: 10px 10px 5px 25px">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
    <div style="margin: 4%">
        <table cellspacing="0" rules="all" class="DataSourceRepeater" border="1" style="border-collapse: collapse;">
            <tr>
                <th align="center" style="width: 15%;">
                    เลขที่บัญชี
                </th>
                <th align="center" style="width: 30%;">
                    ชื่อบัญชี
                </th>
                <th align="center" style="width: 15%;">
                    เลขสมาชิก
                </th>
                <th align="right" style="width:30%;">
                    ชื่อ - นามสกุล
                </th>
                <%-- <th align="right" style="width:10%;">
                    สถานะบัญชี
                </th>--%>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            Width="100%" OnPageIndexChanging="OnPageIndexChanging" PageSize="16" class="DataSourceRepeater"
            PagerSettings-Mode="NumericFirstLast" ShowHeader="false" PagerSettings-FirstPageText="<<"
            PagerSettings-LastPageText=">>" PagerSettings-NextPageText=">" PagerSettings-PreviousPageText="<">
            <Columns>
                <asp:BoundField DataField="deptaccount_no" HeaderText="เลขที่บัญชี">
                    <ItemStyle HorizontalAlign="Center" Width="15%" BorderColor="Black" BorderWidth="1px" />
                </asp:BoundField>
                <asp:BoundField DataField="deptaccount_name" HeaderText="ชื่อบัญชี">
                    <ItemStyle Width="30%" BorderColor="Black" BorderWidth="1px" />
                </asp:BoundField>
                <asp:BoundField DataField="member_no" HeaderText="เลขสมาชิก">
                    <ItemStyle HorizontalAlign="Center" Width="15%" BorderColor="Black" BorderWidth="1px" />
                </asp:BoundField>
                <asp:BoundField DataField="fullname" HeaderText="ชื่อ - นามสกุล">
                    <ItemStyle Width="30%" BorderColor="Black" BorderWidth="1px" />
                </asp:BoundField>
                <%--<asp:BoundField DataField="deptclose_desc" HeaderText="สถานะบัญชี">
                    <ItemStyle HorizontalAlign="Center" Width="10%" BorderColor="Black" BorderWidth="1px" />
                </asp:BoundField>--%>
            </Columns>
            <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NumericFirstLast"
                NextPageText="&gt;" PreviousPageText="&lt;" PageButtonCount="10" Position="Bottom"  >
            </PagerSettings>
        </asp:GridView>
    </div>
    <asp:HiddenField ID="HdRow" Value="" runat="server" />
</asp:Content>
