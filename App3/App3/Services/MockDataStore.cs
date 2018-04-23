﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Json;

using App3.Models;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
[assembly: Xamarin.Forms.Dependency(typeof(App3.Services.MockDataStore))]
namespace App3.Services
{
    public class MockDataStore : IDataStore<Offer>
    {
        List<Offer> items;
        private string ApiUrl = "http://35.225.207.86:8000/api/v1/offers/";
        private string ApiKey = "csrftoken=FztzSbLtmjGLQcR3HXMGdM3BT0LdDgpf9bq02HIzWVchlaqzIvAUdNsmpjirfc16; sessionid=b9tw49rigwlvjufz2nst3et07yfbxgge";
        
        public MockDataStore()
        {
            //string sampleUrl = "https://otodompl-imagestmp.akamaized.net/images_otodompl/23505315_3_1280x1024_wygodne-mieszkanie-na-osiedlu-aquarius-w-sopocie-mieszkania_rev011.jpg";
            items = new List<Offer>();
            var mockItems = new List<Offer>
            {
                new Offer { Id = Guid.NewGuid().ToString(), Title = "First item", Description="This is an item description."},
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Offer item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Offer item)
        {
            var _item = items.Where((Offer arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Offer item)
        {
            var _item = items.Where((Offer arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Offer> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Offer>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<JsonValue> FetchOffersAsync(string city)
        {
            string newUri = ApiUrl;
            if (city != "None")
            {
                newUri += "?city=" + city;
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(newUri));
            request.Headers["Cookie"] = ApiKey;
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    return await Task.FromResult(jsonDoc);
                }
            }
        }
    }
}