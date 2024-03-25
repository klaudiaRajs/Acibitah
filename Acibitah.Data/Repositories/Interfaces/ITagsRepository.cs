using Acibitah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Data.Repositories.Interfaces
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> GetAll();
        bool Save(Tag tag);
    }
}
