using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FormLocationLib
{
    public class FormLocator
    {
        private Form _form;
        private string _settingsFilePath;
        private FormLocator _settings;

        public Point Location { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }


        public FormLocator(Form form)
        {
            SetSettingsPath();
            _form = form;
            if (_form != null)
            {
                _form.Load += InitializeLocator;
                _form.Disposed += SaveOnDisposed;
            }
        }

        // Property used in testing
        public string SettingsPath => _settingsFilePath;

        public string InitializeFormLocator(Form form)
        {
            SetSettingsPath();
            _form = form;

            _form.Load += InitializeLocator;
            _form.Disposed += SaveOnDisposed;
            return _settingsFilePath;
        }

        private void SaveOnDisposed(object sender, EventArgs e)
        {
            var formSettings = RestoreSettingsList();

            if (formSettings.Any())
            {
                // get the object by this form's name
                var settings = formSettings.FirstOrDefault(f => f.Name == _form.Name);
                if (settings == null)
                {
                    var list = formSettings.ToList();
                    list.Add(this);
                    formSettings = list;
                }
                else
                {
                    settings.Location = _form.Location;
                    settings.Size = _form.Size;
                }
                File.WriteAllText(_settingsFilePath, JsonConvert.SerializeObject(formSettings, Formatting.Indented));
            }
            else
            {
                Location = _form.Location;
                Size = _form.Size;
                var list = new List<FormLocator>() { this };
                File.WriteAllText(_settingsFilePath, JsonConvert.SerializeObject(list, Formatting.Indented));
            }
        }

        private void InitializeLocator(object sender, EventArgs e)
        {
            var settings = RestoreSettingsList();

            // get the object by this form's name
            _settings = settings.FirstOrDefault(f => f.Name == _form.Name);
            // if not found, set up the current form values to settings
            if (_settings == null)
            {
                _settings = this;
                _settings.Name = _form.Name;
                _settings.Size = _form.Size;
                _settings.Location = _form.Location;
            }

            // restore settings to objects
            this.Name = _form.Name = _settings.Name;
            this.Size = _form.Size = _settings.Size;
            if (OffScreenLocation(_settings))
                _settings.Location = new Point(200, 200);

            this.Location = _form.Location = _settings.Location;
        }


        private bool OffScreenLocation(FormLocator settings)
        {
            const int pixelBuffer = 50;
            var screen = Screen.FromControl(_form).Bounds;
            var leftAllowablePixels = screen.Left - settings.Size.Width + pixelBuffer;
            var rightAllowablePixels = screen.Right - pixelBuffer;
            var topAllowablePixels = screen.Top - (settings.Size.Height + pixelBuffer);
            var bottomAllowablePixels = screen.Bottom - pixelBuffer;
            return
                (settings.Location.X < leftAllowablePixels
                  ||
                  settings.Location.X > rightAllowablePixels
                  ||
                  settings.Location.Y < topAllowablePixels
                  ||
                  settings.Location.Y > bottomAllowablePixels
                );
        }

        private List<FormLocator> RestoreSettingsList()
        {
            var formSettings = new List<FormLocator>();

            if (File.Exists(_settingsFilePath))
            {
                var fileText = File.ReadAllText(_settingsFilePath);
                formSettings = JsonConvert.DeserializeObject<IEnumerable<FormLocator>>(fileText).ToList();
            }

            return formSettings;
        }

        private void SetSettingsPath()
        {
            var settingsPath =
                 Path.Combine(
                     Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     AppDomain.CurrentDomain.FriendlyName.Substring(0, AppDomain.CurrentDomain.FriendlyName.Length - 4).Replace(":", "").Replace(" ", ""));
            // Insures that the directory exists, if this is new
            Directory.CreateDirectory(settingsPath);

            _settingsFilePath = Path.Combine(settingsPath, "formSettings.json");
        }
    }
}
