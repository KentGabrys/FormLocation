using System.Collections.Generic;
using System.IO;
using System.Linq;
using FormLocationLib;
using Newtonsoft.Json;

namespace FormLocationTests
{
    public static class FormSettings
    {
        public static List<FormLocator> GetSettingsList(string settingsPath)
        {
            var formSettings = new List<FormLocator>();

            if (File.Exists( settingsPath ))
            {
                var fileText = File.ReadAllText( settingsPath );
                formSettings = JsonConvert.DeserializeObject<IEnumerable<FormLocator>>( fileText ).ToList();
            }

            return formSettings;
        }

    }
}