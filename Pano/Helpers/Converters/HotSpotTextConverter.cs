using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Pano.Model.Db.HotSpots;

namespace Pano.Helpers.Converters
{
    public class HotSpotTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HotSpot spot && !string.IsNullOrWhiteSpace(spot?.Text))
            {
                return $"HotSpot: {spot.Text}";
            }

            return "HotSpot";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
