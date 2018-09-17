using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DatabaseConnection.getInstance().ConnectionString = "Data Source=(local);Initial Catalog=TestDb;Persist Security Info=True;User ID=sa;Password=samyan123";
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var con = DatabaseConnection.getInstance().getConnection();
                con.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                String name = nameTxtBox.Text;
                int price = (int)numericUpDown1.Value;

                String cmd = String.Format("INSERT INTO Product(ProductName,ProductPrice) values('{0}','{1}' )",name,price);
                int rows = DatabaseConnection.getInstance().exectuteQuery(cmd);
                MessageBox.Show(String.Format( "{0} rows affected", rows));
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseConnection.getInstance().closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String cmd = "Select * from Product";
            var reader = DatabaseConnection.getInstance().getData(cmd);

            List<Product> products = new List<Product>();
            while(reader.Read())
            {
                Product pro = new Product();
                pro.Name = reader.GetString(1);
                pro.Id = (int)reader.GetValue(0);
                pro.Price =  (int) reader.GetValue(2);
                products.Add(pro);
            }
            dataGridView1.DataSource = products;
            
        }
    }
}
