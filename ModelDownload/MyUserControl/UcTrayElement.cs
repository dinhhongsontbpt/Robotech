using VisionMonitor.Class;
using VisionMonitor.Common;
using System.Drawing;
using System.Windows.Forms;

namespace VisionMonitor.MyUserControl
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
