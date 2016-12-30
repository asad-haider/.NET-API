using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel.Models;

namespace DataAccess.Repositories.Implementation
{
    public class StudentInfoRespository : IStudentInfoRepo
    {
        private studentsContext _context;

        public StudentInfoRespository(studentsContext context)
        {
            _context = context;
        }

        public async Task<int> AddStudentInfo(StudentsInfo studentInfo)
        {
            var entityToAdd = _context.StudentsInfo.Add(studentInfo);

            return await Task.FromResult(await _context.SaveChangesAsync());
        }

        public async Task<int> DeleteStudentInfo(int studentInfoId)
        {
            var entityToUpdate = await _context.StudentsInfo.FirstOrDefaultAsync(x => x.Id == studentInfoId);

            _context.StudentsInfo.Remove(entityToUpdate);

            return await Task.FromResult(await _context.SaveChangesAsync());
        }

        public async Task<ICollection<StudentsInfo>> GetStudentInfo(short? id, int pageNumber, int pageSize)
        {
            if (id.HasValue)
            {
                return await Task.FromResult(_context.StudentsInfo.FirstOrDefault(student => student.Id == id.Value) as ICollection<StudentsInfo>);
            }
            else
            {
                return await Task.FromResult(_context.StudentsInfo.Skip(pageNumber).Take(pageSize).ToList());
            }
        }

        public async Task<int> GetTotalRecords()
        {
            //using (_context)
            //{
                return await Task.FromResult(_context.StudentsInfo.Count());
            //}
        }

        public async Task<int> UpdateStudentInfo(StudentsInfo studentInfo)
        {
            var entityToUpdate = _context.StudentsInfo.Update(studentInfo);
            _context.StudentsInfo.Attach(studentInfo);

            _context.Entry(studentInfo).Property(e => e.Name).IsModified = true;
            _context.Entry(studentInfo).Property(e => e.Department).IsModified = true;
            _context.Entry(studentInfo).Property(e => e.Class).IsModified = true;

            return await Task.FromResult(await _context.SaveChangesAsync());
        }
    }
}
