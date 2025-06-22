using Domain.DTOs;
using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailProjectRepository : IRepository<EmailProject>
    {
        Task<int> GetProjectCountByUserId();
        Task<List<EmailProject>> GetAllByUserId();
        Task<DashboardModelDto> GetDashboardStatsByUserIdAsync();
    }
}
