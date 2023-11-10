<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_reqdetail_lap.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_reqdetail_lap" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    <%=MemberNoSearch%>
    <%=changeDistrict%>
    <%=jsGetPostcode %>
    <script type="text/javascript">
        //        function selectRow(sender, rowNumber, objectName) {
        //            var memberNo = objDwList.GetItem(rowNumber, "member_no");
        //            var coopid = Gcoop.GetEl("HdCoopid").value;
        //            window.opener.GetMemberNoFromDlg(coopid, memberNo);
        //            window.close();

        //        }
        function DialogLoadComplete() {
            if (Gcoop.GetEl("HdPost").value == "true") {
                var ref_id = parent.objDwGain.GetItem(parent.temp_seqno, "ref_id");
                var prename_code = parent.objDwGain.GetItem(parent.temp_seqno, "prename_code");
                var name = parent.objDwGain.GetItem(parent.temp_seqno, "name");
                var surname = parent.objDwGain.GetItem(parent.temp_seqno, "surname");
                var house_no = parent.objDwGain.GetItem(parent.temp_seqno, "house_no");
                var group_no = parent.objDwGain.GetItem(parent.temp_seqno, "group_no");
                var soi = parent.objDwGain.GetItem(parent.temp_seqno, "soi");
                var road = parent.objDwGain.GetItem(parent.temp_seqno, "road");
                var tumbol = parent.objDwGain.GetItem(parent.temp_seqno, "tumbol");
                var district = parent.objDwGain.GetItem(parent.temp_seqno, "district");
                var province = parent.objDwGain.GetItem(parent.temp_seqno, "province");
                var post_code = parent.objDwGain.GetItem(parent.temp_seqno, "post_code");
                var phone_no = parent.objDwGain.GetItem(parent.temp_seqno, "phone_no");
                var fax_no = parent.objDwGain.GetItem(parent.temp_seqno, "fax_no");
                var coop_id = parent.objDwGain.GetItem(parent.temp_seqno, "coop_id"); //coop_id
                //            alert(ref_id);
                objDwList.SetItem(1, "ref_id", ref_id);
                objDwList.SetItem(1, "prename_code", prename_code);
                objDwList.SetItem(1, "name", name);
                objDwList.SetItem(1, "surname", surname);
                objDwList.SetItem(1, "house_no", house_no);
                objDwList.SetItem(1, "group_no", group_no);
                objDwList.SetItem(1, "soi", soi);
                objDwList.SetItem(1, "road", road);
                objDwList.SetItem(1, "tumbol", tumbol);
                objDwList.SetItem(1, "district", district);
                objDwList.SetItem(1, "province", province);
                objDwList.SetItem(1, "post_code", post_code);
                objDwList.SetItem(1, "phone_no", phone_no);
                objDwList.SetItem(1, "fax_no", fax_no);
                objDwList.SetItem(1, "coop_id", coop_id);
            }
        }
        function OnDwListButtonClicked(sender, rowNumber, buttonName) {

            var ref_id = objDwList.GetItem(rowNumber, "ref_id");
            var prename_code = objDwList.GetItem(rowNumber, "prename_code");
            var name = objDwList.GetItem(rowNumber, "name");
            var surname = objDwList.GetItem(rowNumber, "surname");
            var seq_no = objDwList.GetItem(rowNumber, "seq_no");
            var house_no = objDwList.GetItem(rowNumber, "house_no");
            var group_no = objDwList.GetItem(rowNumber, "group_no");
            var soi = objDwList.GetItem(rowNumber, "soi");
            var road = objDwList.GetItem(rowNumber, "road");
            var tumbol = objDwList.GetItem(rowNumber, "tumbol");
            var district = objDwList.GetItem(rowNumber, "district");
            var province = objDwList.GetItem(rowNumber, "province");
            var post_code = objDwList.GetItem(rowNumber, "post_code");
            var phone_no = objDwList.GetItem(rowNumber, "phone_no");
            var fax_no = objDwList.GetItem(rowNumber, "fax_no");
            var coop_id = objDwList.GetItem(rowNumber, "coop_id");

            if (buttonName == "b_1") {
                parent.GetPersonal(ref_id, prename_code, name, surname, seq_no, house_no, group_no, soi, road, tumbol, district, province, post_code, phone_no, fax_no, coop_id);
                parent.RemoveIFrame();
            }
        }

        function OnDwListItemChange(sender, rowNumber, columnName, newValue) {
            if (columnName == "province") {
                objDwList.SetItem(rowNumber, columnName, newValue);
                objDwList.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "province_0";
                changeDistrict();
            }
            else if (columnName == "district") {
                objDwList.SetItem(rowNumber, columnName, newValue);
                objDwList.AcceptText();
                Gcoop.GetEl("Hidlast_focus").value = "district_0";
                jsGetPostcode();

            } else if (columnName == "ref_id") {
                objDwList.SetItem(rowNumber, columnName, newValue);
                objDwList.AcceptText();
                MemberNoSearch();
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_dpservice_reqcodeposit_lap"
        LibraryList="~/DataWindow/ap_deposit/dp_reqdeposit.pbl" RowsPerPage="14" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="selectRow" ClientEventButtonClicked="OnDwListButtonClicked"
        ClientEventItemChanged="OnDwListItemChange">
        <%--<PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Left" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>--%>
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdCoopid" Value="0" runat="server" />
    <asp:HiddenField ID="Hidlast_focus" Value="0" runat="server" />
    <asp:HiddenField ID="HdPost" Value="true" runat="server" />
    </form>
</body>
</html>
