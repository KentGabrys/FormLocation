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

        [Test]
        public void RecorderSavesDataTest()
        {
            var recorder = new WindowRecorder();
            recorder.Size = new Size( 640, 480 );
            recorder.Location = new Point( 200, 200 );
            recorder.Name = "Chester";
            recorder.Save();

            var settingsFilePath = 
                Path.Combine( 
                    Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ),
                    Assembly.GetExecutingAssembly().GetName().Name,
                    "formSettings.json" );
            Assert.IsTrue( File.Exists( settingsFilePath ) );

        }

    }
}