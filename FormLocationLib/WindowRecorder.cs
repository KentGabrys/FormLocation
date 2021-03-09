using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace FormLocationLib
{
    public class WindowRecorder
    {
        public Point Location { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }

        public void Save()
        {
            var settingsPath =
                Path.Combine(
                    Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                    Assembly.GetCallingAssembly().GetName().Name );
            // Insures that the directory exists
            Directory.CreateDirectory( settingsPath );

            var settingsFilePath = Path.Combine( settingsPath, "formSettings.json" );

            File.WriteAllText(settingsFilePath, "");
        }
    }
}
