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

        public async Task<bool> BorrowBookAsync(BorrowBookDto borrowBookDto)
        {
            await _unitOfWork.BeginTranscationAsync();
            try
            {
                var copy = await _unitOfWork.Copies.GetById(borrowBookDto.CopyId);
                if (copy == null)
                    return false;


                if (copy.StatusId != 1)
                    return false;

                // change its status to Borrowed
                copy.StatusId = 2;
                // set borrowing date, expected return date
                BorrowingRecord br = new BorrowingRecord
                {
                    CopyId = borrowBookDto.CopyId,
                    StudentId = borrowBookDto.StudentId,
                    BorrowDate = borrowBookDto.BorrowDate,
                    ExpectedReturnDate = borrowBookDto.ExpectedReturnDate,
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
                record.ActualReturnDate = returnBookDto.ActualReturnDate;

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
