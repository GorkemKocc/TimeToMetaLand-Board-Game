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
        public UserControl gameScreen;
        internal Game game;
        public List<System.Windows.Forms.Button> land;
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
            textBox1.Text = amount.ToString() + "$";
        }

        private void Market_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //exit
            comboBox1.SelectedIndex = 0;
            this.Visible = false;
            game.updateMap();
            gameScreen.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //buy
            MessageBox.Show("alındı");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amount = comboBox1.SelectedIndex;
            textBox1.Text = amount.ToString() + "$";
            price = comboBox1.SelectedIndex;
        }
    }
}
