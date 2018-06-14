using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace Rowatch2.Librarys
{
    public static class Helper
    {
        public static void FixWindowPosition(Form form)
        {
            Screen screen = Screen.FromHandle(form.Handle);

            if (form.Left < screen.WorkingArea.Left)
            {
                form.Left = screen.WorkingArea.Left;
            }
            else if (form.Left + form.Width > screen.WorkingArea.Right)
            {
                form.Left = screen.WorkingArea.Right - form.Width;
            }

            if (form.Top < screen.WorkingArea.Top)
            {
                form.Top = screen.WorkingArea.Top;
            }
            else if (form.Top + form.Height > screen.WorkingArea.Bottom)
            {
                form.Top = screen.WorkingArea.Bottom - form.Height;
            }
        }

        public static void CenterForm(this Form child, Form main)
        {
            child.Left = main.Left + main.Width / 2 - child.Width / 2;
            child.Top = main.Top + main.Height / 2 - child.Height / 2;
        }

        public static string GetFileMD5(string path)
        {
            string result = string.Empty;

            try
            {
                byte[] fileData = File.ReadAllBytes(path);

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] md5Data = md5.ComputeHash(fileData);

                result = BitConverter.ToString(md5Data);
                result = result.Replace("-", "").ToLower();
            }
            catch (Exception)
            {
            }

            return result;
        }

        public static string GetNodeValue(XmlNode node, string childNodeName)
        {
            string result = string.Empty;

            XmlNode childNode = node[childNodeName];
            if (childNode != null)
            {
                result = childNode.InnerText;
            }

            return result;
        }

        public static int GetHexValue(string value)
        {
            int result = 0;
            int.TryParse(value.Trim(), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out result);

            return result;
        }

        public static bool IsNumber(string value)
        {
            foreach (char ch in value)
            {
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }

            return true;
        }


        public static bool IsWindow(Form form)
        {
            if (form != null && !form.IsDisposed && !form.Disposing)
            {
                return true;
            }

            return false;
        }


        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }


        public static float GetPercent(int value, int maxValue)
        {
            float result = 0f;
            if (value >= 0 && maxValue >= value)
            {
                result = (float)value / maxValue;
            }

            return result;
        }
        public static int GetPercentInteger(int value, int maxValue)
        {
            int result = 0;

            float percentValue = GetPercent(value, maxValue);
            result = (int)(percentValue * 100);

            return result;
        }
    }
}