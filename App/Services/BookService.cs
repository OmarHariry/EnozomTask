using App.DataTransferObject;
using App.Models;
using App.Repositories;
using App.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> BorrowBookAsync(int copyId, int studentId)
        {
            await _unitOfWork.BeginTranscationAsync();
            try
            {
                var copy = await _unitOfWork.Copies.GetById(copyId);
                if (copy == null)
                    throw new Exception("Borrowing record not found.");


                if (copy.StatusId != 1)
                    throw new Exception("Copy not found.");

                // change its status to Borrowed
                copy.StatusId = 2;
                // set borrowing date, expected return date
                BorrowingRecord br = new BorrowingRecord
                {
                    CopyId = copyId,
                    StudentId = studentId,
                    BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    ExpectedReturnDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(3),
                    StatusId = 2
                };

                await _unitOfWork.BorrowingRecords.Add(br);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }


        }

        public async Task<bool> ReturnBookAsync(ReturnBookDto returnBookDto)
        {
            await _unitOfWork.BeginTranscationAsync();
            try
            {
                var record = await _unitOfWork.BorrowingRecords.GetById(returnBookDto.RecordId);
                
                if (record == null)
                    throw new Exception("Borrowing record not found.");

                var copy = await _unitOfWork.Copies.GetById(record.CopyId);
                if (copy == null)
                    throw new Exception("Copy not found.");


                // change copy status 
                record.StatusId = returnBookDto.StatusId;
                record.Copy.StatusId = returnBookDto.StatusId;

                // record the actual return date
                record.ActualReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
