using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GuestBook.DAL.Interfaces;
using GuestBook.DAL.Repositories;

namespace GuestBook.BLL.Infrastructure
{
    public static class UnitOfWorksServiceExtensions
    {
        public static void AddUnitOfWorksService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorks, ContextUnitOfWorks>();
        }
    }
}
