using System;
using System.Collections.Generic;
using System.Text;

namespace BEZNgCore
{
    public static class DataTypeConvertor
    {
        private static string skey = "Brillantez6";

        public static string GetEncryptedString(string input)
        {

            return BezLicense.EncryptInput(input, skey);
        }
        public static string GetEncryptedStringCardInfos(string input)
        {

            return BezLicense.EncryptInputForEncryptCard(input, skey);
        }
        public static string GetDecryptedString(string input)
        {
            return BezLicense.DecryptInput(input, skey);
        }
        
        //public static string Decode(string cipherText)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(cipherText))
        //        {
        //            cipherText = HttpContext.Current.Server.UrlDecode(cipherText.Trim());
        //        }
        //        return cipherText;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}


        //public static string Encode(string clearText)
        //{
        //    try
        //    {
        //        return HttpContext.Current.Server.UrlEncode(clearText.Trim());
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public static string Replicate(string v1, int v2)
        {
            string returnOut = "";
            for (int x = 0; x < v2; x++)
            {
                returnOut = returnOut + x.ToString();
            }
            return returnOut;
        }

        public static string GetCleanSQLString(string inputSQL)
        {
            string strReturnValue = "";
            strReturnValue = inputSQL.Replace("'", "\'");
            return strReturnValue;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder builder = new StringBuilder(ba.Length * 2);
            foreach (byte num in ba)
            {
                builder.AppendFormat("{0:x2}", num);
            }
            return builder.ToString();
        }

        public static string ConvertNullableDateTimeToString(DateTime? input)
        {
            string str = "";
            if (input.HasValue)
            {
                str = input.Value.ToString("dd/MM/yyyy");
            }
            return str;
        }

        public static string ConvertNullToString(object o)
        {
            string str = "";
            if (o!=null)
            {
                str = o.ToString();
            }
            return str;
        }
        public static List<Guid?> ConvertObjectArrayToNullableGuidList(object[] objs)
        {
            List<Guid?> list = new List<Guid?>();
            for (int i = 0; i < objs.Length; i++)
            {
                list.Add(ConvertObjectToNullableGuid(objs[i]));
            }
            return list;
        }

        public static Guid ConvertObjectToGuid(object input)
        {
            Guid guid = new Guid();
            if (input != null)
            {
                guid = new Guid(input.ToString());
            }
            return guid;
        }

        public static Guid ConvertStringToGuid(string input)
        {
            Guid guid = new Guid();
            if (input != "")
            {
                guid = new Guid(input.ToString());
            }
            return guid;
        }

        public static char? ConvertObjectToNullableChar(object input)
        {
            char? nullable = null;
            if (input != null)
            {
                nullable = new char?(Convert.ToChar(input));
            }
            return nullable;
        }
        public static char? ConvertEmptyStringToNullableChar(object input)
        {
            char? nullable = null;
            if (input != null)
            {
                if (!string.IsNullOrEmpty(input.ToString()))
                {
                    nullable = new char?(Convert.ToChar(input));
                }

            }
            return nullable;
        }
        public static DateTime? ConvertObjectToNullableDateTime(object input)
        {
            DateTime time;
            DateTime? nullable = null;
            if ((input != null) && DateTime.TryParse(input.ToString(), out time))
            {
                nullable = new DateTime?(time);
            }
            return nullable;
        }
        public static DateTime? ConvertObjectToNullableDateTimeWithFormat(object input)
        {
            DateTime time;
            DateTime? nullable = null;
            string temp = input.ToString();
            if ((input != null) && DateTime.TryParse(temp, out time))
            {
                nullable = new DateTime?(time);
            }
            return nullable;
        }

        public static decimal? ConvertObjectToNullableDecimal(object input)
        {
            decimal num;
            decimal? nullable = null;
            if ((input != null) && decimal.TryParse(input.ToString(), out num))
            {
                nullable = new decimal?(Math.Round(num, 2));
            }
            return nullable;
        }

        public static double? ConvertObjectToNullableDouble(object input)
        {
            double num;
            double? nullable = null;
            if ((input != null) && double.TryParse(input.ToString(), out num))
            {
                nullable = new double?(Math.Round(num, 2));
            }
            return nullable;
        }
       
        public static Guid? ConvertObjectToNullableGuid(object input)
        {
            Guid? nullable = null;
            try
            {
                if (input != null)
                {
                    nullable = new Guid(input.ToString());
                }
            }
            catch (Exception)
            {
            }
            return nullable;
        }

        public static int? ConvertObjectToNullableInteger(object input)
        {
            int num;
            int? nullable = null;
            if ((input != null) && int.TryParse(input.ToString(), out num))
            {
                nullable = new int?(num);
            }
            return nullable;
        }

        public static string ConvertObjectToString(object input)
        {
            string str = null;
            if (input != null)
            {
                str = input.ToString();
            }
            return str;
        }

        public static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] buffer = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
        }
        public static Guid? GetGuid(string skey)
        {
            Guid? nullable;
            Guid? nullable1;
            Guid? nullable2;
            if (!string.IsNullOrEmpty(skey))
            {
                Guid key = new Guid();
                if (Guid.TryParseExact(skey, "D", out key))
                {
                    nullable2 = new Guid?(key);
                }
                else
                {
                    nullable1 = null;
                    nullable2 = nullable1;
                }
                nullable = nullable2;
            }
            else
            {
                nullable1 = null;
                nullable = nullable1;
            }
            return nullable;
        }

    }
}
