using Core.TableModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskProject
{
    public partial class DefaultPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Test
            List<ImageModel> OutImages = new List<ImageModel>();
            string ConnectionString = ConfigurationManager.ConnectionStrings["TaskProjectConnectionString"].ToString();
            using (var sqlCmd = new SqlCommand($"select* from images", new SqlConnection(ConnectionString)))
            {
                try
                {
                    sqlCmd.Connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OutImages.Add(new ImageModel
                        {
                            ImageId = reader["ImageId"].ToString(),
                            ImageFileName = reader["ImageFileName"].ToString(),
                            ImageFileExtension = reader["ImageExtension"].ToString(),
                            ImageAddingTime = DateTime.Parse(reader["ImageAddingTime"].ToString()),
                            DataFromBytes = reader["ImageFileData"] as byte[],
                            ProjectId = reader["ProjectId"].ToString(),
                            TaskId = reader["TaskId"].ToString(),
                            UserId = reader["UserId"].ToString(),

                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}