using System;

namespace Puya.Date
{
    public class DateTimeNow : INow
    {
        public DateTime Value
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
