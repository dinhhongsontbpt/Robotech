using System.Text;
using System;

namespace Cutreson_Utility
{
    public class clsText
    {
        public enum TextType
        {
            LowercaseLetter,
            UppercaseLetter,
            OtherLetter,
            DecimalDigitNumber,
            OtherPunctuation,
            SpaceSeparator
        }
        public static bool TextCheck(TextType TT, string Word)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder(Word);
                for (int i = 0; i < Word.Length; i++)
                {
                    char c = stringBuilder[i];
                    string text = char.GetUnicodeCategory(stringBuilder[i]).ToString();
                    if (text != TT.ToString())
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show("Text 의 형태를 확인 하다가 Error 가 발생하였습니다.");
                return false;
            }
        }
    }
}
