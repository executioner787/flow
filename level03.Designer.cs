namespace flow
{
    partial class level03
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(level03));
            this.canvas = new System.Windows.Forms.PictureBox();
            this.levelBackButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tutorialPanel = new System.Windows.Forms.Panel();
            this.tutorialButton = new System.Windows.Forms.Button();
            this.tutorialText = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LevelName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.tutorialPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(624, 442);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // levelBackButton
            // 
            this.levelBackButton.FlatAppearance.BorderSize = 0;
            this.levelBackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.levelBackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.levelBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.levelBackButton.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9.75F);
            this.levelBackButton.Location = new System.Drawing.Point(5, 5);
            this.levelBackButton.Name = "levelBackButton";
            this.levelBackButton.Size = new System.Drawing.Size(75, 27);
            this.levelBackButton.TabIndex = 1;
            this.levelBackButton.TabStop = false;
            this.levelBackButton.Text = "Back";
            this.levelBackButton.UseVisualStyleBackColor = true;
            this.levelBackButton.Click += new System.EventHandler(this.levelBackButton_Click);
            this.levelBackButton.MouseEnter += new System.EventHandler(this.levelBackButton_MouseEnter);
            this.levelBackButton.MouseLeave += new System.EventHandler(this.levelBackButton_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.tutorialPanel);
            this.panel1.Controls.Add(this.canvas);
            this.panel1.Location = new System.Drawing.Point(477, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 442);
            this.panel1.TabIndex = 2;
            // 
            // tutorialPanel
            // 
            this.tutorialPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tutorialPanel.Controls.Add(this.tutorialButton);
            this.tutorialPanel.Controls.Add(this.tutorialText);
            this.tutorialPanel.Controls.Add(this.pictureBox2);
            this.tutorialPanel.Controls.Add(this.pictureBox1);
            this.tutorialPanel.Location = new System.Drawing.Point(0, 0);
            this.tutorialPanel.Name = "tutorialPanel";
            this.tutorialPanel.Size = new System.Drawing.Size(624, 442);
            this.tutorialPanel.TabIndex = 4;
            // 
            // tutorialButton
            // 
            this.tutorialButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tutorialButton.FlatAppearance.BorderSize = 0;
            this.tutorialButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.tutorialButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.tutorialButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tutorialButton.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9.75F);
            this.tutorialButton.Location = new System.Drawing.Point(115, 258);
            this.tutorialButton.Name = "tutorialButton";
            this.tutorialButton.Size = new System.Drawing.Size(394, 27);
            this.tutorialButton.TabIndex = 4;
            this.tutorialButton.TabStop = false;
            this.tutorialButton.Text = "Next";
            this.tutorialButton.UseVisualStyleBackColor = true;
            this.tutorialButton.Click += new System.EventHandler(this.tutorialButton_Click);
            this.tutorialButton.MouseEnter += new System.EventHandler(this.tutorialButton_MouseEnter);
            this.tutorialButton.MouseLeave += new System.EventHandler(this.tutorialButton_MouseLeave);
            // 
            // tutorialText
            // 
            this.tutorialText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tutorialText.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tutorialText.Location = new System.Drawing.Point(115, 155);
            this.tutorialText.Name = "tutorialText";
            this.tutorialText.Size = new System.Drawing.Size(394, 100);
            this.tutorialText.TabIndex = 2;
            this.tutorialText.Text = "label1";
            this.tutorialText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(115, 290);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(394, 78);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(115, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(394, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LevelName
            // 
            this.LevelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LevelName.AutoSize = true;
            this.LevelName.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9.75F);
            this.LevelName.Location = new System.Drawing.Point(1485, 9);
            this.LevelName.Name = "LevelName";
            this.LevelName.Size = new System.Drawing.Size(82, 19);
            this.LevelName.TabIndex = 3;
            this.LevelName.Text = "LEVEL 03";
            // 
            // level03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1579, 551);
            this.Controls.Add(this.LevelName);
            this.Controls.Add(this.levelBackButton);
            this.Controls.Add(this.panel1);
            this.Name = "level03";
            this.Text = "LEVEL 03";
            this.Load += new System.EventHandler(this.level00redone_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tutorialPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button levelBackButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LevelName;
        private System.Windows.Forms.Panel tutorialPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label tutorialText;
        private System.Windows.Forms.Button tutorialButton;
    }
}