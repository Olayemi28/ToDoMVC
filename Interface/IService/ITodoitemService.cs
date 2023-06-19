using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Interface.IService
{
    public interface ITodoitemService
    {
        Task<BaseResponse<TodoitemDto>> RegisterTodoitem(TodoRequestModel model, int id);

        Task<BaseResponse<TodoitemDto>> UpdateTodoitem(int id, UpdateTodoitemRequestModel model);

        Task<BaseResponse<TodoitemDto>> GetTodoitem(int id);

        Task<BaseResponse<TodoitemDto>> DeleteTodoitem(int id);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodoitem();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodayTask();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllUpcomingTask();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllDoneTask();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllSkippedTask();

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDate(DateTime date);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByTime(DateTime time);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDay(DateTime day);



        Task<BaseResponse<TodoitemDto>> Done(int id);

        Task<BaseResponse<TodoitemDto>> Skipped(int id);

        Task<BaseResponse<TodoitemDto>> GetTodoitemByCustomerId(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTodayTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerDoneTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerSkippedTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerUpcomingTaskById(int customerId);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByDate(int customerId, DateTime date);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByTime(int customerId, DateTime time);

        Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByDay(int customerId, DaysOfTheWeek day);

        Task<BaseResponse<IEnumerable<Todoitem>>> GetAllExpiredTime();

        Task<BaseResponse<bool>> UpdateTodoitemToSkipped(IList<Todoitem> todoitem);

        Task<BaseResponse<IEnumerable<Todoitem>>> GetAllTBUUpcomingTask();
    }
}