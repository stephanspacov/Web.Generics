using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Web.Generics.ApplicationServices.Scheduler
{
    // Raphael Cruzeiro 2010-08-13
    /// <summary>
    /// A jobs scheduling class
    /// </summary>
    public class Scheduler
    {
        public delegate void Exp();
        private IList<Exp> actions;
        private List<Thread> workers;

        public Scheduler()
        {
            workers = new List<Thread>();
            actions = new List<Exp>();
        }

        public void Schedule(Exp exp, Time time)
        {
            actions.Add(exp);

            Thread t = new Thread(new ThreadStart(() => { InitThread(time, exp); }));

            workers.Add(t);

            t.Start();
        }

        private void InitThread(Time time, Exp action)
        {
            while (true)
            {
                Thread.Sleep(GetTimeUntilSleep(time));

                action.Invoke();
            }
        }

        private TimeSpan GetTimeUntilSleep(Time time)
        {

            DateTime execDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time.Hours, time.Minutes, time.Seconds);

            if (DateTime.Now > execDate)
                execDate = execDate.AddDays(1);

            TimeSpan result = execDate - DateTime.Now;

            return result;
        }

    }
}
