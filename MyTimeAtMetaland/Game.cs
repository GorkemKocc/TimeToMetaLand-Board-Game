using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace MyTimeAtMetaland
{
    public class Game
    {
        public GameScreen gameScreen;
        public ShopScreen shopScreen;
        public MarketScreen marketScreen;
        public RealEstateScreen realEstateScreen;
        NpgsqlCommand query;
        public Panel panel;
        public List<Button> land = new List<Button>();
        public int gameSizeX, gameSizeY;
        public NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");

        public Game()
        {
            gameSizeX = 6;
            gameSizeY = 5;
        }
        public void createMap()
        {
            panel.Visible = true;
            connection.Open();

            // query = new NpgsqlCommand("delete from field", connection);
            // query.ExecuteNonQuery();
            // query = new NpgsqlCommand("ALTER SEQUENCE public.field_field_id_seq RESTART WITH 1;", connection);
            //query.ExecuteNonQuery();
            query = new NpgsqlCommand("select user_id from users where name = @v1;", connection);
            query.Parameters.AddWithValue("@v1", "Admin");
            var admin_id = query.ExecuteScalar();

            //query = new NpgsqlCommand("insert into field (field_type, field_owner_id)values (@v1,@v2);", connection);

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
                    // query.Parameters.AddWithValue("@v1", "Field");
                    //query.Parameters.AddWithValue("@v2", admin_id);
                    //                 query.ExecuteNonQuery();

                }
            }
            panel.Location = new Point(land[gameSizeX - 1].Location.X + land[gameSizeX - 1].Size.Width + 20, 0);
            connection.Close();
            //setAdminBusinesses();
        }
        void setAdminBusinesses()
        {

            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE field SET field_type = 'business' WHERE field_id IN (SELECT field_id FROM field ORDER BY field_id LIMIT 3)", connection))
            {
                command.ExecuteNonQuery();
            }
            using (NpgsqlCommand command = new NpgsqlCommand())
            {

            }
            connection.Close();

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
            connection.Open();
            query = new NpgsqlCommand("select field_type from field where field_id = @v1;", connection);
            query.Parameters.AddWithValue("@v1", int.Parse(button.Name));
            var field_type = query.ExecuteScalar();
            //Type type = field_type.GetType();
            if (field_type.ToString() == "business")
            {
                query = new NpgsqlCommand("SELECT business.business_type FROM field JOIN business ON @v1 = business.business_field_id;", connection);
                query.Parameters.AddWithValue("@v1", int.Parse(button.Name));
                field_type = query.ExecuteScalar();
                //MessageBox.Show(field_type.ToString());
                if (field_type.ToString() == "shop")
                {
                    shopScreen.Visible = true;
                    gameScreen.Visible = false;
                }
                else if (field_type.ToString() == "grocery")
                {
                    marketScreen.Visible = true;
                    gameScreen.Visible = false;
                }
                else if (field_type.ToString() == "real_estate")
                {
                    realEstateScreen.Visible = true;
                    gameScreen.Visible = false;
                }

            }

            connection.Close();
        }


        public void updatePlayer()
        {
            string sqlQuery = "SELECT money_quantity FROM users WHERE user_id = " + Convert.ToString(gameScreen.newUsers[0].Item3) + ";";
            connection.Open();
            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
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

            string sqlQuery2 = "SELECT item_quantity FROM users WHERE user_id = " + Convert.ToString(gameScreen.newUsers[0].Item3) + ";";

            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery2, connection))
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

            string sqlQuery3 = "SELECT food_quantity FROM users WHERE user_id = " + Convert.ToString(gameScreen.newUsers[0].Item3) + ";";

            using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery3, connection))
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

            connection.Close();

        }
    }
}
