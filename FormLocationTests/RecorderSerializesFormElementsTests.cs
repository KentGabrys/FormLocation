using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using FormLocationLib;
using NUnit.Framework;

namespace FormLocationTests
{
    [TestFixture]
    public class RecorderSerializesFormElementsTests
    {
        private WindowRecorder _recorder;

        [SetUp]
        public void SetUp()
        {
            _recorder = new WindowRecorder();
            _recorder.Size = new Size( 640, 480 );
            _recorder.Location = new Point( 200, 200 );
            _recorder.Name = "Chester";
            _recorder.Save();
        }


        [Test]
        public void RecorderSavesDataTest()
        {
            var settingsFilePath = 
                Path.Combine( 
                    Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                    Assembly.GetExecutingAssembly().GetName().Name,
                    "formSettings.json" );

            Assert.IsTrue( File.Exists( settingsFilePath ) );

        }

    }
}