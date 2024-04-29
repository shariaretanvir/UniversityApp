using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Common
{
    public static class StaticDeclaration
    {
        public static DateTime GetActualBstTime(DateTime calculatedDatetime)
        {
            TimeZoneInfo bstTime = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(calculatedDatetime, bstTime);
        }
    }
}
