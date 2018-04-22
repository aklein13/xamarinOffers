using App3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.Services
{
    public interface IShare
    {
        Task ShareAsync(Offer item);
    }
}
