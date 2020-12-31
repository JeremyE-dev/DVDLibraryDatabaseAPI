using DVDLibraryDatabaseWebAPIv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{
    public class DvdRepositoryMock : IDvdRepository
    {
        //Sample Data
        //since it is static, it will be shared across all web requests
        // and any changes to data will remain in memory as long as WEp API project is running
        private static List<Dvd> _dvds = new List<Dvd>
        {
            new Dvd {DvdId = 0, Title = "A Great Tale", ReleaseYear = 2015, Director = "Sam Jones",
                Rating = "PG", Notes = "This is a really great tale"},

            new Dvd {DvdId = 1, Title = "A Good Tale", ReleaseYear = 2012, Director = "Joe Smith",
                Rating = "PG-13", Notes = "This is a good tale"}

        };

        //note: 12/31/20 removed "static from the folowing methods, as static methods not allowed when implementing an interface
        //This is a repository for test data and was intended for the data not to change for testing purposes in original
        // project, for this project it will likely not impact functioing properly but data may change in different runs of program
        // because the methods are no longer static.
        // watch for errors in data received.

        //returns all dvds in List
        public List<Dvd> GetAll()
        {
            return _dvds;
        }

        //returns one movie based on ID number
        public Dvd Get(int dvdId)
        {
            return _dvds.FirstOrDefault(m => m.DvdId == dvdId);
        }

        //adds a dvd to a the dvd list
        public void Add(Dvd dvd)
        {
            dvd.DvdId = _dvds.Max(d => d.DvdId) + 1;
            _dvds.Add(dvd);
        }

        //replaces dvd with the dvd passed in through parameter if found
        public void Edit(Dvd dvd)
        {
            var found = _dvds.FirstOrDefault(d => d.DvdId == dvd.DvdId);

            if (found != null)
                found = dvd;
        }

        //removes dvd by id number
        public void Delete(int dvdID)
        {
            _dvds.RemoveAll(d => d.DvdId == dvdID);
        }



    }
}