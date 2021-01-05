﻿using System;
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