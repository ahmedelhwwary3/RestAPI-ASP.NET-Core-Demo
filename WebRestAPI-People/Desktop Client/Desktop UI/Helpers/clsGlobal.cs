using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_UI.Helpers
{
    internal class clsGlobal
    {
        public static readonly HttpClient httpClient;
        static clsGlobal()
        {
            httpClient=new HttpClient();
            httpClient.BaseAddress =new Uri("https://localhost:7201/api/People/");
        }



    }
}
