using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.Scheduler
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// Represents a time
    /// </summary>
    public class Time
    {
        public Time() { }

        public Time(int Hours, int Minutes, int Seconds)
        {
            this.Hours = Hours;
            this.Minutes = Minutes;
            this.Seconds = Seconds;
        }

        public Time(int Hours, int Minutes) : this(Hours, Minutes, 0) { }

        public Time(int Hours) : this(Hours, 0, 0) { }

        public int Hours { get; set; }

        private int minutes;

        public int Minutes
        {   
            get 
            {
                return minutes;
            } 
            set 
            {
                minutes += value;

                if (minutes > 59)
                {
                    this.Hours++;
                    minutes -= 60;
                }
            }
        }

        private int seconds;

        public int Seconds 
        {
            get
            {
                return seconds;
            }
            set 
            {
                seconds += value;

                if (seconds > 59)
                {
                    this.Minutes++;
                    seconds -= 60;
                }
            }
        }

        /// <summary>
        /// Gets a string representation of Time
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}:{1}:{2}", Hours, Minutes, Seconds);
        }

        public static Time operator +(Time t1, Time t2)
        {
            t1.Hours += t2.Hours;
            t1.Minutes += t2.Minutes;
            t1.Seconds += t2.Seconds;

            return t1;
        }

        public static Time operator -(Time t1, Time t2)
        {
            t1.Hours -= t2.Hours;
            t1.Minutes -= t2.Minutes;
            t1.Seconds -= t2.Seconds;

            return t1;
        }

        public static bool operator ==(Time t1, Time t2)
        {
            return (t1.Hours == t2.Hours && t1.Minutes == t2.Minutes && t1.Seconds == t2.Seconds);
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1.Hours == t2.Hours && t1.Minutes == t2.Minutes && t1.Seconds == t2.Seconds);
        }

        public override bool Equals(object obj)
        {
            Time t2 = (obj as Time);

            return (this.Hours == t2.Hours && this.Minutes == t2.Minutes && this.Seconds == t2.Seconds);
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(String.Format("{0}{1}{2}", this.Hours, this.Minutes, this.Seconds));
        }

    }
}
