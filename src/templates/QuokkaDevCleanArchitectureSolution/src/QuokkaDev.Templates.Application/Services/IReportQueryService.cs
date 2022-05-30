using QuokkaDev.Templates.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.Application.Services
{
    public interface IReportQueryService : IQueryService
    {
        Task<dynamic> GetReportAsync(int id);
    }
}
