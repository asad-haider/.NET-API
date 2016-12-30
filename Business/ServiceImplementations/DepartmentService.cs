using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Models;
using DataAccess.Repositories.Interfaces;
using DomainModel.RequestModels;
using DomainModel.ResponseModels;

namespace Business.ServiceImplementations
{
    public class DepartmentService : IDepartment
    {

        private IDepartmentRepo _departmentRepo;

        public DepartmentService(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public async Task<int> AddDepartment(AddUpdateDepartmentRequestDTO departmentRequest)
        {
            Department department = new Department();

            #region Prepare the host object
            department.Name = departmentRequest.Name;
            department.Address = departmentRequest.Address;

            #endregion

            return await _departmentRepo.AddDepartment(department);
        }

        public async Task<int> DeleteDepartment(int departmentId)
        {
            return await _departmentRepo.DeleteDepartment(departmentId);
        }

        public async Task<ICollection<Department>> GetDepartment(short? id)
        {
            GetDepartmentResponseInfo response = new GetDepartmentResponseInfo();
            response.departments = await _departmentRepo.GetDepartment(id);
            return await Task<GetStudentResponseInfo>.FromResult(response.departments);
        }


        public async Task<int> UpdateDepartment(AddUpdateDepartmentRequestDTO departmentRequest)
        {
            Department department = new Department();

            #region Prepare the host object
            department.PkDepartmentId = departmentRequest.id;
            department.Name = departmentRequest.Name;
            department.Address = departmentRequest.Address;

            #endregion

            return await _departmentRepo.UpdateDepartment(department);
        }
    }
}
