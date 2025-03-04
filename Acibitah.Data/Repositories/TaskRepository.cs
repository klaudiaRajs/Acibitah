﻿using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.EntityFrameworkCore;

namespace Acibitah.Data.Repositories
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<ToDoTask> GetAll()
        {
            try
            {
                return _db.ToDoTasks.Where(a => a.Done == false).Include(task => task.TagsTasks).ThenInclude(link => link.Tag).ToList(); 
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

        public IEnumerable<Subtask> GetSubtasksByTask(ToDoTask task)
        {
            try
            {
                using( ApplicationDbContext db = new ApplicationDbContext())
                {
                    return db.Subtasks.Where(a => a.TaskId == task.Id).ToList();
                }
            } catch (Exception ex)
            {
                return Enumerable.Empty<Subtask>();
            }
        }

        public bool MarkAsDone(ToDoTask todo)
        {
            try
            {
                todo.Done = true;
                _db.Update(todo);
                _db.SaveChanges();
                return true; 
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(ToDoTask task)
        {
            try
            {
                _db.Remove(task);
                _db.SaveChanges();
                return true;
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
