using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateSelector.API.Helpers {
    public static class DateTimeExtensions {

        public static DateTime EndOfDay(this DateTime theDate) {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
    }
}
