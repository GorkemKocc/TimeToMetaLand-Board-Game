using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace MyTimeAtMetaland
{
    internal class Game
    {
        public UserControl gameScreen, shopScreen, marketScreen, realEstateScreen;
        public Panel panel;
        public List<Button> land = new List<Button>();
        public int gameSizeX, gameSizeY;

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
            //shopScreen.Visible = true;
            marketScreen.Visible = true;
            gameScreen.Visible = false;
            // gameScreen.show_player();
        }














    }

}
