<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_edit_member_no.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_edit_member_no" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ตั้งค่าเลขทะเบียนสมาชิก</title>

    <script type="text/javascript">
    function getNewData(){
        var str_temp = window.location.toString();
        var str_arr = str_temp.split("?", 2);
        window.location = str_arr[0] + "?cmd=newData";
    }
    function saveData(){
        var TbMainmemno = document.getElementById("TbMainmemno").value;
        var TbComemno = document.getElementById("TbComemno").value;
        var str_temp = window.location.toString();
        var str_arr = str_temp.split("?", 2);
        window.location = str_arr[0] + "?cmd=saveData&mnomain="+TbMainmemno+"&mnoco="+TbComemno;

        
    }
    </script>

</head>
<body bgcolor="#CCFFFF">
    <form id="form1" runat="server">
    แก้ไขทะเบียนล่าสุด
    <table style="width: 100%;" border="0">
        <tr>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            เลขล่าสุด
                        </td>
                        <td>
                            ทะเบียนล่าสุด
                        </td>
                    </tr>
                    <tr>
                        <td>
                            สมาชิกปกติ
                        </td>
                        <td>
                            <asp:TextBox ID="TbMainmemno" runat="server" MaxLength="6" Width="70px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TbMainmem" runat="server" Width="70px" ReadOnly="True" BackColor="#E6E6E6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            สมาชิกสมทบ
                        </td>
                        <td>
                            <asp:TextBox ID="TbComemno" runat="server" MaxLength="6" Width="70px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TbComem" runat="server" Width="70px" ReadOnly="True" BackColor="#E6E6E6"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            <input id="BtSave" type="button" value="บันทึก" style="width: 100px" onclick="saveData();"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="BtGetNew" type="button" value="ดึงข้อมูลใหม่" style="width: 100px" onclick="getNewData();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="BtClose" type="button" value="ปิด" style="width: 100px" onclick="window.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Label ID="LbMessage" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
