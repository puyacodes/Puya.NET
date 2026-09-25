using System;

namespace Puya.Date
{
    public class DateTimeUtcNow : INow
    {
        public DateTime Value
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
