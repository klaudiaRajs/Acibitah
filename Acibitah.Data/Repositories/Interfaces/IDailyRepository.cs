using Acibitah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories.Interfaces
{
    public interface IDailyRepository
    {
        IEnumerable<Daily> GetAll();
        Daily GetById(int id);
        bool MarkAsDone(Daily daily);
        bool Save(Daily habit);
    }
}
