using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuartzWebApp.viewModel
{
    public class Values /*: ScheduleViewModel*/
    {

        public List<int> DayOfMonth { get; set; }
        public List<int> Month { get; set; }
        public List<int> DaysOfWeek { get; set; }
        public Boolean Runtime { get; set; }


        [Required(ErrorMessage = "Enter a number between 0-59!")]
        [RegularExpression(@"^(,{0,1}(\b\d\b|[0-5][0-9]|\*+)(-\b\d\b|-[0-5][0-9]){0,1})$", ErrorMessage = "Enter a number between 0-59!")]
        public object Second { get; set; }


        [Required(ErrorMessage = "Enter a number between 0-59!")]
        [RegularExpression(@"^(,{0,1}(\b\d\b|[0-5][0-9]|\*+)(-\b\d\b|-[0-5][0-9]){0,1})$", ErrorMessage = "Enter a number between 0-59!")]
        public object Minute { get; set; }


        [Required(ErrorMessage = "Enter a number between 0-23!")]
        [RegularExpression(@"^(,{0,1}(\b\d\b|[0-1][0-9]|[2][0-3]|\*+)(-\b\d\b|-[0-1][0-9]|-[2][0-4]){0,1})$", ErrorMessage = "Enter a number between 0-23!")]
        public object Hour { get; set; }


        //public List<SelectListItem> Jobs { get; set; }
        public int Job { get; set; }

        public List<Values> DofMInfo { get; set; }
        public string DofMNo { get; set; }
        public int DofMID { get; set; }


        public List<Values> MonthInfo { get; set; }
        public int monthID { get; set; }
        public string monthName { get; set; }
    

        public List<Values> DayInfo { get; set; }
        public int dayID { get; set; }
        public string dayName { get; set; }


        public List<Values> RunForever { get; set; }
        public string rFSelection { get; set; }



        [Required(ErrorMessage ="Enter a number between 0-99 - 0 = execute once!")]
        [RegularExpression(@"^(\b[0-9]\b|\b[0-9][0-9]\b)$", ErrorMessage ="Enter a number between 0-99!")]
        public int repeatTime { get; set; }

        public bool IsChecked { get; set; }
    }

}
