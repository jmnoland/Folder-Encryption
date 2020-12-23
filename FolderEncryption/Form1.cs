using FolderEncryption.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using FolderEncryption.Models;

namespace FolderEncryption
{
    public partial class Form1 : Form
    {
        private IEncryptionService _encryptionService;
        private IFileEncryptionRepository _fileEncryptionRepository;
        private IFileWatcherService _fileWatcherService;

        private string _newFilePath;
        private Dictionary<string, EncryptionKey> encryptionKeyDict;

        public Form1(IEncryptionService encryptionService,
                     IFileEncryptionRepository fileEncryptionRepository,
                     IFileWatcherService fileWatcherService)
        {
            _encryptionService = encryptionService;
            _fileEncryptionRepository = fileEncryptionRepository;
            encryptionKeyDict = new Dictionary<string, EncryptionKey>();
            _fileWatcherService = fileWatcherService;
            _fileWatcherService.StartWatchers();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTableData();
        }

        private void LoadTableData()
        {
            var encryptionKeys = _fileEncryptionRepository.GetEncryptionKeys();
            foreach (var key in encryptionKeys)
            {
                if (key.Folders == null) continue;
                foreach (var dir in key.Folders)
                {
                    if (!encryptionKeyDict.ContainsKey(dir.Path))
                        encryptionKeyDict.Add(dir.Path, key);

                    this.encryptedFolderList.Items.Add(dir.Path);
                    this.decryptFolderList.Items.Add(dir.Path);
                }
            }
        }

        private void SelectFilePath(Label label)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _newFilePath = fbd.SelectedPath;
                    label.Text = _newFilePath;
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
            _fileWatcherService.StartWatchers();

            this.encryptedFolderList.Items.Clear();
            this.decryptFolderList.Items.Clear();
            LoadTableData();
        }

        private void selectFolderBtn(object sender, EventArgs e)
        {
            SelectFilePath(this.pathTextLabel);
        }

        private void selectDecryptFolderBtn(object sender, EventArgs e)
        {
            SelectFilePath(this.selectDecryptFolder);
        }

        private void encryptedFolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyInfo pi = sender.GetType().GetProperty("SelectedItem");
            string path = (string)(pi.GetValue(sender, null));
            if (path == null) return;
            this.encryptFileList.Items.Clear();
            var files = ListFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                var fileName = files[i].Replace(path + "\\", "");
                ListViewItem t = new ListViewItem(fileName);
                this.encryptFileList.Items.Add(t);
            }
        }

        private void decryptFolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyInfo pi = sender.GetType().GetProperty("SelectedItem");
            string path = (string)(pi.GetValue(sender, null));
            if (path == null) return;
            this.decryptFileList.Items.Clear();
            this.fileInfoKeyName.Text = "";
            encryptionKeyDict.TryGetValue(path, out EncryptionKey val);
            this.fileInfoKeyName.Text = val.PublicKeyName;
            var files = ListFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                var fileName = files[i].Replace(path + "\\", "");
                this.decryptFileList.Items.Add(fileName);
            }
        }

        private string[] ListFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            var file = this.decryptFileList.SelectedItem;
            var password = this.decryptPassword.Text;
            var keyName = this.fileInfoKeyName.Text;
            var output = this.selectDecryptFolder.Text;
            var path = this.decryptFolderList.SelectedItem.ToString();

            if (file == null || password == null || keyName == null || output == null) return;

            encryptionKeyDict.TryGetValue(path, out EncryptionKey val);
            var data = File.ReadAllBytes(path + "\\" + file);
            var decryptedData = _encryptionService.DecryptFile(keyName, val.Password, password, data);
            File.WriteAllBytes(output + "\\" + file, decryptedData);
            this.decryptPassword.Text = "";
            this.selectDecryptFolder.Text = "";
        }
    }
}
