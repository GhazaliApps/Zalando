using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using ZalandoShop.Models;

namespace ZalandoShop.Converters
{
    class formatedPrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<string> allAvialblePrices = new List<string>();

            foreach (Unit s in (List<Unit>)value)
            { 
                if(s!=null)
                allAvialblePrices.Add(s.price.value.ToString()+" EUR");
            }

            //return string.Join(", ", allAvialblePrices);
            return allAvialblePrices[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
