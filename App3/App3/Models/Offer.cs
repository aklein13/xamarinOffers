namespace App3.Models
{
    public class Offer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public string Url { get; set; }
        public string District { get; set; }
        public string Rooms { get; set; }
        private string[] _Images = new string[25];
        public string[] Images
        {
            get
            {
                return _Images;
            }
            set
            {
                _Images = value;
            }
        }
        public bool IsFavourite { get; set; }
    }
}