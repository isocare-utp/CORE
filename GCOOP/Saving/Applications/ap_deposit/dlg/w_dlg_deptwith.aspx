<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_deptwith.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_deptwith" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Open Dialog</title>
    <%=postSubmit%>
    <%=postUp%>
    <%=postDown%>
    <script type="text/javascript">
        function DialogLoadComplete() {
            Gcoop.GetEl('tempFocus').focus();
        }

        function OnKeyUpEnd(e) {
            if (e.keyCode == "27" || e.keyCode == "109" || e.keyCode == "189") {
                parent.RemoveIFrame();
            } else if (e.keyCode == "38") {
                postUp();
            } else if (e.keyCode == "40") {
                postDown();
            } else if (e.keyCode == "37") {
                if (Gcoop.GetEl("DdDeptWith").value == "+") {
                    Gcoop.GetEl("DdDeptWith").value = "/";
                } else if (Gcoop.GetEl("DdDeptWith").value == "-") {
                    Gcoop.GetEl("DdDeptWith").value = "+";
                } else if (Gcoop.GetEl("DdDeptWith").value == "/") {
                    Gcoop.GetEl("DdDeptWith").value = "-";
                } else {
                    Gcoop.GetEl("DdDeptWith").value = "/";
                }
                postSubmit();
            } else if (e.keyCode == "39") {
                if (Gcoop.GetEl("DdDeptWith").value == "+") {
                    Gcoop.GetEl("DdDeptWith").value = "-";
                } else if (Gcoop.GetEl("DdDeptWith").value == "-") {
                    Gcoop.GetEl("DdDeptWith").value = "/";
                } else if (Gcoop.GetEl("DdDeptWith").value == "/") {
                    Gcoop.GetEl("DdDeptWith").value = "+";
                } else {
                    Gcoop.GetEl("DdDeptWith").value = "+";
                }
                postSubmit();
            } else if (e.keyCode == "13") {
                var row = Gcoop.ParseInt(Gcoop.GetEl("HdCurrentRow").value);
                if (row > -1) {
                    row = row + 2;
                    var rowFormat = Gcoop.StringFormat(row, "00");
                    var el = document.getElementById("GridView1_ctl" + rowFormat + "_Label2");
                    Selected(el.innerHTML);
                } else {
                    alert("กรุณาเลือกประเภทรายการ");
                }
            } else if (e.keyCode == "17") {

            } else {
                alert(e.keyCode);
            }
        }

        function Selected(reptCode) {
            try {
                var deptWithCode = document.getElementById("DdDeptWith").value;
                parent.ReDlgDeptWith(this, deptWithCode, reptCode);
            } catch (Err) {
                alert("Error Dlg");
                parent.RemoveIFrame();
            }
        }

        function TestOpener() {
            parent.TestDlg();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LbAccountNo" runat="server" Text="" ForeColor="Red" Font-Size="14px"
            Font-Bold="True"></asp:Label>
        &nbsp;
        &nbsp;
        <br />
        <asp:Label ID="Label1" runat="server" Text="รายการ:" Font-Size="14px" Font-Bold="True"></asp:Label>
        &nbsp; &nbsp;<asp:DropDownList ID="DdDeptWith" runat="server" Width="200px" AutoPostBack="True"
            Font-Size="16px" Font-Bold="True" ForeColor="Blue" OnSelectedIndexChanged="DdDeptWith_SelectedIndexChanged">
            <asp:ListItem Value="">เลือกทำรายการ</asp:ListItem>
            <asp:ListItem Value="+">+ ทำรายการฝาก</asp:ListItem>
            <asp:ListItem Value="-">- ทำรายการถอน</asp:ListItem>
            <asp:ListItem Value="/">/ ทำรายการปิดบัญชี</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="tempFocus" runat="server">
        </asp:DropDownList>
        
        <asp:Label ID="LbAccountName" runat="server" Text="" ForeColor="#009999" Font-Size="14px"
            Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="99%"
            AllowSorting="True" EnableModelValidation="True">
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:TemplateField HeaderText="รหัส">
                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                    <ItemTemplate>
                        <span style="cursor: pointer" onclick="Selected('<%#Eval("RECPPAYTYPE_CODE")%>')">
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("RECPPAYTYPE_CODE")%>'></asp:Label>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ประเภทรายการ">
                    <HeaderStyle HorizontalAlign="Left" Width="75%" />
                    <ItemTemplate>
                        <span style="cursor: pointer" onclick="Selected('<%#Eval("RECPPAYTYPE_CODE")%>')">
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("RECPPAYTYPE_DESC")%>'></asp:Label>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <hr />
        <asp:HiddenField ID="HdCurrentRow" runat="server" />
    </div>
    </form>
</body>
</html>
