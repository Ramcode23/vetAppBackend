using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace vetappback.Utilities
{
    public static class HttpContextExtensions
    {
         public async static Task InsertPagintationToHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double quality = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalRecord", quality.ToString());
        }
    }
        
    }
