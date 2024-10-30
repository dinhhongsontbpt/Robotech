using ModelDownload.Class;
using ModelDownload.Common;
using System.Drawing;
using System.Windows.Forms;

namespace ModelDownload.MyUserControl
{
    public partial class UcTrayElement : UserControl
    {
        public UcTrayElement()
        {
            InitializeComponent();
        }
        public void Update(TrayElement trayElement)
        {
            if (trayElement == null) return;
            ClassCommon.InvokeControl.ControlTextInvoke(lbScrewName, trayElement.ScrewName);
            if (trayElement.IsGlue)
            {
                ClassCommon.InvokeControl.ControlBackColorInvoke(lbGlue, Color.Lime);
            }
            else
            {
                ClassCommon.InvokeControl.ControlBackColorInvoke(lbGlue, Color.WhiteSmoke);
            }
        }
    }
}
