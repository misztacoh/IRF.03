namespace WEEK08.Patterns
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.createTimer = new System.Windows.Forms.Timer(this.components);
            this.conveyorTimer = new System.Windows.Forms.Timer(this.components);
            this.lblNext = new System.Windows.Forms.Label();
            this.carBtn = new System.Windows.Forms.Button();
            this.ballBtn = new System.Windows.Forms.Button();
            this.colorBtn = new System.Windows.Forms.Button();
            this.presentBtn = new System.Windows.Forms.Button();
            this.cBoxBtn = new System.Windows.Forms.Button();
            this.cRibbonBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Location = new System.Drawing.Point(12, 100);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(776, 338);
            this.mainPanel.TabIndex = 0;
            // 
            // createTimer
            // 
            this.createTimer.Enabled = true;
            this.createTimer.Interval = 3000;
            this.createTimer.Tick += new System.EventHandler(this.createTimer_Tick);
            // 
            // conveyorTimer
            // 
            this.conveyorTimer.Enabled = true;
            this.conveyorTimer.Interval = 10;
            this.conveyorTimer.Tick += new System.EventHandler(this.conveyorTimer_Tick);
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Location = new System.Drawing.Point(12, 9);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(68, 13);
            this.lblNext.TabIndex = 0;
            this.lblNext.Text = "Coming next:";
            // 
            // carBtn
            // 
            this.carBtn.Location = new System.Drawing.Point(86, 12);
            this.carBtn.Name = "carBtn";
            this.carBtn.Size = new System.Drawing.Size(75, 23);
            this.carBtn.TabIndex = 1;
            this.carBtn.Text = "Car";
            this.carBtn.UseVisualStyleBackColor = true;
            this.carBtn.Click += new System.EventHandler(this.carBtn_Click);
            // 
            // ballBtn
            // 
            this.ballBtn.Location = new System.Drawing.Point(167, 12);
            this.ballBtn.Name = "ballBtn";
            this.ballBtn.Size = new System.Drawing.Size(75, 23);
            this.ballBtn.TabIndex = 2;
            this.ballBtn.Text = "Ball";
            this.ballBtn.UseVisualStyleBackColor = true;
            this.ballBtn.Click += new System.EventHandler(this.ballBtn_Click);
            // 
            // colorBtn
            // 
            this.colorBtn.BackColor = System.Drawing.Color.Lime;
            this.colorBtn.Location = new System.Drawing.Point(167, 41);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(75, 23);
            this.colorBtn.TabIndex = 3;
            this.colorBtn.UseVisualStyleBackColor = false;
            this.colorBtn.Click += new System.EventHandler(this.colorBtn_Click);
            // 
            // presentBtn
            // 
            this.presentBtn.Location = new System.Drawing.Point(249, 12);
            this.presentBtn.Name = "presentBtn";
            this.presentBtn.Size = new System.Drawing.Size(75, 23);
            this.presentBtn.TabIndex = 4;
            this.presentBtn.Text = "Present";
            this.presentBtn.UseVisualStyleBackColor = true;
            this.presentBtn.Click += new System.EventHandler(this.presentBtn_Click);
            // 
            // cBoxBtn
            // 
            this.cBoxBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cBoxBtn.Location = new System.Drawing.Point(249, 42);
            this.cBoxBtn.Name = "cBoxBtn";
            this.cBoxBtn.Size = new System.Drawing.Size(75, 23);
            this.cBoxBtn.TabIndex = 5;
            this.cBoxBtn.UseVisualStyleBackColor = false;
            this.cBoxBtn.Click += new System.EventHandler(this.cBoxBtn_Click);
            // 
            // cRibbonBtn
            // 
            this.cRibbonBtn.BackColor = System.Drawing.Color.Blue;
            this.cRibbonBtn.Location = new System.Drawing.Point(249, 71);
            this.cRibbonBtn.Name = "cRibbonBtn";
            this.cRibbonBtn.Size = new System.Drawing.Size(75, 23);
            this.cRibbonBtn.TabIndex = 6;
            this.cRibbonBtn.UseVisualStyleBackColor = false;
            this.cRibbonBtn.Click += new System.EventHandler(this.cRibbonBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cRibbonBtn);
            this.Controls.Add(this.cBoxBtn);
            this.Controls.Add(this.presentBtn);
            this.Controls.Add(this.colorBtn);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ballBtn);
            this.Controls.Add(this.lblNext);
            this.Controls.Add(this.carBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Timer createTimer;
        private System.Windows.Forms.Timer conveyorTimer;
        private System.Windows.Forms.Button ballBtn;
        private System.Windows.Forms.Button carBtn;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Button colorBtn;
        private System.Windows.Forms.Button presentBtn;
        private System.Windows.Forms.Button cBoxBtn;
        private System.Windows.Forms.Button cRibbonBtn;
    }
}

