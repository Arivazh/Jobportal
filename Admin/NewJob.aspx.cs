using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["JobPortalConnectionString"].ConnectionString;
        String query;

        public DateTime DateTime { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string concaQuery, imagePath = string.Empty;
                bool isValidToExecute = false;
                con = new SqlConnection(str);
                //if (fuCompanyLogo.HasFile)
                //{
                //    if (IsValidExtension(fuCompanyLogo.FileName)
                //    {
                //        concatQuery = ""
                //    }
                //    else
                //    {



                //    }
                //}
                //else
                //{

                //}

                query = @"Insert into Jobs values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateToApply,
                        @Salary,@JobType,@CompanyName,@CompanyImage,@Website,@Email,@Address,@Country,@State,@CreateDate)";
                DateTime time = DateTime.Now;
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                cmd.Parameters.AddWithValue("@Specialization", txtSpecializatiom.Text.Trim());
                cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                cmd.Parameters.AddWithValue("@JobType", ddlJobType.Text.Trim());
                cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                //cmd.Parameters.AddWithValue("@CompanyImage", txt.Text.Trim());
                cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", ddlCountry.Text.Trim());
                cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                cmd.Parameters.AddWithValue("@CreateDate", time.ToString("yyyy-MM-dd HH:mm:ss"));
                if (fuCompanyLogo.HasFile)
                {
                    if (IsValidExtension(fuCompanyLogo.FileName))
                    {
                        Guid obj = Guid.NewGuid();
                        imagePath = "images/" + obj.ToString() + fuCompanyLogo.FileName;
                        fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/images/") + obj.ToString() + fuCompanyLogo.FileName);

                        cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        isValidToExecute = true;
                    }
                    else
                    {
                        llbMg.Text = "Please select .jpg, .jpeg, .png file for Logo";
                        llbMg.CssClass = "alert alert-danger";
                    }

                }
                else
                {
                    cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                    isValidToExecute = true;

                }

                if (isValidToExecute)
                {
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        llbMg.Text = "Job saved successful..!";
                        llbMg.CssClass = "alert alert-success";
                        Clear();
                    }
                    else
                    {
                        llbMg.Text = "Cannot save the records, please try after sometimes..!";
                        llbMg.CssClass = "alert alert-danger";

                    }

                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script");
            }
            finally
            {
                con.Close();
            }
        }

        private void Clear()
        {
            txtJobTitle.Text = String.Empty;
            txtNoOfPost.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtQualification.Text = String.Empty;
            txtExperience.Text = String.Empty;
            txtSpecializatiom.Text = String.Empty;
            txtLastDate.Text = String.Empty;
            txtSalary.Text = String.Empty;
            txtCompany.Text = String.Empty;
            txtWebsite.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtState.Text = String.Empty;
            ddlJobType.ClearSelection();
            ddlCountry.ClearSelection();

        }

        private bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".jpg", "png", ".jpeg" };
            for (int i = 0; i <= fileExtension.Length - 1; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {

                    isValid = true;
                    break;
                }

            }
            return isValid;
        }

    }


}
