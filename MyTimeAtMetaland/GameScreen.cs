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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MyTimeAtMetaland
{
    public partial class GameScreen : UserControl
    {
        public Panel panel;
        /* public UserControl gameScreen, shopScreen, marketScreen, realEstateScreen;
         public List<Button> land = new List<Button>();
         public int gameSizeX, gameSizeY;*/
        NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user Id=postgres;" +
            "password=admin ");
        NpgsqlDataReader reader;
        NpgsqlCommand query;
        public GameScreen()
        {
            InitializeComponent();
            //gameSizeX = 4;
            //gameSizeY = 3;
            /* connection.Open();
             query = new NpgsqlCommand("Select name, surname from users;", connection);
             reader = query.ExecuteReader();
             //connection.Close(); */
            show_player();
        }


        private void GameScreen_Load(object sender, EventArgs e)
        {
            panel = panel1 as Panel;
        }

        public void show_player()
        {
            connection.Open();
            query = new NpgsqlCommand("Select name, surname from users where user_id = 2;", connection);
            reader = query.ExecuteReader();
            //connection.Close();

            reader.Read();
            string name = reader.GetString(0);
            string surname = reader.GetString(1);
            connection.Close();
            connection.Open();
            query = new NpgsqlCommand();
            query.CommandText = "Select food_quantity from users where user_id=2;";
            query = new NpgsqlCommand("Select food_quantity from users where user_id=2;", connection);
            query.ExecuteNonQuery();
            var foodQuantity = query.ExecuteScalar();

            query.CommandText = "Select item_quantity from users where user_id=2;";
            query.ExecuteNonQuery();
            var itemQuantity = query.ExecuteScalar();

            query.CommandText = "Select money_quantity from users where user_id=2;";
            query.ExecuteNonQuery();
            var moneyQuantity = query.ExecuteScalar();

            label1.Text = name + " " + surname;
            label2.Text = moneyQuantity.ToString();
            label3.Text = itemQuantity.ToString();
            label4.Text = foodQuantity.ToString();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            show_player();

        }
    }
}
