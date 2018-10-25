using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBContextEF.Extensions
{
    public static class QuerableExtensions
    {
        public static async Task<List<T>> GetPageList<T>(this IQueryable<T> source, int pageindex = 1,
            int pagesize = 10)
        {
            return await source.Skip(pagesize * (pageindex - 1)).Take(pagesize).ToListAsync();

        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool preGate, Expression<Func<T, bool>> expressFunc)
        {
            return preGate ? source.Where(expressFunc) : source;

        }
    }
}
