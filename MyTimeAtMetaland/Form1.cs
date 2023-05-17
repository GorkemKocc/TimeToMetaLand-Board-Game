namespace MyTimeAtMetaland
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void gameScreen1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loginScreen1.Visible = false;
            gameScreen1.Visible = true;
            shopScreen1.Visible = false;
            marketScreen1.Visible = false;

            Game game = new Game();

            game.gameScreen = gameScreen1;
            game.shopScreen = shopScreen1;
            game.marketScreen = marketScreen1;
            game.realEstateScreen = realEstateScreen1;
            game.panel = gameScreen1.panel;
            game.createMap();

            marketScreen1.land = game.land;
            marketScreen1.game = game;

            realEstateScreen1.land = game.land;
            realEstateScreen1.game = game;

            shopScreen1.game = game;



            realEstateScreen1.Visible = false;
            realEstateScreen1.drawMap();
            shopScreen1.gameScreen = gameScreen1;
            marketScreen1.gameScreen = gameScreen1;
        }



        private void marketScreen1_Load(object sender, EventArgs e)
        {

        }
    }
}