using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ThinkBridge_Assignment
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable table = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if ((Int32.TryParse(textBoxQuantity.Text, out int value1)) && (Int32.TryParse(textBoxPrice.Text, out int value2)))
            {
                string query = "insert into ShopBridge values(" + Convert.ToInt32(textBoxProductID.Text) + ",'" + textBoxName.Text + "','" + textBoxDiscription.Text + "'," + Convert.ToInt32(textBoxQuantity.Text) + "," + Convert.ToInt32(textBoxPrice.Text) + ")";
                cmd = new SqlCommand(query, cn);
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record Added Successfully !!!");

                    dataGridView1.DataSource = getProductDetails();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cn.Close();
                }
            }
            else
            {
                if ((!Int32.TryParse(textBoxQuantity.Text, out int i1)) && (!Int32.TryParse(textBoxQuantity.Text, out int i2)))
                    MessageBox.Show("Quantity and Price must be integer values !!!");

                else if (!Int32.TryParse(textBoxQuantity.Text, out int val1))
                    MessageBox.Show("Quantity must be integer value !!!");

                else if (!Int32.TryParse(textBoxPrice.Text, out int val2))
                    MessageBox.Show("Price must be integer value !!!");
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if ((Int32.TryParse(textBoxQuantity.Text, out int value1)) && (Int32.TryParse(textBoxPrice.Text, out int value2)))
                {
                    string query = "update ShopBridge set name = '" + textBoxName.Text + "', discription = '" + textBoxDiscription.Text + "', quantity = " + Convert.ToInt32(textBoxQuantity.Text) + ", price = " + Convert.ToInt32(textBoxPrice.Text) + " where productID = " + Convert.ToInt32(textBoxProductID.Text);
                    cmd = new SqlCommand(query, cn);
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record Modified Successfully !!!");

                        dataGridView1.DataSource = getProductDetails();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        cn.Close();
                    }
                }
                else
                {
                    if ((!Int32.TryParse(textBoxQuantity.Text, out int i1)) && (!Int32.TryParse(textBoxQuantity.Text, out int i2)))
                        MessageBox.Show("Quantity and Price must be integer values !!!");

                    else if (!Int32.TryParse(textBoxQuantity.Text, out int val1))
                        MessageBox.Show("Quantity must be integer value !!!");

                    else if (!Int32.TryParse(textBoxPrice.Text, out int val2))
                        MessageBox.Show("Price must be integer value !!!");
                }
            }
            else
                MessageBox.Show("Before modifying the record, Please select the row from the table !!!");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if ((Int32.TryParse(textBoxQuantity.Text, out int value1)) && (Int32.TryParse(textBoxPrice.Text, out int value2)))
                {
                    string query = "delete from ShopBridge where productID = " + Convert.ToInt32(textBoxProductID.Text);
                    cmd = new SqlCommand(query, cn);
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record Deleted Successfully !!!");

                        dataGridView1.DataSource = getProductDetails();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        cn.Close();
                    }
                }
                else
                {
                    if ((!Int32.TryParse(textBoxQuantity.Text, out int i1)) && (!Int32.TryParse(textBoxQuantity.Text, out int i2)))
                        MessageBox.Show("Quantity and Price must be integer values !!!");

                    else if (!Int32.TryParse(textBoxQuantity.Text, out int val1))
                        MessageBox.Show("Quantity must be integer value !!!");

                    else if (!Int32.TryParse(textBoxPrice.Text, out int val2))
                        MessageBox.Show("Price must be integer value !!!");
                }
            }
            else
                MessageBox.Show("Before deleting the record, Please select the row from the table !!!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            textBoxProductID.Text = selectedRow.Cells[0].Value.ToString();
            textBoxName.Text = selectedRow.Cells[1].Value.ToString();
            textBoxDiscription.Text = selectedRow.Cells[2].Value.ToString();
            textBoxQuantity.Text = selectedRow.Cells[3].Value.ToString();
            textBoxPrice.Text = selectedRow.Cells[4].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getProductDetails();

            string query = "select MAX(productid) from ShopBridge";
            int cnt = 0;
            cmd = new SqlCommand(query, cn);
            try
            {
                cn.Open();
                cnt = Convert.ToInt32(cmd.ExecuteScalar());
                textBoxProductID.Text = (cnt + 1).ToString();
                cn.Close();
            }
            catch (Exception)
            {
                cnt = 1;
                textBoxProductID.Text = cnt.ToString();
                cn.Close();
            }
        }

        private DataTable getProductDetails()
        {
            table.Clear();
            dataGridView1.Refresh();

            cmd = new SqlCommand("select * from ShopBridge",cn);
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader();
                table.Load(dr);
                dataGridView1.DataSource = table;
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
            return table;
        }
    }
}
