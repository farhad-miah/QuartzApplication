using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzWebApp.Objects
{
    [Flags]
    public enum SchedAccuracy : int
    {

        None = 0,
        Month = 1,
        Day = 4,
        Minute = 16,
        Hour = 64,
        Second = 256,

        BasicAccuracy = Month | Day,
        MediumAccuracy = Hour | BasicAccuracy,
        HighAccuracy = Minute | MediumAccuracy,
        MaxAccuracy = Second | HighAccuracy
    }
}