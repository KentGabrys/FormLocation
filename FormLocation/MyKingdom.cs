using System.Windows.Forms;
using FormLocationLib;

namespace FormLocation
{
    public partial class MyKingdom : Form
    {
        private WindowRecorder _recorder;

        public MyKingdom()
        {
            InitializeComponent();
            _recorder = new WindowRecorder();
            _recorder.InitializeWindowsSizer( this );
        }
    }
}
