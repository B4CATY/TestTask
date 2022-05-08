using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertPaginationParametrInResponse<T>(this HttpContext httpContext,
            IQueryable<T> queryble, int recordsPerPage)
        {
            double count = await queryble.CountAsync();
            double padesQuantity = Math.Ceiling(count / recordsPerPage);
            httpContext.Response.Headers.Add("pagesQuantity", padesQuantity.ToString());
        }
    }
}
