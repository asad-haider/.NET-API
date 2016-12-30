using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepo
    {
        Task<ICollection<Department>> GetDepartment(short? id);
        Task<int> UpdateDepartment(Department department);
        Task<int> AddDepartment(Department department);
        Task<int> DeleteDepartment(int departmentId);
    }
}
