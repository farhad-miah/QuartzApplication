using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamix.QuartzLibrary
{
    public class UserTimeDayOfMonth
    {

        public event EventHandler DayOfMonthAdded;

        public UserTimeDayOfMonth()
        {
            IsValid = true;
            Values = new List<int>();
        }

        public bool IsValid { get; set; }
        public bool IsAllValues { get; set; }
        private List<int> Values { get; set; }

        public void Add(int value)
        {
            Values.Add(value);
            IsValid = true;
            IsAllValues = false;

            DayOfMonthAdded?.Invoke(this, new EventArgs());
        }

        public void AddRange(IEnumerable<int> values)
        {
            Values.AddRange(values);
            IsValid = true;
            IsAllValues = false;

            DayOfMonthAdded?.Invoke(this, new EventArgs());
        }

        public bool HasValues()
        {
            return Values.Any();
        }

        public override string ToString()
        {
            if (!IsValid)
                return "*";
            else if (IsAllValues || !Values.Any())
                return "?";
            else
            {
                var Days = new List<object>();
                Days.AddRange(Values.Select(v => v).ToRanges());
                //Days.AddRange(Values.Where(v => !(v is int)));
                return string.Join(",", Values.Select(m => m.ToString()).ToArray());
            }


            //if (IsValid)
            //{
            //    var enteredRange = Enumerable.Range(1, 1);

            //    var Days2 = new List<object>();
            //    foreach (var value2 in Values)
            //    {

            //        if (Values.Count > 1)
            //        {
            //            var firstVal = Convert.ToInt32(Values.First());
            //            var lastVal = Convert.ToInt32(Values.Last());

            //            enteredRange = Enumerable.Range(firstVal, lastVal - firstVal + 1).ToList();

            //            Values.Add(enteredRange);
            //        }
            //    }
            //}
        }

        public void Parse(string input)
        {
            var regex = new Regex(@"(,{0,1}(?<start>(\b[1-9]\b|[1-2][0-9]|[3][0-1]|\*+))(-(?<end>\b[1-9]\b|[1-2][0-9]|[3][0-1])){0,1})");

            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                var start = match.Groups["start"].Value;
                var end = match.Groups["end"].Value;


                int.TryParse(start, out int startInt);
                if (int.TryParse(end, out int endInt))
                {
                    var range = Enumerable.Range(startInt, endInt - startInt);

                    foreach (var number in range)
                    {
                        if (!Values.Contains(number))
                        {
                            Add(number);
                        }
                    }
                }
                else
                {
                    if (!Values.Contains(startInt))
                    {
                        Add(startInt);
                    }
                }
            }
        }

    }
}
