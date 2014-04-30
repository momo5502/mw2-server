namespace MW2_Server
{
    partial class Pannel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pannel));
            this.label1 = new System.Windows.Forms.Label();
            this.hostname = new System.Windows.Forms.TextBox();
            this.clients = new System.Windows.Forms.TextBox();
            this.fs_game = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.maxclients = new System.Windows.Forms.TextBox();
            this.mapname = new System.Windows.Forms.TextBox();
            this.gametype = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.spam = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hostname:";
            // 
            // hostname
            // 
            this.hostname.Location = new System.Drawing.Point(78, 13);
            this.hostname.Name = "hostname";
            this.hostname.Size = new System.Drawing.Size(257, 20);
            this.hostname.TabIndex = 1;
            // 
            // clients
            // 
            this.clients.Location = new System.Drawing.Point(78, 117);
            this.clients.Name = "clients";
            this.clients.Size = new System.Drawing.Size(257, 20);
            this.clients.TabIndex = 2;
            // 
            // fs_game
            // 
            this.fs_game.Location = new System.Drawing.Point(78, 143);
            this.fs_game.Name = "fs_game";
            this.fs_game.Size = new System.Drawing.Size(257, 20);
            this.fs_game.TabIndex = 3;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(78, 169);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(257, 20);
            this.port.TabIndex = 4;
            // 
            // maxclients
            // 
            this.maxclients.Location = new System.Drawing.Point(78, 91);
            this.maxclients.Name = "maxclients";
            this.maxclients.Size = new System.Drawing.Size(257, 20);
            this.maxclients.TabIndex = 5;
            // 
            // mapname
            // 
            this.mapname.Location = new System.Drawing.Point(78, 65);
            this.mapname.Name = "mapname";
            this.mapname.Size = new System.Drawing.Size(257, 20);
            this.mapname.TabIndex = 6;
            // 
            // gametype
            // 
            this.gametype.Location = new System.Drawing.Point(78, 39);
            this.gametype.Name = "gametype";
            this.gametype.Size = new System.Drawing.Size(257, 20);
            this.gametype.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Gametype:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Mapname:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Maxclients:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Clients:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Mod:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Port:";
            // 
            // error
            // 
            this.error.Location = new System.Drawing.Point(78, 195);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(257, 20);
            this.error.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Error:";
            // 
            // spam
            // 
            this.spam.Location = new System.Drawing.Point(15, 240);
            this.spam.Name = "spam";
            this.spam.Size = new System.Drawing.Size(320, 151);
            this.spam.TabIndex = 16;
            this.spam.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Spam:";
            // 
            // Pannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 403);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.spam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.error);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gametype);
            this.Controls.Add(this.mapname);
            this.Controls.Add(this.maxclients);
            this.Controls.Add(this.port);
            this.Controls.Add(this.fs_game);
            this.Controls.Add(this.clients);
            this.Controls.Add(this.hostname);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Pannel";
            this.Text = "Pannel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pannel_FormClosing);
            this.Load += new System.EventHandler(this.Pannel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hostname;
        private System.Windows.Forms.TextBox clients;
        private System.Windows.Forms.TextBox fs_game;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox maxclients;
        private System.Windows.Forms.TextBox mapname;
        private System.Windows.Forms.TextBox gametype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox error;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox spam;
        private System.Windows.Forms.Label label9;
    }
}