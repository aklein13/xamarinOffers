using App3.Models;
using App3.Services;
using App3.UWP.Implementations;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

[assembly: Xamarin.Forms.Dependency(typeof(ShareImplementation))]
namespace App3.UWP.Implementations
{
    class ShareImplementation : IShare
    {
        public async Task ShareAsync(Offer item)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(item.Url);
            Clipboard.SetContent(dataPackage);
        }
    }
}
