using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Plugin_Modder_UI
{
    public partial class UI : Form
    {
        string bin64Location = "";
        string pluginsLocation = "";
        string candidatesLocation = "";

        public string[] pluginCandidates = { };
        public UI()
        {
            InitializeComponent();
            activePluginsBox.CheckOnClick = true; //Just one click thanks
            
            if (Directory.Exists(@"C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers\Bin64\"))
            {
                bin64Location = @"C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers\Bin64\";
                BinLocationTextBox.Text = bin64Location;
            }
            
            pluginsLocation = bin64Location + @"PluginManager\Plugins\";
            candidatesLocation = bin64Location + @"PluginManager\PluginCandidates\";
            
            if (Directory.Exists(pluginsLocation) && Directory.Exists(candidatesLocation) && File.Exists(bin64Location + "SEMultiPlugin.dll"))
            {
                ReloadCheckboxes(candidatesLocation, pluginsLocation);
                InstallButton.Text = "Uninstall SEPluginManager";
                InstallButton.Enabled = true;
                RunSE.Enabled = true;
                RefreshList.Enabled = true;
                OpenPluginFolder.Enabled = true;
            } else
            {
                InstallButton.Text = "Install SEPluginManager";
                RunSE.Enabled = false;
                RefreshList.Enabled = false;
                OpenPluginFolder.Enabled = false;
            }

        }

        private void ReloadCheckboxes(string candidates, string alreadyLoaded)
        {
            if (Directory.Exists(candidates) && Directory.Exists(alreadyLoaded))
            {
                activePluginsBox.Items.Clear();
                foreach (string current in Directory.GetFiles(candidates))
                {
                    int index = activePluginsBox.Items.Add(Path.GetFileName(current));
                    bool hasIt = false;
                    foreach (string plugin in Directory.GetFiles(alreadyLoaded))
                    {
                        if (Path.GetFileName(plugin) == Path.GetFileName(current)) { hasIt = true; break; }
                    }
                    if (hasIt)
                    {
                        activePluginsBox.SetItemChecked(index, true);
                    }
                }
                activePluginsBox.Refresh();
            } 
        }

        private void BinLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (Directory.Exists(text))
            {
                bin64Location = text;
                if (Directory.Exists(bin64Location + @"PluginManager\PluginCandidates\") && Directory.Exists(bin64Location + @"PluginManager\Plugins\") && File.Exists(bin64Location + "SEMultiPlugin.dll"))
                {
                    pluginsLocation = bin64Location + @"PluginManager\Plugins\";
                    candidatesLocation = bin64Location + @"PluginManager\PluginCandidates\";
                    ReloadCheckboxes(candidatesLocation, pluginsLocation);
                    InstallButton.Text = "Uninstall SEPluginManager";
                    InstallButton.Enabled = true;
                    RunSE.Enabled = true;
                    RefreshList.Enabled = true;
                    OpenPluginFolder.Enabled = true;
                } else
                {
                    InstallButton.Text = "Install SEPluginManager";
                    InstallButton.Enabled = true;
                    RunSE.Enabled = false;
                    RefreshList.Enabled = false;
                    OpenPluginFolder.Enabled = false;
                }
            } else
            {
                InstallButton.Enabled = false;
                RunSE.Enabled = false;
                RefreshList.Enabled = false;
                OpenPluginFolder.Enabled = false;
            }
        }

        private void OpenPluginFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(pluginsLocation) && Directory.Exists(candidatesLocation))
            {
                Process.Start("explorer.exe", candidatesLocation);
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(pluginsLocation) && Directory.Exists(candidatesLocation) && File.Exists(bin64Location + "SEMultiPlugin.dll"))
            { //Uninstall
                foreach (string file in Directory.GetFiles(pluginsLocation))
                {
                    File.Delete(file);
                }
                Directory.Delete(pluginsLocation);
                File.Delete(bin64Location + "SEMultiPlugin.dll");
                RunSE.Enabled = false;
                RefreshList.Enabled = false;
                OpenPluginFolder.Enabled = false;
                InstallButton.Text = "Install SEPluginManager";
                activePluginsBox.Items.Clear();
            }
            else
            {
                if (Directory.Exists(bin64Location))
                {
                    Directory.CreateDirectory(bin64Location + @"PluginManager\Plugins\");
                    Directory.CreateDirectory(bin64Location + @"PluginManager\PluginCandidates\");
                    pluginsLocation = bin64Location + @"PluginManager\Plugins\";
                    candidatesLocation = bin64Location + @"PluginManager\PluginCandidates\";
                }

                string dllSource = Directory.GetCurrentDirectory() + @"\SEMultiPlugin.dll";
                if (File.Exists(dllSource))
                {
                    File.Copy(dllSource, bin64Location + Path.GetFileName(dllSource));
                }
                ReloadCheckboxes(candidatesLocation, pluginsLocation);
                Process.Start("explorer.exe", candidatesLocation);
                InstallButton.Text = "Uninstall SEPluginManager";
                InstallButton.Enabled = true;
                RunSE.Enabled = true;
                RefreshList.Enabled = true;
                OpenPluginFolder.Enabled = true;
            }
        }

        private void RunSE_Click(object sender, EventArgs e)
        {
            if (File.Exists(bin64Location + "SpaceEngineers.exe"))
            {
                Process.Start(bin64Location + "SpaceEngineers.exe", "-plugin " + "SEMultiPlugin.dll");
            }
        }

        private void activePluginsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> confirmed = new List<string>();
            foreach (object item in activePluginsBox.CheckedItems)
            {
                confirmed.Add(activePluginsBox.GetItemText(item));
            }

            //Delete all
            foreach (string current in Directory.GetFiles(pluginsLocation))
            {
                File.Delete(current);
            }

            //Add them
            foreach (string current in Directory.GetFiles(candidatesLocation))
            {
                if (confirmed.Contains(Path.GetFileName(current)))
                {
                    File.Copy(current, pluginsLocation + Path.GetFileName(current));
                }
            }


        }

        private void RefreshList_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(pluginsLocation) && Directory.Exists(candidatesLocation))
            {
                ReloadCheckboxes(candidatesLocation, pluginsLocation);
            }
        }
    }
}
