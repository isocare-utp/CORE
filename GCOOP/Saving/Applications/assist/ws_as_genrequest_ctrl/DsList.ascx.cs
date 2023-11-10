using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;


namespace Saving.Applications.assist.ws_as_genrequest_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DataListDataTable DATA { get; set; }
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        List<string> ListChecked = new List<string>();

        private int _pageSize = 25;
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
            this.DATA = ds.DataList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }
        public Int64 RetrieveListPage(string Slassisttype_code, int index)
        {
            if (index == 0)
            {
                CurrentPage = 0;
            }
            string search_account = (string)Session["SSsearch_account"];
            ViewState["assisttype_code"] = Slassisttype_code;
            if (search_account == "1")
            {
                Slassisttype_code = "assisttype_code = " + Slassisttype_code;
            }
            else
            {
                Slassisttype_code = "assisttype_code = " + Slassisttype_code + "and account_no = '-' ";
            }

            Int64 countrow = 0;
            String sqlList = @"select  assisttype_code,seq_no,member_no,memb_name,slip_date,mem_age,
                                birth_age,period_pay,maxpermiss_amt,assistcut_amt,itempay_amt,account_no,member_date, 0 as choose_flag
                                from assgenrequestdocno
                                where " + Slassisttype_code + " order by assisttype_code,seq_no";
            sqlList = WebUtil.SQLFormat(sqlList, Slassisttype_code);
            DataTable dtMain = WebUtil.Query(sqlList);
            //this.ImportData(dtMain);
            countrow = dtMain.Rows.Count;
            //Row
            index = index * _pageSize;
            String sqlOffSet = " offset " + index + " rows fetch  next  " + _pageSize + "  rows only ";
            sqlOffSet = sqlList + " " + sqlOffSet;
            //sqlOffSet = WebUtil.SQLFormat(sqlOffSet, "%" + member_no + "%", "%" + fullname + "%" + fullmembgroup +"%");
            DataTable dtOffSet = WebUtil.Query(sqlOffSet);
            if (Session["SSGenrequest"] != null)
            {
                var obj = (List<string>)Session["SSGenrequest"];
                for (int i = 0; i < obj.Count; i++)
                {
                    string val = obj[i].ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        DataRow dr = dtOffSet.Select("member_no='" + val.Trim().ToString() + "'").FirstOrDefault();
                        if (dr != null)
                        {
                            dr["choose_flag"] = 1;
                        }
                    }
                }
            }
            this.ImportData(dtOffSet);
            BindDataIntoRepeater(dtMain);
            return countrow;
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
            string assisttype_code = (string)ViewState["assisttype_code"];
            string search_account = (string)Session["SSsearch_account"];


            RetrieveListPage(assisttype_code, CurrentPage);
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            string assisttype_code = (string)ViewState["assisttype_code"];
            string search_account = (string)Session["SSsearch_account"];

            RetrieveListPage(assisttype_code, CurrentPage);
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            string assisttype_code = (string)ViewState["assisttype_code"];
            string search_account = (string)Session["SSsearch_account"];

            RetrieveListPage(assisttype_code, CurrentPage);
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            string assisttype_code = (string)ViewState["assisttype_code"];
            string search_account = (string)Session["SSsearch_account"];

            RetrieveListPage(assisttype_code, CurrentPage);
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            string assisttype_code = (string)ViewState["assisttype_code"];
            string search_account = (string)Session["SSsearch_account"];

            RetrieveListPage(assisttype_code, CurrentPage);
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = System.Drawing.Color.FromName("#EEEEEE");
        }

        public DataTable RetrieveList(string assisttype_code)
        {
            ViewState["assisttype_code"] = assisttype_code;
            String sqlList = @"select  assisttype_code,seq_no,member_no,memb_name,slip_date,mem_age,
                                birth_age,period_pay,maxpermiss_amt,assistcut_amt,itempay_amt,rtrim(ltrim(account_no)) account_no,member_date, 0 as choose_flag
                                from assgenrequestdocno
                                where assisttype_code ={0}
                                order by assisttype_code,seq_no";
            sqlList = WebUtil.SQLFormat(sqlList, assisttype_code);

            return WebUtil.Query(sqlList);
        }

        public DataTable RetrieveToSave(string assisttype_code, string listStr)
        {
            ViewState["assisttype_code"] = assisttype_code;
            String sqlList = @" select  assisttype_code,seq_no,member_no,memb_name,slip_date,mem_age,
                                birth_age,period_pay,maxpermiss_amt,assistcut_amt,itempay_amt,rtrim(ltrim(account_no)) account_no,member_date, 0 as choose_flag
                                from assgenrequestdocno
                                where assisttype_code ={0} and member_no in (" + listStr + ") order by assisttype_code,seq_no,member_no";
            sqlList = WebUtil.SQLFormat(sqlList, assisttype_code);

            return WebUtil.Query(sqlList);
        }
    }
}