using Acibitah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories.Interfaces
{
    public interface IHabitRepository
    {
        IEnumerable<Habit> GetAll();
        Habit? GetById(int id);
        void IncreaseStreak(Habit? habit);
        bool Save(Habit habit); 
    }
}
