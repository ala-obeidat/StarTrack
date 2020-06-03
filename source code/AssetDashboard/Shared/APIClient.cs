using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace StarTrack.Dashboard.Shared
{
    public class APIClient
    {
        private volatile static HttpClient _client;
        private static object _lockObject = new object();
        public static HttpClient Instance
        {
            get
            {
                if (_client == null)
                    lock (_lockObject)
                    {
                        _client = new HttpClient();
                        _client.DefaultRequestHeaders.Add("ApplicationKey", "UEtTXCszXTQhYg1jXWMzNVxk2bgw2JE22aY42YxjUjXZtzTZvGXZsjPZrjHZpDnZjGQ=");
                        _client.DefaultRequestHeaders.Add("ClientId", "50993C9C8078417BB2D1D9D64C64A65E11139F318AB34BF4AFD06D5EA385110E");
                        _client.DefaultRequestHeaders.Add("SecretKey", "UENVWxdlK2ZoZWVkADlDNXFiA2MRZWFhBDABNn40U2JANWI1cjlROA9mCzEDNAYwQ2MVOXBhNTYPMQVlOzViNVNlD2ZDZVZkCzlLNX5iYGNVZQ1hLzBmNnY0fmImNQA1fDlDOBBmKTEDNCowAmMoOXRhfzYSMWxlczUtNQVlV2Z/ZVhkCTlgNSJi");
                    }
                return _client;
            }
        }
    }
}