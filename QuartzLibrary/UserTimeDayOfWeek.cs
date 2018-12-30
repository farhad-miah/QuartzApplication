using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamix.QuartzLibrary
{
    public class UserTimeDayOfWeek
    {
        public event EventHandler DayOfWeekAdded;

        public UserTimeDayOfWeek()
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

            DayOfWeekAdded?.Invoke(this, new EventArgs());
        }

        public void AddRange(IEnumerable<int> values)
        {
            Values.AddRange(values);
            IsValid = true;
            IsAllValues = false;

            DayOfWeekAdded?.Invoke(this, new EventArgs());
        }

        public void Clear()
        {
            Values.Clear();
            IsAllValues = true;
            IsValid = false;
        }

        public override string ToString()
        {
            if (!IsValid)
                return "?";
            if (IsAllValues || !Values.Any())
                return string.Format("{0}", DateTime.Now.DayOfWeek);
            else
            {
                var Days = new List<int>();

                foreach (var value in Values)
                {
                    //if (value is string)
                    //{
                    //    Days.Add((string)value);
                    //}
                    if (value is int)
                    {
                        var val = Enum.ToObject(typeof(DayName), value);

                        Days.Add((int)val);
                    }
                    //else if (value is DayOfWeek)
                    //{
                    //    Days.Add(((DayOfWeek)value).ToString().Substring(0, 3).ToUpper());
                    //}
                }

            //else
            //{
            //    var Days = new List<object>();
            //    Days.AddRange(Values.Where(v => v is int).Select(v => (int)v).ToRanges());
            //    Days.AddRange(Values.Where(v => IsAllValue))
            //    Days.AddRange(Values.Where(v => !(v is int)));
            //    return string.Join(",", Days.Select(m => m.ToString()).ToArray());
            //}
           return string.Join(",", Days.Select(m => m.ToString()).ToArray());
            }

        }

        public void Parse(string input)
        {
            var regex = new Regex(@"(,{0,1}(?<start>(\b\D{3}\b|\b[1-7]\b|\?|\*+))(-(?<end>\b\D{3}\b|-\b[1-7]\b)){0,1})");

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
