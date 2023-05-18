using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyTimeAtMetaland
{
    public partial class MarketScreen : UserControl
    {
        public GameScreen gameScreen;
        internal Game game;
        public List<System.Windows.Forms.Button> land;
        NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");
        int amount = 0;
        int price = 0;
        public MarketScreen()
        {
            InitializeComponent();
            for (int i = 0; i <= 40; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Market_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //exit
            comboBox1.SelectedIndex = 0;
            this.Visible = false;
            //game.updateMap();
            gameScreen.Visible = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //buy


            string sqlQuery = "UPDATE users SET money_quantity = money_quantity - @Price";
            string sqlQuery2 = "UPDATE users SET food_quantity = food_quantity + @Amount";

            connection.Open();
            NpgsqlCommand query = new NpgsqlCommand(sqlQuery, connection);
            NpgsqlCommand query2 = new NpgsqlCommand(sqlQuery2, connection);
            query.Parameters.AddWithValue("@Price", price * amount);
            query2.Parameters.AddWithValue("@Amount", amount);

            query.ExecuteNonQuery();
            query2.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("alındı");

            game.updatePlayer();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amount = comboBox1.SelectedIndex;

            string sqlQuery = "SELECT grocery_food_price FROM grocery WHERE grocery_field_id = 2";
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        price = reader.GetInt32(0);
                    }
                }
            }
            connection.Close();
            textBox1.Text = (amount * price).ToString() + "$";

        }
    }
}
