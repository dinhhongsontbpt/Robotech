using System.Collections.Generic;
using static OfficeOpenXml.ExcelErrorValue;

namespace ModelDownload.Class
{
    public class TrayElement
    {
        public static List<string> ScrewNames = new List<string>();
        public string ScrewName;
        public bool IsGlue;
        public TrayElement(string screwName, bool isGlue = false)
        {
            ScrewName = screwName;
            IsGlue = isGlue;
        }
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(ScrewName.Trim());
        }
        public override string ToString()
        {
            int index = ScrewNames.IndexOf(ScrewName);
            string result = (index + 1).ToString("D2") + (IsGlue ? "01" : "00");
            return result;
        }
    }
}
