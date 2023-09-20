using Acibitah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        bool Save(ToDoTask task);
        IEnumerable<ToDoTask> GetAll();
        IEnumerable<Subtask> GetSubtasksByTask(ToDoTask task);
        bool Remove(ToDoTask task);
        ToDoTask? GetById(int id);
        bool Update(ToDoTask task);
    }
}
