﻿using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                return _db.Dailies.Include(a => a.Tags).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Daily>();
            }
        }
    }
}
