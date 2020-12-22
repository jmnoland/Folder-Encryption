namespace FolderEncryption
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
            this.Folder = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.encryptedFolderList = new System.Windows.Forms.ListBox();
            this.encryptFileList = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.decryptFolderList = new System.Windows.Forms.ListBox();
            this.fileInfoKeyName = new System.Windows.Forms.Label();
            this.fileInfoKeyLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.selectDecryptFolder = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.decryptFileList = new System.Windows.Forms.ListBox();
            this.addPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.pathTextLabel = new System.Windows.Forms.Label();
            this.keyName = new System.Windows.Forms.TextBox();
            this.keyNameLabel = new System.Windows.Forms.Label();
            this.passwordConfirm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addNew = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.decryptPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Folder.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.addPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // Folder
            // 
            this.Folder.Controls.Add(this.tabPage1);
            this.Folder.Controls.Add(this.tabPage2);
            this.Folder.Controls.Add(this.addPage);
            this.Folder.Location = new System.Drawing.Point(0, 0);
            this.Folder.Name = "Folder";
            this.Folder.SelectedIndex = 0;
            this.Folder.Size = new System.Drawing.Size(800, 449);
            this.Folder.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Encrypted files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.encryptedFolderList);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.encryptFileList);
            this.splitContainer1.Size = new System.Drawing.Size(786, 417);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 0;
            // 
            // encryptedFolderList
            // 
            this.encryptedFolderList.FormattingEnabled = true;
            this.encryptedFolderList.Location = new System.Drawing.Point(0, 3);
            this.encryptedFolderList.Name = "encryptedFolderList";
            this.encryptedFolderList.Size = new System.Drawing.Size(259, 407);
            this.encryptedFolderList.TabIndex = 0;
            this.encryptedFolderList.SelectedIndexChanged += new System.EventHandler(this.encryptedFolderList_SelectedIndexChanged);
            // 
            // encryptFileList
            // 
            this.encryptFileList.FullRowSelect = true;
            this.encryptFileList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.encryptFileList.HideSelection = false;
            this.encryptFileList.Location = new System.Drawing.Point(3, 3);
            this.encryptFileList.Name = "encryptFileList";
            this.encryptFileList.Size = new System.Drawing.Size(512, 407);
            this.encryptFileList.TabIndex = 0;
            this.encryptFileList.UseCompatibleStateImageBehavior = false;
            this.encryptFileList.View = System.Windows.Forms.View.Details;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 423);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Decrypt files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.decryptFolderList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.decryptPassword);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.fileInfoKeyName);
            this.splitContainer2.Panel2.Controls.Add(this.fileInfoKeyLabel);
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.selectDecryptFolder);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.decryptBtn);
            this.splitContainer2.Panel2.Controls.Add(this.decryptFileList);
            this.splitContainer2.Size = new System.Drawing.Size(786, 417);
            this.splitContainer2.SplitterDistance = 262;
            this.splitContainer2.TabIndex = 0;
            // 
            // decryptFolderList
            // 
            this.decryptFolderList.FormattingEnabled = true;
            this.decryptFolderList.Location = new System.Drawing.Point(0, 3);
            this.decryptFolderList.Name = "decryptFolderList";
            this.decryptFolderList.Size = new System.Drawing.Size(259, 407);
            this.decryptFolderList.TabIndex = 0;
            this.decryptFolderList.SelectedIndexChanged += new System.EventHandler(this.decryptFolderList_SelectedIndexChanged);
            // 
            // fileInfoKeyName
            // 
            this.fileInfoKeyName.AutoSize = true;
            this.fileInfoKeyName.Location = new System.Drawing.Point(62, 235);
            this.fileInfoKeyName.Name = "fileInfoKeyName";
            this.fileInfoKeyName.Size = new System.Drawing.Size(0, 13);
            this.fileInfoKeyName.TabIndex = 15;
            // 
            // fileInfoKeyLabel
            // 
            this.fileInfoKeyLabel.AutoSize = true;
            this.fileInfoKeyLabel.Location = new System.Drawing.Point(3, 235);
            this.fileInfoKeyLabel.Name = "fileInfoKeyLabel";
            this.fileInfoKeyLabel.Size = new System.Drawing.Size(57, 13);
            this.fileInfoKeyLabel.TabIndex = 14;
            this.fileInfoKeyLabel.Text = "Key name:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Select";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.selectDecryptFolderBtn);
            // 
            // selectDecryptFolder
            // 
            this.selectDecryptFolder.AutoSize = true;
            this.selectDecryptFolder.Location = new System.Drawing.Point(10, 330);
            this.selectDecryptFolder.Name = "selectDecryptFolder";
            this.selectDecryptFolder.Size = new System.Drawing.Size(0, 13);
            this.selectDecryptFolder.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Select output folder";
            // 
            // decryptBtn
            // 
            this.decryptBtn.Location = new System.Drawing.Point(7, 387);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(105, 23);
            this.decryptBtn.TabIndex = 1;
            this.decryptBtn.Text = "Decrypt selected";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click);
            // 
            // decryptFileList
            // 
            this.decryptFileList.FormattingEnabled = true;
            this.decryptFileList.Location = new System.Drawing.Point(3, 3);
            this.decryptFileList.Name = "decryptFileList";
            this.decryptFileList.Size = new System.Drawing.Size(512, 225);
            this.decryptFileList.TabIndex = 0;
            // 
            // addPage
            // 
            this.addPage.Controls.Add(this.button1);
            this.addPage.Controls.Add(this.pathTextLabel);
            this.addPage.Controls.Add(this.keyName);
            this.addPage.Controls.Add(this.keyNameLabel);
            this.addPage.Controls.Add(this.passwordConfirm);
            this.addPage.Controls.Add(this.label3);
            this.addPage.Controls.Add(this.label2);
            this.addPage.Controls.Add(this.password);
            this.addPage.Controls.Add(this.label1);
            this.addPage.Controls.Add(this.addNew);
            this.addPage.Location = new System.Drawing.Point(4, 22);
            this.addPage.Name = "addPage";
            this.addPage.Padding = new System.Windows.Forms.Padding(3);
            this.addPage.Size = new System.Drawing.Size(792, 423);
            this.addPage.TabIndex = 2;
            this.addPage.Text = "Add folder";
            this.addPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.selectFolderBtn);
            // 
            // pathTextLabel
            // 
            this.pathTextLabel.AutoSize = true;
            this.pathTextLabel.Location = new System.Drawing.Point(126, 32);
            this.pathTextLabel.Name = "pathTextLabel";
            this.pathTextLabel.Size = new System.Drawing.Size(0, 13);
            this.pathTextLabel.TabIndex = 9;
            // 
            // keyName
            // 
            this.keyName.Location = new System.Drawing.Point(20, 29);
            this.keyName.Name = "keyName";
            this.keyName.Size = new System.Drawing.Size(100, 20);
            this.keyName.TabIndex = 8;
            // 
            // keyNameLabel
            // 
            this.keyNameLabel.AutoSize = true;
            this.keyNameLabel.Location = new System.Drawing.Point(17, 12);
            this.keyNameLabel.Name = "keyNameLabel";
            this.keyNameLabel.Size = new System.Drawing.Size(35, 13);
            this.keyNameLabel.TabIndex = 7;
            this.keyNameLabel.Text = "Name";
            // 
            // passwordConfirm
            // 
            this.passwordConfirm.Location = new System.Drawing.Point(20, 117);
            this.passwordConfirm.PasswordChar = '*';
            this.passwordConfirm.Name = "passwordConfirm";
            this.passwordConfirm.Size = new System.Drawing.Size(100, 20);
            this.passwordConfirm.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select a folder";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(20, 73);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(100, 20);
            this.password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password";
            // 
            // addNew
            // 
            this.addNew.Location = new System.Drawing.Point(20, 393);
            this.addNew.Name = "addNew";
            this.addNew.Size = new System.Drawing.Size(75, 23);
            this.addNew.TabIndex = 0;
            this.addNew.Text = "Add";
            this.addNew.UseVisualStyleBackColor = true;
            this.addNew.Click += new System.EventHandler(this.AddNewKey);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // decryptPassword
            // 
            this.decryptPassword.Location = new System.Drawing.Point(7, 276);
            this.decryptPassword.Name = "decryptPassword";
            this.decryptPassword.PasswordChar = '*';
            this.decryptPassword.Size = new System.Drawing.Size(100, 20);
            this.decryptPassword.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Password";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Folder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Folder.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.addPage.ResumeLayout(false);
            this.addPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Folder;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabPage addPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addNew;
        private System.Windows.Forms.TextBox passwordConfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.ListBox decryptFolderList;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ListBox encryptedFolderList;
        private System.Windows.Forms.Label keyNameLabel;
        private System.Windows.Forms.TextBox keyName;
        private System.Windows.Forms.Label pathTextLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView encryptFileList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label selectDecryptFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.ListBox decryptFileList;
        private System.Windows.Forms.Label fileInfoKeyLabel;
        private System.Windows.Forms.Label fileInfoKeyName;
        private System.Windows.Forms.TextBox decryptPassword;
        private System.Windows.Forms.Label label6;
    }
}

