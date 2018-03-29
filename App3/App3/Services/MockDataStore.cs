using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Json;

using App3.Models;
using System.Net;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(App3.Services.MockDataStore))]
namespace App3.Services
{
    public class MockDataStore : IDataStore<Offer>
    {
        List<Offer> items;
        private string ApiUrl = "http://api.voxm.live/api/1/";

        public MockDataStore()
        {
            string sampleUrl = "https://otodompl-imagestmp.akamaized.net/images_otodompl/23505315_3_1280x1024_wygodne-mieszkanie-na-osiedlu-aquarius-w-sopocie-mieszkania_rev011.jpg";
            items = new List<Offer>();
            var mockItems = new List<Offer>
            {
                new Offer { Id = Guid.NewGuid().ToString(), Title = "First item", Description="This is an item description.", Images=sampleUrl},
                new Offer { Id = Guid.NewGuid().ToString(), Title = "Second item", Description="This is an item description.", Images=sampleUrl},
                new Offer { Id = Guid.NewGuid().ToString(), Title = "Third item", Description="This is an item description.", Images=sampleUrl},
                new Offer { Id = Guid.NewGuid().ToString(), Title = "Fourth item", Description="This is an item description.", Images=sampleUrl},
                new Offer { Id = Guid.NewGuid().ToString(), Title = "Fifth item", Description="This is an item description.", Images=sampleUrl},
                new Offer { Id = Guid.NewGuid().ToString(), Title = "Sixth item", Description="This is an item description.", Images=sampleUrl},
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
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(ApiUrl));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    await Task.FromResult(jsonDoc);
                }
            }
            return await Task.FromResult(false);
        }
    }
}