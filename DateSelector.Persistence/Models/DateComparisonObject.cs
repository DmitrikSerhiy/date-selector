using System;

namespace DateSelector.Persistence.Models {
    public class DateComparisonObject {
        public Guid Id { get; set; }
        public Int64 Date { get; set; }

        public DateComparisonObject(Int64 date) {
            Id = Guid.NewGuid();
            Date = date;
        }
    }
}
