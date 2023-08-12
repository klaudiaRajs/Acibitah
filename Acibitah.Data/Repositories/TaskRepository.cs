using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private ApplicationDbContext _db; 
        public TaskRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ToDoTask> GetAll()
        {
            try
            {
                return _db.ToDoTasks.ToList(); 
            } catch (Exception ex) 
            {
                return Enumerable.Empty<ToDoTask>();
            }
        }

        public ToDoTask? GetById(int id)
        {
            try
            {
                return _db.ToDoTasks.FirstOrDefault(a => a.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                //TODO przenieś to do kontrolera? 
                ToDoTask? record = _db.ToDoTasks.FirstOrDefault(a => a.Id == id);
                if (record != null)
                {
                    _db.Remove(record);
                    _db.SaveChanges();
                    return true;
                }
                return false; 
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool Save(ToDoTask task)
        {
            try
            {
                _db.ToDoTasks.Add(task);
                _db.SaveChanges(); 
                return true;
            } catch (Exception ex)
            {
                return false;
            }            
        }

        public bool Update(ToDoTask task)
        {
            try
            {
                //TODO przenieś to do kontrolera? 
                ToDoTask? record = _db.ToDoTasks.FirstOrDefault(a => a.Id == task.Id);
                if (record != null)
                {
                    record.Title = task.Title;
                    record.Content = task.Content;
                    record.Difficulty = task.Difficulty;

                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
