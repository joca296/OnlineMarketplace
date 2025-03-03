﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace OnlineMarketPlace.EfCommands
{
    class Functions
    {
        public static string BaseUrl
        {
            get
            {
                return "https://localhost:44387";
            }
        }

        public static SmtpClient SmtpClient
        {
            get
            {
                return new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("joca296testingemail@gmail.com", "TestingAccount1!"),
                    EnableSsl = true
                };
            }
        }

        public static string CreateSha256Hash(string input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GetUniqID()
        {
            var ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            double t = ts.TotalMilliseconds / 1000;

            int a = (int)Math.Floor(t);
            int b = (int)((t - Math.Floor(t)) * 1000000);

            return a.ToString("x8") + b.ToString("x5");
        }
    }
}
