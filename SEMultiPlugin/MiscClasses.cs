using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace SEMultiPlugin
{
    [Serializable()]
    public class UserPrefrences
    {
        public string GamePath;
        public List<Plugin> Plugins;

        public UserPrefrences()
        {
            GamePath = @"C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers\Bin64";
            Plugins = new List<Plugin>();
        }
    }

    [Serializable()]
    public class Plugin
    {
        public bool Selected { get; set; }
        public string FileName { get; set; }
        public string DisplayName
        {
            get
            {
                return FileName.Split('.').FirstOrDefault();
            }
        }

        public Plugin(string fileName)
        {
            FileName = fileName;
        }

        public Plugin()
        {
            FileName = "";
            Selected = false;
        }

        public override string ToString()
        {
            return FileName;
        }
    }

    public static class Persistence
    {
        private const string ConfigFileName = @"MultiPlugin.cfg";
        public const string PluginPath = @"Plugins\";
        public const string MultiPluginFileName = @"SEMultiPlugin.dll";
        public static UserPrefrences Settings = new UserPrefrences();

        public static void LoadSettings()
        {
            if (File.Exists(ConfigFileName))
            {
                XmlSerializer serializer = new XmlSerializer(Settings.GetType());
                FileStream fs = new FileStream(ConfigFileName, FileMode.Open);
                Settings = serializer.Deserialize(fs) as UserPrefrences;
                fs.Close();
            }
        }

        public static void SaveSettings()
        {
            XmlSerializer serializer = new XmlSerializer(Settings.GetType());
            FileStream fs = new FileStream(ConfigFileName, FileMode.Create);
            TextWriter tw = new StreamWriter(fs);
            serializer.Serialize(tw, Settings);
            tw.Close();
        }
    }
}