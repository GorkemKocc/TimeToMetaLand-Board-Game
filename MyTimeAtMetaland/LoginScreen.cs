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

namespace MyTimeAtMetaland
{
    public partial class LoginScreen : UserControl
    {
        public GameScreen gameScreen;
        public AdminScreen adminScreen;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }
        NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user Id=postgres;" +
            "password=admin ");
        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand add = new NpgsqlCommand("insert into users(name, surname, password) values(@p1, @p2, @p3)", connection);
            add.Parameters.AddWithValue("@p1", textBox1.Text);
            add.Parameters.AddWithValue("@p2", textBox2.Text);
            add.Parameters.AddWithValue("@p3", textBox3.Text);
            add.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("eklnedi");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            gameScreen.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            adminScreen.Visible = true;
        }
    }
}
