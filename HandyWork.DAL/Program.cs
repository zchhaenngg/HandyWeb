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
    public enum t: int
    {
        a = 1,
        b = 2,
        c = 4
    }

    public class EnumWrapper<TEnum>
    {
        private TEnum _tEnum;
        public EnumWrapper(TEnum tEnum)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception(string.Format("{0} 必须为枚举类型", typeof(TEnum).Name));
            }
            _tEnum = tEnum;
        }
    }

    public class Program
    {
        static void a([NotNull]string d)
        {
            Check.NotNull(d, nameof(d));
            Console.WriteLine("aaa");
        }

        static bool IsT(t value,t target)
        {
            return (value & target) > 0;
        }

        static void ttt(t t1)
        {
            if (t1.HasFlag(t.a))
            {
                Console.WriteLine("a");
            }
            if (t1.HasFlag(t.b))
            {
                Console.WriteLine("b");
            }
            if (t1.HasFlag(t.c))
            {
                Console.WriteLine("c");
            }
        }
        static void Main(string[] args)
        {
            t t1 = t.a;
            t t2 = t.b;
            t t3 = t1 | t2;
            ttt(t3);
            Console.WriteLine(t3);
            int d = 1 | 2;
            int d2 = 1 & 3;
            Console.WriteLine(d);
            Console.ReadLine();
        }
    }
}
