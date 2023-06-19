using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;


namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface ITodoitemRepository : IRepository<Todoitem>
    {
        Task<bool> ExistsByNameAndTime(string name, DateTime time);

        Task<bool> ExistsById(int id);

        Task<Todoitem> GetByName(string name);

        Task<Todoitem> Done(int Id);


        Task<IEnumerable<Todoitem>> GetAllCustomerTaskById(int customerId);
        
        Task<IEnumerable<Todoitem>> GetAllCustomerTodayTaskById(int customerId);

       Task<IEnumerable<Todoitem>> GetAllCustomerDoneTaskById(int customerId);

       Task<IEnumerable<Todoitem>> GetAllCustomerSkippedTaskId(int customerId);

       Task<IEnumerable<Todoitem>> GetAllCustomerUpcomingTaskId(int customerId);

       Task<IEnumerable<Todoitem>> GetAllCustomerTaskByDate(int customerId, DateTime date);

       Task<IEnumerable<Todoitem>> GetAllCustomerTaskByTime(int customerId, DateTime time);

       Task<IEnumerable<Todoitem>> GetAllCustomerTaskByDay(int customerId, DaysOfTheWeek day);



       Task<IEnumerable<Todoitem>> GetAllSkippedTask();

       Task<IEnumerable<Todoitem>> GetAllTaskByDate(DateTime date);

       Task<IEnumerable<Todoitem>> GetAllTaskByTime(DateTime time);

       Task<IEnumerable<Todoitem>> GetAllTaskByDay(DateTime day);

       Task<IEnumerable<Todoitem>> GetAllUpcomingTask();

       Task<IEnumerable<Todoitem>> GetAllTodayTask();
	
        Task<IEnumerable<Todoitem>> GetAllDoneTask();


       
       Task<IEnumerable<Todoitem>> GetAllExpiredTime();

       Task<IEnumerable<Todoitem>> GetTodoitemByCategory();

        Task<IEnumerable<Todoitem>> GetAllTBUUpcomingTask();
       Task<bool> Update(IList<Todoitem> todo);
    }
}