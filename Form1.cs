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
    public partial class MainForm : Form
    {
        public MainForm()
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

        //
        //      MENU METHODS
        //

        private void menuToSide()
        {
            this.MenuTopLayer.Visible = false;
            this.MenuTopLayer.Location = new Point(6, 36);
            this.MenuTopLayer.Enabled = false;
        }

        private void menuToCenter()
        {
            this.MenuTopLayer.Visible = true;
            this.MenuTopLayer.Location = new Point((this.MainGroupBox1.Width - this.MenuTopLayer.Width) / 2, 36);
            this.MenuTopLayer.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("settings.ini"))
                {
                    //read settings file
                    readSettings();
                }
                else
                {
                    //default settings
                    saveSettings();
                }
                applySettings();
            }
            catch (Exception exception)
            {

            }
            tryLoad();
            DisableTabStop(this);
            this.SettingsGroup1.Visible = false;
            this.LevelSelectGroup.Visible = false;
            Image img = this.pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            this.pictureBox1.Image = img;
            img = this.pictureBox3.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox3.Image = img;
            img = this.pictureBox4.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox4.Image = img;
            img = this.SettingsWindowRight.BackgroundImage;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.SettingsWindowRight.BackgroundImage = img;
            img = this.pictureBox6.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox6.Image = img;
            img = this.pictureBox5.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox5.Image = img;
            this.SettingsWindowLeft.BackgroundImage = this.pictureBox6.Image;
            this.SettingsScreenLeft.BackgroundImage = this.pictureBox6.Image;
            this.SettingsAccentLeft.BackgroundImage = this.pictureBox6.Image;
            img = this.pictureBox7.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox7.Image = img;
            img = this.pictureBox8.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox8.Image = img;
            this.SettingsWindowRight.BackgroundImage = this.pictureBox8.Image;
            this.SettingsScreenRight.BackgroundImage = this.pictureBox8.Image;
            this.SettingsAccentRight.BackgroundImage = this.pictureBox8.Image;
            handleKeyboard();
            menuToCenter();
        }

        private void DisableTabStop(Control item)
        {
            item.TabStop = false;
            foreach (Control subitem in item.Controls)
            {
                DisableTabStop(subitem);
            }
        }

        //Menu keyboard controls
        private int activeElement = -1; //main:         0 - new game, 1 - settings, 2 - quit, 3 - continue, 4 - levels, -1 - none
                                        //settings:     10 - window, 11 - screen, 12 - colour, 13 - preview, 14 - apply, 15 - cancel, -11 - none
                                        //levels:       20 - back, -21 - none, 21+ - levels

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int maxEl = 2;
            int maxLvl = game.currentLevel;
            if (game.gameStarted)
                maxEl = 4;
            if (this.activeElement == -1)
                this.activeElement = 0;
            if (this.activeElement == -11)
                this.activeElement = 13;
            if (this.activeElement == -21)
                this.activeElement = 20;
            if (activeElement < 10)
            {
                switch (keyData)
                {
                    case Keys.S:
                    case Keys.Down:
                        if (this.activeElement == maxEl)
                            this.activeElement = 0;
                        else
                            this.activeElement++;
                        break;
                    case Keys.W:
                    case Keys.Up:
                        if (this.activeElement == 0)
                            this.activeElement = maxEl;
                        else
                            this.activeElement--;
                        break;
                    case Keys.Enter:
                    case Keys.Tab:
                    case Keys.Space:
                        switch (this.activeElement)
                        {
                            case 0:
                                this.NewButton_Click(new object(), new EventArgs());
                                break;
                            case 1:
                                this.SettingsButton_Click(new object(), new EventArgs());
                                break;
                            case 2:
                                this.QuitButton_Click(new object(), new EventArgs());
                                break;
                            case 3:
                                this.ContinueButton_Click(new object(), new EventArgs());
                                break;
                            case 4:
                                this.LevelButton_Click(new object(), new EventArgs());
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (activeElement < 20)
            {
                switch (keyData)
                {
                    case Keys.S:
                    case Keys.Down:
                        if (this.activeElement == 13 || this.activeElement == 14 || this.activeElement == 15)
                            this.activeElement = 10;
                        else
                            this.activeElement++;
                        break;
                    case Keys.W:
                    case Keys.Up:
                        if (this.activeElement == 10)
                            this.activeElement = 13;
                        else
                            this.activeElement--;
                        break;
                    case Keys.A:
                    case Keys.Left:
                        switch (this.activeElement)
                        {
                            case 10:
                                this.SettingsWindowLeft_Click(new object(), new EventArgs());
                                break;
                            case 11:
                                this.SettingsScreenLeft_Click(new object(), new EventArgs());
                                break;
                            case 12:
                                this.SettingsAccentLeft_Click(new object(), new EventArgs());
                                break;
                            case 13:
                                this.activeElement = 14;
                                break;
                            case 14:
                                this.activeElement = 15;
                                break;
                            case 15:
                                this.activeElement = 13;
                                break;
                            default:
                                break;
                        }
                        break;
                    case Keys.D:
                    case Keys.Right:
                        switch (this.activeElement)
                        {
                            case 10:
                                this.SettingsWindowRight_Click(new object(), new EventArgs());
                                break;
                            case 11:
                                this.SettingsScreenRight_Click(new object(), new EventArgs());
                                break;
                            case 12:
                                this.SettingsAccentRight_Click(new object(), new EventArgs());
                                break;
                            case 13:
                                this.activeElement = 15;
                                break;
                            case 14:
                                this.activeElement = 13;
                                break;
                            case 15:
                                this.activeElement = 14;
                                break;
                            default:
                                break;
                        }
                        break;
                    case Keys.Escape:
                        this.SettingsCancel_Click(new object(), new EventArgs());
                        break;
                    case Keys.Enter:
                    case Keys.Tab:
                    case Keys.Space:
                        switch (this.activeElement)
                        {
                            case 13:
                                this.SettingsPreview_Click(new object(), new EventArgs());
                                break;
                            case 14:
                                this.SettingsApply_Click(new object(), new EventArgs());
                                break;
                            case 15:
                                this.SettingsCancel_Click(new object(), new EventArgs());
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (keyData)
                {
                    case Keys.S:
                    case Keys.Down:
                        if (this.activeElement == 21+maxLvl)
                            this.activeElement = 20;
                        else
                            this.activeElement++;
                        break;
                    case Keys.W:
                    case Keys.Up:
                        if (this.activeElement == 20)
                            this.activeElement = 21+maxLvl;
                        else
                            this.activeElement--;
                        break;
                    case Keys.Escape:
                        this.LevelSelectBack_Click(new object(), new EventArgs());
                        break;
                    case Keys.Enter:
                    case Keys.Tab:
                    case Keys.Space:
                        switch (this.activeElement)
                        {
                            case 20:
                                this.LevelSelectBack_Click(new object(), new EventArgs());
                                break;
                            case 21:
                                this.lvl00Button_Click(new object(), new EventArgs());
                                break;
                            case 22:
                                this.lvl01Button_Click(new object(), new EventArgs());
                                break;
                            case 23:
                                this.lvl02Button_Click(new object(), new EventArgs());
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            handleKeyboard();
            //return base.ProcessCmdKey(ref msg, keyData);
            return true;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void handleKeyboard()
        {
            this.ContinueButton.Font = new Font(this.ContinueButton.Font.Name, 9.75f, FontStyle.Regular);
            if (game.gameStarted)
                this.ContinueButton.ForeColor = this.textColour;
            else
                this.ContinueButton.ForeColor = Color.Silver;
            this.LevelButton.Font = new Font(this.LevelButton.Font.Name, 9.75f, FontStyle.Regular);
            if (game.gameStarted)
                this.LevelButton.ForeColor = this.textColour;
            else
                this.LevelButton.ForeColor = Color.Silver;
            this.NewButton.Font = new Font(this.NewButton.Font.Name, 9.75f, FontStyle.Regular);
            this.NewButton.ForeColor = this.textColour;
            this.SettingsButton.Font = new Font(this.SettingsButton.Font.Name, 9.75f, FontStyle.Regular);
            this.SettingsButton.ForeColor = this.textColour;
            this.QuitButton.Font = new Font(this.QuitButton.Font.Name, 9.75f, FontStyle.Regular);
            this.QuitButton.ForeColor = this.textColour;
            this.SettingsWindowLabel.ForeColor = this.textColour;
            this.SettingsScreenLabel.ForeColor = this.textColour;
            this.label1.ForeColor = this.textColour;
            this.SettingsApply.Font = new Font(this.SettingsApply.Font.Name, 9.75f, FontStyle.Regular);
            this.SettingsApply.ForeColor = this.textColour;
            this.SettingsPreview.Font = new Font(this.SettingsPreview.Font.Name, 9.75f, FontStyle.Regular);
            this.SettingsPreview.ForeColor = this.textColour;
            this.SettingsCancel.Font = new Font(this.SettingsCancel.Font.Name, 9.75f, FontStyle.Regular);
            this.SettingsCancel.ForeColor = this.textColour;
            this.lvl00Button.Font = new Font(this.lvl00Button.Font.Name, 9.75f, FontStyle.Regular);
            this.lvl00Button.ForeColor = this.textColour;
            this.lvl01Button.Font = new Font(this.lvl01Button.Font.Name, 9.75f, FontStyle.Regular);
            if(game.currentLevel>0)
                this.lvl01Button.ForeColor = this.textColour;
            else
                this.lvl01Button.ForeColor = Color.Silver;
            this.lvl02Button.Font = new Font(this.lvl02Button.Font.Name, 9.75f, FontStyle.Regular);
            if (game.currentLevel > 1)
                this.lvl02Button.ForeColor = this.textColour;
            else
                this.lvl02Button.ForeColor = Color.Silver;
            this.lvl03Button.Font = new Font(this.lvl03Button.Font.Name, 9.75f, FontStyle.Regular);
            if (game.currentLevel > 2)
                this.lvl03Button.ForeColor = this.textColour;
            else
                this.lvl03Button.ForeColor = Color.Silver;
            this.LevelSelectBack.Font = new Font(this.LevelSelectBack.Font.Name, 9.75f, FontStyle.Regular);
            this.LevelSelectBack.ForeColor = this.textColour;
            switch (this.activeElement)
            {
                case 0:
                    this.NewButton.Font = new Font(this.NewButton.Font.Name, 9.75f, FontStyle.Underline);
                    this.NewButton.ForeColor = this.accentColour;
                    break;
                case 1:
                    this.SettingsButton.Font = new Font(this.SettingsButton.Font.Name, 9.75f, FontStyle.Underline);
                    this.SettingsButton.ForeColor = this.accentColour;
                    break;
                case 2:
                    this.QuitButton.Font = new Font(this.QuitButton.Font.Name, 9.75f, FontStyle.Underline);
                    this.QuitButton.ForeColor = this.accentColour;
                    break;
                case 3:
                    this.ContinueButton.Font = new Font(this.ContinueButton.Font.Name, 9.75f, FontStyle.Underline);
                    this.ContinueButton.ForeColor = this.accentColour;
                    break;
                case 4:
                    this.LevelButton.Font = new Font(this.LevelButton.Font.Name, 9.75f, FontStyle.Underline);
                    this.LevelButton.ForeColor = this.accentColour;
                    break;
                case 10:
                    this.SettingsWindowLabel.ForeColor = this.accentColour;
                    break;
                case 11:
                    this.SettingsScreenLabel.ForeColor = this.accentColour;
                    break;
                case 12:
                    this.label1.ForeColor = this.accentColour;
                    break;
                case 13:
                    this.SettingsPreview.Font = new Font(this.SettingsPreview.Font.Name, 9.75f, FontStyle.Underline);
                    this.SettingsPreview.ForeColor = this.accentColour;
                    break;
                case 14:
                    this.SettingsApply.Font = new Font(this.SettingsApply.Font.Name, 9.75f, FontStyle.Underline);
                    this.SettingsApply.ForeColor = this.accentColour;
                    break;
                case 15:
                    this.SettingsCancel.Font = new Font(this.SettingsCancel.Font.Name, 9.75f, FontStyle.Underline);
                    this.SettingsCancel.ForeColor = this.accentColour;
                    break;
                case 20:
                    this.LevelSelectBack.Font = new Font(this.LevelSelectBack.Font.Name, 9.75f, FontStyle.Underline);
                    this.LevelSelectBack.ForeColor = this.accentColour;
                    break;
                case 21:
                    this.lvl00Button.Font = new Font(this.lvl00Button.Font.Name, 9.75f, FontStyle.Underline);
                    this.lvl00Button.ForeColor = this.accentColour;
                    break;
                case 22:
                    this.lvl01Button.Font = new Font(this.lvl01Button.Font.Name, 9.75f, FontStyle.Underline);
                    this.lvl01Button.ForeColor = this.accentColour;
                    break;
                case 23:
                    this.lvl02Button.Font = new Font(this.lvl02Button.Font.Name, 9.75f, FontStyle.Underline);
                    this.lvl02Button.ForeColor = this.accentColour;
                    break;
                case 24:
                    this.lvl03Button.Font = new Font(this.lvl03Button.Font.Name, 9.75f, FontStyle.Underline);
                    this.lvl03Button.ForeColor = this.accentColour;
                    break;
                default:
                    break;
            }
        }
        //
        //      SETTINGS METHODS
        //

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

        private void saveSettings()
        {
            using (StreamWriter sw = new StreamWriter("settings.ini", false))
            {
                sw.WriteLine("[Screen Settings]");
                sw.WriteLine("windowStyle=" + this.windowStyle);
                sw.WriteLine("screenWidth=" + this.screenWidth);
                sw.WriteLine("screenHeight=" + this.screenHeight);
                sw.WriteLine("colorAccent=" + this.colorIndex);
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            activeElement = -11;
            this.SettingsGroup1.Visible = true;
            menuToSide();
            if (screenDefaultIndex != -1)
            {
                this.screenWidth = screenWidthSelection[screenDefaultIndex];
                this.screenHeight = screenHeightSelection[screenDefaultIndex];
            }
            //get user native screen resolution
            for (int i = 0; i < screenWidthSelection.Length; i++)
            {
                if (screenWidthSelection[i] == screenWidth && screenHeightSelection[i] == screenHeight)
                {
                    screenIndex = i;
                    screenDefaultIndex = i;
                }
                if (screenWidthSelection[i] == Screen.PrimaryScreen.Bounds.Width && screenHeightSelection[i] == Screen.PrimaryScreen.Bounds.Height)
                    screenMaxIndex = i;
            }
            this.UpdateSettingsMenu();
        }

        private void UpdateSettingsMenu()
        {
            switch (this.windowStyle)
            {
                case 0:
                    this.SettingsWindowStyle.Text = "Windowed";
                    break;
                case 1:
                    this.SettingsWindowStyle.Text = "Borderless";
                    break;
                default:
                    this.SettingsWindowStyle.Text = "Fullscreen";
                    break;
            }
            this.SettingsScreenSize.Text = this.screenWidth + "x" + this.screenHeight;
            this.AccentColor.ForeColor = this.accentColour;
            switch (this.colorIndex)
            {
                case 0:
                    this.AccentColor.Text = "Violet";
                    break;
                case 1:
                    this.AccentColor.Text = "Pink";
                    break;
                case 2:
                    this.AccentColor.Text = "Red";
                    break;
                case 3:
                    this.AccentColor.Text = "Orange";
                    break;
                case 4:
                    this.AccentColor.Text = "Yellow";
                    break;
                case 5:
                    this.AccentColor.Text = "Brown";
                    break;
                case 6:
                    this.AccentColor.Text = "Green";
                    break;
                case 7:
                    this.AccentColor.Text = "Cyan";
                    break;
                case 8:
                    this.AccentColor.Text = "Blue";
                    break;
                case 9:
                    this.AccentColor.Text = "White";
                    break;
                default:
                    this.AccentColor.Text = "Dark";
                    break;
            }
        }

        private void SettingsWindowLeft_Click(object sender, EventArgs e)
        {
            if (this.windowStyle == 0)
                this.windowStyle = 2;
            else
                this.windowStyle--;
            UpdateSettingsMenu();
        }

        private void SettingsWindowRight_Click(object sender, EventArgs e)
        {
            if (this.windowStyle == 2)
                this.windowStyle = 0;
            else
                this.windowStyle++;
            UpdateSettingsMenu();
        }

        private int screenIndex = 0;
        private int screenDefaultIndex = -1;
        private int screenMaxIndex = 0;
        private int[] screenWidthSelection = { 640, 800, 960, 1024, 1024, 1152, 1280, 1280, 1280, 1280, 1366, 1400, 1440, 1440, 1600, 1600, 1680, 1856, 1920, 1920, 1920, 2048, 2560, 2560, 3840, 7680 };
        private int[] screenHeightSelection = { 480, 600, 720, 576, 768, 648, 720, 800, 960, 1024, 768, 1050, 900, 1080, 900, 1200, 1050, 1392, 1080, 1200, 1440, 1536, 1440, 1600, 2160, 4320 };
        private int colorIndex = 0; //11 colours
        private Color[] palette = { Color.BlueViolet, Color.HotPink, Color.Crimson, Color.Coral, Color.Gold, Color.Tan, Color.MediumSeaGreen, Color.DarkTurquoise, Color.DeepSkyBlue, Color.SeaShell, Color.DarkGray };
        private void SettingsScreenLeft_Click(object sender, EventArgs e)
        {
            if (screenIndex == 0)
                screenIndex = screenMaxIndex;
            else
                screenIndex--;
            this.screenWidth = screenWidthSelection[screenIndex];
            this.screenHeight = screenHeightSelection[screenIndex];
            UpdateSettingsMenu();
        }

        private void SettingsScreenRight_Click(object sender, EventArgs e)
        {
            if (screenIndex == screenMaxIndex)
                screenIndex = 0;
            else
                screenIndex++;
            this.screenWidth = screenWidthSelection[screenIndex];
            this.screenHeight = screenHeightSelection[screenIndex];
            UpdateSettingsMenu();
        }

        private void SettingsAccentLeft_Click(object sender, EventArgs e)
        {
            if (colorIndex == 0)
                colorIndex = 10;
            else
                colorIndex--;
            this.accentColour = palette[colorIndex];
            UpdateSettingsMenu();
            applySettings();
        }

        private void SettingsAccentRight_Click(object sender, EventArgs e)
        {
            if (colorIndex == 10)
                colorIndex = 0;
            else
                colorIndex++;
            this.accentColour = palette[colorIndex];
            UpdateSettingsMenu();
            applySettings();
        }

        private void SettingsApply_Click(object sender, EventArgs e)
        {
            screenDefaultIndex = screenIndex;
            saveSettings();
            applySettings();
            this.SettingsGroup1.Visible = !this.SettingsGroup1.Visible;
            reloadLevels();
            activeElement = -1;
            menuToCenter();
        }

        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            readSettings();
            applySettings();
            this.SettingsGroup1.Visible = !this.SettingsGroup1.Visible;
            activeElement = -1;
            menuToCenter();
        }

        private void SettingsPreview_Click(object sender, EventArgs e)
        {
            applySettings();
        }

        //
        //      QUIT METHOD
        //

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //
        //      GAME METHODS
        //

        Game game;
        BinaryFormatter formatter = new BinaryFormatter();
        private void LoadSave()
        {
            using (Stream sr = new FileStream("game.save", FileMode.Open, FileAccess.Read))
            {
                this.game = (Game)formatter.Deserialize(sr);
            }
        }

        private void SaveGame()
        {
            using (Stream sr = new FileStream("game.save", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(sr, this.game);
            }
        }

        private void tryLoad()
        {
            try
            {
                if (File.Exists("game.save"))
                {
                    LoadSave();
                }
                else
                {
                    this.game = new Game();
                    using (StreamWriter sw = new StreamWriter("game.save", false))
                    {
                        sw.WriteLine(); //creates savefile
                    }
                    SaveGame();
                }
            }
            catch (Exception exception)
            {

            }
            if (!this.game.gameStarted)
            {
                this.ContinueButton.Click -= ContinueButton_Click;
                this.ContinueButton.MouseEnter -= ContinueButton_MouseEnter;
                this.ContinueButton.MouseLeave -= ContinueButton_MouseLeave;
                this.LevelButton.Click -= LevelButton_Click;
                this.LevelButton.MouseEnter -= LevelButton_MouseEnter;
                this.LevelButton.MouseLeave -= LevelButton_MouseLeave;
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            reloadLevels();
            LoadSave();
            LoadLevel(game.currentLevel);
        }

        private void LevelButton_Click(object sender, EventArgs e)
        {
            reloadLevels();
            activeElement = -21;
            LoadSave();
            this.LevelSelectGroup.Visible = true;
            this.LevelSelectGroup.Location = new Point(130, 36);
            menuToSide();
            if (game.currentLevel < 1)
            {
                this.lvl01Button.Click -= lvl01Button_Click;
                this.lvl01Button.MouseEnter -= lvl01Button_MouseEnter;
                this.lvl01Button.MouseLeave -= lvl01Button_MouseLeave;
            }
            if (game.currentLevel < 2)
            {
                this.lvl02Button.Click -= lvl02Button_Click;
                this.lvl02Button.MouseEnter -= lvl02Button_MouseEnter;
                this.lvl02Button.MouseLeave -= lvl02Button_MouseLeave;
            }
            if (game.currentLevel < 3)
            {
                this.lvl03Button.Click -= lvl03Button_Click;
                this.lvl03Button.MouseEnter -= lvl03Button_MouseEnter;
                this.lvl03Button.MouseLeave -= lvl03Button_MouseLeave;
            }
            if (game.currentLevel < 4)
                this.lvl04Button.ForeColor = Color.Silver;
            if (game.currentLevel < 5)
                this.lvl05Button.ForeColor = Color.Silver;
            if (game.currentLevel < 6)
                this.lvl06Button.ForeColor = Color.Silver;
            if (game.currentLevel < 7)
                this.lvl07Button.ForeColor = Color.Silver;
            if (game.currentLevel < 8)
                this.lvl08Button.ForeColor = Color.Silver;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            reloadLevels();
            LoadLevel(0);
            if (!game.gameStarted)
            {
                game.gameStarted = true;
                this.ContinueButton.ForeColor = this.textColour;
                this.ContinueButton.Click += ContinueButton_Click;
                this.ContinueButton.MouseEnter += ContinueButton_MouseEnter;
                this.ContinueButton.MouseLeave += ContinueButton_MouseLeave;
                this.LevelButton.ForeColor = this.textColour;
                this.LevelButton.Click += LevelButton_Click;
                this.LevelButton.MouseEnter += LevelButton_MouseEnter;
                this.LevelButton.MouseLeave += LevelButton_MouseLeave;
            }
        }

        //level list
        Form[] levels = { };
        private void reloadLevels()
        {
            this.levels = new Form[] { new level00redone(), new level01(), new level02(), new level03() };
        }
        private void LoadLevel(int levelIndex)
        {
            levels[levelIndex].Show();
        }

        private void lvl00Button_Click(object sender, EventArgs e)
        {
            reloadLevels();
            levels[0].Show();
        }

        private void LevelSelectBack_Click(object sender, EventArgs e)
        {
            activeElement = -1;
            this.LevelSelectGroup.Visible = false;
            this.lvl01Button.Click += lvl01Button_Click;
            this.lvl01Button.MouseEnter += lvl01Button_MouseEnter;
            this.lvl01Button.MouseLeave += lvl01Button_MouseLeave;
            this.lvl02Button.Click += lvl02Button_Click;
            this.lvl02Button.MouseEnter += lvl02Button_MouseEnter;
            this.lvl02Button.MouseLeave += lvl02Button_MouseLeave;
            this.lvl03Button.Click += lvl03Button_Click;
            this.lvl03Button.MouseEnter += lvl03Button_MouseEnter;
            this.lvl03Button.MouseLeave += lvl03Button_MouseLeave;
            menuToCenter();
        }

        private void lvl01Button_Click(object sender, EventArgs e)
        {
            reloadLevels();
            levels[1].Show();
        }

        private void lvl02Button_Click(object sender, EventArgs e)
        {
            reloadLevels();
            levels[2].Show();
        }

        private void lvl03Button_Click(object sender, EventArgs e)
        {
            reloadLevels();
            levels[3].Show();
        }

        //
        //      Button Styling
        //

        private void ContinueButton_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 3;
            handleKeyboard();
            this.ContinueButton.Cursor = Cursors.Hand;
        }

        private void ContinueButton_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -1;
            handleKeyboard();
            this.ContinueButton.Cursor = Cursors.Default;
        }

        private void LevelButton_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 4;
            handleKeyboard();
            this.LevelButton.Cursor = Cursors.Hand;
        }

        private void LevelButton_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -1;
            handleKeyboard();
            this.LevelButton.Cursor = Cursors.Default;
        }

        private void NewButton_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 0;
            handleKeyboard();
            this.NewButton.Cursor = Cursors.Hand;
        }

        private void NewButton_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -1;
            handleKeyboard();
            this.NewButton.Cursor = Cursors.Default;
        }

        private void SettingsButton_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 1;
            handleKeyboard();
            this.SettingsButton.Cursor = Cursors.Hand;
        }

        private void SettingsButton_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -1;
            handleKeyboard();
            this.SettingsButton.Cursor = Cursors.Default;
        }

        private void QuitButton_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 2;
            handleKeyboard();
            this.QuitButton.Cursor = Cursors.Hand;
        }

        private void QuitButton_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -1;
            handleKeyboard();
            this.QuitButton.Cursor = Cursors.Default;
        }

        private void SettingsApply_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 14;
            handleKeyboard();
            this.SettingsApply.Cursor = Cursors.Hand;
        }

        private void SettingsApply_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -11;
            handleKeyboard();
            this.SettingsApply.Cursor = Cursors.Default;
        }

        private void SettingsPreview_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 13;
            handleKeyboard();
            this.SettingsPreview.Cursor = Cursors.Hand;
        }

        private void SettingsPreview_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -11;
            handleKeyboard();
            this.SettingsPreview.Cursor = Cursors.Default;
        }

        private void SettingsCancel_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 15;
            handleKeyboard();
            this.SettingsCancel.Cursor = Cursors.Hand;
        }

        private void SettingsCancel_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -11;
            handleKeyboard();
            this.SettingsCancel.Cursor = Cursors.Default;
        }

        private void lvl00Button_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 21;
            handleKeyboard();
            this.lvl00Button.Cursor = Cursors.Hand;
        }

        private void lvl00Button_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -21;
            handleKeyboard();
            this.lvl00Button.Cursor = Cursors.Default;
        }

        private void lvl01Button_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 22;
            handleKeyboard();
            this.lvl01Button.Cursor = Cursors.Hand;
        }

        private void lvl01Button_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -21;
            handleKeyboard();
            this.lvl01Button.Cursor = Cursors.Default;
        }

        private void lvl02Button_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 23;
            handleKeyboard();
            this.lvl02Button.Cursor = Cursors.Hand;
        }

        private void lvl02Button_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -21;
            handleKeyboard();
            this.lvl02Button.Cursor = Cursors.Default;
        }

        private void lvl03Button_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 24;
            handleKeyboard();
            this.lvl03Button.Cursor = Cursors.Hand;
        }

        private void lvl03Button_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -21;
            handleKeyboard();
            this.lvl03Button.Cursor = Cursors.Default;
        }

        private void LevelSelectBack_MouseEnter(object sender, EventArgs e)
        {
            activeElement = 20;
            handleKeyboard();
            this.LevelSelectBack.Cursor = Cursors.Hand;
        }

        private void LevelSelectBack_MouseLeave(object sender, EventArgs e)
        {
            activeElement = -21;
            handleKeyboard();
            this.LevelSelectBack.Cursor = Cursors.Default;
        }

        private void SettingsWindowLeft_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsWindowLeft.BackgroundImage = this.pictureBox5.Image;
            this.SettingsWindowLeft.Cursor = Cursors.Hand;
        }

        private void SettingsWindowLeft_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsWindowLeft.BackgroundImage = this.pictureBox6.Image;
            this.SettingsWindowLeft.Cursor = Cursors.Default;
        }

        private void SettingsWindowRight_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsWindowRight.BackgroundImage = this.pictureBox7.Image;
            this.SettingsWindowRight.Cursor = Cursors.Hand;
        }

        private void SettingsWindowRight_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsWindowRight.BackgroundImage = this.pictureBox8.Image;
            this.SettingsWindowRight.Cursor = Cursors.Default;
        }

        private void SettingsScreenLeft_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsScreenLeft.BackgroundImage = this.pictureBox5.Image;
            this.SettingsScreenLeft.Cursor = Cursors.Hand;
        }

        private void SettingsScreenLeft_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsScreenLeft.BackgroundImage = this.pictureBox6.Image;
            this.SettingsScreenLeft.Cursor = Cursors.Default;
        }

        private void SettingsScreenRight_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsScreenRight.BackgroundImage = this.pictureBox7.Image;
            this.SettingsScreenRight.Cursor = Cursors.Hand;
        }

        private void SettingsScreenRight_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsScreenRight.BackgroundImage = this.pictureBox8.Image;
            this.SettingsScreenRight.Cursor = Cursors.Default;
        }

        private void SettingsAccentLeft_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsAccentLeft.BackgroundImage = this.pictureBox5.Image;
            this.SettingsAccentLeft.Cursor = Cursors.Hand;
        }

        private void SettingsAccentLeft_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsAccentLeft.BackgroundImage = this.pictureBox6.Image;
            this.SettingsAccentLeft.Cursor = Cursors.Default;
        }

        private void SettingsAccentRight_MouseEnter(object sender, EventArgs e)
        {
            this.SettingsAccentRight.BackgroundImage = this.pictureBox7.Image;
            this.SettingsAccentRight.Cursor = Cursors.Hand;
        }

        private void SettingsAccentRight_MouseLeave(object sender, EventArgs e)
        {
            this.SettingsAccentRight.BackgroundImage = this.pictureBox8.Image;
            this.SettingsAccentRight.Cursor = Cursors.Default;
        }
    }
    [Serializable]
    class Game
    {
        public bool gameStarted;
        public int currentLevel;
        public Game()
        {
            gameStarted = false;
            currentLevel = 0;
        }
    }
}
