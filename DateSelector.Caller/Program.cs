using System;
using System.Linq;
using System.Threading.Tasks;

namespace DateSelector.Caller {
    public class Program {

        private static String _firstDate;
        private static String _secondDate;
        private static readonly HttpCaller Caller = new HttpCaller();

        static async Task Main(String[] args) {
            Console.WriteLine($"1 - Add new date range; 2 - filter date; 3 - show all");
            var key = Console.ReadLine();
            do {
                if (key == "1") {
                    SelectDate();
                    if (await Caller.AddDate(_firstDate, _secondDate)) {
                        Console.WriteLine("Success");
                    } else {
                      Console.WriteLine("Failed");  
                    }

                    if (!ShowNextOperation()) {
                        break;
                    }
                    ShowMessage(out key);
                } else if (key == "2") {
                    SelectDate();
                    
                    var result = await Caller.Filter(_firstDate, _secondDate);
                    ShowResult(result);

                    if (!ShowNextOperation()) {
                        break;
                    }
                    ShowMessage(out key);

                } else if (key == "3") {
                    var result = await Caller.ShowAll();
                    ShowResult(result);

                    if (!ShowNextOperation()) {
                        break;
                    }
                    ShowMessage(out key);
                } else {
                    key = Console.ReadLine();
                }
            } while (true);
        }

        static void SelectDate() {
            Console.Write("Enter first date: ");
            _firstDate = Console.ReadLine();
            Console.WriteLine($"First date selected: {_firstDate}");
            Console.Write("Enter second date: ");
            _secondDate = Console.ReadLine();
            Console.WriteLine($"Date range selected: {_firstDate} - {_secondDate}");
            Console.WriteLine("calling API...");
        }

        static void ShowResult(DateRangeModel[] dateModel) {
            if (!dateModel.Any()) {
                Console.WriteLine("No data");
            }
            foreach (var date in dateModel) {
                Console.Write($"{nameof(date.FirstDate)}: {date.FirstDate} - ");
                Console.Write($"{nameof(date.SecondDate)}: {date.SecondDate}");
                Console.WriteLine();
            }
        }

        static Boolean ShowNextOperation() {
            Console.WriteLine();
            Console.WriteLine("Exit? (yer - y) or (no - any character)");
            var answer = Console.ReadLine();
            return answer != "y";
        }

        static void ShowMessage(out String key) {
            Console.Clear();
            Console.WriteLine($"1 - Add new date range; 2 - filter date; 3 - show all");
            key = Console.ReadLine();
        }

    }
}
