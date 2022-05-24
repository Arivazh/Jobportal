using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;
        string str = ConfigurationManager.ConnectionStrings["JobPortalConnectionString"].ConnectionString;
        public string jobTitle = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                showJobDetails();
                DataBind();
            }
            else
            {
                Response.Redirect("joblisting.aspx");
            }
        }
        private void showJobDetails()
        {
            con = new SqlConnection(str);
            string query = @"Select * from Jobs where JobId = @id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            DataList1.DataSource = dt;
            DataList1.DataBind();
            jobTitle = dt.Rows[0]["Title"].ToString();

        }


        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    try
                    {
                        con = new SqlConnection(str);
                        string query = @"Insert into AppliedJobs where (@JobId, @UserId";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if(r >0)

                        {
                            lblmsg.Visible = true;
                            lblmsg.Text = "Job applied Successfully!";
                            lblmsg.CssClass = "alert alert-success";

                        }
                        else
                        
                            {
                                lblmsg.Visible = true;
                                lblmsg.Text = "Cannot apply the job please try after sometime...!";
                                lblmsg.CssClass = "alert alert-danger";

                            }
                        
                    }
                    catch(Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }

        protected void DataList1_ItemCommand1(object source, DataListCommandEventArgs e)
    {

    }

    protected void DataList1_ItemDataBound1(object sender, DataListItemEventArgs e)
    {

    }
        protected string GetImageUrl(Object url)
        {
            string url1 = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url = "~/Image/No_image.png";

            }
            else
            {
                url1 = string.Format("~/{0}", url);

            }
            return ResolveUrl(url1);
        }
    }
}