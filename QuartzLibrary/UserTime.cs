using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamix.QuartzLibrary
{
    public enum MonthName : int
    {
        Jan = 0, Feb = 1, Mar = 2, Apr = 3, May = 4, Jun = 5, Jul = 6, Aug = 7, Sep = 8, Oct = 9, Nov = 10, Dec = 11
    }

    public enum DayName : int
    {
       Mon = 0, Tue = 1, Wed = 2, Thu = 3, Fri = 4, Sat = 5, Sun = 6
    }

    public class UserTime
    {
        public UserTime()
        {
            second = new UserTimeValuesSeconds();
            minute = new UserTimeValuesMinutes();
            hour = new UserTimeValuesHours();
            dayOfMonth = new UserTimeDayOfMonth();
            month = new UserTimeMonth();
            dayOfWeek = new UserTimeDayOfWeek();

            dayOfMonth.DayOfMonthAdded += DayOfMonth_DayAdded;
            dayOfWeek.DayOfWeekAdded += DayOfWeek_DayOfWeekAdded;

        }

        private void DayOfWeek_DayOfWeekAdded(object sender, EventArgs e)
        {
            if (dayOfMonth.IsValid && dayOfMonth.HasValues())
            {
                dayOfWeek.Clear();
                dayOfWeek.IsValid = false;
            }
        }

        private void DayOfMonth_DayAdded(object sender, EventArgs e)
        {
            Console.WriteLine("Day of month has been added");

            dayOfWeek.Clear();
            dayOfWeek.IsValid = false;
        }

        //create event that checks if anything to day of week has been added. If so, invoke the clear method in day of month and set it to ?.


        public UserTimeValuesSeconds second
        {
            get;
            set;
        }

        public UserTimeValuesMinutes minute
        {
            get;
            set;
        }

        public UserTimeValuesHours hour
        {
            get;
            set;
        }

        public UserTimeDayOfMonth dayOfMonth
        {
            get;
            set;
        }

        public UserTimeMonth month
        {
            get;
            set;
        }

        public UserTimeDayOfWeek dayOfWeek
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", second, minute, hour, dayOfMonth, month, dayOfWeek);
        }
        public string Parse(string input)
        {
            var pieces = input.Split(' ');

            second.Parse(pieces[0]);
            minute.Parse(pieces[1]);
            hour.Parse(pieces[2]);
            dayOfMonth.Parse(pieces[3]);
            month.Parse(pieces[4]);
            dayOfWeek.Parse(pieces[5]);

            


            var sb = new StringBuilder();
            sb.Append(@"^(,{0,1}(?<start>(\b\d\b|[0-5][0-9]|\*+))(-(?<end>\b\d\b|[0-5][0-9]|\*)){0,1})");
            sb.Append(@"+\s+(,{0,1}(?<start>(\b\d\b|[0-5][0-9]|\*+))(-(?<end>\b\d\b|[0-5][0-9]|\*)){0,1})");
            sb.Append(@"+\s+(,{0,1}(?<start>(\b\d\b|[0-5][0-9]|\*+))(-(?<end>\b\d\b|[0-5][0-9]|\*)){0,1})");
            sb.Append(@"+\s+(,{0,1}(?<start>(\b[1-9]\b|[1-2][0-9]|[3][0-1]|\*+))(-(?<end>\b[1-9]\b|[1-2][0-9]|[3][0-1])){0,1})");
            sb.Append(@"+\s+(,{0,1}(?<start>(\b\D{3}\b|\b[1-9]\b|\b[1][0-2]\b|\?|\*+))(-(?<end>\b\D{3}\b|\b[1-9]\b|\b[1][0-2]\b)){0,1})");
            sb.Append(@"+\s+(,{0,1}(?<start>(\b\D{3}\b|\b[1-7]\b|\?|\*+))(-(?<end>\b\D{3}\b|-\b[1-7]\b)){0,1})+$");


            Match match = Regex.Match(input, sb.ToString());

            if (match.Success)
            {
                Console.WriteLine("The cron expression matches the regex");
            }
            else
            {
                Console.WriteLine("Cron expression is wrong");
            }

            return input;
        }
    }
}





