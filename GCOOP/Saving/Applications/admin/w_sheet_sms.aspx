<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sms.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_sms" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            return confirm("คุณต้องการบันทึกข้อมูล ใช่หรือไม่?");
        }

        function OnDwButtomClicked(s, r, c, v) {
            switch (c) {
                case "b_add":
                    break;
            }

        }
        function itemChange(s, r, c, v) {
            if (c == "user_name") {
            }
        }
        function OnDwClicked(s, r, c, v) {

            switch (c) {
                case "user_name":

                    break;
            }
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server" Visible=false>
<iframe src="/CORE/GCOOP/Saving/Applications/admin/SMS/?o=ais" id="SMS" frameborder="0" scrolling=auto width="750" height="750" />
<div style="display:none;"> 
    <div align="center" style="font-size: small; position: relative;display:none;" >
        <div style="display: none;">
            <asp:Label ID="Label1" runat="server" Text="Connection String Target" Visible="false"></asp:Label>
            <asp:TextBox ID="TbConnectionString" runat="server" Width="730px" Text=""></asp:TextBox></div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Btn_Send" runat="server" OnClick="Btn_Send_Click" Text="ส่งข้อมูล"
                        Width="75px" Height="25px" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListAutoRefresh" runat="server" Visible=false>
                        <asp:ListItem Value="-">-</asp:ListItem>
                        <asp:ListItem Value="10">ส่งทุกๆ 10 วินาที</asp:ListItem>
                        <asp:ListItem Value="30">ส่งทุกๆ 30 วินาที</asp:ListItem>
                        <asp:ListItem Value="60">ส่งทุกๆ 1 นาที</asp:ListItem>
                        <asp:ListItem Value="300">ส่งทุกๆ 5 นาที</asp:ListItem>
                        <asp:ListItem Value="600">ส่งทุกๆ 10 นาที</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    วันที่ :
                    <asp:TextBox ID="Txb_send_date" runat="server" Width="65px" MaxLength="8"></asp:TextBox>
                    (yyyymmdd)
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="select sms_trans_code,sms_pattern,enable_flag from SMSPATTERNCONFIG ">สรุปรายการรูปแบบข้อมูล SMS</asp:ListItem>
                        <asp:ListItem Value="SMSNEWS">1.1 จัดการส่งข้อความข่าวสารสมาชิก</asp:ListItem>
                        <asp:ListItem Value="GETDATASMS">1.2 ประมวลดึงรายการรอส่งSMS</asp:ListItem>
                        <asp:ListItem Value="GETCONFIRMSMS">1.3 ประมวลยืนยันรายการรอส่งSMS</asp:ListItem>
                        <asp:ListItem Value="select * from v_SMSTRANSACTION_FLAG_8">2.1 สรุปรายการรอยืนยันส่งSMS</asp:ListItem>
                        <asp:ListItem Value="select * from v_SMSTRANSACTION_FLAG_0">2.2 สรุปรายการรอส่งSMS</asp:ListItem>
                        <asp:ListItem Value="select * from v_SMSTRANSACTION_FLAG_1">3.1 สรุปรายการส่งSMSสำเร็จ</asp:ListItem>
                        <asp:ListItem Value="select * from v_SMSTRANSACTION_FLAG_F">3.2 สรุปรายการส่งSMSไม่สำเร็จ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Btn_Retreive" runat="server" OnClick="Btn_Retreive_Click" Text="ตกลง"
                        Width="55px" Height="25px" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')" />
                </td>
                <td>
                    <asp:Button ID="Btn_Cancel" runat="server" Text="ยกเลิกรายการรอ" Width="95px" Height="25px"
                        OnClick="Btn_Cancel_Click" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="LbServerMessageSender" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="LbServerMessage" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="LbShowKey" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="LbOutput" runat="server" Text=""></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <asp:TextBox ID="TbSQL" runat="server" Visible="false"></asp:TextBox>
    <script language="javascript" type="text/javascript">
        var timeID;
        var active = false;
        function actionEvent() {
            if ($("select[id*='DropDownListAutoRefresh']").val() != "-") {
                $("[id*='Btn_Send']").click();
            }
        }
        function reloadPage() {
            var DropDownListAutoRefresh = $("select[id*='DropDownListAutoRefresh']").val();
            //alert(DropDownListAutoRefresh);
            if (DropDownListAutoRefresh != "-") {
                time = 1000 * DropDownListAutoRefresh;
                //alert(time);
                if (active) actionEvent(); else active = true;
                timeID = setTimeout("reloadPage()", time);
            } else {
                clearTimeout(timeID);
            }
        }
        reloadPage();
    </script>
    <div align="center">
        <canvas id="c" width="500" height="500"></canvas>
    </div>
    <br />
    <table>
        <tr>
            <td width="150px">
            </td>
            <td>
                <table align="center" border="1" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            สรุปรายการส่ง SMS เงินกู้
                        </td>
                        <td style="background-color: rgba(220,220,220,0.2); width: 100px;">
                            ...
                        </td>
                    </tr>
                    <tr>
                        <td>
                            สรุปรายการส่ง SMS เงินฝาก
                        </td>
                        <td style="background-color: rgba(151,187,205,0.2); width: 100px;">
                            ...
                        </td>
                    </tr>
                </table><br />
                <asp:Button ID="Btn_Reset_Trans" runat="server" Text="ล้างข้อมูลSMS" Width="95px"
                    OnClick="Btn_Reset_Trans_Click" OnClientClick="javascript: return confirm('ต้องการดำเนินการใช้หรือไม่?')" />

            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <!-- <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js"></script> -->
    <script src="../../JsCss/Chart.min.js"></script>
    <script>
      var ctx = document.getElementById("c").getContext("2d");
      var data = {
          labels: ["M01", "M02", "M03", "M04", "M05", "M06", "M07", "M08", "M09", "M10", "M11", "M12"],
          datasets: [{
              label: "สรุปรายการส่ง SMS เงินกู้",
              fillColor: "rgba(220,220,220,0.2)",
              strokeColor: "rgba(220,220,220,1)",
              pointColor: "rgba(220,220,220,1)",
              pointStrokeColor: "#fff",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgba(220,220,220,1)",
              data: [<%=SMS_LON_STAT_DATA%>]
          }, {
              label: "สรุปรายการส่ง SMS เงินฝาก",
              fillColor: "rgba(151,187,205,0.2)",
              strokeColor: "rgba(151,187,205,1)",
              pointColor: "rgba(151,187,205,1)",
              pointStrokeColor: "#fff",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgba(151,187,205,1)",
              data: [<%=SMS_DEP_STAT_DATA%>]
          }]
      };
      var MyNewChart = new Chart(ctx).Line(data);
    </script>
 </div>
    </iframe>
</asp:Content>
