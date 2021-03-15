using FormLocation;
using NUnit.Framework;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormLocationLib;

namespace FormLocationTests
{
    [TestFixture]
    public class WillNotRestoreSettingsOffScreenTests
    {

        private string _settingsPath;


        [SetUp]
        public void SetUp()
        {
            using (var form = new MainForm()) { _settingsPath = form.LocatorSettings; }
            if (File.Exists(_settingsPath)) File.Delete(_settingsPath);

        }
        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_settingsPath)) File.Delete(_settingsPath);
        }

        [TestCase(-20000, 200, "Past Left Edge of Window")]
        [TestCase(20000, 200, "Past Right Edge of Window")]
        [TestCase(200, -20000, "Past Top Edge of Window")]
        [TestCase(200, 20000, "Past Bottom Edge of Window")]
        public void FormWillNotAppearOffScreenTest(int X, int Y, string msg)
        {
            var form = new MainForm();
            _settingsPath = form.LocatorSettings;
            var testPoint = new Point(X, Y);
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.GetSettingsList(_settingsPath);
            var offScreenTest = settings.FirstOrDefault(s => s.Name == "MainForm");
            Assert.AreNotEqual(testPoint, offScreenTest.Location);
            Console.WriteLine($"Message: {msg} ---- Before: {testPoint} - After: {offScreenTest.Location}");
        }

        [TestCase(50, true)]
        [TestCase(49, false)]
        public void FormAllowedLeftOffScreenToPixelBuffer(int pixelsAllowed, bool equal)
        {
            var form = new MainForm();
            var formWidth = form.Width;
            var screen = Screen.FromControl(form).Bounds;

            var X = 0 - formWidth + pixelsAllowed;
            var Y = 200;
            _settingsPath = form.LocatorSettings;
            var testPoint = new Point(X, Y);
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.GetSettingsList(_settingsPath);
            var offScreenTest = settings.FirstOrDefault(s => s.Name == "MainForm");
            Console.WriteLine($"Form.Width: {formWidth}");
            Console.WriteLine($"Before: {testPoint} - After: {offScreenTest.Location}");

            AssertLocationPosition(testPoint, offScreenTest, equal);
        }

        [TestCase(50, true)]
        [TestCase(49, false)]
        public void FormAllowedRightOffScreenToPixelBuffer(int pixelsAllowed, bool equal)
        {
            var form = new MainForm();
            _settingsPath = form.LocatorSettings;
            
            // for multiple screens, be sure our test case uses the max right value.
            for ( int i = 0; i < Screen.AllScreens.Length; i++ )
                SetFormLocation( pixelsAllowed, form );
            
            // save the initialized setting.
            var testPoint = form.Location;
            form.Show();
            form.Close();

            var settings = FormSettings.GetSettingsList(_settingsPath);
            var offScreenTest = settings.FirstOrDefault(s => s.Name == "MainForm");
            Console.WriteLine($"Before: {testPoint} - After: {offScreenTest.Location}");

            AssertLocationPosition(testPoint, offScreenTest, equal);
        }

        private static void SetFormLocation( int pixelsAllowed, MainForm form )
        {
            var screen = Screen.FromControl( form ).Bounds;
            var X = screen.Right - pixelsAllowed;
            var Y = 200;
            var testPoint = new Point( X, Y );
            form.Location = testPoint;
        }

        [TestCase(50, true)]
        [TestCase(49, false)]
        public void FormAllowedTopOffScreenToPixelBuffer(int pixelsAllowed, bool equal)
        {
            var form = new MainForm();
            var formHeight = form.Height;
            var screen = Screen.FromControl(form).Bounds;

            var X = 200;
            var Y = screen.Top - formHeight + pixelsAllowed;
            _settingsPath = form.LocatorSettings;
            var testPoint = new Point(X, Y);
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.GetSettingsList(_settingsPath);
            var offScreenTest = settings.FirstOrDefault(s => s.Name == "MainForm");
            Console.WriteLine($"Form.Height: {formHeight}");
            Console.WriteLine($"Before: {testPoint} - After: {offScreenTest.Location}");

            AssertLocationPosition(testPoint, offScreenTest, equal);
        }

        [TestCase(50, true)]
        [TestCase(49, false)]
        public void FormAllowedBottomOffScreenToPixelBuffer(int pixelsAllowed, bool equal)
        {
            var form = new MainForm();
            var screen = Screen.FromControl(form).Bounds;

            var taskBarHeight = 40;
            var X = 200;
            var Y = screen.Bottom - taskBarHeight - pixelsAllowed;
            _settingsPath = form.LocatorSettings;
            var testPoint = new Point(X, Y);
            form.Location = testPoint;
            form.Show();
            form.Close();

            var settings = FormSettings.GetSettingsList(_settingsPath);
            var offScreenTest = settings.FirstOrDefault(s => s.Name == "MainForm");

            Console.WriteLine($"Before: {testPoint} - After: {offScreenTest.Location}");

            AssertLocationPosition(testPoint, offScreenTest, equal);
        }

        private static void AssertLocationPosition(Point testPoint, FormLocator offScreenTest, bool equal)
        {
            if (equal)
                Assert.AreEqual(testPoint, offScreenTest.Location);
            else
                Assert.AreNotEqual(testPoint, offScreenTest.Location);
        }
    }
}