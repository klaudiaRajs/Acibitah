using Acibitah.Models;

namespace Acibitah.Data.Repositories.Interfaces
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> GetAll();
        bool Save(Tag tag);
    }
}
