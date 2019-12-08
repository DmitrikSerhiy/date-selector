using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using DateSelector.API.Dto;
using DateSelector.API.Helpers;
using DateSelector.API.Services;
using DateSelector.Persistence;
using DateSelector.Persistence.Models;

namespace DateSelector.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DateComparisonController : ControllerBase {

        private readonly DateMapper _dateMapper;
        private readonly DateContext _context;
        private readonly FilterService _filterService;

        public DateComparisonController(
            DateContext context, 
            DateMapper dateMapper, 
            FilterService filterService) {
            _context = context;
            _dateMapper = dateMapper;
            _filterService = filterService;
        }

        /// <summary>
        /// Endpoint for getting all available in database date
        /// </summary>
        [HttpGet]
        [Route("All")]
        public ActionResult<DateComparisonObjectDto[]> All() {
            var dateModels = _context.DateComparisonObjects.ToList();
            return Ok(dateModels
                .Select(d => new DateComparisonObjectDto {
                    FirstDate = _dateMapper.MapDate(d.FirstDate),
                    SecondDate = _dateMapper.MapDate(d.SecondDate)
                })
                .ToArray());
        }

        /// <summary>
        /// Endpoint for saving new date interval
        /// </summary>
        /// <param name="firstDate">Date in format dd-MM-yyyy</param>
        /// <param name="secondDate">Date in format dd-MM-yyyy. It has to be later than second date</param>
        [HttpPost]
        public IActionResult Add(String firstDate, String secondDate) {
            try {
                var mappedFirstDate = _dateMapper.MapDate(firstDate);
                var mappedSecondDate = _dateMapper.MapDate(secondDate);

                if (mappedFirstDate > mappedSecondDate) {
                    return Ok(Array.Empty<DateComparisonObjectDto>());
                }

                _context.DateComparisonObjects.Add(
                    new DateComparisonObject(mappedFirstDate, mappedSecondDate));

                _context.SaveChanges();

            } catch (ArgumentException) {
                return BadRequest();
            } catch (NotSupportedException) {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Endpoint for searching date withing first and second date.
        /// </summary>
        /// <param name="firstDate">Date in format dd-MM-yyyy</param>
        /// <param name="secondDate">Date in format dd-MM-yyyy. It has to be after (later) than second date</param>
        [HttpGet]
        [Route("Filter")]
        public ActionResult<DateComparisonObjectDto[]> Filter(String firstDate, String secondDate) {
            try {
                var mappedFirstDate = _dateMapper.MapDate(firstDate);
                var mappedSecondDate = _dateMapper.MapDate(secondDate);

                if (mappedFirstDate > mappedSecondDate) {
                    return Ok(Array.Empty<DateComparisonObjectDto>());
                }

                return Ok(_filterService.Filter(mappedFirstDate, mappedSecondDate));

            } catch (ArgumentException) {
                return BadRequest();
            } catch (NotSupportedException) {
                return BadRequest();
            }
        }
    }
}
