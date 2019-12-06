using System;
using System.Collections.Generic;
using System.Text;
using DateSelector.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DateSelector.Persistence {
    public sealed class DateContext : DbContext {
        public DbSet<DateComparisonObject> DateComparisonObjects { get; set; }

        public DateContext(DbContextOptions<DateContext> options) : base(options) { }
    }
}
