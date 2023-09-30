using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories 
{
    public class DailyRepository : BaseRepository, IDailyRepository
    {
        public DailyRepository(ApplicationDbContext db) : base(db)
        {
        }
        public IEnumerable<Daily> GetAll()
        {
            try
            {
                return _db.Dailies.Where(a => a.Done == false).Include(a => a.Tags).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Daily>();
            }
        }

        public Daily GetById(int id)
        {
            try
            {
                return _db.Dailies.Where(a => a.Id == id).FirstOrDefault(); 
            } catch (Exception ex) {
                return null; 
            }
        }

        public bool MarkAsDone(Daily daily)
        {
            try
            {
                daily.Done = true;
                _db.Update(daily);
                _db.SaveChanges();
                return true;
            } catch( Exception ex)
            {
                return false; 
            }
        }

        public bool Save(Daily daily)
        {
            try
            {
                _db.Add(daily);
                _db.SaveChanges(); 
                return true; 
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
