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
    public partial class level01 : Form
    {
        public level01()
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
            DisableTabStop(this);
            this.pAccent = new Pen(this.accentColour, 2);
            this.pConter = new Pen(this.textColour, 2);
            this.brush = new SolidBrush(this.accentColour);
            Image img = this.pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            this.pictureBox1.Image = img;
            this.tutorialText.Text = "";
            tutorialButton.Text = "Next";
            this.tutorialPanel.Visible = false;
            this.posY = canvas.Height / 2 + 30;
            this.posX = 60;
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
            this.Close();
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
                case 99:
                    (new level02()).Show();
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
                    if (posX >= 60 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 70)
                    {
                        stage = 0;
                        posX -= 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 0 || stage == 2 || stage == 3)
                            stage = 1;
                        else if (stage == 7)
                            stage = 8;
                        else if (stage == 26)
                            stage = 27;
                        else if (stage == 64)
                            stage = 65;
                        posX = canvas.Width / 2 - 140;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 0 || stage == 1 || stage == 5 || stage == 9)
                            stage = 2;
                        else if (stage == 4 || stage == 10)
                            stage = 6;
                        else if (stage == 19 || stage == 21)
                            stage = 22;
                        else if (stage == 55 || stage == 56)
                            stage = 57;
                        if (stage == 2 || posX >= canvas.Width / 2 - 90)
                            posX -= 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 1 || stage == 4 || stage == 12)
                            stage = 3;
                        else if (stage == 5 || stage == 8 || stage == 11)
                            stage = 7;
                        else if (stage == 24 || stage == 25 || stage == 27)
                            stage = 26;
                        else if (stage == 62 || stage == 63 || stage == 65)
                            stage = 64;
                        posX -= 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 3 || stage == 6 || stage == 10 || stage == 12)
                            stage = 4;
                        else if (stage == 2 || stage == 7 || stage == 9 || stage == 11)
                            stage = 5;
                        else if (stage == 19 || stage == 22)
                            stage = 21;
                        else if (stage == 24 || stage == 26)
                            stage = 25;
                        else if (stage == 55 || stage == 57)
                            stage = 56;
                        else if (stage == 62 || stage == 64)
                            stage = 63;
                        posX = canvas.Width / 2 - 40;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 2 || stage == 5 || stage == 15 || stage == 32)
                            stage = 9;
                        else if (stage == 4 || stage == 6 || stage == 16 || stage == 33)
                            stage = 10;
                        else if (stage == 13 || stage == 18)
                            stage = 17;
                        else if (stage == 14 || stage == 21 || stage == 22 || stage == 20)
                            stage = 19;
                        else if (stage == 49 || stage == 48)
                            stage = 50;
                        else if (stage == 53 || stage == 54 || stage == 56 || stage == 57)
                            stage = 55;
                        if (stage == 9 || stage == 10 || stage == 19 || stage == 55 || posX >= canvas.Width / 2)
                            posX -= 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 7 || stage == 5 || stage == 13 || stage == 30)
                            stage = 11;
                        else if (stage == 3 || stage == 4 || stage == 14 || stage == 31)
                            stage = 12;
                        else if (stage == 15 || stage == 23 || stage == 25)
                            stage = 24;
                        else if (stage == 16 || stage == 29)
                            stage = 28;
                        else if (stage == 60 || stage == 61 || stage == 63 || stage == 64)
                            stage = 62;
                        else if (stage == 68 || stage == 69)
                            stage = 70;
                        if (stage == 11 || stage == 12 || stage == 24 || stage == 62 || posX >= canvas.Width / 2)
                            posX -= 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 11 || stage == 17 || stage == 30 || stage == 18)
                            stage = 13;
                        else if (stage == 12 || stage == 19 || stage == 31 || stage == 20)
                            stage = 14;
                        else if (stage == 9 || stage == 24 || stage == 23 || stage == 32)
                            stage = 15;
                        else if (stage == 10 || stage == 28 || stage == 29 || stage == 33)
                            stage = 16;
                        else if (stage == 48 || stage == 50)
                            stage = 49;
                        else if (stage == 53 || stage == 55)
                            stage = 54;
                        else if (stage == 60 || stage == 62)
                            stage = 61;
                        else if (stage == 68 || stage == 70)
                            stage = 69;
                        posX = canvas.Width / 2 + 40;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 11 || stage == 13 || stage == 46 || stage == 47)
                            stage = 30;
                        else if (stage == 12 || stage == 14 || stage == 51 || stage == 52)
                            stage = 31;
                        else if (stage == 15 || stage == 24 || stage == 41 || stage == 40)
                            stage = 23;
                        else if (stage == 16 || stage == 28 || stage == 43 || stage == 44)
                            stage = 29;
                        else if (stage == 34 || stage == 36)
                            stage = 35;
                        else if (stage == 37 || stage == 39)
                            stage = 38;
                        else if (stage == 58 || stage == 59 || stage == 61 || stage == 62)
                            stage = 60;
                        else if (stage == 66 || stage == 67 || stage == 69 || stage == 70)
                            stage = 68;
                        if (stage == 30 || stage == 31 || stage == 23 || stage == 29 || stage == 60 || stage == 68 || posX >= canvas.Width / 2 + 90)
                            posX -= 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 13 || stage == 17 || stage == 34)
                            stage = 18;
                        else if (stage == 14 || stage == 19 || stage == 37)
                            stage = 20;
                        else if (stage == 9 || stage == 15 || stage == 58)
                            stage = 32;
                        else if (stage == 10 || stage == 16 || stage == 66)
                            stage = 33;
                        else if (stage == 41)
                            stage = 42;
                        else if (stage == 44)
                            stage = 45;
                        else if (stage == 47 || stage == 49 || stage == 50)
                            stage = 48;
                        else if (stage == 52 || stage == 54 || stage == 55)
                            stage = 53;
                        if (stage == 18 || stage == 20 || stage == 32 || stage == 33 || stage == 48 || stage == 53 || posX >= canvas.Width / 2 + 90)
                            posX -= 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 18 || stage == 35 || stage == 36)
                            stage = 34;
                        else if (stage == 20 || stage == 38 || stage == 39)
                            stage = 37;
                        else if (stage == 23 || stage == 40 || stage == 42)
                            stage = 41;
                        else if (stage == 29 || stage == 43 || stage == 45)
                            stage = 44;
                        else if (stage == 48 || stage == 30 || stage == 46)
                            stage = 47;
                        else if (stage == 31 || stage == 51 || stage == 53)
                            stage = 52;
                        else if (stage == 32 || stage == 59 || stage == 60)
                            stage = 58;
                        else if (stage == 33 || stage == 67 || stage == 68)
                            stage = 66;
                        posX = canvas.Width / 2 + 140;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX <= canvas.Width - 38 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 35 || stage == 34)
                            stage = 36;
                        else if (stage == 37 || stage == 38)
                            stage = 39;
                        else if (stage == 23 || stage == 41)
                            stage = 40;
                        else if (stage == 29 || stage == 44)
                            stage = 43;
                        else if (stage == 30 || stage == 47)
                            stage = 46;
                        else if (stage == 31 || stage == 52)
                            stage = 51;
                        else if (stage == 58 || stage == 60)
                            stage = 59;
                        else if (stage == 66 || stage == 68)
                            stage = 67;
                        if (stage == 36 || stage == 39 || stage == 40 || stage == 43 || stage == 46 || stage == 51 || stage == 59 || stage == 67)
                            posX -= 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    break;
                case Keys.Right:
                case Keys.D:
                    if (posX >= 60 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 70)
                    {
                        stage = 0;
                        posX += 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 0 || stage == 2 || stage == 3)
                            stage = 1;
                        else if (stage == 7)
                            stage = 8;
                        else if (stage == 26)
                            stage = 27;
                        else if (stage == 64)
                            stage = 65;
                        posX = canvas.Width / 2 - 140;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 0 || stage == 1 || stage == 5 || stage == 9)
                            stage = 2;
                        else if (stage == 4 || stage == 10)
                            stage = 6;
                        else if (stage == 19 || stage == 21)
                            stage = 22;
                        else if (stage == 55 || stage == 56)
                            stage = 57;
                        if (stage == 2 || posX >= canvas.Width / 2 - 110)
                            posX += 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 1 || stage == 4 || stage == 12)
                            stage = 3;
                        else if (stage == 5 || stage == 8 || stage == 11)
                            stage = 7;
                        else if (stage == 24 || stage == 25 || stage == 27)
                            stage = 26;
                        else if (stage == 62 || stage == 63 || stage == 65)
                            stage = 64;
                        posX += 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 3 || stage == 6 || stage == 10 || stage == 12)
                            stage = 4;
                        else if (stage == 2 || stage == 7 || stage == 9 || stage == 11)
                            stage = 5;
                        else if (stage == 19 || stage == 22)
                            stage = 21;
                        else if (stage == 24 || stage == 26)
                            stage = 25;
                        else if (stage == 55 || stage == 57)
                            stage = 56;
                        else if (stage == 62 || stage == 64)
                            stage = 63;
                        posX = canvas.Width / 2 - 40;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 2 || stage == 5 || stage == 15 || stage == 32)
                            stage = 9;
                        else if (stage == 4 || stage == 6 || stage == 16 || stage == 33)
                            stage = 10;
                        else if (stage == 13 || stage == 18)
                            stage = 17;
                        else if (stage == 14 || stage == 21 || stage == 22 || stage == 20)
                            stage = 19;
                        else if (stage == 49 || stage == 48)
                            stage = 50;
                        else if (stage == 53 || stage == 54 || stage == 56 || stage == 57)
                            stage = 55;
                        if (stage == 9 || stage == 10 || stage == 19 || stage == 55 || posX >= canvas.Width / 2)
                            posX += 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 7 || stage == 5 || stage == 13 || stage == 30)
                            stage = 11;
                        else if (stage == 3 || stage == 4 || stage == 14 || stage == 31)
                            stage = 12;
                        else if (stage == 15 || stage == 23 || stage == 25)
                            stage = 24;
                        else if (stage == 16 || stage == 29)
                            stage = 28;
                        else if (stage == 60 || stage == 61 || stage == 63 || stage == 64)
                            stage = 62;
                        else if (stage == 68 || stage == 69)
                            stage = 70;
                        if (stage == 11 || stage == 12 || stage == 24 || stage == 62 || posX >= canvas.Width / 2)
                            posX += 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 11 || stage == 17 || stage == 30 || stage == 18)
                            stage = 13;
                        else if (stage == 12 || stage == 19 || stage == 31 || stage == 20)
                            stage = 14;
                        else if (stage == 9 || stage == 24 || stage == 23 || stage == 32)
                            stage = 15;
                        else if (stage == 10 || stage == 28 || stage == 29 || stage == 33)
                            stage = 16;
                        else if (stage == 48 || stage == 50)
                            stage = 49;
                        else if (stage == 53 || stage == 55)
                            stage = 54;
                        else if (stage == 60 || stage == 62)
                            stage = 61;
                        else if (stage == 68 || stage == 70)
                            stage = 69;
                        posX = canvas.Width / 2 + 40;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 11 || stage == 13 || stage == 46 || stage == 47)
                            stage = 30;
                        else if (stage == 12 || stage == 14 || stage == 51 || stage == 52)
                            stage = 31;
                        else if (stage == 15 || stage == 24 || stage == 41 || stage == 40)
                            stage = 23;
                        else if (stage == 16 || stage == 28 || stage == 43 || stage == 44)
                            stage = 29;
                        else if (stage == 34 || stage == 36)
                            stage = 35;
                        else if (stage == 37 || stage == 39)
                            stage = 38;
                        else if (stage == 58 || stage == 59 || stage == 61 || stage == 62)
                            stage = 60;
                        else if (stage == 66 || stage == 67 || stage == 69 || stage == 70)
                            stage = 68;
                        if (stage == 30 || stage == 31 || stage == 23 || stage == 29 || stage == 60 || stage == 68 || posX >= canvas.Width / 2 + 70)
                            posX += 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 140 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 13 || stage == 17 || stage == 34)
                            stage = 18;
                        else if (stage == 14 || stage == 19 || stage == 37)
                            stage = 20;
                        else if (stage == 9 || stage == 15 || stage == 58)
                            stage = 32;
                        else if (stage == 10 || stage == 16 || stage == 66)
                            stage = 33;
                        else if (stage == 41)
                            stage = 42;
                        else if (stage == 44)
                            stage = 45;
                        else if (stage == 47 || stage == 49 || stage == 50)
                            stage = 48;
                        else if (stage == 52 || stage == 54 || stage == 55)
                            stage = 53;
                        if (stage == 18 || stage == 20 || stage == 32 || stage == 33 || stage == 48 || stage == 53 || posX >= canvas.Width / 2 + 70)
                            posX += 5;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 18 || stage == 35 || stage == 36)
                            stage = 34;
                        else if (stage == 20 || stage == 38 || stage == 39)
                            stage = 37;
                        else if (stage == 23 || stage == 40 || stage == 42)
                            stage = 41;
                        else if (stage == 29 || stage == 43 || stage == 45)
                            stage = 44;
                        else if (stage == 48 || stage == 30 || stage == 46)
                            stage = 47;
                        else if (stage == 31 || stage == 51 || stage == 53)
                            stage = 52;
                        else if (stage == 32 || stage == 59 || stage == 60)
                            stage = 58;
                        else if (stage == 33 || stage == 67 || stage == 68)
                            stage = 66;
                        posX = canvas.Width / 2 + 140;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX < canvas.Width - 38 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 35 || stage == 34)
                            stage = 36;
                        else if (stage == 37 || stage == 38)
                            stage = 39;
                        else if (stage == 23 || stage == 41)
                            stage = 40;
                        else if (stage == 29 || stage == 44)
                            stage = 43;
                        else if (stage == 30 || stage == 47)
                            stage = 46;
                        else if (stage == 31 || stage == 52)
                            stage = 51;
                        else if (stage == 58 || stage == 60)
                            stage = 59;
                        else if (stage == 66 || stage == 68)
                            stage = 67;
                        if (stage == 36 || stage == 39 || stage == 40 || stage == 43 || stage == 46 || stage == 51 || stage == 59 || stage == 67)
                            posX += 5;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    checkWin();
                    break;
                case Keys.Up:
                case Keys.W:
                    if (posX >= 60 && posX <= canvas.Width / 2 - 150 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 70)
                    {
                        stage = 0;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 0 || stage == 2 || stage == 3)
                            stage = 1;
                        else if (stage == 7)
                            stage = 8;
                        else if (stage == 26)
                            stage = 27;
                        else if (stage == 64)
                            stage = 65;
                        posX = canvas.Width / 2 - 140;
                        if (stage == 1 || posY <= canvas.Height / 2 + 10)
                            posY -= 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX < canvas.Width / 2 - 40 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 0 || stage == 1 || stage == 5 || stage == 9)
                            stage = 2;
                        else if (stage == 4 || stage == 10)
                            stage = 6;
                        else if (stage == 19 || stage == 21)
                            stage = 22;
                        else if (stage == 55 || stage == 56)
                            stage = 57;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX < canvas.Width / 2 - 40 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 1 || stage == 4 || stage == 12)
                            stage = 3;
                        else if (stage == 5 || stage == 8 || stage == 11)
                            stage = 7;
                        else if (stage == 24 || stage == 25 || stage == 27)
                            stage = 26;
                        else if (stage == 62 || stage == 63 || stage == 65)
                            stage = 64;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 3 || stage == 6 || stage == 10 || stage == 12)
                            stage = 4;
                        else if (stage == 2 || stage == 7 || stage == 9 || stage == 11)
                            stage = 5;
                        else if (stage == 19 || stage == 22)
                            stage = 21;
                        else if (stage == 24 || stage == 26)
                            stage = 25;
                        else if (stage == 55 || stage == 57)
                            stage = 56;
                        else if (stage == 62 || stage == 64)
                            stage = 63;
                        posX = canvas.Width / 2 - 40;
                        if (stage == 4 || stage == 5 || stage == 25 || stage == 63 || posY >= canvas.Height / 2 - 20)
                            if (stage == 4 || stage == 5 || stage == 21 || stage == 56 || posY <= canvas.Height / 2)
                                posY -= 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 40 && posX < canvas.Width / 2 + 40 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 2 || stage == 5 || stage == 15 || stage == 32)
                            stage = 9;
                        else if (stage == 4 || stage == 6 || stage == 16 || stage == 33)
                            stage = 10;
                        else if (stage == 13 || stage == 18)
                            stage = 17;
                        else if (stage == 14 || stage == 21 || stage == 22 || stage == 20)
                            stage = 19;
                        else if (stage == 49 || stage == 48)
                            stage = 50;
                        else if (stage == 53 || stage == 54 || stage == 56 || stage == 57)
                            stage = 55;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 40 && posX < canvas.Width / 2 + 40 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 7 || stage == 5 || stage == 13 || stage == 30)
                            stage = 11;
                        else if (stage == 3 || stage == 4 || stage == 14 || stage == 31)
                            stage = 12;
                        else if (stage == 15 || stage == 23 || stage == 25)
                            stage = 24;
                        else if (stage == 16 || stage == 29)
                            stage = 28;
                        else if (stage == 60 || stage == 61 || stage == 63 || stage == 64)
                            stage = 62;
                        else if (stage == 68 || stage == 69)
                            stage = 70;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 11 || stage == 17 || stage == 30 || stage == 18)
                            stage = 13;
                        else if (stage == 12 || stage == 19 || stage == 31 || stage == 20)
                            stage = 14;
                        else if (stage == 9 || stage == 24 || stage == 23 || stage == 32)
                            stage = 15;
                        else if (stage == 10 || stage == 28 || stage == 29 || stage == 33)
                            stage = 16;
                        else if (stage == 48 || stage == 50)
                            stage = 49;
                        else if (stage == 53 || stage == 55)
                            stage = 54;
                        else if (stage == 60 || stage == 62)
                            stage = 61;
                        else if (stage == 68 || stage == 70)
                            stage = 69;
                        posX = canvas.Width / 2 + 40;
                        if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 49 || stage == 54 || posY <= canvas.Height / 2 + 10)
                            if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 61 || stage == 69 || posY >= canvas.Height / 2 - 20)
                                posY -= 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 40 && posX < canvas.Width / 2 + 140 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 11 || stage == 13 || stage == 46 || stage == 47)
                            stage = 30;
                        else if (stage == 12 || stage == 14 || stage == 51 || stage == 52)
                            stage = 31;
                        else if (stage == 15 || stage == 24 || stage == 41 || stage == 40)
                            stage = 23;
                        else if (stage == 16 || stage == 28 || stage == 43 || stage == 44)
                            stage = 29;
                        else if (stage == 34 || stage == 36)
                            stage = 35;
                        else if (stage == 37 || stage == 39)
                            stage = 38;
                        else if (stage == 58 || stage == 59 || stage == 61 || stage == 62)
                            stage = 60;
                        else if (stage == 66 || stage == 67 || stage == 69 || stage == 70)
                            stage = 68;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 40 && posX < canvas.Width / 2 + 140 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 13 || stage == 17 || stage == 34)
                            stage = 18;
                        else if (stage == 14 || stage == 19 || stage == 37)
                            stage = 20;
                        else if (stage == 9 || stage == 15 || stage == 58)
                            stage = 32;
                        else if (stage == 10 || stage == 16 || stage == 66)
                            stage = 33;
                        else if (stage == 41)
                            stage = 42;
                        else if (stage == 44)
                            stage = 45;
                        else if (stage == 47 || stage == 49 || stage == 50)
                            stage = 48;
                        else if (stage == 52 || stage == 54 || stage == 55)
                            stage = 53;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 60 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 18 || stage == 35 || stage == 36)
                            stage = 34;
                        else if (stage == 20 || stage == 38 || stage == 39)
                            stage = 37;
                        else if (stage == 23 || stage == 40 || stage == 42)
                            stage = 41;
                        else if (stage == 29 || stage == 43 || stage == 45)
                            stage = 44;
                        else if (stage == 48 || stage == 30 || stage == 46)
                            stage = 47;
                        else if (stage == 31 || stage == 51 || stage == 53)
                            stage = 52;
                        else if (stage == 32 || stage == 59 || stage == 60)
                            stage = 58;
                        else if (stage == 33 || stage == 67 || stage == 68)
                            stage = 66;
                        posX = canvas.Width / 2 + 140;
                        if (stage == 34 || stage == 37 || stage == 41 || stage == 44 || stage == 47 || stage == 52 || stage == 58 || stage == 66 || posY <= canvas.Height / 2 + 10)
                            posY -= 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 140 && posX < canvas.Width - 38 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 35 || stage == 34)
                            stage = 36;
                        else if (stage == 37 || stage == 38)
                            stage = 39;
                        else if (stage == 23 || stage == 41)
                            stage = 40;
                        else if (stage == 29 || stage == 44)
                            stage = 43;
                        else if (stage == 30 || stage == 47)
                            stage = 46;
                        else if (stage == 31 || stage == 52)
                            stage = 51;
                        else if (stage == 58 || stage == 60)
                            stage = 59;
                        else if (stage == 66 || stage == 68)
                            stage = 67;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    break;
                case Keys.Down:
                case Keys.S:
                    if (posX >= 60 && posX < canvas.Width / 2 - 150 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 70)
                    {
                        stage = 0;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 150 && posX <= canvas.Width / 2 - 130 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 0 || stage == 2 || stage == 3)
                            stage = 1;
                        else if (stage == 7)
                            stage = 8;
                        else if (stage == 26)
                            stage = 27;
                        else if (stage == 64)
                            stage = 65;
                        posX = canvas.Width / 2 - 140;
                        if (stage == 1 || posY <= canvas.Height / 2 + 10)
                            posY += 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX < canvas.Width / 2 - 40 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 0 || stage == 1 || stage == 5 || stage == 9)
                            stage = 2;
                        else if (stage == 4 || stage == 10)
                            stage = 6;
                        else if (stage == 19 || stage == 21)
                            stage = 22;
                        else if (stage == 55 || stage == 56)
                            stage = 57;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 140 && posX < canvas.Width / 2 - 40 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 1 || stage == 4 || stage == 12)
                            stage = 3;
                        else if (stage == 5 || stage == 8 || stage == 11)
                            stage = 7;
                        else if (stage == 24 || stage == 25 || stage == 27)
                            stage = 26;
                        else if (stage == 62 || stage == 63 || stage == 65)
                            stage = 64;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 50 && posX <= canvas.Width / 2 - 30 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 3 || stage == 6 || stage == 10 || stage == 12)
                            stage = 4;
                        else if (stage == 2 || stage == 7 || stage == 9 || stage == 11)
                            stage = 5;
                        else if (stage == 19 || stage == 22)
                            stage = 21;
                        else if (stage == 24 || stage == 26)
                            stage = 25;
                        else if (stage == 55 || stage == 57)
                            stage = 56;
                        else if (stage == 62 || stage == 64)
                            stage = 63;
                        posX = canvas.Width / 2 - 40;
                        if (stage == 4 || stage == 5 || stage == 25 || stage == 63 || posY >= canvas.Height / 2 - 20)
                            if (stage == 4 || stage == 5 || stage == 21 || stage == 56 || posY <= canvas.Height / 2)
                                posY += 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 40 && posX < canvas.Width / 2 + 40 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 2 || stage == 5 || stage == 15 || stage == 32)
                            stage = 9;
                        else if (stage == 4 || stage == 6 || stage == 16 || stage == 33)
                            stage = 10;
                        else if (stage == 13 || stage == 18)
                            stage = 17;
                        else if (stage == 14 || stage == 21 || stage == 22 || stage == 20)
                            stage = 19;
                        else if (stage == 49 || stage == 48)
                            stage = 50;
                        else if (stage == 53 || stage == 54 || stage == 56 || stage == 57)
                            stage = 55;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 - 40 && posX < canvas.Width / 2 + 40 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 7 || stage == 5 || stage == 13 || stage == 30)
                            stage = 11;
                        else if (stage == 3 || stage == 4 || stage == 14 || stage == 31)
                            stage = 12;
                        else if (stage == 15 || stage == 23 || stage == 25)
                            stage = 24;
                        else if (stage == 16 || stage == 29)
                            stage = 28;
                        else if (stage == 60 || stage == 61 || stage == 63 || stage == 64)
                            stage = 62;
                        else if (stage == 68 || stage == 69)
                            stage = 70;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 30 && posX <= canvas.Width / 2 + 50 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 11 || stage == 17 || stage == 30 || stage == 18)
                            stage = 13;
                        else if (stage == 12 || stage == 19 || stage == 31 || stage == 20)
                            stage = 14;
                        else if (stage == 9 || stage == 24 || stage == 23 || stage == 32)
                            stage = 15;
                        else if (stage == 10 || stage == 28 || stage == 29 || stage == 33)
                            stage = 16;
                        else if (stage == 48 || stage == 50)
                            stage = 49;
                        else if (stage == 53 || stage == 55)
                            stage = 54;
                        else if (stage == 60 || stage == 62)
                            stage = 61;
                        else if (stage == 68 || stage == 70)
                            stage = 69;
                        posX = canvas.Width / 2 + 40;
                        if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 49 || stage == 54 || posY <= canvas.Height / 2 + 10)
                            if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 61 || stage == 69 || posY >= canvas.Height / 2 - 20)
                                posY += 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 40 && posX < canvas.Width / 2 + 140 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 11 || stage == 13 || stage == 46 || stage == 47)
                            stage = 30;
                        else if (stage == 12 || stage == 14 || stage == 51 || stage == 52)
                            stage = 31;
                        else if (stage == 15 || stage == 24 || stage == 41 || stage == 40)
                            stage = 23;
                        else if (stage == 16 || stage == 28 || stage == 43 || stage == 44)
                            stage = 29;
                        else if (stage == 34 || stage == 36)
                            stage = 35;
                        else if (stage == 37 || stage == 39)
                            stage = 38;
                        else if (stage == 58 || stage == 59 || stage == 61 || stage == 62)
                            stage = 60;
                        else if (stage == 66 || stage == 67 || stage == 69 || stage == 70)
                            stage = 68;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 40 && posX < canvas.Width / 2 + 140 && posY >= canvas.Height / 2 + 30 && posY <= canvas.Height / 2 + 60)
                    {
                        if (stage == 13 || stage == 17 || stage == 34)
                            stage = 18;
                        else if (stage == 14 || stage == 19 || stage == 37)
                            stage = 20;
                        else if (stage == 9 || stage == 15 || stage == 58)
                            stage = 32;
                        else if (stage == 10 || stage == 16 || stage == 66)
                            stage = 33;
                        else if (stage == 41)
                            stage = 42;
                        else if (stage == 44)
                            stage = 45;
                        else if (stage == 47 || stage == 49 || stage == 50)
                            stage = 48;
                        else if (stage == 52 || stage == 54 || stage == 55)
                            stage = 53;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 130 && posX <= canvas.Width / 2 + 150 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 + 40)
                    {
                        if (stage == 18 || stage == 35 || stage == 36)
                            stage = 34;
                        else if (stage == 20 || stage == 38 || stage == 39)
                            stage = 37;
                        else if (stage == 23 || stage == 40 || stage == 42)
                            stage = 41;
                        else if (stage == 29 || stage == 43 || stage == 45)
                            stage = 44;
                        else if (stage == 48 || stage == 30 || stage == 46)
                            stage = 47;
                        else if (stage == 31 || stage == 51 || stage == 53)
                            stage = 52;
                        else if (stage == 32 || stage == 59 || stage == 60)
                            stage = 58;
                        else if (stage == 33 || stage == 67 || stage == 68)
                            stage = 66;
                        posX = canvas.Width / 2 + 140;
                        if (stage == 34 || stage == 37 || stage == 41 || stage == 44 || stage == 47 || stage == 52 || stage == 58 || stage == 66 || posY <= canvas.Height / 2 + 10)
                            posY += 5;
                        this.Refresh();
                    }
                    else if (posX >= canvas.Width / 2 + 140 && posX < canvas.Width - 38 && posY >= canvas.Height / 2 - 70 && posY <= canvas.Height / 2 - 30)
                    {
                        if (stage == 35 || stage == 34)
                            stage = 36;
                        else if (stage == 37 || stage == 38)
                            stage = 39;
                        else if (stage == 23 || stage == 41)
                            stage = 40;
                        else if (stage == 29 || stage == 44)
                            stage = 43;
                        else if (stage == 30 || stage == 47)
                            stage = 46;
                        else if (stage == 31 || stage == 52)
                            stage = 51;
                        else if (stage == 58 || stage == 60)
                            stage = 59;
                        else if (stage == 66 || stage == 68)
                            stage = 67;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    break;
                case Keys.Escape:
                    this.levelBackButton_Click(new object(), new EventArgs());
                    break;
                case Keys.Enter:
                case Keys.Tab:
                case Keys.Space:
                    if(tutorialStage==99)
                    {
                        this.tutorialButton_Click(new object(), new EventArgs());
                    }
                    break;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            //draw startpoint
            gr.DrawArc(pConter, 40, canvas.Height / 2 + 30, 40, 40, 0, 360);
            gr.DrawArc(new Pen(this.backColour, 2), 40, canvas.Height / 2 + 30, 40, 40, -30, 60);
            //draw endpoint
            gr.DrawArc(pConter, canvas.Width - 60, canvas.Height / 2 - 70, 40, 40, 0, 360);
            gr.DrawArc(new Pen(this.backColour, 2), canvas.Width - 60, canvas.Height / 2 - 70, 40, 40, 154, 54);
            //draw pipes bottom
            gr.DrawLine(pConter, 76, canvas.Height / 2 + 40, canvas.Width / 2 - 150, canvas.Height / 2 + 40);
            gr.DrawLine(pConter, canvas.Width / 2 - 130, canvas.Height / 2 + 40, canvas.Width / 2 - 50, canvas.Height / 2 + 40);
            gr.DrawLine(pConter, canvas.Width / 2 - 30, canvas.Height / 2 + 40, canvas.Width / 2 + 30, canvas.Height / 2 + 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 50, canvas.Height / 2 + 40, canvas.Width / 2 + 130, canvas.Height / 2 + 40);
            gr.DrawLine(pConter, 76, canvas.Height / 2 + 60, canvas.Width / 2 + 150, canvas.Height / 2 + 60);
            //draw pipes top
            gr.DrawLine(pConter, canvas.Width / 2 - 150, canvas.Height / 2 - 60, canvas.Width - 56, canvas.Height / 2 - 60);
            gr.DrawLine(pConter, canvas.Width / 2 - 130, canvas.Height / 2 - 40, canvas.Width / 2 - 50, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 - 30, canvas.Height / 2 - 40, canvas.Width / 2 + 30, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 50, canvas.Height / 2 - 40, canvas.Width / 2 + 130, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 150, canvas.Height / 2 - 40, canvas.Width - 56, canvas.Height / 2 - 40);
            //draw pipes vertical
            gr.DrawLine(pConter, canvas.Width / 2 - 150, canvas.Height / 2 + 40, canvas.Width / 2 - 150, canvas.Height / 2 - 60);
            gr.DrawLine(pConter, canvas.Width / 2 - 130, canvas.Height / 2 + 40, canvas.Width / 2 - 130, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 - 50, canvas.Height / 2 + 40, canvas.Width / 2 - 50, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 - 30, canvas.Height / 2 + 40, canvas.Width / 2 - 30, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 30, canvas.Height / 2 + 40, canvas.Width / 2 + 30, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 50, canvas.Height / 2 + 40, canvas.Width / 2 + 50, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 130, canvas.Height / 2 + 40, canvas.Width / 2 + 130, canvas.Height / 2 - 40);
            gr.DrawLine(pConter, canvas.Width / 2 + 150, canvas.Height / 2 + 60, canvas.Width / 2 + 150, canvas.Height / 2 - 40);
            //draw circle
            gr.FillEllipse(brush, 40, canvas.Height / 2 + 30, 39, 39);
        }

        bool mouseDown = false;
        int posX;
        int posY;
        int stage = 0;

        private void DrawPath()
        {
            gr.FillEllipse(brush, posX - 20, posY, 39, 39);
            switch(stage)
            {
                case 0:
                    gr.FillRectangle(brush, 60, posY + 10, posX - 60, 19);
                    break;
                case 1:
                    DrawElement1();
                    gr.FillRectangle(brush, canvas.Width / 2 - 150, posY + 10, 19, canvas.Height / 2 + 49 - posY);
                    break;
                case 2:
                    DrawElement1();
                    gr.FillRectangle(brush, canvas.Width / 2 - 140, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 140), 19);
                    break;
                case 3:
                    DrawElement1();
                    DrawElement2();
                    gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 60, posX - 60 - (canvas.Width / 2 - 140 - 60), 19);
                    break;
                case 4:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, 19, posY - (canvas.Height / 2 - 60) + 10);
                    break;
                case 5:
                    DrawElement1();
                    DrawElement4();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, posY + 10, 19, canvas.Height / 2 + 49 - posY);
                    break;
                case 6:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 - 40 - posX, 19);
                    break;
                case 7:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 - 40 - posX, 19);
                    break;
                case 8:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement3();
                    gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 60, 19, posY - canvas.Height / 2 + 70);
                    break;
                case 9:
                    DrawElement1();
                    DrawElement4();
                    gr.FillRectangle(brush, canvas.Width / 2 - 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40), 19);
                    break;
                case 10:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    gr.FillRectangle(brush, canvas.Width / 2 - 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40), 19);
                    break;
                case 11:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 50), 19);
                    break;
                case 12:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 50), 19);
                    break;
                case 13:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 14:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 15:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 16:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 17:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement8();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 40 - posX, 19);
                    break;
                case 18:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement8();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 19:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 40 - posX, 19);
                    break;
                case 20:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 21:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 22:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    DrawElement6();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 - 40 - posX, 19);
                    break;
                case 23:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 24:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 40 - posX, 19);
                    break;
                case 25:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 26:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement7();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 - 40 - posX, 19);
                    break;
                case 27:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement7();
                    DrawElement3();
                    gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 28:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement8();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 40 - posX, 19);
                    break;
                case 29:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement8();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 30:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 31:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 - 60, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 32:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 33:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 + 40, posX - (canvas.Width / 2 - 40) - 80, 19);
                    break;
                case 34:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 35:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 140 - posX, 19);
                    break;
                case 36:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 37:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 38:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 140 - posX, 19);
                    break;
                case 39:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement8();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 40:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 41:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 42:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 150 - posX, 19);
                    break;
                case 43:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 44:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 45:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement8();
                    DrawElement10();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 150 - posX, 19);
                    break;
                case 46:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 47:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 48:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 150 - posX, 19);
                    break;
                case 49:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 50:
                    DrawElement1();
                    DrawElement4();
                    DrawElement5();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 50 - posX, 19);
                    break;
                case 51:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 52:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 53:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 150 - posX, 19);
                    break;
                case 54:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 55:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 + 50 - posX, 19);
                    break;
                case 56:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    DrawElement6();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 57:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement7();
                    DrawElement10();
                    DrawElement11();
                    DrawElement9();
                    DrawElement6();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 + 40, canvas.Width / 2 - 30 - posX, 19);
                    break;
                case 58:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 59:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 60:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 140 - posX, 19);
                    break;
                case 61:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 62:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 40 - posX, 19);
                    break;
                case 63:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    DrawElement7();
                    gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 64:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    DrawElement7();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 - 50 - posX, 19);
                    break;
                case 65:
                    DrawElement1();
                    DrawElement4();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    DrawElement7();
                    DrawElement3();
                    gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 66:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement9();
                    gr.FillRectangle(brush, canvas.Width / 2 + 130, posY + 10, 19, canvas.Height / 2 + 50 - posY);
                    break;
                case 67:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, canvas.Width / 2 + 140, canvas.Height / 2 - 60, posX - (canvas.Width / 2 + 140), 19);
                    break;
                case 68:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 140 - posX, 19);
                    break;
                case 69:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 19, posY - 131);
                    break;
                case 70:
                    DrawElement1();
                    DrawElement2();
                    DrawElement3();
                    DrawElement5();
                    DrawElement6();
                    DrawElement9();
                    DrawElement11();
                    DrawElement10();
                    gr.FillRectangle(brush, posX, canvas.Height / 2 - 60, canvas.Width / 2 + 40 - posX, 19);
                    break;
                default:
                    break;
            }
        }

        private void DrawElement1()
        {
            gr.FillRectangle(brush, 60, canvas.Height / 2 + 40, canvas.Width / 2 - 200, 19);
        }//bottom start
        private void DrawElement2()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 50, 19, 109);
        }//first vertical
        private void DrawElement3()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 150, canvas.Height / 2 - 60, (canvas.Width / 2 - 40) - 60 - (canvas.Width / 2 - 140 - 60), 19);
        }//top first
        private void DrawElement4()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 140, canvas.Height / 2 + 40, 100, 19);
        }//bottom first
        private void DrawElement5()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, 19, 119);
        }//second vertical
        private void DrawElement6()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 40, canvas.Height / 2 + 40, 80, 19);
        }//bottom second
        private void DrawElement7()
        {
            gr.FillRectangle(brush, canvas.Width / 2 - 50, canvas.Height / 2 - 60, 90, 19);
        }//top second
        private void DrawElement8()
        {
            gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 19, 119);
        }//third vertical
        private void DrawElement9()
        {
            gr.FillRectangle(brush, canvas.Width / 2 + 40, canvas.Height / 2 + 40, 110, 19);
        }//bottom third
        private void DrawElement10()
        {
            gr.FillRectangle(brush, canvas.Width / 2 + 30, canvas.Height / 2 - 60, 120, 19);
        }//top third
        private void DrawElement11()
        {
            gr.FillRectangle(brush, canvas.Width / 2 + 130, canvas.Height / 2 - 60, 19, 100);
        }//fourth vertical

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
                if (e.Location.X > posX - 20 && e.Location.X < posX + 20 && e.Location.Y > posY && e.Location.Y < posY + 40)
                {
                    if (e.Location.X >= 60 && e.Location.X < canvas.Width / 2 - 150 && e.Location.Y >= canvas.Height / 2 + 30 && e.Location.Y <= canvas.Height / 2 + 70)
                    {
                        stage = 0;
                        posX = e.Location.X;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 150 && e.Location.X <= canvas.Width / 2 - 130 && e.Location.Y >= canvas.Height / 2 - 60 && e.Location.Y <= canvas.Height / 2 + 40)
                    {
                        if (stage == 0 || stage == 2 || stage == 3)
                            stage = 1;
                        else if (stage == 7)
                            stage = 8;
                        else if (stage == 26)
                            stage = 27;
                        else if (stage == 64)
                            stage = 65;
                        posX = canvas.Width / 2 - 140;
                        if (stage == 1 || e.Location.Y <= canvas.Height / 2 + 10)
                            posY = e.Location.Y - 10;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 140 && e.Location.X < canvas.Width / 2 - 40 && e.Location.Y >= canvas.Height / 2 + 30 && e.Location.Y <= canvas.Height / 2 + 60)
                    {
                        if (stage == 0 || stage == 1 || stage == 5 || stage == 9)
                            stage = 2;
                        else if (stage == 4 || stage == 10)
                            stage = 6;
                        else if (stage == 19 || stage == 21)
                            stage = 22;
                        else if (stage == 55 || stage == 56)
                            stage = 57;
                        if (stage == 2 || e.Location.X >= canvas.Width / 2 - 110)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 140 && e.Location.X < canvas.Width / 2 - 40 && e.Location.Y >= canvas.Height / 2 - 70 && e.Location.Y <= canvas.Height / 2 - 30)
                    {
                        if (stage == 1 || stage == 4 || stage == 12)
                            stage = 3;
                        else if (stage == 5 || stage == 8 || stage == 11)
                            stage = 7;
                        else if (stage == 24 || stage == 25 || stage == 27)
                            stage = 26;
                        else if (stage == 62 || stage == 63 || stage == 65)
                            stage = 64;
                        posX = e.Location.X;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 50 && e.Location.X <= canvas.Width / 2 - 30 && e.Location.Y >= canvas.Height / 2 - 60 && e.Location.Y <= canvas.Height / 2 + 40)
                    {
                        if (stage == 3 || stage == 6 || stage == 10 || stage == 12)
                            stage = 4;
                        else if (stage == 2 || stage == 7 || stage == 9 || stage == 11)
                            stage = 5;
                        else if (stage == 19 || stage == 22)
                            stage = 21;
                        else if (stage == 24 || stage == 26)
                            stage = 25;
                        else if (stage == 55 || stage == 57)
                            stage = 56;
                        else if (stage == 62 || stage == 64)
                            stage = 63;
                        posX = canvas.Width / 2 - 40;
                        if (stage == 4 || stage == 5 || stage == 25 || stage == 63 || e.Location.Y >= canvas.Height / 2 - 20)
                            if (stage == 4 || stage == 5 || stage == 21 || stage == 56 || e.Location.Y <= canvas.Height / 2)
                                posY = e.Location.Y - 10;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 40 && e.Location.X < canvas.Width / 2 + 40 && e.Location.Y >= canvas.Height / 2 + 30 && e.Location.Y <= canvas.Height / 2 + 60)
                    {
                        if (stage == 2 || stage == 5 || stage == 15 || stage == 32)
                            stage = 9;
                        else if (stage == 4 || stage == 6 || stage == 16 || stage == 33)
                            stage = 10;
                        else if (stage == 13 || stage == 18)
                            stage = 17;
                        else if (stage == 14 || stage == 21 || stage == 22 || stage == 20)
                            stage = 19;
                        else if (stage == 49 || stage == 48)
                            stage = 50;
                        else if (stage == 53 || stage == 54 || stage == 56 || stage == 57)
                            stage = 55;
                        if (stage == 9 || stage == 10 || stage == 19 || stage == 55 || e.Location.X >= canvas.Width / 2 - 10)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 - 40 && e.Location.X < canvas.Width / 2 + 40 && e.Location.Y >= canvas.Height / 2 - 70 && e.Location.Y <= canvas.Height / 2 - 30)
                    {
                        if (stage == 7 || stage == 5 || stage == 13 || stage == 30)
                            stage = 11;
                        else if (stage == 3 || stage == 4 || stage == 14 || stage == 31)
                            stage = 12;
                        else if (stage == 15 || stage == 23 || stage == 25)
                            stage = 24;
                        else if (stage == 16 || stage == 29)
                            stage = 28;
                        else if (stage == 60 || stage == 61 || stage == 63 || stage == 64)
                            stage = 62;
                        else if (stage == 68 || stage == 69)
                            stage = 70;
                        if (stage == 11 || stage == 12 || stage == 24 || stage == 62 || e.Location.X >= canvas.Width / 2 - 10)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 + 30 && e.Location.X <= canvas.Width / 2 + 50 && e.Location.Y >= canvas.Height / 2 - 60 && e.Location.Y <= canvas.Height / 2 + 40)
                    {
                        if (stage == 11 || stage == 17 || stage == 30 || stage == 18)
                            stage = 13;
                        else if (stage == 12 || stage == 19 || stage == 31 || stage == 20)
                            stage = 14;
                        else if (stage == 9 || stage == 24 || stage == 23 || stage == 32)
                            stage = 15;
                        else if (stage == 10 || stage == 28 || stage == 29 || stage == 33)
                            stage = 16;
                        else if (stage == 48 || stage == 50)
                            stage = 49;
                        else if (stage == 53 || stage == 55)
                            stage = 54;
                        else if (stage == 60 || stage == 62)
                            stage = 61;
                        else if (stage == 68 || stage == 70)
                            stage = 69;
                        posX = canvas.Width / 2 + 40;
                        if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 49 || stage == 54 || e.Location.Y <= canvas.Height / 2 + 10)
                            if (stage == 13 || stage == 14 || stage == 15 || stage == 16 || stage == 61 || stage == 69 || e.Location.Y >= canvas.Height / 2 - 20)
                                posY = e.Location.Y - 10;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 + 40 && e.Location.X < canvas.Width / 2 + 140 && e.Location.Y >= canvas.Height / 2 - 70 && e.Location.Y <= canvas.Height / 2 - 30)
                    {
                        if (stage == 11 || stage == 13 || stage == 46 || stage == 47)
                            stage = 30;
                        else if (stage == 12 || stage == 14 || stage == 51 || stage == 52)
                            stage = 31;
                        else if (stage == 15 || stage == 24 || stage == 41 || stage == 40)
                            stage = 23;
                        else if (stage == 16 || stage == 28 || stage == 43 || stage == 44)
                            stage = 29;
                        else if (stage == 34 || stage == 36)
                            stage = 35;
                        else if (stage == 37 || stage == 39)
                            stage = 38;
                        else if (stage == 58 || stage == 59 || stage == 61 || stage == 62)
                            stage = 60;
                        else if (stage == 66 || stage == 67 || stage == 69 || stage == 70)
                            stage = 68;
                        if (stage == 30 || stage == 31 || stage == 23 || stage == 29 || stage == 60 || stage == 68 || e.Location.X >= canvas.Width / 2 + 70)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 - 70;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 + 40 && e.Location.X < canvas.Width / 2 + 140 && e.Location.Y >= canvas.Height / 2 + 30 && e.Location.Y <= canvas.Height / 2 + 60)
                    {
                        if (stage == 13 || stage == 17 || stage == 34)
                            stage = 18;
                        else if (stage == 14 || stage == 19 || stage == 37)
                            stage = 20;
                        else if (stage == 9 || stage == 15 || stage == 58)
                            stage = 32;
                        else if (stage == 10 || stage == 16 || stage == 66)
                            stage = 33;
                        else if (stage == 41)
                            stage = 42;
                        else if (stage == 44)
                            stage = 45;
                        else if (stage == 47 || stage == 49 || stage == 50)
                            stage = 48;
                        else if (stage == 52 || stage == 54 || stage == 55)
                            stage = 53;
                        if (stage == 18 || stage == 20 || stage == 32 || stage == 33 || stage == 48 || stage == 53 || e.Location.X >= canvas.Width / 2 + 70)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 + 30;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 + 130 && e.Location.X <= canvas.Width / 2 + 150 && e.Location.Y >= canvas.Height / 2 - 60 && e.Location.Y <= canvas.Height / 2 + 40)
                    {
                        if (stage == 18 || stage == 35 || stage == 36)
                            stage = 34;
                        else if (stage == 20 || stage == 38 || stage == 39)
                            stage = 37;
                        else if (stage == 23 || stage == 40 || stage == 42)
                            stage = 41;
                        else if (stage == 29 || stage == 43 || stage == 45)
                            stage = 44;
                        else if (stage == 48 || stage == 30 || stage == 46)
                            stage = 47;
                        else if (stage == 31 || stage == 51 || stage == 53)
                            stage = 52;
                        else if (stage == 32 || stage == 59 || stage == 60)
                            stage = 58;
                        else if (stage == 33 || stage == 67 || stage == 68)
                            stage = 66;
                        posX = canvas.Width / 2 + 140;
                        if (stage == 34 || stage == 37 || stage == 41 || stage == 44 || stage == 47 || stage == 52 || stage == 58 || stage == 66 || e.Location.Y <= canvas.Height / 2 + 10)
                            posY = e.Location.Y - 10;
                        this.Refresh();
                    }
                    else if (e.Location.X >= canvas.Width / 2 + 140 && e.Location.X < canvas.Width - 38 && e.Location.Y >= canvas.Height / 2 - 70 && e.Location.Y <= canvas.Height / 2 - 30)
                    {
                        if (stage == 35 || stage == 34)
                            stage = 36;
                        else if (stage == 37 || stage == 38)
                            stage = 39;
                        else if (stage == 23 || stage == 41)
                            stage = 40;
                        else if (stage == 29 || stage == 44)
                            stage = 43;
                        else if (stage == 30 || stage == 47)
                            stage = 46;
                        else if (stage == 31 || stage == 52)
                            stage = 51;
                        else if (stage == 58 || stage == 60)
                            stage = 59;
                        else if (stage == 66 || stage == 68)
                            stage = 67;
                        if (stage == 36 || stage == 39 || stage == 40 || stage == 43 || stage == 46 || stage == 51 || stage == 59 || stage == 67)
                            posX = e.Location.X;
                        posY = canvas.Height / 2 - 70;
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
                if (game.currentLevel < 2)
                    game.currentLevel = 2;
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
