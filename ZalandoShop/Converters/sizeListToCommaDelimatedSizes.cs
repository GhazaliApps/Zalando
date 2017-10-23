using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using ZalandoShop.Models;

namespace ZalandoShop.Converters
{
    class sizeListToCommaDelimatedSizes : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<string> allAvialbleSizes = new List<string>();

            foreach (Unit s in (List<Unit>)value)
            {
                if (s!=null)
                allAvialbleSizes.Add( s.size.ToString());
            }
            //return string.Join(", ", allAvialbleSizes);
            return allAvialbleSizes[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
