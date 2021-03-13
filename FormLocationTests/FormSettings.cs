using System.Collections.Generic;
using System.IO;
using System.Linq;
using FormLocationLib;
using Newtonsoft.Json;

namespace FormLocationTests
{
    public static class FormSettings
    {
        public static List<WindowRecorder> GetSettingsList(string settingsPath)
        {
            var formSettings = new List<WindowRecorder>();

            if (File.Exists( settingsPath ))
            {
                var fileText = File.ReadAllText( settingsPath );
                formSettings = JsonConvert.DeserializeObject<IEnumerable<WindowRecorder>>( fileText ).ToList();
            }

            return formSettings;
        }

    }
}