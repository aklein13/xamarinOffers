using System;

namespace App3.Models
{
    public class Offer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Images{ get; set; }
        public bool IsFavourite { get; set; }
    }
}