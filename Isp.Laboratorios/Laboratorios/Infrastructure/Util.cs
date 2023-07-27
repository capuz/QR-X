using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isp.Laboratorios.DataAccessLayer;

namespace Isp.Laboratorios.Infrastructure
{
    public static class Util
    {
        public static string ReplaceTokens(string template, Dictionary<string, string> tokens)
        {
            var sb = new StringBuilder(template, template.Length * 2);
            var sbResult = new StringBuilder();

            foreach (KeyValuePair<string, string> token in tokens)
            {
                sbResult = sb.Replace(token.Key, token.Value);
            }

            return sbResult.ToString();
        }
        /// <summary>
        /// Calculates number of business days, taking into account:
        ///  - weekends (Saturdays and Sundays)
        ///  - bank holidays in the middle of the week
        /// </summary>
        /// <param name="firstDay">First day in the time interval</param>
        /// <param name="lastDay">Last day in the time interval</param>
        /// <returns>Number of business days during the 'span'</returns>
        public static int BusinessDaysUntil(this DateTime firstDay, DateTime lastDay)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;

            var span = lastDay - firstDay;
            var businessDays = span.Days + 1;
            var fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                var firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)firstDay.DayOfWeek;
                var lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

       
            //obtener feriados
            IEnumerable<DateTime> bankHolidays;
            using (var db = new UnitOfWork())
            {
                 bankHolidays = db.Feriados.ObtenerPorRangoFecha(firstDay, DateTime.Now);
            }

            foreach (var bh in bankHolidays.Select(bankHoliday => bankHoliday.Date).Where(bh => firstDay <= bh && bh <= lastDay))
            {
                --businessDays;
            }

            return businessDays;
        }
    }
}