using System.Threading.Tasks;
using App3.Models;
using App3.Services;
using Plugin.Share;

[assembly: Xamarin.Forms.Dependency(typeof(App3.Droid.Implementations.ShareImplementation))]
namespace App3.Droid.Implementations
{
    class ShareImplementation : IShare
    {
        public async Task ShareAsync(Offer item)
        {
            await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Title = item.Title,
                Text = item.Url
            });
        }
    }
}
