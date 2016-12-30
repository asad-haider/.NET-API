using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IStudentInfoRepo
    {
        Task<ICollection<StudentsInfo>> GetStudentInfo(short? id, int pageNumber, int pageSize);
        Task<int> GetTotalRecords();
        Task<int> UpdateStudentInfo(StudentsInfo studentInfo);
        Task<int> AddStudentInfo(StudentsInfo studentInfo);
        Task<int> DeleteStudentInfo(int studentInfoId);
    }
}
