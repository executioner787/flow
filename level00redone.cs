using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace flow
{
    public partial class level00redone : Form
    {
        public level00redone()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        //settingsVariables
        private int windowStyle = 0; //0 - windowed, 1 - borderless, 2 - fullscreen
        private int screenHeight = 720;
        private int screenWidth = 1280;
        private Color accentColour = Color.BlueViolet;
        private Color backColour = Color.White;
        private Color textColour = Color.Black;

        private void level00redone_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("settings.ini"))
                {
                    //read settings file
                    readSettings();
                }
                applySettings();
            }
            catch (Exception exception)
            {

            }
            this.pAccent = new Pen(this.accentColour, 2);
            this.pConter = new Pen(this.textColour, 2);
            this.brush = new SolidBrush(this.accentColour);
            Image img = this.pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            this.pictureBox1.Image = img;
            this.tutorialText.Text = "Welcome!\nYour goal is to move circle from its starting position to the ending.";
            tutorialButton.Text = "Next";
            this.posY = canvas.Height / 2 - 20;
            this.posX = 60;
            DisableTabStop(this);
        }

        private void DisableTabStop(Control item)
        {
            item.TabStop = false;
            foreach (Control subitem in item.Controls)
            {
                DisableTabStop(subitem);
            }
        }

        private int colorIndex = 0; //11 colours
        private Color[] palette = { Color.BlueViolet, Color.HotPink, Color.Crimson, Color.Coral, Color.Gold, Color.Tan, Color.MediumSeaGreen, Color.DarkTurquoise, Color.DeepSkyBlue, Color.SeaShell, Color.DarkGray };

        private void applySettings()
        {
            switch (this.windowStyle)
            {
                case 0:
                    this.Size = new Size(this.screenWidth, this.screenHeight);
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    this.WindowState = FormWindowState.Normal;
                    break;
                case 1:
                    this.Size = new Size(this.screenWidth, this.screenHeight);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Normal;
                    break;
                default:
                    this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    break;
            }
            this.accentColour = palette[colorIndex];
            if (new int[] { 4, 7, 9, 10 }.Contains(colorIndex))
            {
                this.backColour = Color.DimGray;
                this.textColour = Color.Black;
            }
            else
            {
                this.backColour = Color.White;
                this.textColour = Color.Black;
            }
            this.BackColor = this.backColour;
            this.CenterToScreen();
        }

        private void readSettings()
        {
            using (StreamReader sr = new StreamReader("settings.ini"))
            {
                sr.ReadLine();
                string line = sr.ReadLine();
                this.windowStyle = Int32.Parse(line.Substring(line.IndexOf("=") + 1));
                line = sr.ReadLine();
                this.screenWidth = Int32.Parse(line.Substring(line.IndexOf("=") + 1));
                line = sr.ReadLine();
                this.screenHeight = Int32.Parse(line.Substring(line.IndexOf("=") + 1));
                line = sr.ReadLine();
                this.colorIndex = Int32.Parse(line.Substring(line.IndexOf("=") + 1));
            }
        }

        private void levelBackButton_Click(object sender, EventArgs e)
        {
            if(tutorialStage==1)
            {
                tutorialStage--;
                this.tutorialText.Text = "Welcome!\nYour goal is to move circle from its starting position to the ending.";
                tutorialButton.Text = "Next";
            } else
            {
                this.Close();
            }
        }

        private void levelBackButton_MouseEnter(object sender, EventArgs e)
        {
            this.levelBackButton.Font = new Font(this.levelBackButton.Font.Name, 9.75f, FontStyle.Underline);
            this.levelBackButton.ForeColor = this.accentColour;
            this.levelBackButton.Cursor = Cursors.Hand;
        }

        private void levelBackButton_MouseLeave(object sender, EventArgs e)
        {
            this.levelBackButton.Font = new Font(this.levelBackButton.Font.Name, 9.75f, FontStyle.Regular);
            this.levelBackButton.ForeColor = this.textColour;
            this.levelBackButton.Cursor = Cursors.Default;
        }

        private int tutorialStage = 0;
        private void tutorialButton_Click(object sender, EventArgs e)
        {
            switch(tutorialStage)
            {
                case 0:
                    tutorialStage++;
                    this.tutorialText.Text = "To control the Circle use [Arrows] on keyboard, [Mouse] or [Touchscreen]";
                    tutorialButton.Text = "Start";
                    break;
                case 1:
                    tutorialStage++;
                    this.tutorialPanel.Visible = false;
                    break;
                case 99:
                    (new level01()).Show();
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Left:
                case Keys.A:
                    if (posX > 60 && tutorialStage>1)
                    {
                        posX-=5;
                        this.Refresh();
                    }
                    break;
                case Keys.Right:
                case Keys.D:
                    if (posX < canvas.Width - 40 && tutorialStage > 1)
                    {
                        posX+=5;
                        this.Refresh();
                    }
                    checkWin();
                    break;
                case Keys.Escape:
                    this.levelBackButton_Click(new object(), new EventArgs());
                    break;
                case Keys.Enter:
                case Keys.Tab:
                case Keys.Space:
                    if(tutorialStage<2||tutorialStage==99)
                    {
                        this.tutorialButton_Click(new object(), new EventArgs());
                    }else
                    {

                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        private void tutorialButton_MouseEnter(object sender, EventArgs e)
        {
            this.tutorialButton.Font = new Font(this.tutorialButton.Font.Name, 9.75f, FontStyle.Underline);
            this.tutorialButton.ForeColor = this.accentColour;
            this.tutorialButton.Cursor = Cursors.Hand;
        }

        private void tutorialButton_MouseLeave(object sender, EventArgs e)
        {
            this.tutorialButton.Font = new Font(this.tutorialButton.Font.Name, 9.75f, FontStyle.Regular);
            this.tutorialButton.ForeColor = this.textColour;
            this.tutorialButton.Cursor = Cursors.Default;
        }

        //graphics
        Graphics gr;
        Pen pAccent;
        Pen pConter;
        SolidBrush brush;

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            gr = e.Graphics;
            DrawBG();
            DrawPath();
        }

        private void DrawBG()
        {
            gr.DrawArc(pConter, 40, canvas.Height / 2 - 20, 40, 40, 0, 360);
            gr.DrawArc(new Pen(this.backColour, 2), 40, canvas.Height / 2 - 20, 40, 40, -30, 60);
            gr.DrawLine(pConter, 76, canvas.Height / 2 - 10, canvas.Width - 56, canvas.Height / 2 - 10);
            gr.DrawLine(pConter, 76, canvas.Height / 2 + 10, canvas.Width - 56, canvas.Height / 2 + 10);
            gr.DrawArc(pConter, canvas.Width - 60, canvas.Height / 2 - 20, 40, 40, 0, 360);
            gr.DrawArc(new Pen(this.backColour, 2), canvas.Width - 60, canvas.Height / 2 - 20, 40, 40, 154, 54);
            gr.FillEllipse(brush, 40, canvas.Height / 2 - 20, 39, 39);
        }

        bool mouseDown = false;
        int posX = 60;
        int posY;

        private void DrawPath()
        {
            gr.FillEllipse(brush, posX - 20, posY, 39, 39);
            gr.FillRectangle(brush, 60, posY + 10, posX - 60, 19);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            checkWin();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown)
            {
                if(e.Location.X > posX - 20 && e.Location.X < posX + 20)
                {
                    if(e.Location.X>=60 && e.Location.X <= canvas.Width - 40)
                    {
                        posX = e.Location.X;
                        this.Refresh();
                    }
                }
            }
        }

        private void checkWin()
        {
            if(posX>=canvas.Width - 40)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Game game = new Game();
                using (Stream sr = new FileStream("game.save", FileMode.Open, FileAccess.Read))
                {
                    game = (Game)formatter.Deserialize(sr);
                }
                game.gameStarted = true;
                if (game.currentLevel < 1)
                    game.currentLevel = 1;
                using (Stream sr = new FileStream("game.save", FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(sr, game);
                }
                tutorialStage = 99;
                tutorialPanel.Visible = true;
                tutorialText.Text = "Congratulations!\nYou have just finished this level!";
                tutorialButton.Text = "Next Level";
            }
        }
    }
}
