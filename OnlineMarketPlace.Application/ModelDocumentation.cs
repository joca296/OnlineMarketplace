using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace OnlineMarketPlace.Application
{
    public class ModelDocumentation
    {
        public static string XmlFile
        {
            get
            {
                return $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            }
        }

        public static string BasePath
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }
    }
}
