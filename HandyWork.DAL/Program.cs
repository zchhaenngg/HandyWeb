using EFCache;
using Handy.Shared;
using HandyWork.Model;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    public class Program
    {
        static void a([NotNull]string d)
        {
            Check.NotNull(d, nameof(d));
            Console.WriteLine("aaa");
        }
        static void Main(string[] args)
        {
            // populate and print data
            a(null);
            Console.ReadLine();
        }
    }
}
