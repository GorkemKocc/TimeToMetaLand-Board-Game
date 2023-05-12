using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTimeAtMetaland
{
    public partial class RealEstateScreen : UserControl
    {
        public UserControl gameScreen;
        internal Game game;
        public List<System.Windows.Forms.Button> land;
        int amount = 0;
        int price = 0;

        public RealEstateScreen()
        {
            InitializeComponent();
        }

        public void drawMap()
        {
            /*  
             for (int i = 0; i < land.Count; i++)
              {
                  int x, y, size;
                  x = i % 10;
                  y = i / 10;
                  size = 600 / Math.Max(10, 15);
                  land[i].Size = new Size(size, size);
                  land[i].Location = new Point(size * x + 80, size * y + 50);
                  land[i].BackColor = Color.Green;
                  panel1.Controls.Add(land[i]);
              }*/
            int gameSizeX = 14,
                gameSizeY = 8;
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
                    panel1.Controls.Add(plot);
                }
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
