using DVDLibraryDatabaseWebAPIv2.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DVDLibraryDatabaseWebAPIv2.Models
{
    public class RepositoryFactory
    {
        public static IDvdRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode) 
            {
                case "SampleData":
                    return new DvdRepositoryMock();

                case "EntityFramework":
                    return new DvdRepositoryEF();

                case "ADO":
                    return new DVDRepositoryADO();
                
                default:
                    throw new Exception("Mode in web config is not valid");

            }

        }

    }
}