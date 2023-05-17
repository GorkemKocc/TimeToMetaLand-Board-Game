using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace MyTimeAtMetaland
{
    public class Game
    {
        public GameScreen gameScreen;
        public ShopScreen shopScreen;
        public MarketScreen marketScreen;
        public RealEstateScreen realEstateScreen;
        public Panel panel;
        public List<Button> land = new List<Button>();
        public int gameSizeX, gameSizeY;
        public NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=bunuunutmalütfen21");

        public Game()
        {
            gameSizeX = 10;
            gameSizeY = 15;
        }
        public void createMap()
        {
            panel.Visible = true;
            for (int j = 0; j < gameSizeY; j++)
            {
                for (int i = 0; i < gameSizeX; i++)
                {
                    Button plot = new Button();
                    int size = 700 / Math.Max(gameSizeX, gameSizeY);
                    plot.Size = new Size(size, size);
                    plot.Location = new Point(size * i + 80, size * j + 50);
                    plot.BackColor = Color.Brown;
                    plot.Click += new EventHandler(plot_Click);
                    gameScreen.Controls.Add(plot);
                    land.Add(plot);
                    plot.Name = land.Count.ToString();
                }
            }
            panel.Location = new Point(land[gameSizeX - 1].Location.X + land[gameSizeX - 1].Size.Width + 20, 0);

        }

        public void updateMap()
        {

            for (int i = 0; i < land.Count; i++)
            {
                land[i].BackColor = Color.Green;
            }

        }

        
        private void plot_Click(object sender, EventArgs e)
        {
            // Tıklanan butonun text özelliğini al
            Button button = sender as Button;
            string buttonText = button.Text;
            shopScreen.Visible = true;
            //marketScreen.Visible = true;
            gameScreen.Visible = false;
            // gameScreen.show_player();
        }
        

        public void updatePlayer()
        {


           
           

            string sqlQuery = "SELECT money_quantity FROM users WHERE id = 57"; // İlgili tablo ve koşulları burada belirtin.
            baglanti.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, baglanti))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int data = reader.GetInt32(0);
                        gameScreen.label2.Text = data.ToString();

                    }
                }
            }
            
            string sqlQuery2 = "SELECT item_quantity FROM users WHERE id = 57"; // İlgili tablo ve koşulları burada belirtin.
           
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery2, baglanti))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int data = reader.GetInt32(0);
                        gameScreen.label3.Text = data.ToString();
                    }
                }
            }
            
            string sqlQuery3 = "SELECT food_quantity FROM users WHERE id = 57"; // İlgili tablo ve koşulları burada belirtin.
            
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery3, baglanti))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int data = reader.GetInt32(0);
                        gameScreen.label4.Text = data.ToString();
                    }
                }
            }



            baglanti.Close();

            
        }













    }

}
