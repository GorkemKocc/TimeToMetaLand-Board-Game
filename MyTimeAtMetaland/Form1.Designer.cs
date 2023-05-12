namespace MyTimeAtMetaland
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameScreen1 = new MyTimeAtMetaland.GameScreen();
            this.shopScreen1 = new MyTimeAtMetaland.ShopScreen();
            this.marketScreen1 = new MyTimeAtMetaland.MarketScreen();
            this.realEstateScreen1 = new MyTimeAtMetaland.RealEstateScreen();
            this.loginScreen1 = new MyTimeAtMetaland.LoginScreen();
            this.SuspendLayout();
            // 
            // gameScreen1
            // 
            this.gameScreen1.Location = new System.Drawing.Point(0, 0);
            this.gameScreen1.Name = "gameScreen1";
            this.gameScreen1.Size = new System.Drawing.Size(870, 850);
            this.gameScreen1.TabIndex = 0;
            this.gameScreen1.Visible = false;
            this.gameScreen1.Load += new System.EventHandler(this.gameScreen1_Load);
            // 
            // shopScreen1
            // 
            this.shopScreen1.Location = new System.Drawing.Point(0, 0);
            this.shopScreen1.Name = "shopScreen1";
            this.shopScreen1.Size = new System.Drawing.Size(987, 555);
            this.shopScreen1.TabIndex = 1;
            // 
            // marketScreen1
            // 
            this.marketScreen1.Location = new System.Drawing.Point(0, 0);
            this.marketScreen1.Name = "marketScreen1";
            this.marketScreen1.Size = new System.Drawing.Size(987, 560);
            this.marketScreen1.TabIndex = 2;
            this.marketScreen1.Load += new System.EventHandler(this.marketScreen1_Load);
            // 
            // realEstateScreen1
            // 
            this.realEstateScreen1.Location = new System.Drawing.Point(0, 0);
            this.realEstateScreen1.Name = "realEstateScreen1";
            this.realEstateScreen1.Size = new System.Drawing.Size(998, 555);
            this.realEstateScreen1.TabIndex = 3;
            // 
            // loginScreen1
            // 
            this.loginScreen1.Location = new System.Drawing.Point(0, 0);
            this.loginScreen1.Name = "loginScreen1";
            this.loginScreen1.Size = new System.Drawing.Size(1000, 555);
            this.loginScreen1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(988, 553);
            this.Controls.Add(this.loginScreen1);
            this.Controls.Add(this.realEstateScreen1);
            this.Controls.Add(this.marketScreen1);
            this.Controls.Add(this.shopScreen1);
            this.Controls.Add(this.gameScreen1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GameScreen gameScreen1;
        private ShopScreen shopScreen1;
        private MarketScreen marketScreen1;
        private RealEstateScreen realEstateScreen1;
        private LoginScreen loginScreen1;
    }
}