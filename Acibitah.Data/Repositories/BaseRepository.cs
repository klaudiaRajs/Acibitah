using Acibitah.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories
{
    public class BaseRepository
    {
        protected ApplicationDbContext _db;
        public BaseRepository(ApplicationDbContext db)
        {
           _db = db;
        }
    }
}
