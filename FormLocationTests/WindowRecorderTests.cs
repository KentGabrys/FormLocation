using System;
using System.Drawing;
using System.Windows.Forms;
using FormLocation;
using FormLocationLib;
using NUnit.Framework;

namespace FormLocationTests
{

    [TestFixture]
    public class WindowRecorderTests
    {
        private WindowRecorder _recorder;
        private Point _formLocation;
        private MainForm _form;

        [SetUp]
        public void SetUp()
        {
            _recorder = new WindowRecorder();
            _formLocation = new Point( 130, 130 );
            _recorder.Location = _formLocation;
            _form = new MainForm();
        }

        private void FormOnFormClosing( object sender, FormClosingEventArgs e )
        {
            var form = sender as MainForm;
            _recorder.Name = form.Name;
            _recorder.Size = form.Size;
            _recorder.Location = form.Location;
        }

        private void FormOnLoad( object sender, EventArgs e )
        {
            var form = sender as MainForm;
            form.Location = _recorder.Location;
        }



        [Test]
        public void WindowRecorderTest()
        {
            Assert.AreEqual( _formLocation, _recorder.Location );
        }

        [Test]
        public void FormLoadRestoresLocationFromRecorder()
        {
            _form.Load += FormOnLoad;
            _form.Show();

            Assert.AreEqual( _form.Location, _recorder.Location );
        }

        [Test]
        public void FormClosingSavesFormLocation()
        {
            _form.Load += FormOnLoad;
            _form.FormClosing += FormOnFormClosing;
            _form.Show();
            var newLocation = new Point( 200, 200 );
            _form.Location = newLocation;
            _form.Close();

            Assert.AreEqual( newLocation, _recorder.Location );
        }

        [Test]
        public void FormRecorderSavesFormName()
        {
            _form.Load += FormOnLoad;
            _form.FormClosing += FormOnFormClosing;
            _form.Show();
            _form.Close();

            Assert.AreEqual("MainForm", _recorder.Name );
        }

        [Test]
        public void FormRecorderSavesFormSize()
        {
            _form.Load += FormOnLoad;
            _form.FormClosing += FormOnFormClosing;
            _form.Show();
            _form.Close();

            Assert.AreEqual( _form.Size, _recorder.Size );
        }



    }
}
