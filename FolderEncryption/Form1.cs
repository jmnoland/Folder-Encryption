using FolderEncryption.Interfaces;
using FolderEncryption.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace FolderEncryption
{
    public partial class Form1 : Form
    {
        private IEncryptionService _encryptionService;
        private string _newFilePath;

        public Form1(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SelectFilePath()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _newFilePath = fbd.SelectedPath;
                    this.pathTextLabel.Text = _newFilePath;
                }
            }
        }

        private void AddNewKey(object sender, EventArgs e)
        {
            if (_newFilePath == null) return;
            var password = this.password.Text;
            var password2 = this.passwordConfirm.Text;
            if (password != password2)
            {
                this.errorProvider1.SetError(this, "password doesnt match");
                return;
            }
            var name = this.keyName.Text;
            _encryptionService.CreateNewKey(name, password, _newFilePath);
            this.password.Text = "";
            this.passwordConfirm.Text = "";
            this.keyName.Text = "";
            this.pathTextLabel.Text = "";
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void selectFolderBtn(object sender, EventArgs e)
        {
            SelectFilePath();
        }
    }
}
