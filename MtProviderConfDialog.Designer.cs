namespace Sdl.Community.ArgosTranslateTradosPlugin
{
	partial class MtProviderConfDialog
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.engineListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.chkPlainTextOnly = new System.Windows.Forms.CheckBox();
            this.btnDeleteSavedKeys = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(212, 358);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 3;
            this.btn_OK.Text = "&OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(302, 358);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(384, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Authentication";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.engineListBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox1.Size = new System.Drawing.Size(366, 121);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Engines";
            // 
            // engineListBox
            // 
            this.engineListBox.FormattingEnabled = true;
            this.engineListBox.Location = new System.Drawing.Point(8, 19);
            this.engineListBox.Name = "engineListBox";
            this.engineListBox.Size = new System.Drawing.Size(346, 94);
            this.engineListBox.TabIndex = 0;
            this.engineListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.engineListBox_ItemCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.apiKeyTextBox);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Location = new System.Drawing.Point(5, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.groupBox3.Size = new System.Drawing.Size(366, 55);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Insert here your API key";
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(8, 20);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            this.apiKeyTextBox.Size = new System.Drawing.Size(305, 20);
            this.apiKeyTextBox.TabIndex = 26;
            this.apiKeyTextBox.TextChanged += new System.EventHandler(this.apiKeyTextBox_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(319, 23);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(33, 15);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Help";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 130);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 222);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBoxAuth);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(this.chkPlainTextOnly);
            this.groupBoxAuth.Controls.Add(this.btnDeleteSavedKeys);
            this.groupBoxAuth.Location = new System.Drawing.Point(6, 6);
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.Size = new System.Drawing.Size(365, 180);
            this.groupBoxAuth.TabIndex = 6;
            this.groupBoxAuth.TabStop = false;
            this.groupBoxAuth.Text = "Options";
            // 
            // chkPlainTextOnly
            // 
            this.chkPlainTextOnly.AutoSize = true;
            this.chkPlainTextOnly.Location = new System.Drawing.Point(8, 21);
            this.chkPlainTextOnly.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkPlainTextOnly.Name = "chkPlainTextOnly";
            this.chkPlainTextOnly.Size = new System.Drawing.Size(183, 19);
            this.chkPlainTextOnly.TabIndex = 24;
            this.chkPlainTextOnly.Text = "Send plain text only (no tags)";
            this.chkPlainTextOnly.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSavedKeys
            // 
            this.btnDeleteSavedKeys.AutoSize = true;
            this.btnDeleteSavedKeys.Enabled = false;
            this.btnDeleteSavedKeys.Location = new System.Drawing.Point(5, 260);
            this.btnDeleteSavedKeys.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDeleteSavedKeys.Name = "btnDeleteSavedKeys";
            this.btnDeleteSavedKeys.Size = new System.Drawing.Size(145, 25);
            this.btnDeleteSavedKeys.TabIndex = 16;
            this.btnDeleteSavedKeys.Text = "Delete saved keys";
            this.btnDeleteSavedKeys.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Sdl.Community.ArgosTranslateProvider.PluginResources.banner;
            this.pictureBox1.InitialImage = global::Sdl.Community.ArgosTranslateProvider.PluginResources.argos_small;
            this.pictureBox1.Location = new System.Drawing.Point(5, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(388, 108);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // MtProviderConfDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 397);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MtProviderConfDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox apiKeyTextBox;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBoxAuth;
		private System.Windows.Forms.CheckBox chkPlainTextOnly;
		private System.Windows.Forms.Button btnDeleteSavedKeys;
		private System.Windows.Forms.CheckedListBox engineListBox;
	}
}