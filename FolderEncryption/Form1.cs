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
        private IFileWatcherService _fileEncryptionService;

        public Form1(IFileWatcherService fileEncryptionService)
        {
            _fileEncryptionService = fileEncryptionService;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
