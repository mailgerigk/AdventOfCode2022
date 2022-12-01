using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
[AttributeUsage(AttributeTargets.Method)]
class DayLevelAttribute : Attribute
{
    public int Year { get; }
    public int Day { get; }
    public int Level { get; }

    public DayLevelAttribute(int year, int day, int level)
    {
        Year = year;
        Day = day;
        Level = level;
    }
}
