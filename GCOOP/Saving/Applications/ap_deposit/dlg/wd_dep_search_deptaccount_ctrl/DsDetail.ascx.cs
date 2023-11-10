using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;

namespace Saving.Applications.ap_deposit.dlg.wd_dep_search_deptaccount_ctrl
{
    public partial class DsDetail : DataSourceRepeater
    {
        public DataSet1.DTDEPTMASTERDataTable DATA { get; set; }

        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 15;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        public void InitDsDetail(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTDEPTMASTER;
            this.EventItemChanged = "OnDsDetailItemChanged";
            this.EventClicked = "OnDsDetailClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsDetail");
            this.Register();
        }
        public void RetrieveListPage(string deptaccount_no, string deptaccount_name, string member_no, string fullname, decimal prncbal, string ls_sqlext, int index)
        {
            ViewState["deptaccount_no"] = deptaccount_no;
            ViewState["deptaccount_name"] = deptaccount_name;
            ViewState["member_no"] = member_no;
            ViewState["fullname"] = fullname;
            ViewState["prncbal"] = prncbal;

            String sqlMain = @"   SELECT  DPDEPTMASTER.MEMBER_NO, 
                    MBUCFPRENAME.PRENAME_DESC||MBMEMBMASTER.MEMB_NAME||'  '||MBMEMBMASTER.MEMB_SURNAME as fullname, 
                    DPDEPTMASTER.PRNCBAL,
                    DPDEPTMASTER.DEPTACCOUNT_NO,
                    DPDEPTMASTER.DEPTACCOUNT_NAME
                    FROM MBMEMBMASTER INNER JOIN DPDEPTMASTER ON MBMEMBMASTER.MEMBER_NO =DPDEPTMASTER.MEMBER_NO
                    AND MBMEMBMASTER.COOP_ID= DPDEPTMASTER.COOP_ID
                    INNER JOIN MBUCFPRENAME ON MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE  
                    WHERE  DPDEPTMASTER.DEPTCLOSE_STATUS=0 AND
                    DPDEPTMASTER.COOP_ID = '" + state.SsCoopControl + "' " + ls_sqlext + " ORDER BY DPDEPTMASTER.DEPTACCOUNT_NO,DPDEPTMASTER.MEMBER_NO";
            sqlMain = WebUtil.SQLFormat(sqlMain);
            DataTable dtMain = WebUtil.Query(sqlMain);


            //Row
            index = index * _pageSize;
            String sqlOffSet = " offset " + index + " rows fetch  next  " + _pageSize + "  rows only ";
            sqlOffSet = sqlMain + " " + sqlOffSet;
            //sqlOffSet = WebUtil.SQLFormat(sqlOffSet, "%" + member_no + "%", "%" + fullname + "%" + fullmembgroup +"%");
            DataTable dtOffSet = WebUtil.Query(sqlOffSet);
            this.ImportData(dtOffSet);

            BindDataIntoRepeater(dtMain);

        }
        private void BindDataIntoRepeater(DataTable dtMain)
        {
            _pgsource.DataSource = dtMain.DefaultView;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            //lbPaging.Text = 
            lblpage.Text = "หน้าที่ " + (CurrentPage + 1).ToString("#,##0") + " จาก " + _pgsource.PageCount.ToString("#,##0");
            // Enable First, Last, Previous, Next buttons
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;
            lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;

            // Bind data into repeater
            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            HandlePaging();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }


        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            string deptaccount_no = (string)ViewState["deptaccount_no"];
            string deptaccount_name = (string)ViewState["deptaccount_name"];
            string member_no = (string)ViewState["member_no"];
            string fullname = (string)ViewState["fullname"];
            decimal prncbal = (decimal)ViewState["prncbal"];
            RetrieveListPage(deptaccount_no, deptaccount_name, member_no, fullname, prncbal, "", CurrentPage);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            string deptaccount_no = (string)ViewState["deptaccount_no"];
            string deptaccount_name = (string)ViewState["deptaccount_name"];
            string member_no = (string)ViewState["member_no"];
            string fullname = (string)ViewState["fullname"];
            decimal prncbal = (decimal)ViewState["prncbal"];
            RetrieveListPage(deptaccount_no, deptaccount_name, member_no, fullname, prncbal, "", CurrentPage);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            string deptaccount_no = (string)ViewState["deptaccount_no"];
            string deptaccount_name = (string)ViewState["deptaccount_name"];
            string member_no = (string)ViewState["member_no"];
            string fullname = (string)ViewState["fullname"];
            decimal prncbal = (decimal)ViewState["prncbal"];
            RetrieveListPage(deptaccount_no, deptaccount_name, member_no, fullname, prncbal, "", CurrentPage);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            string deptaccount_no = (string)ViewState["deptaccount_no"];
            string deptaccount_name = (string)ViewState["deptaccount_name"];
            string member_no = (string)ViewState["member_no"];
            string fullname = (string)ViewState["fullname"];
            decimal prncbal = (decimal)ViewState["prncbal"];
            RetrieveListPage(deptaccount_no, deptaccount_name, member_no, fullname, prncbal, "", CurrentPage);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            string deptaccount_no = (string)ViewState["deptaccount_no"];
            string deptaccount_name = (string)ViewState["deptaccount_name"];
            string member_no = (string)ViewState["member_no"];
            string fullname = (string)ViewState["fullname"];
            decimal prncbal = (decimal)ViewState["prncbal"];
            RetrieveListPage(deptaccount_no, deptaccount_name, member_no, fullname, prncbal, "", CurrentPage);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = System.Drawing.Color.FromName("#EEEEEE");
        }

    
    }
}