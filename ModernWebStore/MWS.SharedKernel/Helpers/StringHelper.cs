﻿using System.Security.Cryptography;
using System.Text;

namespace MWS.SharedKernel.Helpers
{
    public static class StringHelper
    {
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            value += "|54be1d80-b6d0-45c0-b8d7-13b3c798729f";
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sbString = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString();
        }
    }
}