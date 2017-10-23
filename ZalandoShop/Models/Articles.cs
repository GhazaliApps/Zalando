using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoShop.Views;
namespace ZalandoShop.Models
{
   public class Articles
    {
        public List<Content> content { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
    public class BrandFamily
    {
        public string key { get; set; }
        public string name { get; set; }
        public string shopUrl { get; set; }
    }

    public class Brand
    {
        public string key { get; set; }
        public string name { get; set; }
        public string logoUrl { get; set; }
        public string logoLargeUrl { get; set; }
        public string shopUrl { get; set; }
        public BrandFamily brandFamily { get; set; }
    }

    public class Attribute
    {
        public string name { get; set; }
        public List<string> values { get; set; }
    }

    public class Price
    {
        public string currency { get; set; }
        public double value { get; set; }
        public string formatted { get; set; }
    }

    public class OriginalPrice
    {
        public string currency { get; set; }
        public double value { get; set; }
        public string formatted { get; set; }
    }

    public class Unit
    {
        public string id { get; set; }
        public string size { get; set; }
        public Price price { get; set; }
        public OriginalPrice originalPrice { get; set; }
        public bool available { get; set; }
        public int stock { get; set; }
    }

    public class Image
    {
        public int orderNumber { get; set; }
        public string type { get; set; }
        public string thumbnailHdUrl { get; set; }
        public string smallUrl { get; set; }
        public string smallHdUrl { get; set; }
        public string mediumUrl { get; set; }
        public string mediumHdUrl { get; set; }
        public string largeUrl { get; set; }
        public string largeHdUrl { get; set; }
    }

    public class Media
    {
        public List<Image> images { get; set; }
    }

    public class Content
    {
        public string id { get; set; }
        public string modelId { get; set; }
        public string name { get; set; }
        public string shopUrl { get; set; }
        public string color { get; set; }
        public bool available { get; set; }
        public string season { get; set; }
        public string seasonYear { get; set; }
        public DateTime activationDate { get; set; }
        public List<object> additionalInfos { get; set; }
        public List<string> genders { get; set; }
        public List<string> ageGroups { get; set; }
        public Brand brand { get; set; }
        public List<string> categoryKeys { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<Unit> units { get; set; }
        public Media media { get; set; }
        public List<object> tags { get; set; }
    }

}
