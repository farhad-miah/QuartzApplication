using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamix.QuartzLibrary
{
    public class UserTimeMonth
    {
        public UserTimeMonth()
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
        }

        public void AddRange(IEnumerable<int> values)
        {
            Values.AddRange(values);
            IsValid = true;
            IsAllValues = false;
        }
        
        public override string ToString()
        {

            if (!IsValid)
                return "*";


            if (IsAllValues || !Values.Any())
                return string.Format("{0}", DateTime.Now.Month);
            else
            {
                var Months = new List<int>();

                foreach (var value in Values)
                {
                    //if (value is string)
                    //{
                    //    Months.Add((string)value);
                    //}
                    if (value is int)
                    {
                        //Months.Add(Enum.GetName(typeof(MonthName), (int)value).Substring(0, 3).ToUpper());
                        var val = Enum.ToObject(typeof(MonthName), value);
                        Months.Add((int)val);
                    }
                }

                return string.Join(",", Months.Select(m => m.ToString()).ToArray());
            }

        }

        public void Parse(string input)
        {
            var regex = new Regex(@"(,{0,1}(?<start>(\b\D{3}\b|\b[1-9]\b|\b[1][0-2]\b|\?|\*+))(-(?<end>\b\D{3}\b|\b[1-9]\b|\b[1][0-2]\b)){0,1})");

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

