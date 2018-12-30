using QuartzWebApp.Models;
using QuartzWebApp.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuartzWebApp.viewModel
{
    public class ScheduleViewModel : Schedule
    { 
        public string JobName
        {
            get;
            set;
        }


        [Required(ErrorMessage = "Enter a number between 0-59!")]
        [RegularExpression(@"^(?:[0-5]?[0-9]|\*+)(?:-(?:[0-5]?[0-9]|\*+))?(?:,(?:[0-5]?[0-9]|\*+)(?:-(?:[0-5]?[0-9]|\*+))?)*$", ErrorMessage = "Enter a number between 0-59!")]
        public string Second { get; set; }


        [Required(ErrorMessage = "Enter a number between 0-59!")]
        [RegularExpression(@"^(?:[0-5]?[0-9]|\*+)(?:-(?:[0-5]?[0-9]|\*+))?(?:,(?:[0-5]?[0-9]|\*+)(?:-(?:[0-5]?[0-9]|\*+))?)*$", ErrorMessage = "Enter a number between 0-59!")]
        public string Minute { get; set; }


        [Required(ErrorMessage = "Enter a number between 0-23!")]
        [RegularExpression(@"^(?:[0-1]?[0-9]|2[0-3]|\*+)(?:-(?:[0-1]?[0-9]|2[0-3]|\*+))?(?:,(?:[0-1]?[0-9]|2[0-3]|\*+)(?:-(?:[0-1]?[0-9]|2[0-3]|\*+))?)*$", ErrorMessage = "Enter a number between 0-23!")]
        public string Hour { get; set; }

        [Required(ErrorMessage ="A name must be entered for a job!")]
        public string Name { get; set; }

        public string Description { get; set; }

        public SchedAccuracy SchedAccLevel { get; set; }

        //Second regex(,{ 0,1}(?<start>(\b\d\b|[0-5] [0-9]|\*+))(-(?<end>\b\d\b|[0-5] [0-9])){0,1})

        //Minute regex(,{ 0,1}(?<start>(\b\d\b|[0-5] [0-9]|\*+))(-(?<end>\b\d\b|[0-5] [0-9])){0,1})

        //Hour regex(,{ 0,1}(?<start>(\b\d\b|[0-1] [0-9]|[2] [0-3]|\*+))(-(?<end>\b\d\b|[0-1] [0-9]|[2] [0-3])){0,1})


        //^(?:[01]?[0 - 9]|2[0-3]|\*+)(?:-(?:[01]?[0 - 9]|2[0-3]|\*+))?(?:,(?:[01]?[0 - 9]|2[0-3]|\*+)(?:-(?:[01]?[0 - 9]|2[0-3]|\*+))?)*$

        //public List<SelectListItem> Jobs { get; set; }
        public int Selected_JobID { get; set; }

        public List<DofMInfoViewModel> DofMInfo { get; set; }

        public List<MonthInfoViewModel> MonthInfo { get; set; }

        public List<DayInfoViewModel> DayInfo { get; set; }


        public bool RunForever { get; set; }
        public string rFSelection { get; set; }

        [Required(ErrorMessage = "Enter a number between 0-99 - 0 = execute once!")]
        [RegularExpression(@"^(\b[0-9]\b|\b[0-9][0-9]\b)$", ErrorMessage = "Enter a number between 0-99!")]
        public int repeatTime1 { get; set; }
    }

    public class JobViewModel
    {
        public int Job_Id { get; set; }
        public string JobName { get; set; }
    }

    public class DayInfoViewModel
    {
        public int dayID { get; set; }
        public string dayName { get; set; }
        public bool IsChecked { get; set; }
    }

    public class MonthInfoViewModel
    {
        public int monthID { get; set; }
        public string monthName { get; set; }
        public int NofD { get; set; }
        public bool IsChecked { get; set; }
    }

    public class DofMInfoViewModel
    {
        public string DofMNo { get; set; }
        public int DofMID { get; set; }
        public bool IsChecked { get; set; }
    }

    public class AddEditScheduleViewModel
    {
        public ScheduleViewModel Schedule { get; set; }

        public AddEditScheduleViewModel()
        {
            Schedule = new ScheduleViewModel();
        }
    }

    public class AddScheduleViewModel : AddEditScheduleViewModel
    {
        public AddScheduleViewModel() : base()
        { }
    }

    public class EditScheduleViewModel : AddEditScheduleViewModel
    {
        public EditScheduleViewModel() : base()
        { }
    }
}