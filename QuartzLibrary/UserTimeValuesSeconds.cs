using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dynamix.QuartzLibrary
{
    public class UserTimeValuesSeconds
    {

        public UserTimeValuesSeconds(string allValuesString = "")
        {
            IsValid = false;
            AllValuesString = allValuesString;
            Values = new List<int>();
        }

        public string AllValuesString { get; private set; }
        public bool AllValues { get; set; }
        private List<int> Values { get; set; }
        public bool IsAllValues { get; set; }

        public void Add(int value)
        {
            Values.Add(value);
            IsValid = true;
            AllValues = false;
        }
        public void AddRange(IEnumerable<int> values)
        {
            Values.AddRange(values);
            IsValid = true;
            IsAllValues = false;
        }

        public bool IsValid { get; set; }


        public override string ToString()
        {
            if (!IsValid)
                return "*";

            if (AllValues || !Values.Any())
                return string.Format("{0}", DateTime.Now.Second);
            else
            {
                return string.Join(",", Values.Select(m => m.ToString()).ToArray());
            }
        }
        public void Parse(string input)
        {
            var regex = new Regex(@"(,{0,1}(?<start>(\b\d\b|[0-5][0-9]|\*+))(-(?<end>\b\d\b|[0-5][0-9]|\*)){0,1})");

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