using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteElectronics
{
    public partial class BillingForm : Form
    {
        public BillingForm()
        {
            InitializeComponent();
            Populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7GUAQOO\SQLEXPRESS;Initial Catalog=EliteElectronics;Integrated Security=True");
        private void Populate()
        {
            con.Open();
            string query = "select * from ProductsTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductlistDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void UpdateProduct()

        {
            int newQty = stock - Convert.ToInt32(PquantityTb.Text);
            try
            {
                con.Open();
                string query = @"Update ProductsTb1 set (pquantity= " + newQty + " where pid= " + key + ")";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Product Updated Successfully");
                con.Close();
                Populate();
                //Reset();

            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);

            }
        }

        int n = 0, GrdTotal=0;
        private void addtobillBtn_Click(object sender, EventArgs e)
        {
            if (PquantityTb.Text == "" || Convert.ToInt32(PquantityTb.Text) > stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(PquantityTb.Text) * Convert.ToInt32(PpriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(billDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PtitleTb.Text;
                newRow.Cells[2].Value = PquantityTb.Text;
                newRow.Cells[3].Value = PpriceTb.Text;
                newRow.Cells[4].Value = total;
                billDGV.Rows.Add(newRow);
                n++;
                UpdateProduct();
                GrdTotal = GrdTotal + total;
                TotalLbl.Text = "Total " + GrdTotal + " BDT ";

            }
        }


        int key = 0, stock=0;
        private void ProductlistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //this.PidTb.Text = this.ProductlistDGV.CurrentRow.Cells[0].Value.ToString();
            this.PtitleTb.Text = this.ProductlistDGV.CurrentRow.Cells[1].Value.ToString();
            //this.PbrandTb.Text = this.ProductlistDGV.CurrentRow.Cells[2].Value.ToString();
            //this.PcatCb.Text = this.ProductlistDGV.CurrentRow.Cells[3].Value.ToString();
            this.PpriceTb.Text = this.ProductlistDGV.CurrentRow.Cells[4].Value.ToString();
            //this.PquantityTb.Text = this.ProductlistDGV.CurrentRow.Cells[5].Value.ToString();

            if (PtitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(this.ProductlistDGV.CurrentRow.Cells[1].Value.ToString());
                stock = Convert.ToInt32(this.ProductlistDGV.CurrentRow.Cells[5].Value.ToString());
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (clientNameTb.Text == "" || PtitleTb.Text == "")
            {
                MessageBox.Show("Select Client Name");

            }
            else
            {
                //'" + UserNameLb1.Text + "',
                try
                {
                    con.Open();
                    string query = "Insert into BillTb1 values('"+snameLb+"','" + clientNameTb.Text + "', " + GrdTotal + ")";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved Successfully");
                    con.Close();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);

                }

                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }

                if(clientNameTb.Text == "" || PtitleTb.Text == "")
                    {
                    MessageBox.Show("Missing Information !");
                }
                else
                {
                    try
                    {
                        con.Open();
                        string query = "insert into BillTb1 values('" + snameLb.Text + "','" + clientNameTb.Text + "','" + GrdTotal + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product Saved Successfully !");
                        con.Close();
                       
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                    

                }



            }
        }
        int pid, pqty, pprice, total, pos = 60;

        private void label4_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void billDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void snameLb_Click(object sender, EventArgs e)
        {

        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
           snameLb.Text = SellerLogInForm.Sellername;
        }

        String pname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Elite Electronics ", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in billDGV.Rows)
            {
                pid = Convert.ToInt32(row.Cells["Column1"].Value);
                pname = "" + row.Cells["Column2"].Value;
                pqty = Convert.ToInt32(row.Cells["Column3"].Value);
                pprice = Convert.ToInt32(row.Cells["Column4"].Value);
                total = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + pid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + pname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + pqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + pprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;

            }
            e.Graphics.DrawString("Grand Total : " + GrdTotal + " TAKA", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("**** Elite Electronics ****", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            billDGV.Rows.Clear();
            billDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        private void Reset()
        {
          
            PtitleTb.Text = "";
            PpriceTb.Text = "";
            PquantityTb.Text = "";
            clientNameTb.Text = "";
        }


        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
