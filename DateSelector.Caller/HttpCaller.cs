using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DateSelector.Caller {
    public class HttpCaller {
        private const String _apiUrl = "http://localhost:57572/";
        private const String _addDatePartialUrl = "DateComparison";
        private const String _filterDatePartialUrl = "DateComparison/Filter";
        private const String _showAllPartialUrl = "DateComparison/All";


        public async Task<Boolean> AddDate(String firstDate, String secondDate) {
            var query = $"?firstDate={firstDate}&secondDate={secondDate}";
            using (var client = new HttpClient()) {
                var result = await client.PostAsync($"{_apiUrl}{_addDatePartialUrl}{query}", new StringContent(""));
                return result.StatusCode.Equals(HttpStatusCode.OK);
            }
        }

        public async Task<DateRangeModel[]> Filter(String firstDate, String secondDate) {
            using (var client = new HttpClient()) {
                var result = await client
                    .GetAsync($"{_apiUrl}{_filterDatePartialUrl}?firstDate={firstDate}&secondDate={secondDate}");
                if (!result.IsSuccessStatusCode) {
                    return Array.Empty<DateRangeModel>();
                }
                var resultJson = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DateRangeModel[]>(resultJson);
            }
        }

        public async Task<DateRangeModel[]> ShowAll() {
            using (var client = new HttpClient()) {
                var result = await client.GetAsync($"{_apiUrl}{_showAllPartialUrl}");
                if (!result.IsSuccessStatusCode) {
                    return Array.Empty<DateRangeModel>();
                }
                var resultJson = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DateRangeModel[]>(resultJson);
            }
        }
    }
}
