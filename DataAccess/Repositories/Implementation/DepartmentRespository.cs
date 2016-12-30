using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation
{
    public class DepartmentRespository : IDepartmentRepo
    {
        private studentsContext _context;

        public DepartmentRespository(studentsContext context)
        {
            _context = context;
        }

        public async Task<int> AddDepartment(Department department)
        {
            using (_context)
            {
                var entityToAdd = _context.Department.Add(department);

                return await Task.FromResult(await _context.SaveChangesAsync());
            }
        }

        public async Task<int> DeleteDepartment(int departmentId)
        {
            using (_context)
            {
                var entityToUpdate = await _context.Department.FirstOrDefaultAsync(x => x.PkDepartmentId == departmentId);

                _context.Department.Remove(entityToUpdate);

                return await Task.FromResult(await _context.SaveChangesAsync());
            }
        }

        public async Task<ICollection<Department>> GetDepartment(short? id)
        {
            using (_context)
            {
                if (id.HasValue)
                {
                    _context.Department.FirstOrDefault(h => h.PkDepartmentId == id.Value);

                    return await Task.FromResult(_context.Department.FirstOrDefault(h => h.PkDepartmentId == id.Value) as ICollection<Department>);
                }
                else
                {
                    return await Task.FromResult(_context.Department.ToList());
                }
            }
        }

        public async Task<int> UpdateDepartment(Department department)
        {
            using (_context)
            {
                var entityToUpdate = _context.Department.Update(department);
                _context.Department.Attach(department);

                _context.Entry(department).Property(e => e.Name).IsModified = true;
                _context.Entry(department).Property(e => e.Address).IsModified = true;

                return await Task.FromResult(await _context.SaveChangesAsync());
            }
        }
    }
}
