using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public interface IDvdRepository
    {
        //returns all dvds in List
        List<Dvd> GetAll();


        //returns one movie based on ID number
        Dvd GetById(int dvdId);
        Dvd GetByReleaseYear(int releaseYear);
        Dvd GetByTitle(string title);

        Dvd GetByDirectorName(string director);
        Dvd GetByRating(string rating);


        //adds a dvd to a the dvd list
        void Add(Dvd dvd);


        //replaces dvd with the dvd passed in through parameter if found
        void Edit(Dvd dvd);


        //removes dvd by id number
        void Delete(int dvdID);
       
    }
}