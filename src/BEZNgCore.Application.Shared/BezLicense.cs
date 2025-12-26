using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BEZNgCore
{
    public class BezLicense
    {
        private string _Salt = "HMS License";
        private string _strKey = "Brillantez Solution Pte Ltd";
        private byte[] encrypted;
        LicenseStatus lStatusFail = LicenseStatus.Fail;
        private byte[] Encrypt(string input)
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                byte[] buffer;
                Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(this._strKey, Encoding.ASCII.GetBytes(this._Salt));
                MD5 md = new MD5CryptoServiceProvider();
                provider.Key = bytes.GetBytes(0x10);
                provider.IV = bytes.GetBytes(0x10);
                ICryptoTransform transform = provider.CreateEncryptor(provider.Key, provider.IV);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(stream2))
                        {
                            writer.Write(input);
                        }
                        buffer = stream.ToArray();
                    }
                }
                return md.ComputeHash(buffer);
            }
        }

        private string FormatedEncrypt(string input)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(_strKey, Encoding.ASCII.GetBytes(_Salt));
                aesAlg.Key = keyGenerator.GetBytes(16);
                aesAlg.IV = keyGenerator.GetBytes(16);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream. ascii range 128
                            swEncrypt.Write(input);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Encoding.ASCII.GetString(encrypted);
        }

        private string FormatedDecrypt(string input)
        {
            byte[] iv = Encoding.ASCII.GetBytes(_Salt);
            byte[] encrptValue;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(_strKey, iv);
                aesAlg.Key = keyGenerator.GetBytes(16);
                aesAlg.IV = keyGenerator.GetBytes(16);
                ICryptoTransform encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(input);
                        }
                    }
                    encrptValue = msEncrypt.ToArray();
                }
            }
            return Encoding.ASCII.GetString(encrptValue);
        }

        private byte[] getHashedBytes(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return sec.ComputeHash(bt);
        }

        public string GenerateLicenceKey(string companyName, decimal iModuleNo, decimal iRooms, decimal iUsers, DateTime expireDate)
        {
            string input = string.Format("{0}-{1}-{2}-{3}-{4}", new object[] { this.removeSpace(companyName), iModuleNo, iRooms, iUsers, expireDate.ToString("yyyy-MM-dd") });
            byte[] bt = this.Encrypt(input);
            string licenceKey = this.get32bitFiexedFormat(bt);
            string str3 = this.getCheckSumStrings(licenceKey);
            return (licenceKey + str3);
        }

        public string GetLicenseExpiryDate(string licensekey, string saltkey)
        {
            try
            {
                string result;
                string sString = Decryption(licensekey, saltkey);
                if (sString != lStatusFail.ToString())
                {
                    string[] sArray = sString.Split('-');
                    string sStartEnd = sArray[sArray.Count() - 2] + "-" + sArray[sArray.Count() - 1] + "-" + sArray[sArray.Count() - 3];
                    result = sStartEnd;
                }
                else
                {
                    result = lStatusFail.ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                return lStatusFail.ToString();
            }
        }
        public string[] GetLicenseExpiryDate_New(string licensekey, string saltkey)
        {

            string hName = string.Empty;
            string sStartEnd = string.Empty;
            string rCount = string.Empty;
            string sString = Decryption(licensekey, saltkey);

            string[] sArray = sString.Split('-');
            sStartEnd = sArray[sArray.Count() - 2] + "-" + sArray[sArray.Count() - 1] + "-" + sArray[sArray.Count() - 3];
            rCount = sArray[sArray.Count() - 4];
            if (sArray.Count() == 7)
            {
                hName = sArray[sArray.Count() - 7] + "-" + sArray[sArray.Count() - 6];
            }
            else if (sArray.Count() == 8)
            {
                hName = sArray[sArray.Count() - 8] + "-" + sArray[sArray.Count() - 7] + "-" + sArray[sArray.Count() - 6];
            }
            else
            {
                hName = sArray[sArray.Count() - 6];
            }
            string[] result = { sStartEnd, rCount, hName };
            return result;

        }
        public string GenerateFormatedLicenceKey(string companyName, decimal iModuleNo, decimal iRooms, decimal iUsers, DateTime expireDate)
        {
            string input = string.Format("{0}-{1}-{2}-{3}-{4}", new object[] { this.removeSpace(companyName), iModuleNo, iRooms, iUsers, expireDate.ToString("yyyy-MM-dd") });
            string encrypt = this.FormatedEncrypt(input);
            byte[] bt = this.getHashedBytes(encrypt);
            string licenceKey = this.get32bitFiexedFormat(bt);
            string str3 = this.getCheckSumStrings(licenceKey);
            return (licenceKey + str3);
            return licenceKey;
        }

        public string GenerateMaintainanceKey(string licenceKey, DateTime expriyDate)
        {
            string str = expriyDate.ToString("yyMMdd");
            string input = string.Format("{0}-{1}", licenceKey, str);
            byte[] bt = this.Encrypt(input);
            string str3 = this.get32bitFiexedFormat(bt);
            string str4 = this.getCheckSumStrings(str3);
            return (str3 + str4);
        }

        private string get32bitFiexedFormat(byte[] bt)
        {
            string str = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte num2 = bt[i];
                int num3 = num2;
                int num4 = num3 & 15;
                int num5 = (num3 >> 4) & 15;
                if (num5 > 9)
                {
                    char ch = (char)((num5 - 10) + 0x41);
                    str = str + ch.ToString();
                }
                else
                {
                    str = str + num5.ToString();
                }
                if (num4 > 9)
                {
                    str = str + ((char)((num4 - 10) + 0x41)).ToString();
                }
                else
                {
                    str = str + num4.ToString();
                }
                if (((i + 1) != bt.Length) && (((i + 1) % 3) == 0))
                {
                    str = str + "-";
                }
            }
            return str;
        }

        private string getCheckSumStrings(string licenceKey)
        {
            if (!this.preValidateFormats(licenceKey))
            {
                return null;
            }
            char[] chArray = new char[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            char[] chArray2 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] chArray3 = licenceKey.Replace("-", "").ToArray<char>();
            int num = 0;
            for (int i = 0; i < chArray3.Length; i++)
            {
                num += chArray3[i] + (10 * i);
            }
            int index = num % 0x1a;
            int num4 = num % 10;
            char ch = chArray[index];
            char ch2 = chArray2[num4];
            return string.Format("{0}{1}", ch, ch2);
        }

        private bool preValidateFormats(string inputKey)
        {
            if (string.IsNullOrEmpty(inputKey))
            {
                return false;
            }
            if (inputKey.Length != 0x25)
            {
                return false;
            }
            string[] strArray = inputKey.Split(new char[] { '-' });
            if (strArray.Length != 6)
            {
                return false;
            }
            for (int i = 0; i < 5; i++)
            {
                if (strArray[i].Length != 6)
                {
                    return false;
                }
            }
            if (strArray[5].Length != 2)
            {
                return false;
            }
            return true;
        }

        private string removeSpace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            char[] chArray = input.ToArray<char>();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] != ' ')
                {
                    builder.Append(chArray[i]);
                }
            }
            return builder.ToString();
        }

        public static string Encryption(string plainText, string saltkey, string saltvalue)
        {
            string PasswordHash = "Brillantez";
            string SaltKey = saltkey;
            string VIKey = saltvalue;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string Decryption(string encryptedText, string saltkey)
        {
            try
            {
                string PasswordHash = "Brillantez";
                string SaltKey = saltkey;
                string VIKey = "123456789012345678";
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                //return "fail";
                return lStatusFail.ToString();
            }
        }
        public static string EncryptInput(string plainText, string saltKey)
        {
            string PasswordHash = "Brillantez";
            string SaltKey = saltKey;//Brillantez6
            string VIKey = "123456789012345678";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
        public static string EncryptInputForEncryptCard(string plainText, string saltKey)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                plainText = "Brillantez";
            }

            string PasswordHash = "Brillantez";
            string SaltKey = saltKey;//Brillantez6
            string VIKey = "123456789012345678";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);            
        }
        public static string DecryptInput(string encryptedText, string saltkey)
        {
            string decrypt = string.Empty;
            try
            {
                string PasswordHash = "Brillantez";
                string SaltKey = saltkey;
                string VIKey = "123456789012345678";
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                decrypt = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                decrypt = encryptedText;
            }
            return decrypt;
        }
    }

    public enum LicenseStatus
    {
        Success,
        Fail,
        Expiry,
        Warning
    }
}
