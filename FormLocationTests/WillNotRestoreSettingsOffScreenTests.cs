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
    public class WillNotRestoreSettingsOffScreenTests
    {

        private string _settingsPath;
        

        [SetUp]
        public void SetUp()
        {
            using ( var form = new MainForm() ) { _settingsPath = form.RecorderSettings; }
            if (File.Exists( _settingsPath )) File.Delete( _settingsPath );

        }

        [Test]
        public void FormWillNotAppearOffScreenToLeftTest()
        {
            var form = new MainForm();
            var testPoint = new Point( -20000, 200 );
            form.Location = testPoint;
            _settingsPath = form.RecorderSettings;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Assert.AreNotEqual(testPoint, offScreenTest.Location );
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}");
        }


        [Test]
        public void FormWillNotAppearOffScreenToRightTest()
        {
            var form = new MainForm();
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( 20000, 200 );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Assert.AreNotEqual( testPoint, offScreenTest.Location );
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}" );
        }


        [Test]
        public void FormWillNotAppearOffScreenToTopTest()
        {
            var form = new MainForm();
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( 200, -20000 );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Assert.AreNotEqual( testPoint, offScreenTest.Location );
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}" );
        }

        [Test]
        public void FormWillNotAppearOffScreenToBottomTest()
        {
            var form = new MainForm();
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( 200, 20000 );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Assert.AreNotEqual( testPoint, offScreenTest.Location );
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}" );
        }

        [Test]
        public void FormAllowedLeftOffScreenToPixelBuffer()
        {
            const int pixelsAllowed = 20;
            
            var form = new MainForm();
            var formWidth = form.Width;
            var screen = Screen.FromControl( form ).Bounds;
            
            
            var X = 0 - formWidth + pixelsAllowed;
            var Y = 200;
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( X, Y );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Console.WriteLine( $"Form.Width: {formWidth}");
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}" );

            Assert.AreEqual( testPoint, offScreenTest.Location );

        }

        [Test]
        public void FormAllowedRightOffScreenToPixelBuffer()
        {
            const int pixelsAllowed = 20;

            var form = new MainForm();
            var screen = Screen.FromControl( form ).Bounds;

            var X = screen.Right - pixelsAllowed;
            var Y = 200;
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( X, Y );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Console.WriteLine( $"Before: {testPoint} - After: {offScreenTest.Location}" );

            Assert.AreEqual( testPoint, offScreenTest.Location );

        }


        [TestCase( -20000, 200, "Past Left Edge of Window" )]
        [TestCase( 20000, 200, "Past Right Edge of Window" )] 
        [TestCase( 200, -20000, "Past Top Edge of Window" )] 
        [TestCase( 200, 20000, "Past Bottom Edge of Window" )] 
        public void FormWillNotAppearOffScreenTest(int X, int Y, string msg)
        {
            var form = new MainForm();
            _settingsPath = form.RecorderSettings;
            var testPoint = new Point( X, Y );
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.RestoreSettingsList( _settingsPath );
            var offScreenTest = settings.FirstOrDefault( s => s.Name == "MainForm" );
            Assert.AreNotEqual( testPoint, offScreenTest.Location );
            Console.WriteLine( $"Message: {msg} ---- Before: {testPoint} - After: {offScreenTest.Location}" );
        }

    }
}