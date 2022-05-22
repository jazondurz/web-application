using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace _6thWebApplication_text_with_picture
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               LoadImages();
            }
        }

        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-9L6TBG72\SQLEXPRESS;Initial Catalog=1MyTest_DB;Integrated Security=True");
       




        //Reset
        private void ClearControls()
        {
            txtAuthor.Text = string.Empty;
            txtBookName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtPublisher.Text = string.Empty;
            txtIDdisplay.Text = string.Empty;
            Image1.ImageUrl = null;
            txtBookName.Focus();
        }

        //Method for display picture
        private void LoadImages()
        {
            // string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            // using (SqlConnection con = new SqlConnection(cs))
            /* {
                
                SqlCommand cmd = new SqlCommand("Select * from BookDetails", con);
                 con.Open();
                ds = new DataSet();
               // da.Fill(ds);
                SqlDataReader rdr = cmd.ExecuteReader();
                // GridView1.DataSource = rdr;
                 GridView1.DataBind(); 
             }
            */

            /* try
             {

                 da = new SqlDataAdapter("select * from BookDetails", con);
                 ds = new DataSet();
                 da.Fill(ds);
                 //GridView1.DataSource = ds;
                 GridView1.DataBind();
             }
             catch (Exception ex)
             {
                 //upload.Text = ex.Message;
             }*/

            //SqlConnection con = new SqlConnection(constr);
            string str = "Select * from BookDetails; select * from BookDetails";
            con.Open();
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            DataView dv = dt.DefaultView;
            //GridView1.DataSource = dv;
            GridView1.DataBind();
            con.Close();

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            Byte[] bytes;
            FileStream fs;
            BinaryReader br;

            SqlCommand cmd = new SqlCommand("InsertBookDetails_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookName", txtBookName.Text.Trim());
            cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
            cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));

            try
            {
                if (flupBookPic.HasFile)
                {
                    fileName = flupBookPic.FileName;
                    filePath = Server.MapPath("BookPictures/" + System.Guid.NewGuid() + fileName);
                    flupBookPic.SaveAs(filePath);

                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    bytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                    br.Close();
                    fs.Close();
                    cmd.Parameters.AddWithValue("@BookPic", bytes);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                lblStatus.Text = "Book Record saved successfully";
                lblStatus.ForeColor = Color.Green;
                ClearControls();
            }
            catch (Exception)
            {
                lblStatus.Text = "Book Record could not be saved";
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
                fileName = null;
                filePath = null;
                fs = null;
                br = null;
            }
            LoadImages();
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
           

            /*  SqlDataReader dr = null;
              byte[] bytes;
              string Base64String = string.Empty;
              SqlCommand cmd = new SqlCommand("GetBookDetails_Sp", con);
              cmd.CommandType = CommandType.StoredProcedure;
              con.Open();
              try
              {
                  dr = cmd.ExecuteReader();
                  if (dr.HasRows)
                  {
                      dr.Read();
                      txtBookName.Text = Convert.ToString(dr["BookName"]);
                      txtAuthor.Text = Convert.ToString(dr["Author"]);
                      txtPublisher.Text = Convert.ToString(dr["Publisher"]);
                      txtPrice.Text = Convert.ToString(dr["Price"]);

                      if (!string.IsNullOrEmpty(Convert.ToString(dr["BookPic"])))
                      {
                          bytes = (byte[])dr["BookPic"];
                          Base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                          Image1.ImageUrl = "data:image/png;base64," + Base64String;
                      }
                      lblStatus.Text = "Book record retrieved successfully";
                      lblStatus.ForeColor = Color.Green;
                  }
              }
              catch (Exception)
              {
                  lblStatus.Text = "Book record could not be retrieved";
                  lblStatus.ForeColor = Color.Red;
              }
              finally
              {
                  dr.Dispose();
                  con.Close();
                  cmd.Dispose();
                  bytes = null;
                  Base64String = null;
              } */
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
            lblStatus.Text = string.Empty;
            LoadImages();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            GridViewRow gr = GridView1.SelectedRow;
            txtIDdisplay.Text = gr.Cells[1].Text;
            txtBookName.Text = gr.Cells[2].Text;
            txtAuthor.Text = gr.Cells[3].Text;
            txtPublisher.Text = gr.Cells[4].Text;
            txtPrice.Text = gr.Cells[5].Text;

            con.Open();
            SqlCommand cmd = new SqlCommand("select BookPic from BookDetails where BookId='" + txtIDdisplay.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

           

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    byte[] imageData = (byte[])dr["BookPic"];
                    string img = Convert.ToBase64String(imageData, 0, imageData.Length);
                    Image1.ImageUrl = "data:Image/png;base64," + img;
                }
            }
            else { } 

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
             int id = int.Parse(txtIDdisplay.Text);
             con.Open();
             SqlCommand co = new SqlCommand("exec DeleteBooks_Sp '" + id + "'", con);
             co.ExecuteNonQuery();
             con.Close();
            LoadImages();
            ClearControls();
        }

        protected void btnDelete0_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            



            int id = int.Parse(txtIDdisplay.Text);
             string BookName = txtBookName.Text, Author = txtAuthor.Text, Publisher = txtPublisher.Text, Price = txtPrice.Text;

             con.Open();
             SqlCommand co = new SqlCommand("exec UpdateBookDetails_Sp '" + id + "','" + BookName + "','" + Author + "','" + Publisher + "', '" + Price + "'", con);
             co.ExecuteNonQuery();
             con.Close();

             // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
             LoadImages();
             ClearControls();





            /*<asp:TemplateField HeaderText="Image">
                                  <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" Height="15px" Width="20px"
                                  ImageUrl='<%# "data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("BookPic")) %>' /> 
                                 </ItemTemplate>
                             </asp:TemplateField>

                    <asp:HyperLinkField /> */


            /* default
             
             <asp:TemplateField HeaderText="Image" SortExpression="BookPic">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BookPic") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Height="15px" ImageUrl='<%# Eval("BookPic") %>' Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>*/


            /*<!--  <httpRuntime executionTimeout="3600" maxRequestLength="108576"/> -->*/


            /*<asp:TemplateField HeaderText="Image">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>*/

            /*<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="562px" DataKeyNames="BookId" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged2">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="BookId" HeaderText="BookId" InsertVisible="False" ReadOnly="True" SortExpression="BookId" />
                    <asp:BoundField DataField="BookName" HeaderText="BookName" SortExpression="BookName" />
                    <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                    <asp:BoundField DataField="Publisher" HeaderText="Publisher" SortExpression="Publisher" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                    <asp:TemplateField HeaderText="BookPic" SortExpression="BookPic">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BookPic") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("BookPic", "{0}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="Gray" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>*/

            /*<!-- <asp:ImageField DataImageUrlField="BookPic" HeaderText="Image"  HeaderStyle-Width="30px" >
                    </asp:ImageField>-->*/

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }

        protected void GridView1_SelectedIndexChanged2(object sender, EventArgs e)
        {
            GridViewRow gr = GridView1.SelectedRow;
            txtIDdisplay.Text = gr.Cells[1].Text;
            txtBookName.Text = gr.Cells[2].Text;
            txtAuthor.Text = gr.Cells[3].Text;
            txtPublisher.Text = gr.Cells[4].Text;
            txtPrice.Text = gr.Cells[5].Text;

            con.Open();
            SqlCommand cmd = new SqlCommand("select BookPic from BookDetails where BookId='" + txtIDdisplay.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();


            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    byte[] imageData = (byte[])dr["BookPic"];
                    string img = Convert.ToBase64String(imageData, 0, imageData.Length);
                    Image1.ImageUrl = "data:Image/png;base64," + img;
                }
            }
            else { }


        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["BookPic"]);
                (e.Row.FindControl("Image1") as Image).ImageUrl = imageUrl;
            }*/
            
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }
    }
}