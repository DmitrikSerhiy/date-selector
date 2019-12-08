using System;

namespace DateSelector.Persistence.Models {
    public class DateComparisonObject {
        public Guid Id { get; set; }
        public Int64 FirstDate { get; set; }
        public Int64 SecondDate { get; set; }

        public DateComparisonObject(Int64 firstDate, Int64 secondDate) {
            Id = Guid.NewGuid();
            FirstDate = firstDate;
            SecondDate = secondDate;
        }
    }
}
