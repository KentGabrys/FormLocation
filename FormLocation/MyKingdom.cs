using System.Windows.Forms;
using FormLocationLib;

namespace FormLocation
{
    public partial class MyKingdom : Form
    {
        private FormLocator _locator;

        public MyKingdom()
        {
            InitializeComponent();
            _locator = new FormLocator(this);
        }
    }
}
