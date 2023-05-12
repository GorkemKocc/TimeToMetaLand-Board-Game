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
    public partial class ShopScreen : UserControl
    {
        public UserControl gameScreen;
        int amount = 0;
        int price = 0;

        public ShopScreen()
        {
            InitializeComponent();
            for (int i = 0; i <= 40; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
            textBox2.Text = amount.ToString() + "$";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            amount = comboBox1.SelectedIndex;
            textBox2.Text = amount.ToString() + "$";
            price = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("alındı");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //exit
            comboBox1.SelectedIndex = 0;
            this.Visible = false;
            gameScreen.Visible = true;
        }

    }
}
