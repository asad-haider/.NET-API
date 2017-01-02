using DomainModel.Models;
using DomainModel.RequestModels;
using DomainModel.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudent
    {
        Task<ListModelResponse<StudentsInfo>> GetStudentsInfo(int pageNumber, int pageSize);
        Task<SingleModelResponse<StudentsInfo>> GetStudent(short id);
        Task<int> UpdateStudentInfo(AddUpdateStudentRequestDTO studentRequest);
        Task<int> AddStudentInfo(AddUpdateStudentRequestDTO studentRequest);
        Task<int> DeleteStudentInfo(int studentId);
    }
}
