using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace MyTimeAtMetaland
{
    public partial class RealEstateScreen : UserControl
    {
        public GameScreen gameScreen;
        internal Game game;
        public List<System.Windows.Forms.Button> land;
        public NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5432; Database=MetaLand; user ID=postgres; password=admin");
        int amount = 0;
        int price = 0;
        public int estateId;
        public int Commission;
        private Button exButton = new Button();
        int c = 0;
        internal AdminScreen adminScreen;


        public RealEstateScreen()
        {
            InitializeComponent();
        }


        public void drawMap()
        {

            foreach (Control control in panel1.Controls)
            {
                if (control is Button)
                {
                    panel1.Controls.Remove(control);
                    control.Dispose();
                }
            }
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT money_quantity FROM users WHERE user_id = @v1", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@v1", gameScreen.newUsers[0].Item3);
                var money = command.ExecuteScalar();
                label2.Text = money.ToString();
                connection.Close();
            }

            using (NpgsqlCommand query3 = new NpgsqlCommand("SELECT estate_commission FROM real_estate WHERE real_estate_field_id = @v3;", connection))
            {
                connection.Open();
                query3.Parameters.AddWithValue("@v3", estateId);
                query3.ExecuteNonQuery();
                var commission = query3.ExecuteScalar();
                Commission = Convert.ToInt32(commission);
                connection.Close();
            }


            int gameSizeX = game.gameSizeX,
                gameSizeY = game.gameSizeY;
            int buttonPlace = 1;
            for (int j = 0; j < gameSizeY; j++)
            {
                for (int i = 0; i < gameSizeX; i++)
                {
                    Button plot = new Button();
                    int size;
                    //if (Math.Abs(gameSizeX - gameSizeY) < 4)
                    size = 350 / Math.Min(gameSizeX, gameSizeY);
                    // else
                    // size = 350 / Math.Min(gameSizeX, gameSizeY);

                    plot.Size = new Size(size, size);

                    if (gameSizeX < gameSizeY)
                        plot.Location = new Point(size * j + 20, size * i + 10);
                    else
                        plot.Location = new Point(size * i + 20, size * j + 10);

                    plot.BackColor = Color.Brown;
                    plot.Name = buttonPlace.ToString();

                    NpgsqlCommand query = new NpgsqlCommand("SELECT on_sale FROM field WHERE field_id = @v2;", connection);
                    connection.Open();
                    query.Parameters.AddWithValue("@v2", Convert.ToInt32(plot.Name));
                    query.ExecuteNonQuery();
                    var onSale = query.ExecuteScalar();
                    bool boolonSale = false;
                    if (onSale != null && onSale != DBNull.Value)
                    {
                        boolonSale = Convert.ToBoolean(onSale);
                    }
                    connection.Close();
                    NpgsqlCommand query2 = new NpgsqlCommand("SELECT rental FROM field WHERE field_id = @v4;", connection);
                    connection.Open();
                    query2.Parameters.AddWithValue("@v4", Convert.ToInt32(plot.Name));
                    query2.ExecuteNonQuery();
                    var rental = query2.ExecuteScalar();
                    bool boolrental = false;
                    if (rental != null && rental != DBNull.Value)
                    {
                        boolrental = Convert.ToBoolean(rental);
                    }
                    if (boolonSale && !boolrental)
                    {
                        plot.BackColor = Color.Brown;
                    }
                    else if (!boolonSale && boolrental)
                    {
                        plot.BackColor = Color.Pink;
                    }
                    else if (!boolonSale && !boolrental)
                    {
                        plot.BackColor = Color.Gray;
                    }
                    connection.Close();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT field_type FROM field WHERE field_id = @v1", connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@v1", int.Parse(plot.Name));
                        command.ExecuteNonQuery();
                        var type = command.ExecuteScalar();

                        plot.Text = type.ToString();


                        connection.Close();

                    }
                    plot.Click += new EventHandler(land_Click);
                    panel1.Controls.Add(plot);

                    buttonPlace++;
                }
            }
            label1.Text = "Fiyat";
            label3.Text = "Komisyon";
            label4.Visible = false;
            textBox4.Visible = false;
        }

        private void land_Click(object sender, EventArgs e)
        {
            if (c == 1)
                exButton.BackColor = Color.Brown;
            if (c == 2)
                exButton.BackColor = Color.Pink;
            c = 0;
            // exButton.BackColor = Color.Brown;


            Button button = sender as Button;
            string buttonText = button.Text;

            exButton = button;

            button.BackColor = Color.Green;
            connection.Open();


            using (NpgsqlCommand command = new NpgsqlCommand("SELECT sale_price FROM field WHERE field_id = @v1;", connection))
            {
                command.Parameters.AddWithValue("@v1", Convert.ToInt32(button.Name));
                var salePrice = command.ExecuteScalar();

                Commission = (Convert.ToInt32(salePrice) * Convert.ToInt32(Commission) / 100);

                textBox1.Text = salePrice.ToString();
                textBox2.Text = Commission.ToString();

                NpgsqlCommand query = new NpgsqlCommand("SELECT on_sale FROM field WHERE field_id = @v2;", connection);
                query.Parameters.AddWithValue("@v2", Convert.ToInt32(button.Name));
                query.ExecuteNonQuery();
                var onSale = query.ExecuteScalar();
                bool boolonSale = false;
                if (onSale != null && onSale != DBNull.Value)
                {
                    boolonSale = Convert.ToBoolean(onSale);
                }


                NpgsqlCommand query2 = new NpgsqlCommand("SELECT rental FROM field WHERE field_id = @v4;", connection);
                query2.Parameters.AddWithValue("@v4", Convert.ToInt32(button.Name));
                query2.ExecuteNonQuery();
                var rental = query2.ExecuteScalar();
                bool boolrental = false;
                if (rental != null && rental != DBNull.Value)
                {
                    boolrental = Convert.ToBoolean(rental);
                }

                if (boolonSale && !boolrental)
                {
                    button2.Enabled = true;
                    button3.Enabled = false;
                    c++;
                }
                else if (!boolonSale && boolrental)
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                    c += 2;
                }
                else if (!boolonSale && !boolrental)
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                    exButton.BackColor = Color.Gray;
                }




                command.ExecuteNonQuery();


            }
            connection.Close();




        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            gameScreen.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kirala
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //satÄ±n al

            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE users SET money_quantity = money_quantity - @Price WHERE user_id = @v1", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@v1", gameScreen.newUsers[0].Item3);
                command.Parameters.AddWithValue("@Price", (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text)));
                command.ExecuteNonQuery();
                connection.Close();
                game.updatePlayer();
            }
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT money_quantity FROM users WHERE user_id = @v1", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@v1", gameScreen.newUsers[0].Item3);
                var money = command.ExecuteScalar();
                label2.Text = money.ToString();
                command.ExecuteNonQuery();
                connection.Close();
            }
            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE users SET money_quantity = money_quantity + @Price WHERE user_id = @v1", connection))
            {
                connection.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("SELECT field_owner_id FROM field WHERE field_id = @v2", connection);
                command2.Parameters.AddWithValue("@v2", Convert.ToInt32(exButton.Name));
                var ownerId = command2.ExecuteScalar();
                command.Parameters.AddWithValue("@v1", Convert.ToInt32(ownerId));
                command.Parameters.AddWithValue("@Price", Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text));
                command2.ExecuteNonQuery();
                command.ExecuteNonQuery();
                connection.Close();
                game.updatePlayer();
            }
            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE users SET money_quantity = money_quantity + @Price WHERE user_id = @v1", connection))
            {
                connection.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("SELECT field.field_owner_id FROM business JOIN field ON @estateId = field.field_id", connection);
                command2.Parameters.AddWithValue("@estateId", estateId);
                var ownerId = command2.ExecuteScalar();
                command.Parameters.AddWithValue("@v1", Convert.ToInt32(ownerId));
                command.Parameters.AddWithValue("@Price", Convert.ToInt32(textBox2.Text));
                command2.ExecuteNonQuery();
                command.ExecuteNonQuery();
                connection.Close();
                game.updatePlayer();
            }
            using (NpgsqlCommand command = new NpgsqlCommand("UPDATE field SET field_owner_id = @v1, on_sale = @v3, rental = @v4 WHERE field_id = @v2", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@v1", gameScreen.newUsers[0].Item3);
                command.Parameters.AddWithValue("@v2", Convert.ToInt32(exButton.Name));
                command.Parameters.AddWithValue("@v3", false);
                command.Parameters.AddWithValue("@v4", false);
                command.ExecuteNonQuery();
                connection.Close();
                game.updatePlayer();
                drawMap();
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            //grocery
            button5.BackColor = Color.Yellow;
            label4.Visible = true;
            textBox4.Visible = true;
            if (button4.BackColor == Color.Yellow || button6.BackColor == Color.Yellow)
            {
                button4.BackColor = Color.White;
                button6.BackColor = Color.White;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //shop
            //int cost = adminScreen.businessCost;
            button4.BackColor = Color.Yellow;
            label4.Visible = true;
            textBox4.Visible = true;
            label4.Text = "Eşya fiyatı";
            textBox3.Text = adminScreen.textBox14.Text;
            if (button5.BackColor == Color.Yellow || button6.BackColor == Color.Yellow)
            {
                button5.BackColor = Color.White;
                button6.BackColor = Color.White;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //real estate
            button6.BackColor = Color.Yellow;
            label4.Visible = true;
            textBox4.Visible = true;
            if (button5.BackColor == Color.Yellow || button4.BackColor == Color.Yellow)
            {
                button5.BackColor = Color.White;
                button4.BackColor = Color.White;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //kur

            if (button4.BackColor == Color.Yellow)
            {
                /*   using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO business (business_type, business_level, business_capacity, business_employee_count, business_income_amount, business_income_rate, business_level_start_date, business_field_id) VALUES (@v1, @v2, @v3, @v4, @v5, @v6, @v7, @v8);", connection))
                   {
                       connection.Open();
                       command.Parameters.AddWithValue("@v1", "shop");
                       command.Parameters.AddWithValue("@v2", 1);
                       command.Parameters.AddWithValue("@v3", 3);
                       command.Parameters.AddWithValue("@v4", 0);
                       command.Parameters.AddWithValue("@v5", 0);
                       command.Parameters.AddWithValue("@v6", 0);
                       command.Parameters.AddWithValue("@v7", new DateTime(2022, 2, 12));
                       command.Parameters.AddWithValue("@v8", Convert.ToInt32(exButton.Name));
                       command.ExecuteNonQuery();
                       connection.Close();
                   }*/
                using (NpgsqlCommand command = new NpgsqlCommand("insert into business (business_type, business_level, business_capacity, business_employee_count, business_income_amount, business_income_rate, business_level_start_date, business_field_id)values(@v1, @v2, @v3, @v4, @v5, @v6, @v7, @v8);;", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@v1", "shop");
                    command.Parameters.AddWithValue("@v2", 1);
                    command.Parameters.AddWithValue("@v3", 3);
                    command.Parameters.AddWithValue("@v4", 0);
                    command.Parameters.AddWithValue("@v5", 0);
                    command.Parameters.AddWithValue("@v6", 0);
                    command.Parameters.AddWithValue("@v7", new DateTime(2022, 2, 12));
                    command.Parameters.AddWithValue("@v8", Convert.ToInt32(exButton.Name));
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                using (NpgsqlCommand command = new NpgsqlCommand("insert into shop (shop_field_id, shop_item_price)values (@v1,@v2);", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@v1", Convert.ToInt32(exButton.Name));
                    command.Parameters.AddWithValue("@v2", Convert.ToInt32(textBox4.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE field SET field_type = @v1 WHERE field_id = @v2;", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@v1", "business");
                    command.Parameters.AddWithValue("@v2", Convert.ToInt32(exButton.Name));
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                drawMap();

            }
        }
    }
}