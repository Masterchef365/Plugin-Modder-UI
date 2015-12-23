using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Forms = System.Windows.Forms;

namespace SEMultiPlugin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Persistence.LoadSettings();
            UpdatePluginList();
        }

        #region Control Event Handlers
        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            //Open a folder select dialog and set the game path to the result.
            var dialog = new Forms.FolderBrowserDialog();
            dialog.Description = "Select the Bin64 folder in your Space Engineers directory and click OK.";
            dialog.SelectedPath = Persistence.Settings.GamePath;
            dialog.ShowDialog();
            Persistence.SaveSettings();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Persistence.SaveSettings();

            string gamePluginDirectory = Persistence.Settings.GamePath + @"\Plugins";

            //(Re)place the existing multiplugin.
            string MultiPluginFile = Persistence.Settings.GamePath + @"\" + Persistence.MultiPluginFileName;
            if (File.Exists(MultiPluginFile))
            {
                File.Delete(MultiPluginFile);
            }

            if (!File.Exists(Persistence.MultiPluginFileName))
            {
                MessageBox.Show("MultiPlugin DLL not found!", "File not found");
                return;
            }

            File.Copy(Persistence.MultiPluginFileName, MultiPluginFile);

            //Create remote plugin directory if it doesn't exist.
            if (!Directory.Exists(gamePluginDirectory))
            {
                Directory.CreateDirectory(gamePluginDirectory);
            }

            //Delete all plugins in the remote plugin directory.
            foreach (var item in Directory.EnumerateFiles(gamePluginDirectory))
            {
                File.Delete(item);
            }

            //Copy enabled plugins to the remote plugin directory.
            foreach (var plugin in Persistence.Settings.Plugins)
            {
                if (plugin.Selected)
                {
                    File.Copy(Persistence.PluginPath + @"\" + plugin.FileName, gamePluginDirectory + @"\" + plugin.FileName);
                }
            }

            //Try to start Space Engineers.
            try
            {
                Process.Start(Persistence.Settings.GamePath + @"\SpaceEngineers.exe", "-plugin SEMultiPlugin.dll");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            UpdatePluginList();
        }

        private void btnKill_Click(object sender, RoutedEventArgs e)
        {
            //Kill every process named "SpaceEngineers."
            foreach (var process in Process.GetProcessesByName("SpaceEngineers"))
            {
                process.Kill();
            }
        }

        private void btnUninstall_Click(object sender, RoutedEventArgs e)
        {
            //Remove the remote Plugins directory and all of its contents.
            if (Directory.Exists(Persistence.Settings.GamePath + @"\Plugins"))
            {
                Directory.Delete(Persistence.Settings.GamePath + @"\Plugins");
            }

            //Remove the remote multiplugin.
            if (File.Exists(Persistence.Settings.GamePath + @"\" + Persistence.MultiPluginFileName))
            {
                File.Delete(Persistence.Settings.GamePath + @"\" + Persistence.MultiPluginFileName);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WrapPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (var child in ((WrapPanel)sender).Children)
            {
                if (child is CheckBox)
                {
                    var cb = (CheckBox)child;
                    cb.IsChecked = !cb.IsChecked;
                }
            }
        }

        private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        private void UpdatePluginList()
        {
            const string path = Persistence.PluginPath;
            lbPlugins.ItemsSource = Persistence.Settings.Plugins;

            //Create Plugin directory if it doesn't exist.
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Enumerate plugins in folder and add new ones to the persistent list.
            var pluginsInFolder = Directory.EnumerateFiles(path, "*.dll");
            foreach (var pluginPath in pluginsInFolder)
            {
                string fileName = pluginPath.Split('\\').LastOrDefault();
                if (!Persistence.Settings.Plugins.Any(p => p.FileName == fileName))
                {
                    Persistence.Settings.Plugins.Add(new Plugin(fileName));
                }
            }

            //Remove plugins that are no longer in the directory.
            Persistence.Settings.Plugins.RemoveAll(p => !File.Exists(Persistence.PluginPath + p.FileName));

            lbPlugins.Items.Refresh();
        }
    }
}
