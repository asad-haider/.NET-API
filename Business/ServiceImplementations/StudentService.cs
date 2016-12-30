﻿using Business.Interfaces;
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
        private IStudentInfoRepo _studentInfoRepo;
        private List<int> pageSizes;
        private const int DEFAULT_PAGE_SIZE = 10;

        public StudentService(IStudentInfoRepo studentInfoRepo)
        {
            _studentInfoRepo = studentInfoRepo;
            pageSizes = new List<int>();
            pageSizes.Add(10);
            pageSizes.Add(25);
            pageSizes.Add(50);
            pageSizes.Add(100);
        }


        public async Task<int> AddStudentInfo(AddUpdateStudentRequestDTO studentRequest)
        {
            StudentsInfo studentInfo = new StudentsInfo();

            #region Prepare the host object
            studentInfo.Name = studentRequest.Name;
            studentInfo.Class = studentRequest.Class;
            studentInfo.Department = studentRequest.Department;

            #endregion

            return await _studentInfoRepo.AddStudentInfo(studentInfo);
        }

        public async Task<int> DeleteStudentInfo(int studentId)
        {
            return await _studentInfoRepo.DeleteStudentInfo(studentId);
        }

        public async Task<IListModelResponse<StudentsInfo>> GetStudentsInfo(short? id, int page, int pageSize)
        {
            ListModelResponse<StudentsInfo> response = new ListModelResponse<StudentsInfo>();

            response.PageNumber = (page == 0) ? 1 : page;
            response.RequestTimeStamp = DateTime.Now;

            page = (page - 1) * pageSize;

            if (pageSizes.Contains(pageSize))
            {
                response.PageSize = (pageSize == 0) ? DEFAULT_PAGE_SIZE : pageSize;
                response.Model = await _studentInfoRepo.GetStudentInfo(id, page, pageSize);
            }
            else
            {
                response.PageSize = DEFAULT_PAGE_SIZE;
                response.Model= await _studentInfoRepo.GetStudentInfo(id, page, DEFAULT_PAGE_SIZE);
            }

            response.TotalRecords = await _studentInfoRepo.GetTotalRecords();

            return await Task<ListModelResponse<StudentsInfo>>.FromResult(response);
        }

        public async Task<int> UpdateStudentInfo(AddUpdateStudentRequestDTO studentRequest)
        {
            StudentsInfo studentInfo = new StudentsInfo();

            #region Prepare the host object
            studentInfo.Id = studentRequest.id;
            studentInfo.Name = studentRequest.Name;
            studentInfo.Department = studentRequest.Department;
            studentInfo.Class = studentRequest.Class;

            #endregion

            return await _studentInfoRepo.UpdateStudentInfo(studentInfo);
        }
    }
}