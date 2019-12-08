using DateSelector.API.Dto;
using DateSelector.API.Helpers;
using DateSelector.Persistence;
using DateSelector.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DateSelector.API.Services {
    public class FilterService {

        private readonly DateContext _context;
        private readonly DateMapper _dateMapper;

        public FilterService(DateContext context, 
            DateMapper dateMapper) {
            _context = context;
            _dateMapper = dateMapper;
        }

        public DateComparisonObjectDto[] Filter(Int64 firstDate, Int64 secondDate) {
            var dateIntervals = _context.DateComparisonObjects.ToList();
            var filteredDates = new List<DateComparisonObjectDto>();

            foreach (var dInterval in dateIntervals) {
                if (firstDate == dInterval.FirstDate || firstDate == dInterval.SecondDate) {
                    AddInterval(filteredDates, dInterval);
                    continue;
                }
                if (secondDate == dInterval.FirstDate || secondDate == dInterval.SecondDate) {
                    AddInterval(filteredDates, dInterval);
                    continue;
                }

                if (dInterval.FirstDate < firstDate && dInterval.SecondDate > firstDate) {
                    AddInterval(filteredDates, dInterval);
                    continue;
                }

                if (dInterval.FirstDate > firstDate && dInterval.SecondDate < secondDate) {
                    AddInterval(filteredDates, dInterval);
                    continue;
                }

                if (dInterval.FirstDate > firstDate && dInterval.SecondDate > secondDate) {
                    AddInterval(filteredDates, dInterval);
                }
            }

            return filteredDates.ToArray();
        }

        private void AddInterval(ICollection<DateComparisonObjectDto> list, DateComparisonObject dateInterval) {
            list.Add(new DateComparisonObjectDto {
                FirstDate = _dateMapper.MapDate(dateInterval.FirstDate),
                SecondDate = _dateMapper.MapDate(dateInterval.SecondDate)
            });
        }
    }
}
