﻿using Acibitah.Models;
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
        bool Remove(int id);
        ToDoTask? GetById(int id);
        bool Update(ToDoTask task);
    }
}
