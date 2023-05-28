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


        public RealEstateScreen()
        {
            InitializeComponent();
        }


        public void drawMap()
        {
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
        }

        private void land_Click(object sender, EventArgs e)
        {

            exButton.BackColor = Color.Brown;


            Button button = sender as Button;
            string buttonText = button.Text;

            exButton = button;

            button.BackColor = Color.Green;
            connection.Open();


            using (NpgsqlCommand command = new NpgsqlCommand("SELECT sale_price FROM field WHERE field_id = @v1;", connection))
            {
                command.Parameters.AddWithValue("@v1", Convert.ToInt32(button.Name));
                var salePrice = command.ExecuteScalar();



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
                }
                else if (!boolonSale && boolrental)
                {
                    button2.Enabled = false;
                    button3.Enabled = true;
                }
                else if (!boolonSale && !boolrental)
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
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

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}