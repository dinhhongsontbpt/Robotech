using System.Drawing;
using System.Windows.Forms;

namespace ModelDownload.Common
{
    public class ClassInvokeControl
    {
        public void ControlTextInvoke(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.Text = text;
                });
            }
            else
            {
                control.Text = text;
            }
        }
        public void ControlBackColorInvoke(Control control, Color color)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.BackColor = color;
                });
            }
            else
            {
                control.BackColor = color;
            }
        }
        public void ControlEnableInvoke(Control control, bool enable)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.Enabled = enable;
                });
            }
            else
            {
                control.Enabled = enable;
            }
        }
        public void ControlForeColorInvoke(Control control, Color color)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.ForeColor = color;
                });
            }
            else
            {
                control.ForeColor = color;
            }
        }

        public void BackgroundImageInvoke(Control control, Image image)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.BackgroundImage = image;
                });
            }
            else
            {
                control.BackgroundImage = image;
            }
        }
    }
}
