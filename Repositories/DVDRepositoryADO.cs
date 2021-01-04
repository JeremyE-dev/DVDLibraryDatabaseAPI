using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DVDLibraryDatabaseWebAPIv2.Models;
using System.Data;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{
    public class DVDRepositoryADO : IDvdRepository
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
            //throw new NotImplementedException();
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=localhost; Database=DvdLibrary; User Id=sa;Password=sqlserver";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectAll";
                conn.Open();
            
                //add SQLdataReader Block next

            }

            return dvds;


        }

        public Dvd GetByDirectorName(string director)
        {
            throw new NotImplementedException();
        }

        public Dvd GetById(int dvdId)
        {
            throw new NotImplementedException();
        }

        public Dvd GetByRating(string rating)
        {
            throw new NotImplementedException();
        }

        public Dvd GetByReleaseYear(int releaseYear)
        {
            throw new NotImplementedException();
        }

        public Dvd GetByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}