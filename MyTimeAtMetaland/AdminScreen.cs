﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyTimeAtMetaland
{
    public partial class AdminScreen : UserControl
    {
        public LoginScreen loginScreen;
        public int foodPrice, itemPrice, fieldPrice, estateCommission;
        public AdminScreen()
        {
            InitializeComponent();
        }

        private void AdminScreen_Load(object sender, EventArgs e)
        {
            resetText();
        }
        public void resetText()
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox12.Text = "0";
            textBox13.Text = "0";
            textBox14.Text = "0";

        }
        public void save_rules()
        {
            NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");
            connection.Open();

            NpgsqlCommand query = new NpgsqlCommand("delete from game", connection);
            /*query.ExecuteNonQuery();
            query = new NpgsqlCommand("ALTER SEQUENCE public.game_game_id_seq RESTART WITH 1;", connection);
            query.ExecuteNonQuery();
            */
            query = new NpgsqlCommand("insert into game (initial_food_quantity, initial_item_quantity, initial_money_quantity, daily_food_expense, daily_item_expense, daily_money_expense, map_size, admin_business_salary, business_cost) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8, @p9)", connection);
            query.Parameters.AddWithValue("@p1", Convert.ToInt32(textBox1.Text));
            query.Parameters.AddWithValue("@p2", Convert.ToInt32(textBox2.Text));
            query.Parameters.AddWithValue("@p3", Convert.ToInt32(textBox3.Text));
            query.Parameters.AddWithValue("@p4", Convert.ToInt32(textBox10.Text));
            query.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox9.Text));
            query.Parameters.AddWithValue("@p6", Convert.ToInt32(textBox8.Text));
            query.Parameters.AddWithValue("@p7", Convert.ToDouble(textBox7.Text) + Convert.ToDouble(textBox4.Text) / 10);
            query.Parameters.AddWithValue("@p8", Convert.ToInt32(textBox6.Text));
            query.Parameters.AddWithValue("@p9", Convert.ToInt32(textBox14.Text));
            query.ExecuteNonQuery();

            itemPrice = Convert.ToInt32(textBox5.Text);
            foodPrice = Convert.ToInt32(textBox11.Text);
            fieldPrice = Convert.ToInt32(textBox12.Text);
            estateCommission = Convert.ToInt32(textBox13.Text);
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //save
            if (!int.TryParse(textBox1.Text, out int result0) ||
                !int.TryParse(textBox2.Text, out int result1) ||
                !int.TryParse(textBox3.Text, out int result2) ||
                !int.TryParse(textBox4.Text, out int result3) ||
                !int.TryParse(textBox5.Text, out int result5) ||
                !int.TryParse(textBox6.Text, out int result6) ||
                !int.TryParse(textBox7.Text, out int result7) ||
                !int.TryParse(textBox8.Text, out int result8) ||
                !int.TryParse(textBox9.Text, out int result9) ||
                !int.TryParse(textBox10.Text, out int result10) ||
                !int.TryParse(textBox11.Text, out int result11) ||
                !int.TryParse(textBox12.Text, out int result12) ||
                !int.TryParse(textBox13.Text, out int result13) ||
                !int.TryParse(textBox14.Text, out int result14))


            {
                MessageBox.Show("Eksik veya Yanlış karakter girildi");
            }
            else
            {
                save_rules();
                MessageBox.Show("Eklendi");
            }


        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            //exit
            this.Visible = false;
            loginScreen.Visible = true;
            //resetText();
        }

        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox clickedTextBox = (TextBox)sender;
            clickedTextBox.Text = string.Empty;
        }


    }
}