using DVDLibraryDatabaseWebAPIv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{


    //per specifications, this repo must provide
    //--COMPLETED---------------------------
    //Retreive a Dvd by ID - get --COMPLETED
    //Retreive all DVDs - get --COMPLETED
    //Update an Existing DVD - PUT --COMPLETED
    //Create a new DVD - Post -- COMPLETED
    //Delete an ID - Delete --COMPLETED

    //--NEED TO COMPLETE
    //Retreive DVD by title - get
    //Retreive DVD by Release Year - get
    //Retreive DVD by DirectorName - get
    //Retreive DVD by Ratinhg - get

    public class DvdRepositoryMock : IDvdRepository
    {
        //Sample Data
        //since it is static, it will be shared across all web requests
        // and any changes to data will remain in memory as long as WEp API project is running
        private static List<Dvd> _dvds = new List<Dvd>
        {
            new Dvd {DvdId = 0, Title = "A Great Tale", ReleaseYear1 = 2015, director = "Sam Jones",
                rating = "PG", Notes = "This is a really great tale"},

            new Dvd {DvdId = 1, Title = "A Good Tale", ReleaseYear1 = 2012, director = "Joe Smith",
                rating = "PG-13", Notes = "This is a good tale"}

        };

        //note: 12/31/20 removed "static from the folowing methods, as static methods not allowed when implementing an interface
        //This is a repository for test data and was intended for the data not to change for testing purposes in original
        // project, for this project it will likely not impact functioing properly but data may change in different runs of program
        // because the methods are no longer static.
        // watch for errors in data received.

        //returns all dvds in List
        //Retreive all DVDs - GET
        public List<Dvd> GetAll()
        {
            return _dvds;
        }

        //returns one movie based on ID number
        //Retreive a Dvd by ID - GET
        public Dvd GetById(int dvdId)
        {
            return _dvds.FirstOrDefault(m => m.DvdId == dvdId);
        }

        public List<Dvd> GetByReleaseYear(int releaseYear)
        {
            List<Dvd> listOfDvdsByReleaseYear = new List<Dvd>();
            var dvdsByReleaseYear = from d in _dvds
                                 where d.ReleaseYear1 == releaseYear
                                 select d;

            foreach (Dvd x in dvdsByReleaseYear)
            {
                listOfDvdsByReleaseYear.Add(x);
            }

            return listOfDvdsByReleaseYear;
        }
        
        //Retreive DVD by title - get'
        public Dvd GetByTitle(String title)
        {
            return _dvds.FirstOrDefault(m => m.Title == title);
        }

        //Retreive all DVDs by DirectorName - get
        public List<Dvd> GetByDirectorName(String director)
        {
            List<Dvd> listOfDvdsByDirector = new List<Dvd>();
            var dvdsByDirector = from d in _dvds
                                 where d.director == director
                                 select d;
            
            foreach(Dvd x in dvdsByDirector)
            {
                listOfDvdsByDirector.Add(x);
            }

            return listOfDvdsByDirector;
        }
        //Retreive DVD by Rating - get
        public List<Dvd> GetByRating(String rating)
        {
            List<Dvd> listOfDvdsByRating = new List<Dvd>();
            var dvdsByRating = from d in _dvds
                                 where d.rating == rating
                                 select d;

            foreach (Dvd x in dvdsByRating)
            {
                listOfDvdsByRating.Add(x);
            }

            return listOfDvdsByRating;
        }
        //Create a new DVD - Post
        public void Add(Dvd dvd)
        {
            dvd.DvdId = _dvds.Max(d => d.DvdId) + 1;
            _dvds.Add(dvd);
        }

        //replaces dvd with the dvd passed in through parameter if found
        ////Update an Existing DVD - PUT
        public void Edit(Dvd dvd)
        {
            var found = _dvds.FirstOrDefault(d => d.DvdId == dvd.DvdId);

            if (found != null)
                found = dvd;
        }

        //removes dvd by id number
        ////Delete an ID - Delete
        public void Delete(int dvdID)
        {
            _dvds.RemoveAll(d => d.DvdId == dvdID);
        }

        List<Dvd> IDvdRepository.GetByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}