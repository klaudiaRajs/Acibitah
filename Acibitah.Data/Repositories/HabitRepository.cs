using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.EntityFrameworkCore;
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
                return _db.Habits.Include(a => a.Tags).ToList();
            }
            catch (Exception ex)
            {
                //TODO dodaj logger 
                return Enumerable.Empty<Habit>();
            }
        }

        public bool Save(Habit habit)
        {
            try
            {
                _db.Habits.Add(habit);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
