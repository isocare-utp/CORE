<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_adu_position.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_adu_position" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
     function selectRow(poscode,postdesc) {
                try {
                    window.opener.GetPositionFromDlg(poscode, postdesc);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlg(memberno);
                    parent.RemoveIFrame();
                }
            }
     </script>
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:400px;">
            <tr>
                <td class="style2">
                    <asp:Label ID="LblPosDesc" runat="server" BackColor="#CAE4FF" 
                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="Tahoma" Font-Size="12pt" style="text-align: center" 
                        Text="ตำแหน่ง" Width="100px" Height="20px"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="TxtbPosDesc" runat="server" style="text-align: right" 
                        Width="270px" AutoPostBack="True" BackColor="#FFFFE8" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                        ontextchanged="TxtbPosDesc_TextChanged" Font-Names="Tahoma"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="3"  >
                    <asp:GridView ID="GridView1" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Width="380px" AllowPaging="True" BorderColor="Black" 
                        CellPadding="4" EnableModelValidation="False" ForeColor="#333333" 
                         OnPageIndexChanging="GridView1_PageIndexChanging" PageIndex="1" 
                         OnRowCommand="GridView1_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" Text="+" CommandName="SelectRow">
                            <ControlStyle BackColor="#FFE6FF"  />
                            </asp:ButtonField>
                        </Columns>
                        <EditRowStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Angsana New" 
                            Font-Size="12pt" BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Bold="True" Font-Names="Tahoma" Font-Size="12pt" BorderColor="Black" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Wrap="False" />
                        <PagerStyle BackColor="#2461BF" BorderColor="Black" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="White" HorizontalAlign="Center" 
                            VerticalAlign="Bottom" />
                        <RowStyle BackColor="#EFF3FB" BorderColor="Black" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="Tahoma" Font-Size="12pt" HorizontalAlign="Left" 
                            VerticalAlign="Bottom" Wrap="False" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
