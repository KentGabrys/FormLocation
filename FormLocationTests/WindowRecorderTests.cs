using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FormLocation;
using FormLocationLib;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FormLocationTests
{

    [TestFixture]
    public class WindowLocatorTests
    {
        private FormLocator _locator;
        private Point _formLocation;
        private MainForm _form;
       // private string _settingsFile;

        [SetUp]
        public void SetUp()
        {
            _formLocation = new Point( 300, 300 );

            _form = new MainForm();
            _form.StartPosition = FormStartPosition.Manual;
            _form.Location = _formLocation;

            _locator = new FormLocator(_form);
            if ( File.Exists( _locator.SettingsPath ) ) File.Delete( _locator.SettingsPath );

            _form.Show();
        }

        [Test]
        public void WindowLocatorTest()
        {
            Assert.AreEqual( _formLocation, _locator.Location );
        }

        [Test]
        public void FormLoadRestoresLocationFromLocator()
        {
            Assert.AreEqual( _form.Location, _locator.Location );
        }

        [Test]
        public void FormClosingSavesFormLocationChange()
        {
            var newLocation = new Point( 200, 200 );
            Assert.AreNotEqual( _form.Location, newLocation );

            _form.Location = newLocation;
            _form.Close();
            var locator = GetFormLocator();
            Assert.AreEqual( newLocation, locator.Location);
        }
        
        [Test]
        public void FormLocatorSavesFormName()
        {
            _form.Close();
            var locator = GetFormLocator();
            Assert.AreEqual("MainForm", locator.Name );
        }

        [Test]
        public void FormLocatorSavesFormSize()
        {
            _form.Close();
            var locator = GetFormLocator();
            Assert.AreEqual( _form.Size, locator.Size );
        }

        private FormLocator GetFormLocator()
        {   
            var formSettings = FormSettings.GetSettingsList( _locator.SettingsPath );
            return formSettings.FirstOrDefault( f => f.Name == _form.Name );
        }


    }
}
