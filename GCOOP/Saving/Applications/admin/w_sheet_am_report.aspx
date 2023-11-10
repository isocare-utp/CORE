<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_report.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var isIE = document.all ? true : false;
        if (!isIE) document.captureEvents(Event.CLICK);
        document.onclick = getMousePosition;
        function getMousePosition(e) {
            var _x;
            var _y;
            if (!isIE) {
                _x = e.pageX;
                _y = e.pageY;
            }
            if (isIE) {
                _x = event.clientX + document.body.scrollLeft;
                _y = event.clientY + document.body.scrollTop;
            }

            posX = _x;
            posY = _y;

            return true;
        }

        function clickTest(e) {
            getMousePosition(e);
            //alert("X-position:&nbsp;" + posX + ";&nbsp;Y-position:&nbsp;" + posY + ".");
            document.getElementById("divCalendarPicker").style.position = "absolute";
            document.getElementById("divCalendarPicker").style.top = posY + "px";
            document.getElementById("divCalendarPicker").style.left = posX + "px";
            document.getElementById("divCalendarPicker").style.visibility = "visible";
            document.getElementById("divCalendarPicker").style.zIndex = "10";
        }

        function SheetLoadComplete() {
            //document.onclick = getMousePosition;
        }

        function DatePickerCancel() {
            document.getElementById("divCalendarPicker").style.zIndex = "-1";
            document.getElementById("divCalendarPicker").style.visibility = "hidden";
        }
    </script>
    <style type="text/css">
        #divCalendarPicker
        {
            background-color: #5e9df4;
            width: 261px;
            height: 210px;
            border: 1px solid #000000;
            position: relative;
            top: 100px;
            left: 80px;
            z-index: 10;
        }
        #divCalendarPicker div
        {
            background-color: rgb(211,231,255);
            width: 33px;
            height: 18px;
            border: 1px solid #000000;
            float: left;
            margin-top: 2px;
            margin-left: 2px;
            text-align: center;
            font-family: Tahoma;
            font-size: 12px;
            cursor: pointer;
        }
        .divMoveYearMonth
        {
            font-size: 10px;
        }
        .tbDatePickerDay
        {
            position: relative;
            width: 15px;
            top: 2px;
            left: 1px;
            border: none;
            font-family: Tahoma;
            font-size: 12px;
        }
        .tbDatePickerYear
        {
            position: relative;
            width: 30px;
            top: 2px;
            left: 1px;
            border: none;
            font-family: Tahoma;
            font-size: 12px;
        }
        .ddDatePickerDay
        {
            position: relative;
            width: 18px;
            height: 13px;
            top: 0px;
            left: 0px;
            font-family: Tahoma;
            font-size: 12px;
        }
        #btDatePickerToday
        {
            width: 50px;
            height: 20px;
            margin-top: 5px;
        }
        #btDatePickerSubmit
        {
            width: 50px;
            height: 20px;
            margin-top: 5px;
        }
        #btDatePickerCancel
        {
            width: 50px;
            height: 20px;
            margin-top: 5px;
        }
        /*style="width: 16px; height: 14px; margin-left: 0px; margin-top: 0px;"*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <%--<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>--%>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    &nbsp;
    <input type="button" value="text" onclick="clickTest(event)" />
    <br />
    <div id="divCalendarPicker">
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            จ.
        </div>
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            อ.
        </div>
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            พ.
        </div>
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            พฤ.
        </div>
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            ศ.
        </div>
        <div style="background-color: #04306d; color: #FFFFFF; cursor: auto; font-weight: bold;">
            ส.
        </div>
        <div style="background-color: #CC0000; color: #FFFFFF; cursor: auto; font-weight: bold;">
            อา.
        </div>
        <script>
            for (var i = 0; i < 42; i++) {
                document.write("<div id=\"divDayPickerIndex" + i + "\"></div>");
            }
        </script>
        <div style="width: 25px; margin-top: 6px; background-color: #04306d; color: #FFFFFF;">
            < ปี
        </div>
        <div style="width: 25px; margin-top: 6px; background-color: #04306d; color: #FFFFFF;">
            < ด
        </div>
        <div style="width: 139px; cursor: auto; text-align: justify; margin-top: 6px;">
            <input type="text" class="tbDatePickerDay" value="10" />
            <select class="ddDatePickerDay">
                <script>
                    for (var i = 1; i <= 31; i++) {
                        document.write("<option value=\"" + i + "\">" + i + "</option>");
                    }
                </script>
            </select>
            <input type="text" class="tbDatePickerDay" value="10" />
            <select class="ddDatePickerDay">
                <script>
                    for (var i = 1; i <= 12; i++) {
                        document.write("<option value=\"" + i + "\">" + i + "</option>");
                    }
                </script>
            </select>
            <input type="text" class="tbDatePickerYear" value="2556" />
            <select class="ddDatePickerDay">
                <script>
                    for (var i = 1; i <= 12; i++) {
                        document.write("<option value=\"" + i + "\">" + i + "</option>");
                    }
                </script>
            </select>
        </div>
        <div style="width: 25px; margin-top: 6px; background-color: #04306d; color: #FFFFFF;">
            ด >
        </div>
        <div style="width: 25px; margin-top: 6px; background-color: #04306d; color: #FFFFFF;">
            ปี >
        </div>
        <div align="center" style="width: 98%; border: none; background-color: #5e9df4;">
            <input id="btDatePickerToday" type="button" value="วันนี้" />
            <input id="btDatePickerSubmit" type="button" value="ตกลง" />
            <input id="btDatePickerCancel" type="button" value="ยกเลิก" onclick="DatePickerCancel()" />
        </div>
    </div>
</asp:Content>
