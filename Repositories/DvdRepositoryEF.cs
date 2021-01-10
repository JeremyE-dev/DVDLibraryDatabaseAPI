using DVDLibraryDatabaseWebAPIv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{
    public class DvdRepositoryEF : IDvdRepository
    {
        public void Add(Dvd dvd)
        {
            throw new NotImplementedException();
        }

        public void Delete(int dvdID)
        {
            throw new NotImplementedException();
        }

        public void Edit(Dvd dvd)
        {
            throw new NotImplementedException();
        }

        public Dvd Get(int dvdId)
        {
            throw new NotImplementedException();
        }

        public List<Dvd> GetAll()
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                          select d).ToList();

            return allDvds;
        

        }

        public List<Dvd> GetByDirectorName(string director)
        {
            throw new NotImplementedException();
        }

        public Dvd GetById(int dvdId)
        {
            throw new NotImplementedException();
        }

        public List<Dvd> GetByRating(string rating)
        {
            throw new NotImplementedException();
        }

        public List<Dvd> GetByReleaseYear(int releaseYear)
        {
            throw new NotImplementedException();
        }

        public Dvd GetByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}