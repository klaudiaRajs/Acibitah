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
    public class HabitRepository : BaseRepository, IHabitRepository
    {
        public HabitRepository(ApplicationDbContext db) : base(db)
        {
        }
        public IEnumerable<Habit> GetAll()
        {
            try
            {
                return _db.Habits.ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Habit>();
            }
        }
    }
}
