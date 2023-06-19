using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniqueTodoApplication.DTOs;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Enum;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.Implementation.Service
{
    public class TodoitemService : ITodoitemService
    {
        private readonly ITodoitemRepository _todoitemRepository;
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TodoitemService(ITodoitemRepository todoitemRepository, IReminderRepository reminderRepository, IUserRepository userRepository, ICustomerRepository customerRepository, ICategoryRepository categoryRepository)
        {
            _customerRepository = customerRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _reminderRepository = reminderRepository;
            _todoitemRepository = todoitemRepository;

        }

        public async Task<BaseResponse<TodoitemDto>> DeleteTodoitem(int id)
        {
            var todoitem = await _todoitemRepository.Get(id);
            if (todoitem == null)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "Todoitem not found",
                    Success = false
                };
            }
            todoitem.IsDeleted = true;
            await _todoitemRepository.SaveChanges();
            return new BaseResponse<TodoitemDto>
            {
                Message = $"Todoitem with name {todoitem.Name} deleted successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }
            };
        }

        public async Task<BaseResponse<TodoitemDto>> Done(int id)
        {
            var done = await _todoitemRepository.Get(id);
            if (done == null)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "not found ",
                    Success = false
                };
            }
            done.Status = Enum.Status.TaskDone;
            await _todoitemRepository.Update(done);
            return new BaseResponse<TodoitemDto>
            {
                Message = "Todoitem updated successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = done.Id,
                    Name = done.Name,
                    Description = done.Description,
                    OriginalTime = done.OriginalTime,
                    StartingTime = done.StartingTime,
                    Status = done.Status,
                    TimeInterval = done.TimeInterval,
                    Priority = done.Priority,
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerDoneTaskById(int customerId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerDoneTaskById(customerId);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer successfully retrived",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerSkippedTaskById(int customerId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerSkippedTaskId(customerId);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByDate(int customerId, DateTime date)
        {

            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerTaskByDate(customerId, date);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByDay(int customerId, DaysOfTheWeek day)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerTaskByDay(customerId, day);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskById(int customerId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "Customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerTaskById(customerId);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTaskByTime(int customerId, DateTime time)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerTaskByTime(customerId, time);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerTodayTaskById(int customerId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerTodayTaskById(customerId);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer successfully retrived",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllCustomerUpcomingTaskById(int customerId)
        {
            var customer = await _customerRepository.Get(customerId);
            if (customer == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            var todo = await _todoitemRepository.GetAllCustomerUpcomingTaskId(customerId);
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer successfully retrived",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllDoneTask()
        {
            var todoitem = await _todoitemRepository.GetAllDoneTask();
            if (todoitem == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "No task found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Task successfully retrieved",
                Success = true,
                Data = todoitem.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<Todoitem>>> GetAllExpiredTime()
        {
            var time = await _todoitemRepository.GetAllExpiredTime();
            return new BaseResponse<IEnumerable<Todoitem>>
            {
                Message = "Task retrived successfully",
                Success = true,
                Data = time.Select(time => new Todoitem
                {
                    Id = time.Id,
                    Name = time.Name,
                    Description = time.Description,
                    OriginalTime = time.OriginalTime,
                    StartingTime = time.StartingTime,
                    Status = time.Status,
                    TimeInterval = time.TimeInterval,
                    Priority = time.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllSkippedTask()
        {
            var todoitem = await _todoitemRepository.GetAllSkippedTask();
            if (todoitem == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "No task found",
                    Success = false,
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Task successfully retrieved",
                Success = true,
                Data = todoitem.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDate(DateTime date)
        {
            var todo = await _todoitemRepository.GetAllTaskByDate(date);
            if (todo == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }   
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByDay(DateTime day)
        {
             var todo = await _todoitemRepository.GetAllTaskByDay(day);
            if (todo == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todo => new TodoitemDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    OriginalTime = todo.OriginalTime,
                    StartingTime = todo.StartingTime,
                    Status = todo.Status,
                    TimeInterval = todo.TimeInterval,
                    Priority = todo.Priority,
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTaskByTime(DateTime time)
        {
           var todo = await _todoitemRepository.GetAllTaskByTime(time);
            if (todo == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "customer not found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Customer retrieved succesfully",
                Success = true,
                Data = todo.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodayTask()
        {
            var todoitem = await _todoitemRepository.GetAllTodayTask();
            if (todoitem == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "No task found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Task successfully retrieved",
                Success = true,
                Data = todoitem.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllTodoitem()
        {
            var todoitem = await _todoitemRepository.GetAll();
            if (todoitem == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "Todoitems does not exist",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Todoitems retrieved successfully",
                Success = true,
                Data = todoitem.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority,
                    CustomerId = todoitem.CustomerId
                }).ToList()
            };
        }

        public async Task<BaseResponse<IEnumerable<TodoitemDto>>> GetAllUpcomingTask()
        {
            var todoitem = await _todoitemRepository.GetAllUpcomingTask();
            if (todoitem == null)
            {
                return new BaseResponse<IEnumerable<TodoitemDto>>
                {
                    Message = "No task found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<TodoitemDto>>
            {
                Message = "Task successfully retrieved",
                Success = true,
                Data = todoitem.Select(todoitem => new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority
                }).ToList()
            };
        }

        public async Task<BaseResponse<TodoitemDto>> GetTodoitem(int id)
        {
            var todoitem = await _todoitemRepository.Get(id);
            if (todoitem == null)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "Todoitem not found",
                    Success = false
                };
            }
            return new BaseResponse<TodoitemDto>
            {
                Message = "Todoitem retrieved successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority,
                    CustomerId = todoitem.CustomerId,
                    Categories = todoitem.TodoitemCategory.Select(t => new CategoryDto
                    {
                        Id = t.CategoryId,
                        Name = t.Category.Name,
                    }).ToList()
                }
            };
        }

        public Task<BaseResponse<TodoitemDto>> GetTodoitemByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        private List<DateTime> GenerateInterval(DateTime start, DateTime endTime, string interval)
        {
            var intervalConverted = TimeSpan.Parse(interval);
            List<DateTime> TimeList = new List<DateTime>();
            TimeList.Add(start);
            while (start < endTime)
            {
                var t = start;
                var r = t + intervalConverted;
                TimeList.Add(r);
                start = r;
                var sample = (r.Date.AddHours(r.Hour) + intervalConverted) + intervalConverted;
                if (sample > endTime) { break; }
            }
            if (start < endTime)
            {
                TimeList.Add(endTime);
            }
            return TimeList;
        }

        public async Task<BaseResponse<TodoitemDto>> RegisterTodoitem(TodoRequestModel model, int id)
        {
            var user = await _userRepository.Get(id);
            var customer = await _customerRepository.GetByEmail(user.Email);
            var todoitem = await _todoitemRepository.ExistsByNameAndTime(model.Name, model.OriginalTime);

            if (todoitem == true)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "Todoitem already exist"
                };
            }
            var todoitems = new Todoitem
            {
                Name = model.Name,
                Description = model.Description,
                Priority = model.Priority,
                TimeInterval = TimeSpan.Parse(model.TimeInterval),
                OriginalTime = model.OriginalTime,
                StartingTime = model.StartingTime,
                CustomerId = customer.Id
            };
            var categories = await _categoryRepository.GetSelectedCategories(model.Categories);
            foreach(var category in categories)
            {
                var todoitemCategory = new TodoitemCategory
                {
                    Todoitem = todoitems,
                    TodoitemId = todoitems.Id,
                    Category = category,
                    CategoryId = category.Id
                };
                todoitems.TodoitemCategory.Add(todoitemCategory);
            }

            var x = await _todoitemRepository.Create(todoitems);
            List<Reminder> reminders = new List<Reminder>();
            var reminderIntervals = GenerateInterval(model.StartingTime, model.OriginalTime, model.TimeInterval);
            foreach (var time in reminderIntervals)
            {
                var reminder = new Reminder
                {
                    Time = time,
                    TodoitemId = todoitems.Id
                };
                reminders.Add(reminder);
            }
            await _reminderRepository.AddRange(reminders);
            if (DateTime.Now.Date == model.OriginalTime.Date)
            {
                todoitems.Status = Status.Today;
                await _todoitemRepository.Update(todoitems);
            }
            else
            {
                todoitems.Status = Status.UpComing;
                await _todoitemRepository.Update(todoitems);
            }
            return new BaseResponse<TodoitemDto>
            {
                Message = "Todoitem created successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = todoitems.Id,
                    Name = todoitems.Name,
                    Description = todoitems.Description,
                    OriginalTime = todoitems.OriginalTime,
                    StartingTime = todoitems.StartingTime,
                    Status = todoitems.Status,
                    TimeInterval = todoitems.TimeInterval,
                    Priority = todoitems.Priority
                }
            };
        }

        public async Task<BaseResponse<TodoitemDto>> UpdateTodoitem(int id, UpdateTodoitemRequestModel model)
        {
            var todoitem = await _todoitemRepository.Get(id);
            if (todoitem == null)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "Todoitem to be updated does not exist",
                    Success = false
                };
            }
            todoitem.Description = model.Description ?? todoitem.Description;
            todoitem.Name = model.Name ?? todoitem.Name;
            todoitem.TimeInterval = model.TimeInterval;
            todoitem.OriginalTime = model.OriginalTime;
            todoitem.StartingTime = model.StartingTime;
            await _todoitemRepository.Update(todoitem);
            return new BaseResponse<TodoitemDto>
            {
                Message = "Todoitem updated successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = todoitem.Id,
                    Name = todoitem.Name,
                    Description = todoitem.Description,
                    OriginalTime = todoitem.OriginalTime,
                    StartingTime = todoitem.StartingTime,
                    Status = todoitem.Status,
                    TimeInterval = todoitem.TimeInterval,
                    Priority = todoitem.Priority,
                }
            };
        }

        public async Task<BaseResponse<bool>> UpdateTodoitemToSkipped(IList<Todoitem> todoitem)
        {
            foreach (var item in todoitem)
            {
                item.Status = Enum.Status.Skipped;
            }
            await _todoitemRepository.Update(todoitem);
            return new BaseResponse<bool>
            {
                Message = "Task updated successfully",
                Success = true,
                Data = true
            };
        }

        public async Task<BaseResponse<TodoitemDto>> Skipped(int id)
        {
            var skip = await _todoitemRepository.Get(id);
            if (skip == null)
            {
                return new BaseResponse<TodoitemDto>
                {
                    Message = "not found ",
                    Success = false
                };
            }
            //    if(DateTime.Now !> skip.OriginalTime)
            //    {
            //         return null;
            //    }
            skip.Status = Enum.Status.Skipped;
            await _todoitemRepository.Update(skip);
            return new BaseResponse<TodoitemDto>
            {
                Message = "Todoitem updated successfully",
                Success = true,
                Data = new TodoitemDto
                {
                    Id = skip.Id,
                    Name = skip.Name,
                    Description = skip.Description,
                    OriginalTime = skip.OriginalTime,
                    StartingTime = skip.StartingTime,
                    Status = skip.Status,
                    TimeInterval = skip.TimeInterval,
                    Priority = skip.Priority
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<Todoitem>>> GetAllTBUUpcomingTask()
        {
            var task = await _todoitemRepository.GetAllTBUUpcomingTask();
            if(task == null)
            {
                return new BaseResponse<IEnumerable<Todoitem>>
                {
                    Message = "No task found",
                    Success = false
                };
            }
            return new BaseResponse<IEnumerable<Todoitem>>
            {
                Message = "Task successfully retrieved ",
                Success = true,
                Data = task.Select(task => new Todoitem
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    OriginalTime = task.OriginalTime,
                    StartingTime = task.StartingTime,
                    Status = task.Status,
                    TimeInterval = task.TimeInterval,
                    Priority = task.Priority
                }).ToList()
            };
        }
    }
}