using DVDLibraryDatabaseWebAPIv2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{
    public class DvdRepositoryEF : IDvdRepository
    {
        public void Add(Dvd dvd)
        {
            var repository = new DvdLibraryEntities();
            Dvd d = new Dvd();

            List<Dvd> allDvds = (from a in repository.Dvds
                                 select a).ToList();
            d.DvdId = dvd.DvdId;
            d.Title = dvd.Title;
            d.DirectorName = dvd.DirectorName;
            d.DirectorId = dvd.DirectorId;
            d.RatingName = dvd.RatingName;
            d.RatingId = dvd.RatingId;
            d.ReleaseYearName = dvd.ReleaseYearName;
            d.ReleaseYearId = dvd.ReleaseYearId;
            d.Notes = dvd.Notes;

            repository.Dvds.Add(d);
            repository.SaveChanges();

            //Dvd addedDvd = new Dvd();

            //ReleaseYear test = new DVDLibraryDatabaseWebAPIv2.Models.ReleaseYear();



            //var repository = new DvdLibraryEntities();
            //List<Dvd> allDvds = (from d in repository.Dvds
            //                     select d).ToList();
            //dvd.DvdId = allDvds.Count() + 1;

            //int? releaseYearId = null;
            //int? directorId = null;
            //int? ratingId = null;

            //if(dvd.ReleaseYearName != null)
            //{
            //    //get the id of that releaseyear if it exists
            //    releaseYearId = (from r in repository.ReleaseYears
            //                     where r.ReleaseYearName == dvd.ReleaseYearName 
            //                     select r.ReleaseYearId).First();
            //    if (releaseYearId == null)
            //    {
            //        releaseYearId = repository.ReleaseYears.Count() + 1;
            //        //add releaseYear and name to table
            //        //ReleaseYear r = new ReleaseYear();
            //        test.ReleaseYearId = (int)releaseYearId;
            //        test.ReleaseYearName = dvd.ReleaseYearName;
            //        repository.ReleaseYears.Add(test);
            //        repository.SaveChanges();
            //    }

            //}


            //if (dvd.DirectorName != null)
            //{
            //    //get the id of that releaseyear if it exists
            //   directorId = (from d in repository.Directors
            //                     where d.DirectorName == dvd.DirectorName
            //                     select d.DirectorId).First();
            //    if (directorId == null)
            //    {
            //        directorId = repository.Directors.Count() + 1;
            //        //add releaseYear and name to table
            //        Director d = new Director();
            //        d.DirectorId = (int)directorId;
            //        d.DirectorName = dvd.DirectorName;
            //        repository.Directors.Add(d);
            //        repository.SaveChanges();
            //    }

            //}

            //ratingId = (from r in repository.Ratings
            //            where r.RatingName == dvd.RatingName
            //            select r.RatingId).First();

            //if (dvd.RatingName != null)
            //{
            //    //get the id of that releaseyear if it exists
            //    ratingId = (from d in repository.Ratings
            //                  where d.RatingName == dvd.RatingName
            //                  select d.RatingId).First();
            //    if (ratingId == null)
            //    {
            //        ratingId = repository.Directors.Count() + 1;
            //        //add releaseYear and name to table
            //        Rating r = new Rating();
            //        r.RatingId = (int)ratingId;
            //        r.RatingName = dvd.RatingName;
            //        repository.Ratings.Add(r);
            //        repository.SaveChanges();
            //    }

            //}

            //addedDvd.DvdId = dvd.DvdId;
            //addedDvd.Title = dvd.Title;
            //addedDvd.DirectorName = dvd.DirectorName;
            //addedDvd.DirectorId = dvd.DirectorId;
            //addedDvd.RatingName = dvd.RatingName;
            //addedDvd.RatingId = dvd.RatingId;
            //addedDvd.ReleaseYearName = dvd.ReleaseYearName;
            //addedDvd.ReleaseYearId = dvd.ReleaseYearId;
            //addedDvd.Notes = dvd.Notes;

            //repository.Dvds.Add(addedDvd);
            //repository.SaveChanges();

        }

        public void Delete(int dvdID)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 select d).ToList();


            Dvd dvdToDelete =  allDvds.FirstOrDefault(d => d.DvdId == dvdID);

            if(dvdToDelete != null)
            {
                repository.Dvds.Remove(dvdToDelete);
                repository.SaveChanges();
            }
        }

        public void Edit(Dvd dvd)
        {
            var repository = new DvdLibraryEntities();


            Dvd d = new Dvd();

            //List<Dvd> allDvds = (from a in repository.Dvds
                                 //select a).ToList();
            d.DvdId = dvd.DvdId;
            d.Title = dvd.Title;
            d.DirectorName = dvd.DirectorName;
            d.DirectorId = dvd.DirectorId;
            d.RatingName = dvd.RatingName;
            d.RatingId = dvd.RatingId;
            d.ReleaseYearName = dvd.ReleaseYearName;
            d.ReleaseYearId = dvd.ReleaseYearId;
            d.Notes = dvd.Notes;

            repository.Entry(d).State = EntityState.Modified;
            repository.SaveChanges();
        }

        //may not need this
        //public Dvd Get(int dvdId)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Dvd> GetAll()
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                          select d).ToList();

            return allDvds;

            
        

        }

        public List<Dvd> GetByDirectorName(string director)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 where d.DirectorName == director
                                 select d).ToList();

            return allDvds;
        }

        public Dvd GetById(int dvdId)
        {

            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 select d).ToList();


             return allDvds.FirstOrDefault(d => d.DvdId == dvdId);
                       
        }

        public List<Dvd> GetByRating(string rating)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 where d.RatingName == rating
                                 select d).ToList();

            return allDvds;
        }

        public List<Dvd> GetByReleaseYear(int releaseYear)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 where d.ReleaseYearName == releaseYear
                                 select d).ToList();

            return allDvds;
        }

        public Dvd GetByTitle(string title)
        {
            var repository = new DvdLibraryEntities();
            List<Dvd> allDvds = (from d in repository.Dvds
                                 select d).ToList();

            return allDvds.FirstOrDefault(d => d.Title == title);
        }
    }
}