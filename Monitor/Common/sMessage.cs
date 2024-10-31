using System.Windows.Forms;

namespace VisionMonitor.Common
{
    public class sMessage
    {
        public static void Info(string Content)
        {
            MessageBox.Show(Content, DialogCaptions.Infor, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(string Content)
        {
            MessageBox.Show(Content, DialogCaptions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult Question_YesNo(string Content)
        {
            return MessageBox.Show(Content, DialogCaptions.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Question_YesNoCancel(string Content)
        {
            return MessageBox.Show(Content, DialogCaptions.Infor, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
        public static void Error(string Content)
        {
            MessageBox.Show(Content, DialogCaptions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public class DialogCaptions
    {
        public const string Infor = "Infor";
        public const string Warning = "Warning";
        public const string Error = "Error";
        public const string Question = "Question";
        public const string SaveFile = "Save File...";
        public const string OpenFile = "Open File...";
    }
}
