using System;
using System.Linq;

namespace XDetroit.WebFrontend.Extensions
{
    public static class Paging
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> elements, int pageSize, int pageNumber)
        {
            if (pageSize < 1 || pageNumber <0)
            {
                throw new ArgumentException("Page size must be greater or equal to 1 and page number must be greater or equal to 0.");
            }

            int skipCount = pageSize * pageNumber;

            return elements.Skip(skipCount).Take(pageSize);
        }
    }
}