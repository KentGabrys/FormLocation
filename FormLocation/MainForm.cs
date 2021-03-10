using System.Windows.Forms;
using FormLocationLib;

namespace FormLocation
{
    public partial class MainForm : Form
    {
        private readonly WindowRecorder _recorder;
        private string _settingsPath;
        public string RecorderSettings => _settingsPath;
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
