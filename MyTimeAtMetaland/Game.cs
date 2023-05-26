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
        public AdminScreen adminScreen;
        public ShopScreen shopScreen;
        public MarketScreen marketScreen;
        public RealEstateScreen realEstateScreen;
        NpgsqlCommand query;
        public Panel panel;
        public List<Button> land = new List<Button>();
        public int gameSizeX, gameSizeY;
        public NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");
        public DateTime gameDate = new DateTime(2023, 1, 1);
        public Game()
        {
            gameSizeX = 6;
            gameSizeY = 5;
        }
        public void createMap()
        {
            panel.Visible = true;
            connection.Open();
            query = new NpgsqlCommand("delete from shop", connection);
            query.ExecuteNonQuery();

            query = new NpgsqlCommand("delete from grocery", connection);
            query.ExecuteNonQuery();

            query = new NpgsqlCommand("delete from real_estate", connection);
            query.ExecuteNonQuery();

            query = new NpgsqlCommand("delete from business", connection);
            query.ExecuteNonQuery();
            query = new NpgsqlCommand("ALTER SEQUENCE public.business_business_field_id_seq RESTART WITH 1;", connection);
            query.ExecuteNonQuery();

            query = new NpgsqlCommand("delete from field", connection);
            query.ExecuteNonQuery();
            query = new NpgsqlCommand("ALTER SEQUENCE public.field_field_id_seq RESTART WITH 1;", connection);
            query.ExecuteNonQuery();

            query = new NpgsqlCommand("select user_id from users where name = @v1;", connection);
            query.Parameters.AddWithValue("@v1", "Admin");
            var admin_id = query.ExecuteScalar();

            query = new NpgsqlCommand("insert into field (field_type, field_owner_id, on_sale, sale_price, rental)values (@v1,@v2,@v3,@v4,@v5);", connection);

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
                    query.Parameters.AddWithValue("@v1", "Field");
                    query.Parameters.AddWithValue("@v2", admin_id);
                    query.Parameters.AddWithValue("@v3", true);
                    query.Parameters.AddWithValue("@v4", adminScreen.fieldPrice);
                    query.Parameters.AddWithValue("@v5", false);
                    query.ExecuteNonQuery();

                }
            }
            panel.Location = new Point(land[gameSizeX - 1].Location.X + land[gameSizeX - 1].Size.Width + 20, 0);
            connection.Close();
            setAdminBusinesses();
        }
        void setAdminBusinesses()
        {
            // ADMİN EKRANINDAN GELELN EŞYA YEMEK COMİSYON SET ET ///////////////

            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE field SET on_sale = @v1, field_type = 'business' WHERE field_id IN (SELECT field_id FROM field ORDER BY field_id LIMIT 3)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@v1", false);
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (NpgsqlCommand query = new NpgsqlCommand("INSERT INTO business (business_field_id, business_level, business_capacity, business_employee_count, business_income_amount, business_income_rate, business_level_start_date, business_type) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8)", connection))
            {
                connection.Open();
                query.Parameters.AddWithValue("@v1", 1);
                query.Parameters.AddWithValue("@v2", 1);
                query.Parameters.AddWithValue("@v3", 3);
                query.Parameters.AddWithValue("@v4", 0);
                query.Parameters.AddWithValue("@v5", 100);
                query.Parameters.AddWithValue("@v6", 1);
                query.Parameters.AddWithValue("@v7", gameDate);
                query.Parameters.AddWithValue("@v8", "shop");
                query.ExecuteNonQuery();

                query.Parameters.Clear();

                query.Parameters.AddWithValue("@v1", 2);
                query.Parameters.AddWithValue("@v2", 1);
                query.Parameters.AddWithValue("@v3", 3);
                query.Parameters.AddWithValue("@v4", 0);
                query.Parameters.AddWithValue("@v5", 100);
                query.Parameters.AddWithValue("@v6", 1);
                query.Parameters.AddWithValue("@v7", gameDate);
                query.Parameters.AddWithValue("@v8", "grocery");
                query.ExecuteNonQuery();

                query.Parameters.Clear();

                query.Parameters.AddWithValue("@v1", 3);
                query.Parameters.AddWithValue("@v2", 1);
                query.Parameters.AddWithValue("@v3", 3);
                query.Parameters.AddWithValue("@v4", 0);
                query.Parameters.AddWithValue("@v5", 100);
                query.Parameters.AddWithValue("@v6", 1);
                query.Parameters.AddWithValue("@v7", gameDate);
                query.Parameters.AddWithValue("@v8", "real_estate");
                query.ExecuteNonQuery();

                query.Parameters.Clear();

                connection.Close();
            }
            //"INSERT INTO grocery (grocery_field_id, grocery_food_price) VALUES (@v1, @v2)"
            using (NpgsqlCommand query = new NpgsqlCommand("INSERT INTO shop (shop_field_id, shop_item_price) VALUES (@v1, @v2)", connection))
            {
                connection.Open();
                query.Parameters.AddWithValue("@v1", 1);
                query.Parameters.AddWithValue("@v2", adminScreen.itemPrice);
                query.ExecuteNonQuery();
                connection.Close();
            }
            using (NpgsqlCommand query = new NpgsqlCommand("INSERT INTO grocery (grocery_field_id, grocery_food_price) VALUES (@v1, @v2)", connection))
            {
                connection.Open();
                query.Parameters.AddWithValue("@v1", 2);
                query.Parameters.AddWithValue("@v2", adminScreen.foodPrice);
                query.ExecuteNonQuery();
                connection.Close();
            }
            using (NpgsqlCommand query = new NpgsqlCommand("INSERT INTO real_estate (real_estate_field_id, estate_commission) VALUES(@v1, @v2)", connection))
            {
                connection.Open();
                query.Parameters.AddWithValue("@v1", 3);
                query.Parameters.AddWithValue("@v2", adminScreen.estateCommission);
                query.ExecuteNonQuery();
                connection.Close();
            }
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
                    realEstateScreen.estateId = int.Parse(button.Name);
                    realEstateScreen.Visible = true;
                    gameScreen.Visible = false;
                    realEstateScreen.drawMap();
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
