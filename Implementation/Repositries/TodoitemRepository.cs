using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Implementation.Repositries
{
    public class TodoitemRepository : BaseRepository<Todoitem>, ITodoitemRepository
    {
        public TodoitemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Todoitem> Done(int Id)
        {
            return await _context.Todoitems.FirstOrDefaultAsync(n => n.Id == Id && n.IsDeleted == false);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Todoitems.AnyAsync(t => t.Id == id && t.IsDeleted == false);
        }

        public async Task<bool> ExistsByNameAndTime(string name, DateTime time)
        {
            return await _context.Todoitems.AnyAsync(d => d.Name.Equals(name) && d.OriginalTime == time && d.IsDeleted == false);
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerDoneTaskById(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.TaskDone && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerSkippedTaskId(int customerId)
        {
           return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.Skipped && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerTaskByDate(int customerId, DateTime date)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.Date == date.Date && a.CustomerId == customerId && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerTaskByDay(int customerId, DaysOfTheWeek day)
        {
            var selected = (DayOfWeek)day;
            var items = await _context.Todoitems.Where(a => a.OriginalTime.DayOfWeek == selected && a.CustomerId == customerId && a.IsDeleted == false).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerTaskById(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerTaskByTime(int customerId, DateTime time)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.ToShortTimeString() == time.ToShortTimeString() && a.CustomerId == customerId && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerTodayTaskById(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.Today && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllCustomerUpcomingTaskId(int customerId)
        {
            return await _context.Todoitems.Where(a => a.CustomerId == customerId && a.Status == Enum.Status.UpComing && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllDoneTask()
        {
            return await _context.Todoitems.Where(d => d.Status == Enum.Status.TaskDone && d.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllExpiredTime()
        {
            return await _context.Todoitems.Where(d => d.Status == Enum.Status.Today && d.OriginalTime < DateTime.Now && d.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllSkippedTask()
        {
            return await _context.Todoitems.Where(d => d.Status == Enum.Status.Skipped && d.OriginalTime < DateTime.Now && d.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllTaskByDate(DateTime date)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.Date == date.Date && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllTaskByDay(DateTime day)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.DayOfWeek == day.DayOfWeek && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllTaskByTime(DateTime time)
        {
            return await _context.Todoitems.Where(a => a.OriginalTime.ToShortTimeString() == time.ToShortTimeString() && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllTBUUpcomingTask()
        {
           return await _context.Todoitems.Where(d => d.Status == Enum.Status.UpComing && d.OriginalTime == DateTime.Now && d.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllTodayTask()
        {
           return await _context.Todoitems.Where(d => d.Status == Enum.Status.Today && d.OriginalTime == DateTime.Now && d.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Todoitem>> GetAllUpcomingTask()
        {
            return await _context.Todoitems.Where(d => d.Status == Enum.Status.UpComing && d.OriginalTime > DateTime.Now && d.IsDeleted == false).ToListAsync();
        }

        public async Task<Todoitem> GetByName(string name)
        {
            return await _context.Todoitems.FirstOrDefaultAsync(n => n.Name.Equals(name) && n.IsDeleted == false);
        }

        public Task<IEnumerable<Todoitem>> GetTodoitemByCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(IList<Todoitem> todo)
        {
            _context.Todoitems.UpdateRange(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}