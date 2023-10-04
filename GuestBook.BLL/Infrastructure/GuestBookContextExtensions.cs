using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GuestBook.DAL.Context;
using System.Runtime.CompilerServices;

namespace GuestBook.BLL.Infrastructure
{
    public static class GuestBookContextExtensions
    {
        public static void AddGuestBookContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<GuestBookContext>(options => options.UseSqlServer(connection));
        }
    }
}

