<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_picture.aspx.cs" Inherits="Saving.Applications.w_dlg_picture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<%=savePic%>
   <title>UpLoad Pictuer</title>
    <style type="text/css">
        .textMenu14
        {
            font-family: "Times New Roman" , Times, Sans-Serif;
            font-weight: bold;
            font-size: 14px;
        }
        .tdSol
        {
            border: 1px solid #333333;
        }
    </style>
     <script type="text/javascript">
         function Save_Click() {

             savePic();
             window.opener.GetShow();
             window.close();
         }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table style="width: 100%; background-color: #EEEEEE;" class="tdSol" cellpadding="0"
        cellspacing="0">
        <tr>
            <td align="center" style="height: 25px;" class="textMenu14">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 30px;" class="textMenu14">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:FileUpload ID="FileUpload" runat="server" />
                &nbsp;
                 <input id="b_save" style="width: 100px; height: 35px; color: White; font-size: medium;
                    font-weight: bold; background-color: Green;" type="button" onclick="Save_Click()"
                    value="บันทึก" />
              
               
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center">
        <asp:Label ID="Label2" runat="server" Text="&nbsp;"></asp:Label>
        <asp:Image ID="Image1" runat="server" Height="182px" Width="163px" />
         <asp:Literal ID="ltImage" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>