using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using ZalandoShop.Models;

namespace ZalandoShop.Converters
{
    class ImageUriToBitmapImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<Image> Images = (value as Media).images;
            string imageUrl="";
            foreach (Image i in Images)
            {
                imageUrl = i.smallUrl;
            }
            try
            {
                return new BitmapImage(new Uri((string)imageUrl));
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
