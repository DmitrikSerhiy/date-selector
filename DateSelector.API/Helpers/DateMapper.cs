using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace DateSelector.API.Helpers {
    public class DateMapper {
        private readonly ILogger<DateMapper> _logger;
        private readonly String _dateFormat = "yyyy-MM-dd";

        public DateMapper(ILogger<DateMapper> logger) {
            _logger = logger;
        }

        public String MapDate(Int64 dateInTickFormat) {
            return new DateTime(dateInTickFormat).ToString(_dateFormat);
        }

        public Int64 MapDate(String date) {
            if (String.IsNullOrWhiteSpace(date)) {
                throw new ArgumentNullException(nameof(date));
            }

            if (!DateTime.TryParseExact(
                    date,
                    _dateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AdjustToUniversal,
                    out var convertedDate)) {
                _logger.LogWarning("Invalid date format");
                throw new ArgumentException(nameof(date));
            }
            return convertedDate.Ticks;
        }
    }
}
