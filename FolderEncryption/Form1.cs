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

namespace FolderEncryption
{
    public partial class Form1 : Form
    {
        private IEncryptionService _encryptionService;

        public Form1(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddNewKey(object sender, EventArgs e)
        {
            var password = this.password.Text;
            var password2 = this.passwordConfirm.Text;
            if (password != password2)
            {
                this.errorProvider1.SetError(this, "password doesnt match");
                return;
            }
            var name = this.keyName.Text;
            _encryptionService.CreateNewKey(name, password);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
