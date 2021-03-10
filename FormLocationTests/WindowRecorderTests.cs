using System;
using System.Collections;
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
    public class WindowRecorderTests
    {
        private WindowRecorder _recorder;
        private Point _formLocation;
        private MainForm _form;
        private string _settingsFile;

        [SetUp]
        public void SetUp()
        {
            if ( File.Exists( _settingsFile ) ) File.Delete( _settingsFile );

            _formLocation = new Point( 130, 130 );

            _form = new MainForm();
            _form.StartPosition = FormStartPosition.Manual;
            _form.Location = _formLocation;
            

            _recorder = new WindowRecorder();
            _settingsFile = _recorder.InitializeWindowsSizer(_form);

            _form.Show();

        }

        [Test]
        public void WindowRecorderTest()
        {
            Assert.AreEqual( _formLocation, _recorder.Location );
        }

        [Test]
        public void FormLoadRestoresLocationFromRecorder()
        {
            Assert.AreEqual( _form.Location, _recorder.Location );
        }

        [Test]
        public void FormClosingSavesFormLocationChange()
        {
            var newLocation = new Point( 200, 200 );
            Assert.AreNotEqual( _form.Location, newLocation );

            _form.Location = newLocation;
            _form.Close();
            var recorder = GetFormRecorder();
            Assert.AreEqual( newLocation, recorder.Location);
        }
        
        [Test]
        public void FormRecorderSavesFormName()
        {
            _form.Close();
            var recorder = GetFormRecorder();
            Assert.AreEqual("MainForm", recorder.Name );
        }

        [Test]
        public void FormRecorderSavesFormSize()
        {
            _form.Close();
            var recorder = GetFormRecorder();
            Assert.AreEqual( _form.Size, recorder.Size );
        }

        private WindowRecorder GetFormRecorder()
        {
            var jsonText = File.ReadAllText( _settingsFile );
            var formSettings = JsonConvert.DeserializeObject<IEnumerable<WindowRecorder>>( jsonText );
            var thisTestRecorder = formSettings.FirstOrDefault( f => f.Name == _form.Name );
            return thisTestRecorder;
        }


    }
}
