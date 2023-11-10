using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_as_search_deptaccount_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_ListDataTable DATA { get; set; }
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

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_List;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
//        public void RetrieveDeptno(String memberno)
//        {
//            string sql = @"
//                         select t.depttype_code,t.depttype_code ||' - '||t.depttype_desc as display,m.deptaccount_no,m.deptaccount_name from dpdeptmaster m left join dpdepttype t  on m.depttype_code = t.depttype_code
//                         where m.coop_id={0} and m.member_no={1}  and m.deptclose_status=0
//                         order by m.deptaccount_no                         
//                         ";
//            sql = WebUtil.SQLFormat(sql, state.SsCoopId, memberno);
//            DataTable dt = WebUtil.Query(sql);
//            this.ImportData(dt);
//        }

        public void RetrieveListPage(string sqltext, int index)
        {
            ViewState["sqltext"] = sqltext;
            if (index == 0)
            {
                CurrentPage = 0;
            }
            String sqlMain = @"select dpdepttype.depttype_code,dpdepttype.depttype_code +' - '+dpdepttype.depttype_desc as display,
                                dpdeptmaster.deptaccount_no,
                                dpdeptmaster.deptaccount_name 
                                from dpdeptmaster  left join dpdepttype   on dpdeptmaster.depttype_code = dpdepttype.depttype_code
                                where dpdeptmaster.coop_id={0}  and dpdeptmaster.deptclose_status=0 "+sqltext+" order by dpdeptmaster.deptaccount_no";
            sqlMain = WebUtil.SQLFormat(sqlMain, state.SsCoopControl);
            DataTable dtMain = WebUtil.Query(sqlMain);
            //Row
            index = index * _pageSize;
            String sqlOffSet = " offset " + index + " rows fetch  next  " + _pageSize + "  rows only ";
            sqlOffSet = sqlMain + " " + sqlOffSet;
            sqlOffSet = WebUtil.SQLFormat(sqlOffSet);
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
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
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
            string sqltext = (string)ViewState["sqltext"];
            RetrieveListPage(sqltext, CurrentPage);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            string sqltext = (string)ViewState["sqltext"];
            RetrieveListPage(sqltext, CurrentPage);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            string sqltext = (string)ViewState["sqltext"];
            RetrieveListPage(sqltext, CurrentPage);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            string sqltext = (string)ViewState["sqltext"];
            RetrieveListPage(sqltext, CurrentPage);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            string sqltext = (string)ViewState["sqltext"];
            RetrieveListPage(sqltext, CurrentPage);
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