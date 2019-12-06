using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using DateSelector.Persistence;
using DateSelector.Persistence.Models;

namespace DateSelector.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DateComparisonController : ControllerBase {

        private readonly ILogger<DateComparisonController> _logger;
        private readonly DateContext _context;

        public DateComparisonController(ILogger<DateComparisonController> logger, 
            DateContext context) {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Endpoint for getting all available in database date
        /// </summary>
        /// <param name="firstDate">Date in format dd-MM-yyyy</param>
        /// <param name="secondDate">Date in format dd-MM-yyyy. It has to be after (later) than second date</param>
        [HttpGet]
        [Route("All")]
        public ActionResult<String[]> All() {
            var dateModels = _context.DateComparisonObjects.ToList();
            return Ok(dateModels.Select(d => MapDate(d.Date)).ToArray());
        }


        [HttpPost]
        public IActionResult Add(String firstDate, String secondDate) {
            try {
                var mappedFirstDate = MapDate(firstDate);
                var mappedSecondDate = MapDate(secondDate);

                if (mappedFirstDate > mappedSecondDate) {
                    return BadRequest();
                }

                _context.DateComparisonObjects.Add(new DateComparisonObject(mappedFirstDate));
                _context.DateComparisonObjects.Add(new DateComparisonObject(mappedSecondDate));

            } catch (ArgumentNullException) {
                return BadRequest();
            } catch (NotSupportedException) {
                return BadRequest();
            }

            return Ok();
        }

        private String MapDate(Int64 dateInTickFormat) {
            return new DateTime(dateInTickFormat).ToString("dd-MM-yyyy");
        }

        private Int64 MapDate(String date) {
            if (String.IsNullOrWhiteSpace(date)) {
                throw new ArgumentNullException(nameof(date));
            }
            try {
                DateTime.TryParse(
                    date,
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.AdjustToUniversal,
                    out var convertedDate);
                return convertedDate.Ticks;
            } catch (NotSupportedException ex) {
                _logger.LogError("Invalid date format", ex);
                throw;
            }
        }
    }
}
