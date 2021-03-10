using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FormLocationLib
{
    public class WindowRecorder
    {
        private Form _form;
        private string _settingsFilePath;
        private WindowRecorder _settings;


        public string InitializeWindowsSizer( Form form )
        {
            SetSettingsPath();
            _form = form;
            _form.Load += InitializeRecorder;
            _form.Disposed += FormOnDisposed ;
            return _settingsFilePath;
        }

        private void FormOnDisposed( object sender, EventArgs e )
        {
            var formSettings = RestoreSettingsList();

            if (formSettings.Any())
            {
                // get the object by this form's name
                var settings = formSettings.FirstOrDefault( f => f.Name == _form.Name );
                if (settings == null)
                {
                    var list = formSettings.ToList();
                    UpdateFromForm();
                    list.Add( this );
                    formSettings = list;
                }
                else
                {
                    settings.Location = _form.Location;
                    settings.Size = _form.Size;
                }
                File.WriteAllText( _settingsFilePath, JsonConvert.SerializeObject( formSettings, Formatting.Indented ) );
            }
            else
            {
                UpdateFromForm();
                var list = new List<WindowRecorder>() { this };
                File.WriteAllText( _settingsFilePath, JsonConvert.SerializeObject( list, Formatting.Indented ) );
            }
        }


        private List<WindowRecorder> RestoreSettingsList()
        {
            var formSettings = new List<WindowRecorder>();

            if ( File.Exists( _settingsFilePath ) )
            {
                var fileText = File.ReadAllText( _settingsFilePath );
                formSettings = JsonConvert.DeserializeObject<IEnumerable<WindowRecorder>>( fileText ).ToList();
            }

            return formSettings;
        }

        private void UpdateFromForm()
        {
            Location = _form.Location;
            Size = _form.Size;
            Name = _form.Name;
        }

        private void SetSettingsPath()
        {
            var settingsPath =
                 Path.Combine(
                     Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                     AppDomain.CurrentDomain.FriendlyName.Substring(0, AppDomain.CurrentDomain.FriendlyName.Length - 4 ) );
            // Insures that the directory exists, if this is new
            Directory.CreateDirectory( settingsPath );

            _settingsFilePath = Path.Combine( settingsPath, "formSettings.json" );
        }


        private void InitializeRecorder( object sender, EventArgs e )
        {

            if (File.Exists( _settingsFilePath ))
            {
                var fileText = File.ReadAllText( _settingsFilePath );
                //deserialize to objects
                var formSettings = JsonConvert.DeserializeObject<IEnumerable<WindowRecorder>>( fileText );
                // get the object by this form's name
                _settings = formSettings.FirstOrDefault( f => f.Name == _form.Name );
                // if found
                if (_settings != null)
                {
                    this.Name = _form.Name = _settings.Name;
                    this.Size = _form.Size = _settings.Size;
                    this.Location = _form.Location = _settings.Location;
                    return;
                }
            }
            _settings = this;
            _settings.Name = _form.Name;
            _settings.Size = _form.Size;
            _settings.Location = _form.Location;
        }

        public Point Location { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }


    }

}
