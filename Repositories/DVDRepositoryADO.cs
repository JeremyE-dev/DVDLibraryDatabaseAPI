using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DVDLibraryDatabaseWebAPIv2.Models;
using System.Data;
using System.Configuration;

namespace DVDLibraryDatabaseWebAPIv2.Repositories
{
    public class DVDRepositoryADO : IDvdRepository
    {
        //Takes in a dvd model and adds to DvdLibrary via stored procedure
        //Stored procedure takes in a ReleaseYear, Director, ratingID, but DVD 
        public void Add(Dvd dvd)
        {
            //1.) create dvd id
            int newDvdId = GetAll().Count + 1;

            //if release year passed in is not null
            if(dvd.ReleaseYear != null)
            {
                //find th id number of the release year passed in 
                int? releaseYearId = GetReleaseYearId(dvd.ReleaseYear);
                
                //at this point in code if GetReleaseYeaqrId -- from db-- is null then the release year doesnot exist, so create a record with 
                //an id number which is total records in the release year table plus one and and the release year passed in
                if (releaseYearId == null)
                {
                    //1/7/21 - create procedure to calculate id number
                    //releaseYearId = number of records in release year table plus one
                    AddReleaseYearAndIdToTable((int)releaseYearId, (int)dvd.ReleaseYear);
                }            
            }
        
            //2.) Does Year Included Exist?
            // if there isno release yearid associated with the release year, then create one.
        


            // 2.) Take in Year from request
            // if null - leave null
            // if year included - check db to see if it exists
            // if exists - get id and add to request model
            // if does not exists - get number of records add 1 to that number
            // add that number and the release year to the release Year Table
            // repeat process for director and rating


            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdInsert";
                //sql model needs a ReleaseYearId versus year -- 
                //cmd.Parameters.AddWithValue("@ReleaseYearId", dvd.ReleaseYear);
                cmd.Parameters.AddWithValue("@DvdId", newDvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                conn.Open();
            }
        }

        public void Delete(int dvdID)
        {
            throw new NotImplementedException();
        }

        public void Edit(Dvd dvd)
        {
            throw new NotImplementedException();
        }

    

        public List<Dvd> GetAll()
        {
            //this is the LIST of DVds
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectAll";
                conn.Open();
            
                //add SQLdataReader Block next

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data
                      
                        // This is a SINGLE dvd that will be added to the list
                        Dvd currentRow = new Dvd();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];

                        currentRow.DirectorName = dr["DirectorName"].ToString();
                        currentRow.RatingName = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;


        }

        public List<Dvd> GetByDirectorName(string directorName)
        {
            //throw new NotImplementedException();
            //this is the LIST of DVds
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByDirectorName";
                cmd.Parameters.AddWithValue("@DirectorName", directorName);

                conn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data

                        // This is a SINGLE dvd that will be added to the list
                        Dvd currentRow = new Dvd();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];

                        currentRow.DirectorName = dr["DirectorName"].ToString();
                        currentRow.RatingName = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;

        }

        public Dvd GetById(int dvdId)
        {
            //throw new NotImplementedException();
            //this is the LIST of DVds
            Dvd dvd = new Dvd();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectById";
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data
                        
                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            dvd.ReleaseYear = (int)dr["ReleaseYear"];

                        dvd.DirectorName = dr["DirectorName"].ToString();
                        dvd.RatingName = dr["RatingName"].ToString();
                        dvd.Notes = dr["Notes"].ToString();

                        
                    }
                }
            }

            return dvd;
        }

        public List<Dvd> GetByRating(string rating)
        {
            //throw new NotImplementedException();
            //this is the LIST of DVds
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByRating";
                cmd.Parameters.AddWithValue("@RatingName", rating);

                conn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data

                        // This is a SINGLE dvd that will be added to the list
                        Dvd currentRow = new Dvd();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];

                        currentRow.DirectorName = dr["DirectorName"].ToString();
                        currentRow.RatingName = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public List<Dvd> GetByReleaseYear(int releaseYear)
        {
            
            //this is the LIST of DVds
            List<Dvd> dvds = new List<Dvd>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByReleaseYear";
                cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);

                conn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data

                        // This is a SINGLE dvd that will be added to the list
                        Dvd currentRow = new Dvd();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            currentRow.ReleaseYear = (int)dr["ReleaseYear"];

                        currentRow.DirectorName = dr["DirectorName"].ToString();
                        currentRow.RatingName = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public Dvd GetByTitle(string title)
        {
            Dvd dvd = new Dvd();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdSelectByTitle";
                cmd.Parameters.AddWithValue("@Title", title);

                conn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    { //inside while loop dealing with single row of data

                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();

                        //check for null values before casting from ReleaseYear since it is a nullable int
                        if (dr["ReleaseYear"] != DBNull.Value)
                            dvd.ReleaseYear = (int)dr["ReleaseYear"];

                        dvd.DirectorName = dr["DirectorName"].ToString();
                        dvd.RatingName = dr["RatingName"].ToString();
                        dvd.Notes = dr["Notes"].ToString();


                    }
                }
            }

            return dvd;
        }

        public int? GetReleaseYearId(int? releaseYear)
        {

            int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetReleaseYearID";
                cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    
                    while (dr.Read())
                    { //inside while loop dealing with single row of data

                        if (dr["ReleaseYear"] != DBNull.Value)
                            id = (int)dr["ReleaseYearId"];


                    }
                }

            }
            return id;
        }

        public void AddReleaseYearAndIdToTable(int releaseYearId, int releaseYear)
        {

            int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertReleaseYearIdandYear";
                cmd.Parameters.AddWithValue("@ReleaseYearId", releaseYearId);
                cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);


                conn.Open();

                cmd.ExecuteNonQuery();

            }
            
        }

    }
}