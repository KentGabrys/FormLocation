using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using FormLocation;
using NUnit.Framework;

namespace FormLocationTests
{
    [TestFixture]
    public class RecorderSerializesFormElementsToFileTests
    {
        private MainForm _form;
        private MainForm _form2;
        private string _settingsPath;

        [SetUp]
        public void SetUp()
        {
            if (File.Exists( _settingsPath )) File.Delete( _settingsPath );

            _form = new MainForm();
            _form.Size = new Size( 640, 480 );
            _form.Location = new Point( 200, 200 );
            _form.Name = "Chester";
            _settingsPath = _form.RecorderSettings;

            _form.Show();
            _form.Close();


            _form2 = new MainForm();
            _form2.Size = new Size( 800, 600 );
            _form2.Location = new Point( 300, 400 );
            _form2.Name = "Chumley";

            _form2.Show();
            _form2.Close();
        }


        [Test]
        public void RecorderSavesDataTest()
        {
            Assert.IsTrue( File.Exists( _settingsPath ) );
        }

        [Test]
        public void RecorderSavesChesterSizeTest()
        {
            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var chester = settings.FirstOrDefault( s => s.Name == "Chester" );
            Assert.AreEqual( chester.Size, new Size( 640, 480 ) );
        }

        [Test]
        public void RecorderSavesChesterLocationTest()
        {
            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var chester = settings.FirstOrDefault( s => s.Name == "Chester" );
            Assert.AreEqual( chester.Location, new Point( 200, 200 ) );
        }


        [Test]
        public void RecorderSavesChumleySizeTest()
        {
            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var chumley = settings.FirstOrDefault( s => s.Name == "Chumley" );
            Assert.AreEqual( chumley.Size, new Size( 800, 600 ) );
        }

        [Test]
        public void RecorderSavesChumleyLocationTest()
        {
            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var chumley = settings.FirstOrDefault( s => s.Name == "Chumley" );
            Assert.AreEqual( chumley.Location, new Point( 300, 400 ) );
        }

    }
}