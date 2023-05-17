using Npgsql;
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

namespace MyTimeAtMetaland
{
    public partial class ShopScreen : UserControl
    {
        public GameScreen gameScreen;
        public Game game;
        int amount = 0;
        int price = 0;
        public NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=bunuunutmalütfen21");


        public ShopScreen()
        {
            InitializeComponent();
            for (int i = 0; i <= 40; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amount = comboBox1.SelectedIndex;
            
            string sqlQuery = "SELECT shop_item_price FROM shop WHERE shop_id = 1"; // İlgili tablo ve koşulları burada belirtin.
            baglanti.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, baglanti))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        price = reader.GetInt32(0); // İlgili sütunun indeksini veya adını belirtin.
                                                        // Değeri istediğiniz şekilde kullanabilirsiniz.
                    }
                }
            }
            baglanti.Close();
            textBox2.Text = (amount*price).ToString() + "$";

            // price = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string sqlQuery = "UPDATE users SET money_quantity = money_quantity - @Price";
            string sqlQuery2 = "UPDATE users SET item_quantity = item_quantity + @Amount";

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand(sqlQuery, baglanti);
            NpgsqlCommand komut2 = new NpgsqlCommand(sqlQuery2, baglanti);
            komut.Parameters.AddWithValue("@Price", price*amount);
            komut2.Parameters.AddWithValue("@Amount", amount);


            komut.ExecuteNonQuery();
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("alındı");
            
            game.updatePlayer();
            

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //exit
            comboBox1.SelectedIndex = 0;
            this.Visible = false;
            gameScreen.Visible = true;
        }

        private void ShopScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
