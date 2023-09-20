using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories
{
    public class HeroRepository : BaseRepository, IHeroRepository
    {
        const int CURRENT_USER_ID = 4;

        public HeroRepository(ApplicationDbContext db) : base(db)
        {
        }


    }
}
