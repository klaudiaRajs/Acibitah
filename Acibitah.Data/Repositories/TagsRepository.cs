﻿using Acibitah.Data.Data;
using Acibitah.Data.Repositories.Interfaces;
using Acibitah.Models;

namespace Acibitah.Data.Repositories
{
    public class TagsRepository : BaseRepository, ITagsRepository
    {
        public TagsRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Tag> GetAll()
        {
            try
            {
                return _db.Tags.ToList();
            } catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Tag tag)
        {
            try
            {
                _db.Tags.Add(tag);
                _db.SaveChanges();
                return true; 
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
