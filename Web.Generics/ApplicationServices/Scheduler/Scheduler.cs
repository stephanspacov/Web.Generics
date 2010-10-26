/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
