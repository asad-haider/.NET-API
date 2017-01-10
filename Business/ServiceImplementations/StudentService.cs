using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Models;
using DomainModel.RequestModels;
using DataAccess.Repositories.Interfaces;
using DomainModel.ResponseModels;

namespace Business.ServiceImplementations
{
    public class StudentService : IStudent
    {
        private readonly IStudentInfoRepo _studentInfoRepo;
        private readonly List<int> _pageSizes;
        private const int DefaultPageSize = 10;

        public StudentService(IStudentInfoRepo studentInfoRepo)
        {
            _studentInfoRepo = studentInfoRepo;
            _pageSizes = new List<int> {10, 25, 50, 100};
        }


        public async Task<int> AddStudentInfo(AddUpdateStudentRequestDTO studentRequest)
        {
            var studentInfo = new StudentsInfo
            {
                Name = studentRequest.Name,
                Class = studentRequest.Class,
                Department = studentRequest.Department
            };

            #region Prepare the host object

            #endregion

            return await _studentInfoRepo.AddStudentInfo(studentInfo);
        }

        public async Task<int> DeleteStudentInfo(int studentId)
        {
            return await _studentInfoRepo.DeleteStudentInfo(studentId);
        }

        public async Task<ListModelResponse<StudentsInfo>> GetStudentsInfo(int pageNumber, int pageSize)
        {
            var response = new ListModelResponse<StudentsInfo>
            {
                PageNumber = (pageNumber == 0) ? 1 : pageNumber,
                RequestTimeStamp = DateTime.Now
            };

            pageNumber = (pageNumber - 1) * pageSize;

            if (_pageSizes.Contains(pageSize))
            {
                response.PageSize = (pageSize == 0) ? DefaultPageSize : pageSize;
                response.Model = await _studentInfoRepo.GetStudentsInfo(pageNumber, pageSize);
            }
            else
            {
                response.PageSize = DefaultPageSize;
                response.Model= await _studentInfoRepo.GetStudentsInfo(pageNumber, DefaultPageSize);
            }

            response.TotalRecords = await _studentInfoRepo.GetTotalRecords();
            response.Success = true;

            return await Task<ListModelResponse<StudentsInfo>>.FromResult(response);
        }

        public async Task<SingleModelResponse<StudentsInfo>> GetStudent(short id)
        {
            var response = new SingleModelResponse<StudentsInfo>
            {
                Success = true,
                RequestTimeStamp = DateTime.Now,
                Model = await _studentInfoRepo.GetStudent(id)
            };
            return await Task<SingleModelResponse<StudentsInfo>>.FromResult(response);
        }

        public async Task<int> UpdateStudentInfo(AddUpdateStudentRequestDTO studentRequest)
        {
            #region Prepare the host object

            var studentInfo = new StudentsInfo
            {
                Id = studentRequest.Id,
                Name = studentRequest.Name,
                Department = studentRequest.Department,
                Class = studentRequest.Class
            };

            #endregion

            return await _studentInfoRepo.UpdateStudentInfo(studentInfo);
        }
    }
}
