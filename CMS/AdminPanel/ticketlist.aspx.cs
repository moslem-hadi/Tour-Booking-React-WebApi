using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.IO;

namespace CMS.Manage
{
    public partial class ticketlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();
            if (Request.QueryString["key"] != null)
            {
                if (!IsPostBack)
                {
                    string key = Request.QueryString["key"].Replace("+", " ");
                    TextBox1.Text = key;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataAccessDataContext _data = new DataAccessDataContext();
            CheckBox chkAdd;
            int rowCount = GridView1.Rows.Count;
            for (int i = 0; i <= (rowCount - 1); i++)
            {
                chkAdd = (CheckBox)GridView1.Rows[i].FindControl("chkBxSelect");
                int ID = int.Parse(GridView1.DataKeys[i].Value.ToString());
                if (chkAdd.Checked == true)
                {
                    ticket tmp = _data.tickets.Single(a => a.ID == ID);

                    FileStream stream = null;

                    if (tmp.FileName != "none" && File.Exists(Server.MapPath("~/content/temp/") + tmp.FileName))
                        try
                        {

                            FileInfo file = new FileInfo(Server.MapPath("~/content/temp/") + tmp.FileName);
                            while (IsFileLocked(file))
                                System.Threading.Thread.Sleep(1000);
                            file.Delete();
                        }
                        catch { }

                    foreach (ticketReply item in _data.ticketReplies.Where(a => a.TicketID == tmp.ID))
                    {
                        _data.ticketReplies.DeleteOnSubmit(item);

                    }
                    _data.tickets.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();

                }
            }
            GridView1.DataBind();
        }



        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text.Trim() == string.Empty)
                Response.Redirect("ticketlist.aspx");
            string url = Request.Url.GetLeftPart(UriPartial.Path);
            Response.Redirect(url + "?key=" + TextBox1.Text.Trim().Replace(" ", "+"));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool isnew = (bool)DataBinder.Eval(e.Row.DataItem, "IsmanageRead");
                if (!isnew)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#ffe7ad"); // is a "new" row
                    e.Row.Style.Add("background-image", "none");
                }
            }
        }

        [WebMethod]
        public static void close(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return;
            }
            DataAccessDataContext _data = new DataAccessDataContext();
            ids = ids.Remove(ids.Length - 1);
            foreach (var item in ids.Split(','))
            {
                ticket tmp = _data.tickets.Single(a => a.ID == int.Parse(item));
                tmp.Status = 3;
                tmp.IsManageRead = true;
                _data.SubmitChanges();

            }
        }



        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
       
            if (e.CommandName == "Del")
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    int id = int.Parse(e.CommandArgument.ToString());

                    ticket tmp = _data.tickets.Single(a => a.ID == id);

                    FileStream stream = null;

                    if (tmp.FileName != "none" && File.Exists(Server.MapPath("~/content/temp/") + tmp.FileName))
                        try
                        {

                            FileInfo file = new FileInfo(Server.MapPath("~/content/temp/") + tmp.FileName);
                            while (IsFileLocked(file))
                                System.Threading.Thread.Sleep(500);
                            file.Delete();
                        }
                        catch { }

                    foreach (ticketReply item in _data.ticketReplies.Where(a => a.TicketID == tmp.ID))
                    {
                        _data.ticketReplies.DeleteOnSubmit(item);
                    }
                    _data.tickets.DeleteOnSubmit(tmp);
                    _data.SubmitChanges();
                    Response.Redirect("ticketlist.aspx");
                }
            }
             

        }
    }
}