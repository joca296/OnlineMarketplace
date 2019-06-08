using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Helpers
{
    public class ImageUploadHelper
    {
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpeg", ".jpg", ".gif", ".png" };
        public static double MaxSize
        {
            get
            {
                return 8192 * Math.Pow(1024,2);
            }
        }
    }
}
