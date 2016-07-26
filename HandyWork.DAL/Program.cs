using EFCache;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    public class Configuration : DbConfiguration
    {
        public Configuration()
        {
            var transactionHandler = new CacheTransactionHandler(Program.Cache);

            AddInterceptor(transactionHandler);

            Loaded +=
              (sender, args) => args.ReplaceService<DbProviderServices>(
                (s, _) => new CachingProviderServices(s, transactionHandler));
        }
    }

    public class Program
    {
        internal static readonly InMemoryCache Cache = new InMemoryCache();

        private static void Seed()
        {
            using (HistoryEntities context = new HistoryEntities())
            {
                context.Person.Add(
                   new Person
                   {
                       Id = Guid.NewGuid().ToString(),
                       FirstName = "cheng",
                       LastName = "zhang",
                       Email = "cheng.zhang@united-imaging.com",
                       Phone = "13248191050"
                   });
                context.Person.Add(
                   new Person
                   {
                       Id = Guid.NewGuid().ToString(),
                       FirstName = "xuefeng",
                       LastName = "yin",
                       Email = "xuefeng.yin@united-imaging.com",
                       Phone = "11122233344"
                   });
                context.SaveChanges();
            }
        }
        
        private static void RemoveData()
        {
            using (var ctx = new HistoryEntities())
            {
                foreach (var item in ctx.Person)
                {
                    ctx.Person.Remove(item);
                }
            }
        }

        private static void PrintData()
        {
            using (var ctx = new HistoryEntities())
            {
                foreach (var person in ctx.Person)
                {
                    Console.WriteLine("{0}: {1}", person.FirstName, person.LastName);
                }
            }
        }

        static void Main(string[] args)
        {
            // populate and print data
            Console.WriteLine("Entries in cache: {0}", Cache.Count);
            RemoveData();
            Seed();
            PrintData();
            Console.WriteLine("\nEntries in cache: {0}", Cache.Count);
            RemoveData();
            PrintData();
            Console.WriteLine("\nEntries in cache: {0}", Cache.Count);
            Seed();
            Console.WriteLine("\nEntries in cache: {0}", Cache.Count);
            PrintData();
            Console.WriteLine("\nEntries in cache: {0}", Cache.Count);
        }
    }
}
