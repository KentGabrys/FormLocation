using System.Windows.Forms;
using FormLocationLib;

namespace FormLocation
{
    public partial class MainForm : Form
    {
        private readonly FormLocator _locator;

        #region For Testing Purposes Only
        // the following is for the test environment and not required for use
        public string LocatorSettings => _locator.SettingsPath;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            _locator = new FormLocator(this);
        }

        private void btnOpenMyKingdom_Click( object sender, System.EventArgs e )
        {
            var form = new MyKingdom();
            form.Show();
        }
    }
}
