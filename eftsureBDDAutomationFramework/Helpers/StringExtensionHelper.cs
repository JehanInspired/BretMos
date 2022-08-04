using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TakealotBDDAutomationFramework.Helpers
{
    
    public static class StringExtensionHelper
    {
        static Random _uniqueIdRandom = new Random(DateTime.Now.Millisecond);
        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //const string NUMBERS = "0123456789";

        public static string GenerateRandomCharacters(byte length = 10, bool alwaysStartWithLetter = true)//removed "this string s,"
        {
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                var randomNum = _uniqueIdRandom.Next(CHARS.Length);

                if (alwaysStartWithLetter && i == 0 && randomNum >= CHARS.Length - 10)
                    randomNum -= 10;

                stringChars[i] = CHARS[randomNum];
            }

            return new string(stringChars);
        }

        public static string GenerateRandomNumbers(byte length = 10, string NUMBERS = "0123456789")
        {
            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                var randomNum = _uniqueIdRandom.Next(NUMBERS.Length);

                stringChars[i] = NUMBERS[randomNum];
            }

            return new string(stringChars);
        }

        internal static string GenerateDateTimeID()
        {
            string dtid = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
            return RemoveSpecialCharacters(dtid);            
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string RemoveSpecificCharacter(this string str, char charToRemove)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ( c != charToRemove)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
