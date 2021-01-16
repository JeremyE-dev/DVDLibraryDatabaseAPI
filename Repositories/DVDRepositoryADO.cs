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
            
           
            //assign ID to getall + 1
            dvd.DvdId = GetAll().Count + 1;

            int? releaseYearId = null;
            int? directorId = null;
            int? ratingId = null;

            //IF DVD RELEASE YEAR IS NULL IN THE DVD PASSED IN LEAVE IT NULL
            //-- USER IS NOT REQUIRED TO INCLUDE A RELEASE YEAR--
            //IF IT IS NOT NULL - FIND THE ID NUMBER OF THE RELEASE YEAR AND ADD THAT ID NUMBER TO THE DVD WHEN AADDING IT TO THE TABLE
            
            //debug notes: if a releaseyear is passed in, get he releaseYearid of that year
            if(dvd.releaseYear != null)
            {
                //IF A RELEASE YEAR IS INCLUDED IN THE ADD REQUEST
                //--SEARCH FOR RELEASE YEAR IN DATABASE, SET INTEGER TO DATA RETURNED BY SEARCH METHOD
                //--IF DATA RETURNED BY SEARCH METHOD IS NULL
                //----ADD THE RELEASEYEAR(OF THE DVD PASSED IN) AND RELEASEYEARID TO THE TABLE
              
                releaseYearId = GetIdByTableContentInteger(dvd.releaseYear, "GetReleaseYearId", "@ReleaseYear", 
                    "releaseYear", "ReleaseYearId");

                //IF RELEASEYEAR IS NOT NULL, THEN IT IS IN THE TABLE AND CAN ADD RELEASEYEARID TO DVD
                if (releaseYearId == null)
                {
                    //SET ID NUMBER USING NUMBER OF RECORDS METHOD AND ADD ONE
                    //releaseYearId = GetNumberOfRecordsInReleaseYear() + 1;
                    //USING GENERALIZED VERSION
                    releaseYearId = GetNumberOfRecordsInTable("NumberOfRecordsInReleaseYear") + 1;

                    //ADD THE RELEASE YEAR AND ID TO TABLE
                    //AddReleaseYearAndIdToTable((int)releaseYearId, (int)dvd.ReleaseYear);
                    //USING GENERALIZED VERSION
                    AddDataAndIdToTable((int)releaseYearId, (int)dvd.releaseYear, "InsertReleaseYearIdAndYear", 
                        "@ReleaseYearId", "@ReleaseYear");
                }
            }

            //is director name null at this point
            if (dvd.director != null)
            {
                //IF A RELEASE YEAR IS INCLUDED IN THE ADD REQUEST
                //--SEARCH FOR RELEASE YEAR IN DATABASE, SET INTEGER TO DATA RETURNED BY SEARCH METHOD
                //--IF DATA RETURNED BY SEARCH METHOD IS NULL
                //----ADD THE RELEASEYEAR(OF THE DVD PASSED IN) AND RELEASEYEARID TO THE TABLE

                directorId = GetIdByTableContentString(dvd.director, "GetDirectorId", "@DirectorName",
                    "DirectorName", "DirectorId");

                //IF RELEASEYEAR IS NOT NULL, THEN IT IS IN THE TABLE AND CAN ADD RELEASEYEARID TO DVD
                if (directorId == null)
                {
                    //SET ID NUMBER USING NUMBER OF RECORDS METHOD AND ADD ONE
                    //releaseYearId = GetNumberOfRecordsInReleaseYear() + 1;
                    //USING GENERALIZED VERSION
                    directorId = GetNumberOfRecordsInTable("NumberOfRecordsInDirector") + 1;

                    //ADD THE RELEASE YEAR AND ID TO TABLE
                    //AddReleaseYearAndIdToTable((int)releaseYearId, (int)dvd.ReleaseYear);
                    //USING GENERALIZED VERSION
                    AddStringDataAndIdToTable((int)directorId, dvd.director, "InsertDirectorIdAndName",
                        "@DirectorId", "@DirectorName");
                }
            }

            //rating
            if (dvd.rating != null)
            {
                //IF A RELEASE YEAR IS INCLUDED IN THE ADD REQUEST
                //--SEARCH FOR RELEASE YEAR IN DATABASE, SET INTEGER TO DATA RETURNED BY SEARCH METHOD
                //--IF DATA RETURNED BY SEARCH METHOD IS NULL
                //----ADD THE RELEASEYEAR(OF THE DVD PASSED IN) AND RELEASEYEARID TO THE TABLE

                //START HERE 1/8/21 CONVER TO USE RATINGnAME DATA
                ratingId = GetIdByTableContentString(dvd.rating, "GetRatingId", "@RatingName",
                    "RatingName", "RatingId");

                //IF RELEASEYEAR IS NOT NULL, THEN IT IS IN THE TABLE AND CAN ADD RELEASEYEARID TO DVD
                if (directorId == null)
                {
                    //SET ID NUMBER USING NUMBER OF RECORDS METHOD AND ADD ONE
                    //releaseYearId = GetNumberOfRecordsInReleaseYear() + 1;
                    //USING GENERALIZED VERSION
                    directorId = GetNumberOfRecordsInTable("NumberOfRecordsInRating") + 1;

                    //ADD THE RELEASE YEAR AND ID TO TABLE
                    //AddReleaseYearAndIdToTable((int)releaseYearId, (int)dvd.ReleaseYear);
                    //USING GENERALIZED VERSION
                    AddStringDataAndIdToTable((int)ratingId, dvd.rating, "InsertRatingIdAndName",
                        "@RatingId", "@RatingName");
                }
            }

            // repeat process for director and rating


            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdInsert";
            
                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                //do I want the releaseYear or Release Year ID
                cmd.Parameters.AddWithValue("@ReleaseYearId", releaseYearId);
                cmd.Parameters.AddWithValue("@DirectorId", directorId);
                cmd.Parameters.AddWithValue("@RatingId", ratingId);

                
                // cmd.Parameters.AddWithValue("@ReleaseYearId", releaseYearId);
                //cmd.Parameters.AddWithValue("@DirectorId", directorId);
                //cmd.Parameters.AddWithValue("@RatingId", ratingId);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int dvdID)
        {
            using (SqlConnection conn = new SqlConnection()) //
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                
                    cmd.Connection = conn; //not including this seemed to cause a no initialization error error
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DvdDelete";
                    cmd.Parameters.AddWithValue("@DvdId", dvdID);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
        
               
            }
        }

        //when it gets here dvd id is null
        public void Edit(Dvd dvd)
        {


            int? releaseYearId = null;
            int? directorId = null;
            int? ratingId = null;

         
            if (dvd.releaseYear != null)
            {
                releaseYearId = GetIdByTableContentInteger(dvd.releaseYear, "GetReleaseYearId", "@ReleaseYear",
                    "ReleaseYear", "ReleaseYearId");

                if (releaseYearId == null)
                {
                    releaseYearId = GetNumberOfRecordsInTable("NumberOfRecordsInReleaseYear") + 1;
                    AddDataAndIdToTable((int)releaseYearId, (int)dvd.releaseYear, "InsertReleaseYearIdAndYear",
                        "@ReleaseYearId", "@ReleaseYear");
                }
            }

            if (dvd.director != null)
            {
                directorId = GetIdByTableContentString(dvd.director, "GetDirectorId", "@DirectorName",
                    "DirectorName", "DirectorId");

                if (directorId == null)
                {
                    directorId = GetNumberOfRecordsInTable("NumberOfRecordsInDirector") + 1;
                    AddStringDataAndIdToTable((int)directorId, dvd.director, "InsertDirectorIdAndName",
                        "@DirectorId", "@DirectorName");
                }
            }

            if (dvd.rating != null)
            {
                ratingId = GetIdByTableContentString(dvd.rating, "GetRatingId", "@RatingName",
                    "RatingName", "RatingId");

                if (directorId == null)
                { 
                    directorId = GetNumberOfRecordsInTable("NumberOfRecordsInRating") + 1;
                    AddStringDataAndIdToTable((int)ratingId, dvd.rating, "InsertRatingIdAndName",
                        "@RatingId", "@RatingName");
                }
            }

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DVdUpdate";
                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@ReleaseYearId",releaseYearId );
                cmd.Parameters.AddWithValue("@DirectorId", directorId);
                cmd.Parameters.AddWithValue("@RatingId", ratingId );
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                //cmd.Parameters.AddWithValue("@ReleaseYearId", releaseYearId);
                //cmd.Parameters.AddWithValue("@DirectorId", directorId);
                //cmd.Parameters.AddWithValue("@RatingId", ratingId);

                conn.Open();

                cmd.ExecuteNonQuery();

            }
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
                            currentRow.releaseYear = (int?)dr["ReleaseYear"];

                        currentRow.director = dr["DirectorName"].ToString();
                        currentRow.rating = dr["RatingName"].ToString();
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
                            currentRow.releaseYear = (int)dr["ReleaseYear"];

                        currentRow.director = dr["DirectorName"].ToString();
                        currentRow.rating = dr["RatingName"].ToString();
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
                            dvd.releaseYear = (int)dr["ReleaseYear"];

                        dvd.director = dr["DirectorName"].ToString();
                        dvd.rating = dr["RatingName"].ToString();
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
                            currentRow.releaseYear = (int)dr["ReleaseYear"];

                        currentRow.director = dr["DirectorName"].ToString();
                        currentRow.rating = dr["RatingName"].ToString();
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
                            currentRow.releaseYear = (int)dr["ReleaseYear"];

                        currentRow.director = dr["DirectorName"].ToString();
                        currentRow.rating = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public List<Dvd> GetByTitle(string title)
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
                cmd.CommandText = "DvdSelectByTitle";
                cmd.Parameters.AddWithValue("@Title", title);

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
                            currentRow.releaseYear = (int)dr["ReleaseYear"];

                        currentRow.director = dr["DirectorName"].ToString();
                        currentRow.rating = dr["RatingName"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;

        }



        //USE THIS TO GET ID FROM SQL DATABASE BY PASSING IN TABLE DATA (i.e. ReleaseYear, Director, Rating)
        //                                       //1974               // GetReleaseYearID    //@ReleaseYear    //
        public int? GetIdByTableContentInteger(int? tablecontent, string commandText, string sqlParameter, string sqlTableName, string sqlFieldInTable )
        {

            int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                //set connection string
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                
                //initialize comand object
                SqlCommand cmd = new SqlCommand();
                
                //initialize connection property
                cmd.Connection = conn;
                
                //set command type to stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                
                //set command type to the name of the stored procedure passed in
                cmd.CommandText = commandText; //ex. "GetReleaseYearID"

                //matc the sql parameter to the parameter passes in //ex. @ReleaseYear, releaseYear
                cmd.Parameters.AddWithValue(sqlParameter, tablecontent);

                //open the connection
                conn.Open();

                id = (int?)cmd.ExecuteScalar();

                //initialize datareader object
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    //call dr.Read()
                //    //-- data reader object contains rows and colums of results
                //    // Read() - returns true if there is a row of data to raed in, false at end of result set
                //    //this will be reading each row in the table
                //    // Id - 1, Year 1974
                //    //Id -2, Year 1975
                //    // I dont need to read each rowe because Id by release year returns a single value, either the id or null
                //    while (dr.Read())
                //    { //inside while loop dealing with single row of data

                //        //is this true or false
                //        //if it gets here there is at leat sonr row of data to read
                //        // if dr[ReleaseYear] is not null 
                //        //doe 1974 exist in the table
                //        if (dr[sqlTableName] != DBNull.Value) //ex. "ReleaseYear" - out of range is here
                //            id = (int)dr[sqlFieldInTable]; // ex. "ReleaseYearId"



                //    }
                //}
                //LOGIC: 
                //--IF THIS YEAR "1974" EXISTS IN THE TABLE, GIVE ME THE ID OF THAT ROW

            }
            return id;
        }

        //USE THIS TO GET ID FROM SQL DATABASE BY PASSING IN TABLE DATA (i.e. ReleaseYear, Director, Rating)
        public int? GetIdByTableContentString(string tablecontent, string commandText, string sqlParameter, string sqlTableName, string sqlFieldInTable)
        {

            int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandText; //ex. "GetReleaseYearID" --name of stored procedure
                cmd.Parameters.AddWithValue(sqlParameter, tablecontent); //ex. @ReleaseYear, releaseYear

                conn.Open();

                id = (int?)cmd.ExecuteScalar();

                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{

                //    while (dr.Read())
                //    { //inside while loop dealing with single row of data

                //        if (dr[sqlTableName] != DBNull.Value) //ex. "ReleaseYear"
                //            id = (int)dr[sqlFieldInTable]; // ex. "ReleaseYearId"

                //    }
                //}

            }
            return id;
        }

        public int GetNumberOfRecordsInTable(string nameOfStoredProcedure)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nameOfStoredProcedure; //ex."NumberOfRecordsInReleaseYear" --name of stored procedure


                conn.Open();

                count = (int)cmd.ExecuteScalar();

            }

            return count;
        }
 
        public void AddDataAndIdToTable(int id, int data, string nameOfStoredProcedure, string nameOfSqlId, string nameOfSqlData)
        {

            //int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nameOfStoredProcedure; //name of stored procedure -- ex. "InsertReleaseYearIdandYear"
                cmd.Parameters.AddWithValue(nameOfSqlId,id);
                cmd.Parameters.AddWithValue(nameOfSqlData, data);


                conn.Open();

                cmd.ExecuteNonQuery();

            }

        }
        public void AddStringDataAndIdToTable(int id, string data, string nameOfStoredProcedure, string nameOfSqlId, string nameOfSqlData)
        {

            //int? id = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DvdLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nameOfStoredProcedure; //name of stored procedure -- ex. "InsertReleaseYearIdandYear"
                cmd.Parameters.AddWithValue(nameOfSqlId, id);
                cmd.Parameters.AddWithValue(nameOfSqlData, data);


                conn.Open();

                cmd.ExecuteNonQuery();

            }

        }
    }
}