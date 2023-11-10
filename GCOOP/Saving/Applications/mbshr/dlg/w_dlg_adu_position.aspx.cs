using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Saving.Applications.mbshr.dlg
{
    public partial class w_dlg_adu_position : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String Sql = "";
                DataTable dt = new DataTable();
                Sql = "select position_code as รหัสตำแหน่ง,position_desc as ชื่อตำเเหน่ง from mbucfposition order by position_desc";
                dt = WebUtil.Query(Sql);
               
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void TxtbPosDesc_TextChanged(object sender, EventArgs e)
        {
            String Sql = "";
            DataTable dt = new DataTable();
            Sql = "select position_code as รหัสตำแหน่ง,position_desc as ชื่อตำเเหน่ง from mbucfposition where position_desc like '%" + TxtbPosDesc.Text + "%'  order by position_desc";
            dt = WebUtil.Query(Sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            String Sql = "";
            DataTable dt = new DataTable();
            GridView1.PageIndex = e.NewPageIndex;
            Int32 RowNum = GridView1.PageIndex * GridView1.PageSize;
            Sql = "select position_code as รหัสตำแหน่ง,position_desc as ชื่อตำเเหน่ง from mbucfposition where position_desc like '%" + TxtbPosDesc.Text + "%'  order by position_desc";
            dt = WebUtil.Query(Sql); //bindgridview will get the data source and bind it again
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String PosCode = "", PosDesc = "";
            if (e.CommandName == "SelectRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                PosCode = GridView1.Rows[index].Cells[1].Text;
                PosDesc = GridView1.Rows[index].Cells[2].Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", " selectRow('" + PosCode + "','" + PosDesc + "')", true);  
 
            }
        }
      
       
    }
}