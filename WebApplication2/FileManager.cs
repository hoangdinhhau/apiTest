using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class FileManager
    {
        public void OpenFile()
        {
            string env = RufaToolkit._ENVIRONMENT;

            RufaToolkit rufaToolkit = new RufaToolkit();
            string uat = rufaToolkit._UATCONFIG;
        }
    }
}