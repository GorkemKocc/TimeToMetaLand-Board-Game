using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyTimeAtMetaland
{
    public partial class AdminScreen : UserControl
    {
        public LoginScreen loginScreen;
        public AdminScreen()
        {
            InitializeComponent();
        }

        private void AdminScreen_Load(object sender, EventArgs e)
        {



        }

        public void save_rules()
        {
            NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");
            connection.Open();
            NpgsqlCommand query = new NpgsqlCommand("insert into game (initial_food_quantity, initial_item_quantity, initial_money_quantity, daily_food_expense, daily_item_expense, daily_money_expense, map_size, admin_business_salary) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", connection);
            query.Parameters.AddWithValue("@p1", Convert.ToInt32(textBox1.Text));
            query.Parameters.AddWithValue("@p2", Convert.ToInt32(textBox2.Text));
            query.Parameters.AddWithValue("@p3", Convert.ToInt32(textBox3.Text));
            query.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox10.Text));
            query.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox9.Text));
            query.Parameters.AddWithValue("@p6", Convert.ToInt32(textBox8.Text));
            query.Parameters.AddWithValue("@p7", Convert.ToInt32(textBox7.Text));
            query.Parameters.AddWithValue("@p8", Convert.ToInt32(textBox6.Text));
            query.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save_rules();
            MessageBox.Show("eklendi");

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
            loginScreen.Visible = true;
        }
    }
}
