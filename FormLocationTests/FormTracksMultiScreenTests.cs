using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FormLocation;
using NUnit.Framework;

namespace FormLocationTests
{
    [TestFixture]
    public class FormTracksMultiScreenTests
    {
        private string _settingsPath;
        private Rectangle _bounds;

        [SetUp]
        public void SetUp()
        {
            var left = 0;
            var right = 0; 
            var top = 0; 
            var bottom = 0;
            foreach ( var s in Screen.AllScreens )
            {
                left = Math.Min( s.Bounds.Left, left );
                right = Math.Max( s.Bounds.Right, right );
                top = Math.Min( s.Bounds.Top, top );
                bottom = Math.Max( s.Bounds.Bottom, bottom );
            }
            _bounds = new Rectangle( left, top,  right, bottom );
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists( _settingsPath )) File.Delete( _settingsPath );
        }

        [Test]
        public void SetFormIntoSecondScreen()
        {
            if (File.Exists( _settingsPath )) File.Delete( _settingsPath );

            var _form = new MainForm();
            _form.Size = new Size( 640, 480 );
            // location to the right screen
            _form.Location = new Point( _bounds.Right - 50, 200 );
            _form.Name = "Chewbaca";
            
            _settingsPath = _form.LocatorSettings;

            _form.Show();
            _form.Close();

            var settings = FormSettings.GetSettingsList( _settingsPath );
            var thisSetting = settings.FirstOrDefault( f => f.Name == _form.Name );
            Assert.AreEqual( _bounds.Right - 50, thisSetting.Location.X );

        }


        [Test]
        public void SetFormIntoSecondScreenOutOfBounds()
        {
            if (File.Exists( _settingsPath )) File.Delete( _settingsPath );

            var _form = new MainForm();
            _form.Size = new Size( 640, 480 );
            // location to the right screen
            _form.Location = new Point( _bounds.Right - 49, 200 );
            _form.Name = "Chewbaca";

            _settingsPath = _form.LocatorSettings;

            _form.Show();
            _form.Close();

            var settings = FormSettings.GetSettingsList( _settingsPath );
            var thisSetting = settings.FirstOrDefault( f => f.Name == _form.Name );
            Assert.AreEqual( 200, thisSetting.Location.X );

        }

    }
}