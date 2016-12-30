using DomainModel.Models;
using DomainModel.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IDepartment
    {
        Task<ICollection<Department>> GetDepartment(short? id);
        Task<int> UpdateDepartment(AddUpdateDepartmentRequestDTO departmentRequest);
        Task<int> AddDepartment(AddUpdateDepartmentRequestDTO departmentRequest);
        Task<int> DeleteDepartment(int departmentId);
    }
}
