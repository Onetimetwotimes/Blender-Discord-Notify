﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BlenderBot
{

    public partial class Preferences : Form
    {
        public string appdata = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        [STAThread]
        static void Main()
        {

        }
        public Preferences()
        {
            InitializeComponent();
        }

        private void Readprefs(object sender, EventArgs e)
        {
            string[] cfg = File.ReadAllLines(appdata + @"\BlenderBot\prefs.cfg");
            BlenderExe.Text = cfg[0].Split('=').Skip(1).FirstOrDefault();
            ProjectFolder.Text = cfg[1].Split('=').Skip(1).FirstOrDefault();
            ChannelId.Text = cfg[2].Split('=').Skip(1).FirstOrDefault();
            Format.Text = cfg[3].Split('=').Skip(1).FirstOrDefault();
        }

        #region Exe
        private void SelectExe(object sender, EventArgs e)
        {
            BlenderSelector.ShowDialog();
        }

        private void SelectExe_C(object sender, CancelEventArgs e)
        {
            BlenderExe.Text = '"' + BlenderSelector.FileName + '"';
        }
        #endregion

        #region Dir
        private void SelectDir(object sender, EventArgs e)
        {
            var fsd = new BlenderBot.FolderSelectDialog();
            fsd.Title = "Select Project Folder";
            fsd.InitialDirectory = @"C:\";
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                ProjectFolder.Text = '"' + fsd.FileName + @"\";
            }

        }
        #endregion

        private void ConfirmPrefs(object sender, EventArgs e)
        {
            string[] cfg = File.ReadAllLines(appdata + @"\BlenderBot\prefs.cfg");
            cfg[0] = "BlenderExecutable=" + BlenderExe.Text;
            cfg[1] = "ProjectFolder=" + ProjectFolder.Text;
            cfg[2] = "ChannelID=" + ChannelId.Text;
            cfg[3] = "ImageFormat=" + Format.Text;
            File.WriteAllLines(appdata + @"\BlenderBot\prefs.cfg", cfg);
            this.Close();
            
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    /*BlenderExecutable= 
ProjectFolder= 
ImageFormat=

*DO NOT EDIT THIS FILE MANUALLY. USE THE PREFERENCES MENU IN THE APPLICATION*
(in the event that you have managed to break something, delete this file; A new one will be generated the next time you start the application)
*/
}
