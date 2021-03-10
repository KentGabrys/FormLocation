using System.Windows.Forms;
using FormLocationLib;

namespace FormLocation
{
    public partial class MainForm : Form
    {
        private readonly WindowRecorder _recorder;

        #region For Testing Purposes Only
        // the following is for the test environment and not required for use
        private string _settingsPath;
        public string RecorderSettings => _settingsPath;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            _recorder = new WindowRecorder();
            _settingsPath = _recorder.InitializeWindowsSizer( this );
        }

        private void btnOpenMyKingdom_Click( object sender, System.EventArgs e )
        {
            var form = new MyKingdom();
            form.Show();
        }
    }
}
