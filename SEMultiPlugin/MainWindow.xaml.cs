using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
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

            Persistence.Load();
            tbPath.Text = Persistence.Settings.GamePath;
            UpdatePluginList();
        }

        #region Control Event Handlers
        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            //Open a folder select dialog and set the game path to the result.
            var dialog = new Forms.FolderBrowserDialog();
            dialog.SelectedPath = Persistence.Settings.GamePath;
            dialog.ShowDialog();
            tbPath.Text = Persistence.Settings.GamePath = dialog.SelectedPath;
            Persistence.Save();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Persistence.Save();

            string gamePluginDirectory = Persistence.Settings.GamePath + @"\Plugins";

            //(Re)place the existing multiplugin.
            string MultiPluginFile = Persistence.Settings.GamePath + @"\" + Persistence.MultiPluginFileName;
            if (File.Exists(MultiPluginFile))
            {
                File.Delete(MultiPluginFile);
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
        #endregion

        private void UpdatePluginList()
        {
            lbPlugins.ItemsSource = Persistence.Settings.Plugins;
            const string path = Persistence.PluginPath;

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
