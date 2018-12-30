using QuartzWebApp.Models;
using QuartzWebApp.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Dynamix.QuartzLibrary;
using Quartz;
using static QuartzWebApp.viewModel.ScheduleViewModel;
using QuartzWebApp.Objects;

namespace QuartzWebApp.Controllers
{
    [Authorize]
    public class SchedulerController : Controller
    {

        #region Default Values for fields
        public ScheduleViewModel GetDefaultValues()
        {
            var svm = new ScheduleViewModel();


            //add data val field for number of days in each month

            svm.MonthInfo = new List<MonthInfoViewModel>();
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 0, monthName = "Jan", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 1, monthName = "Feb", NofD = 28, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 2, monthName = "Mar", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 3, monthName = "Apr", NofD = 30, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 4, monthName = "May", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 5, monthName = "Jun", NofD = 30, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 6, monthName = "Jul", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 7, monthName = "Aug", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 8, monthName = "Sep", NofD = 30, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 9, monthName = "Oct", NofD = 31, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 10, monthName = "Nov", NofD = 30, IsChecked = false });
            svm.MonthInfo.Add(new MonthInfoViewModel { monthID = 11, monthName = "Dec", NofD = 31, IsChecked = false });

            svm.DayInfo = new List<DayInfoViewModel>();
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 0, dayName = "Mon", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 1, dayName = "Tue", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 2, dayName = "Wed", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 3, dayName = "Thu", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 4, dayName = "Fri", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 5, dayName = "Sat", IsChecked = false });
            svm.DayInfo.Add(new DayInfoViewModel { dayID = 6, dayName = "Sun", IsChecked = false });

            svm.DofMInfo = new List<DofMInfoViewModel>();
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 0, DofMNo = "1", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 1, DofMNo = "2", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 2, DofMNo = "3", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 3, DofMNo = "4", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 4, DofMNo = "5", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 5, DofMNo = "6", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 6, DofMNo = "7", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 7, DofMNo = "8", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 8, DofMNo = "9", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 9, DofMNo = "10", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 10, DofMNo = "11", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 11, DofMNo = "12", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 12, DofMNo = "13", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 13, DofMNo = "14", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 14, DofMNo = "15", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 15, DofMNo = "16", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 16, DofMNo = "17", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 17, DofMNo = "18", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 18, DofMNo = "19", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 19, DofMNo = "20", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 20, DofMNo = "21", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 21, DofMNo = "22", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 22, DofMNo = "23", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 23, DofMNo = "24", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 24, DofMNo = "25", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 25, DofMNo = "26", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 26, DofMNo = "27", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 27, DofMNo = "28", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 28, DofMNo = "29", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 29, DofMNo = "30", IsChecked = false });
            svm.DofMInfo.Add(new DofMInfoViewModel { DofMID = 30, DofMNo = "31", IsChecked = false });

            svm.RunForever = false;


            return svm;
        }
        #endregion

        #region Show current jobs for users
        public ActionResult Index()
        {
            ScheduleEntity db = new ScheduleEntity();

            var UserIden = User.Identity.GetUserId();

            List<ScheduleViewModel> listSched = db.Schedule.Where(x => x.AspNetUsersId == UserIden && x.IsDeleted == false).Select(x => new ScheduleViewModel { ScheduleID = x.ScheduleID, JobName = x.Job.JobName, NextRunTime = x.NextRunTime, RepeatTime = x.RepeatTime, Run_Forever = x.Run_Forever, Name = x.Name }).ToList();

            ViewBag.listSched = listSched;

            return View();
        }
        #endregion

        #region Delete Jobs
        public JsonResult Delete(int ScheduleID)
        {
            ScheduleEntity db = new ScheduleEntity();

            bool result = false;
            Schedule sched = db.Schedule.SingleOrDefault(x => x.IsDeleted == false && x.ScheduleID == ScheduleID);
            if (sched != null)
            {
                sched.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Display the default values to the add page
        public ActionResult Add()
        {
            var model = new AddScheduleViewModel();
            model.Schedule = GetDefaultValues();

            Entities entities = new Entities();

            var getJobList = entities.Jobs.ToList();
            SelectList list = new SelectList(getJobList, "JobId", "JobName");
            ViewBag.jobListName = list;

            ScheduleEntity db = new ScheduleEntity();

            var UserIden = User.Identity.GetUserId();

            //ScheduleViewModel schedvm = new ScheduleViewModel();

            var SchedAccVal = db.AspNetUsers.Where(x => x.Id == UserIden).Select(x => x.SchedAccuracy).First();


            SchedAccuracy SchedAccName = (SchedAccuracy)Enum.ToObject(typeof(SchedAccuracy), SchedAccVal);

            model.Schedule.SchedAccLevel = SchedAccName;


            return View(model);
        }
        #endregion
        
        #region Post values from user and create record in database 1/2
        [HttpPost]
        public ActionResult Add(AddScheduleViewModel model/*, int Selected_JobID*/)
        {
            //var job = Selected_JobID;


            if (ModelState.IsValid)
            {
                try
                {
                    var ut = new UserTime();
                    var hr = new List<string>();

                    if (model.Schedule.Second == null)
                    {
                        model.Schedule.Second = "0";
                    }

                    if (model.Schedule.Minute == null)
                    {
                        model.Schedule.Minute = "0";
                    }

                    if (model.Schedule.Hour == null)
                    {
                        model.Schedule.Hour = "12";
                    }

                    ut.second.Parse(model.Schedule.Second);

                    ut.minute.Parse(model.Schedule.Minute);

                    ut.hour.Parse(model.Schedule.Hour);

                    ut.dayOfMonth.AddRange(model.Schedule.DofMInfo.Where(i => i.IsChecked).Select(i => i.DofMID + 1));

                    ut.month.AddRange(model.Schedule.MonthInfo.Where(i => i.IsChecked).Select(i => i.monthID + 1));

                    ut.dayOfWeek.AddRange(model.Schedule.DayInfo.Where(i => i.IsChecked).Select(i => i.dayID + 1));


                    var cronString = ut.ToString();

                    var trigger = TriggerBuilder.Create()
                    .WithCronSchedule(cronString)
                    .StartNow()
                    .Build();

                    var next = trigger.GetFireTimeAfter(DateTimeOffset.UtcNow);
                    var previous = trigger.GetPreviousFireTimeUtc();

                    ScheduleEntity db = new ScheduleEntity();


                    Schedule sched = new Schedule
                    {
                        CronExpression = cronString
                    };

                    if (next.HasValue)
                    {
                        sched.NextRunTime = next.Value.UtcDateTime;
                    }

                    if (previous.HasValue)
                    {
                        sched.PreviousRunTime = previous.Value.UtcDateTime;
                    }

                    sched.JobId = model.Schedule.Selected_JobID;
                    sched.AspNetUsersId = User.Identity.GetUserId();
                    sched.RepeatTime = model.Schedule.repeatTime1;
                    sched.Run_Forever = model.Schedule.RunForever;
                    sched.Name = model.Schedule.Name;
                    sched.Description = model.Schedule.Description;

                    db.Schedule.Add(sched);
                    db.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);


            #endregion

            #region Display the selected values to the user after they've clicked the submit button 2/2 (Not In Effect)
            Entities entities = new Entities();

            var getJobList = entities.Jobs.ToList();
            SelectList list = new SelectList(getJobList, "JobId", "JobName");
            ViewBag.jobListName = list;

            //catch errors from model state
            //var modelErrors = ModelState.Values.SelectMany(i => i.Errors);

            var model2 = new AddScheduleViewModel();
            model2.Schedule = GetDefaultValues();

            model2.Schedule.Selected_JobID = model.Schedule.Selected_JobID;

            model2.Schedule.Second = model.Schedule.Second;

            model2.Schedule.Minute = model.Schedule.Minute;

            model2.Schedule.Hour = model.Schedule.Hour;


            foreach (var month in model.Schedule.MonthInfo.Where(i => i.IsChecked))
            {
                var month2 = model2.Schedule.MonthInfo.SingleOrDefault(i => i.monthID == month.monthID);
                if (month2 != null)
                {
                    month2.IsChecked = true;
                }
            }

            foreach (var dofm in model.Schedule.DofMInfo.Where(i => i.IsChecked))
            {
                var dofm2 = model2.Schedule.DofMInfo.SingleOrDefault(i => i.DofMID == dofm.DofMID);
                if(dofm2 != null)
                {
                    dofm2.IsChecked = true;
                }
            }

            foreach(var day in model.Schedule.DayInfo.Where(i => i.IsChecked))
            {
                var day2 = model2.Schedule.DayInfo.SingleOrDefault(i => i.dayID == day.dayID);
                if(day2 != null)
                {
                    day2.IsChecked = true;
                }
            }


            model2.Schedule.RunForever = model.Schedule.RunForever;

            model2.Schedule.repeatTime1 = model.Schedule.repeatTime1;

            model2.Schedule.Name = model.Schedule.Name;

            model2.Schedule.Description = model.Schedule.Description;

            return View(model2);
        }
        #endregion

        #region Edit record
        public ActionResult Edit(int ScheduleID)
        {
            var model = new EditScheduleViewModel();
            Entities entity = new Entities();

            var getJobList = entity.Jobs.ToList();
            SelectList list = new SelectList(getJobList, "JobId", "JobName");
            ViewBag.jobListName = list;

            model.Schedule = GetDefaultValues();
            var UserIden = User.Identity.GetUserId();
            ScheduleEntity db = new ScheduleEntity();
            var SchedAccVal = db.AspNetUsers.Where(x => x.Id == UserIden).Select(x => x.SchedAccuracy).First();
            SchedAccuracy SchedAccName = (SchedAccuracy)Enum.ToObject(typeof(SchedAccuracy), SchedAccVal);
            model.Schedule.SchedAccLevel = SchedAccName;

            if (ScheduleID > 0)
            {
                Schedule sched = db.Schedule.SingleOrDefault(x => x.IsDeleted == false && x.ScheduleID == ScheduleID);

                var ut = new UserTime();

                ut.Parse(sched.CronExpression);
                ut.ToString();

                model.Schedule.Second = Convert.ToString(ut.second);
                model.Schedule.Minute = Convert.ToString(ut.minute);
                model.Schedule.Hour = Convert.ToString(ut.hour);


                List<int> dofm = new List<int>();
                var dayOfMonth = Convert.ToString(ut.dayOfMonth);

                dofm = dayOfMonth.Split(',').Select(s =>
                {
                    int.TryParse(s, out int i);
                    return i - 1;
                }).ToList();

                foreach(var a in model.Schedule.DofMInfo)
                {
                    foreach (var b in dofm)
                    {
                        if(a.DofMID.Equals(b))
                        {
                            a.IsChecked = true;
                        }
                    }
                }

                List<int> months = new List<int>();
                var month = Convert.ToString(ut.month);

                months = month.Split(',').Select(s =>
                {
                    int.TryParse(s, out int i);
                    return i-1;
                }).ToList();

               foreach(var a in model.Schedule.MonthInfo)
                {
                    foreach(var b in months)
                    {
                        if (a.monthID.Equals(b))
                        {
                            a.IsChecked = true;
                        }
                    }
                }

                List<int> days = new List<int>();
                var day = Convert.ToString(ut.dayOfWeek);

                days = day.Split(',').Select(s =>
                {
                    int.TryParse(s, out int i);
                    return i - 1;
                }).ToList();

                foreach (var a in model.Schedule.DayInfo)
                {
                    foreach (var b in days)
                    {
                        if (a.dayID.Equals(b))
                        {
                            a.IsChecked = true;
                        }
                    }
                }


                model.Schedule.Name = sched.Name;
                model.Schedule.Description = sched.Description;
                model.Schedule.repeatTime1 = Convert.ToInt32(sched.RepeatTime);
                model.Schedule.ScheduleID = ScheduleID;
                model.Schedule.RunForever = sched.Run_Forever;

                if (sched.Run_Forever==true)
                {
                    model.Schedule.Run_Forever = true;
                }

            }

            return View(model);
        }


        #endregion

        #region Post Edited record back to database

        [HttpPost]
        public ActionResult Edit(EditScheduleViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ScheduleEntity db = new ScheduleEntity();

                    var sched = db.Schedule.Where(x => x.ScheduleID == model.Schedule.ScheduleID).First();




                    var ut = new UserTime();
                    var hr = new List<string>();

                    if (model.Schedule.Second == null)
                    {
                        model.Schedule.Second = "0";
                    }

                    if (model.Schedule.Minute == null)
                    {
                        model.Schedule.Minute = "0";
                    }

                    if (model.Schedule.Hour == null)
                    {
                        model.Schedule.Hour = "12";
                    }

                    ut.second.Parse(model.Schedule.Second);

                    ut.minute.Parse(model.Schedule.Minute);

                    ut.hour.Parse(model.Schedule.Hour);

                    ut.dayOfMonth.AddRange(model.Schedule.DofMInfo.Where(i => i.IsChecked).Select(i => i.DofMID + 1));

                    ut.month.AddRange(model.Schedule.MonthInfo.Where(i => i.IsChecked).Select(i => i.monthID + 1));

                    ut.dayOfWeek.AddRange(model.Schedule.DayInfo.Where(i => i.IsChecked).Select(i => i.dayID + 1));


                    var cronString = ut.ToString();

                    var trigger = TriggerBuilder.Create()
                    .WithCronSchedule(cronString)
                    .StartNow()
                    .Build();

                    var next = trigger.GetFireTimeAfter(DateTimeOffset.UtcNow);
                    var previous = trigger.GetPreviousFireTimeUtc();


                    sched.CronExpression = cronString;

                    if (next.HasValue)
                    {
                        sched.NextRunTime = next.Value.UtcDateTime;
                    }

                    if (previous.HasValue)
                    {
                        sched.PreviousRunTime = previous.Value.UtcDateTime;
                    }

                    //sched.ScheduleID = ScheduleID;
                    sched.JobId = model.Schedule.Selected_JobID;
                    sched.AspNetUsersId = User.Identity.GetUserId();
                    sched.RepeatTime = model.Schedule.repeatTime1;
                    sched.Run_Forever = model.Schedule.RunForever;
                    sched.Name = model.Schedule.Name;
                    sched.Description = model.Schedule.Description;

                    db.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View(model);

        }

        #endregion
    }
}