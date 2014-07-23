using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using AdvancedREI.Net.Http.Compression;
using Newtonsoft.Json;
using System.Data;

namespace PhoneApp1
{
    class IOandConversion
    {


        public Task<string> readCompressedHtmlPage(String pageAdress)
        {
            CompressedHttpClientHandler handler = new CompressedHttpClientHandler();
            HttpClient client = new HttpClient(handler);

            //TODO Actually should get whole GET response with GetAsync and check that status code is valid.
            Task<string> response = client.GetStringAsync(pageAdress);

            return response;
        }

        public  readJsonTextToDataset(String text)
        {
            


        }

    }
}
