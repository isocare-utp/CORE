<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_setPrinter_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

    <table class="DataSourceRepeater" style="width: 2800px;">
        <tr>
            <th>
                Printing_name
            </th>
            <th>
                Printer_desc
            </th>
            <th>
                Primary_Column
            </th>
            <th>
                Row_Perpage
            </th>
            <th>
                Auto_Print
            </th>
            <th>
                Auto_close
            </th>
            <th width="30%">
                Printer_name
            </th>
            <th>
                Paper_size
            </th>
            <th>
                Font_size
            </th>
            <th>
                Font_name
            </th>
            <th>
                Font_color
            </th>
            <th>
                Font_bius
            </th>
            <th>
                Head_Height
            </th>
            <th>
                Detail_height
            </th>
            <th>
                Test_width
            </th>
            <th>
                Test_height
            </th>
            <th>
                Url_XmlTest
            </th>
            <th>
                Orientation
            </th>
            <th>
                Datawindow
            </th>
            <th>
                Papersize
            </th>
            <th>
                Paper_height
            </th>
            <th>
                Paper_width
            </th>
            <th>
                Margin_flag
            </th>
            <th>
                Margin_top
            </th>
            <th>
                Margin_bottom
            </th>
            <th>
                Margin_left
            </th>
            <th>
                Margin_right
            </th>
            <th width="30">
                ลบ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="PRINTING_NAME" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PRINTING_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PRIMARY_COLUMN" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ROW_PERPAGE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="AUTO_PRINT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="AUTO_CLOSE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PRINTER_NAMES" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PAPER_SIZE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="FONT_SIZE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="FONT_NAME" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="FONT_COLOR" runat="server" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="FONT_BIUS" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="HEAD_HEIGHT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="DETAIL_HEIGHT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="TEST_WIDTH" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="TEST_HEIGHT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="URL_XMLTEST" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="ORIENTATION" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="DATAWINDOW" runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="PAPERSIZE" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="PAPER_HEIGHT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="PAPER_WIDTH" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                      <td >
                        <asp:TextBox ID="MARGIN_FLAG" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="MARGIN_TOP" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="MARGIN_BOTTOM" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="MARGIN_LEFT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                     <td >
                        <asp:TextBox ID="MARGIN_RIGHT" runat="server" style="text-align:center;"></asp:TextBox>
                    </td>
                      <td >
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

