using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Net.Http.Headers;
using System.IO;
using System.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace EzTimeAndroid
{
    class Global
    {
        public async Task<string> FetchAsync(string url)
        {

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));



            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {

                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {

                    StreamReader sr = new StreamReader(stream);
                    string result = sr.ReadToEnd();

                    return result;
                }
            }
        }
    }
}