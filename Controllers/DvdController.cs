using DVDLibraryDatabaseWebAPIv2.Models;
using DVDLibraryDatabaseWebAPIv2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DVDLibraryDatabaseWebAPIv2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        [Route("dvds/all")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(RepositoryFactory.Create().GetAll());
        }

        //this action menthod will repspond to dvds/get/?dvdId=x (x is replaced with the id of the movie ex 1)
        //[Route("dvds/get/")]

        //this action menthod will repspond to dvds/get/1,  (1 is the id of the movie)
        [Route("dvds/get/{dvdId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int dvdId)
        {
            Dvd dvd = RepositoryFactory.Create().GetById(dvdId);

            if(dvd == null)
            {
                return NotFound(); 
            }

            else
            {
                return Ok(dvd);
            }
        }

        //add controller methods
        //getbyDirector

        [Route("dvds/get/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string director)
        {
            List<Dvd> dvds = RepositoryFactory.Create().GetByDirectorName(director);

            if (dvds == null || dvds.Count() == 0)
            {
                return NotFound();
            }

            else
            {
                return Ok(dvds);
            }
        }

        //getby release year
        [Route("dvds/get/year/{year}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByYear(int year)
        {
            List<Dvd> dvds = RepositoryFactory.Create().GetByReleaseYear(year);

            if (dvds == null || dvds.Count() == 0)
            {
                return NotFound();
            }

            else
            {
                return Ok(dvds);
            }
        }

        //get by title
        [Route("dvds/get/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string title)
        {
            List<Dvd> dvds = RepositoryFactory.Create().GetByTitle(title);

            if (dvds == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(dvds);
            }
        }

        [Route("dvds/get/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string rating)
        {
            List<Dvd> dvds = RepositoryFactory.Create().GetByRating(rating);

            if (dvds == null || dvds.Count() == 0)
            {
                return NotFound();
            }

            else
            {
                return Ok(dvds);
            }
        }




        [Route("dvds/add")]
        [AcceptVerbs("POST")]
        //changed parameter type to Dvd, was originally AddDVdRequest
        public IHttpActionResult Add(Dvd request)
        {
            //the model state in this case is just checking if the client includes at least the Titile and Rating,
            //by including the [Required] attribute over those two fields
            // could add more required fields by following same process
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //inorder to pass the model to the database it needs
            // 1.) DvdvId to be created
            // 2.) Take in Year from request
            // if null - leave null
            // if year included - check db to see if it exists
            // if exists - get id and add to request model
            // if does not exists - get number of records add 1 to that number
            // add that number and the release year to the release Year Table
            // repeat process for director and rating

            Dvd dvd = new Dvd()
            {
                Title = request.Title,
                ReleaseYear1 = request.ReleaseYear1,
                director = request.director,
                rating = request.rating,
                Notes = request.Notes

            };

            //pass the dvd information to the repository 
            RepositoryFactory.Create().Add(dvd);
            return Created($"dvds/get/{dvd.DvdId}", dvd);

        }

        [Route ("dvds/update/{dvdId}")]
        [AcceptVerbs("PUT")]

        //changed from UpdateDvdRequets to Dvd
        // was UpdateDvdRequest
        public IHttpActionResult Update(Dvd request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //put null check in ado code
            Dvd dvd = RepositoryFactory.Create().GetById(request.DvdId);
                  
                        
            dvd.DvdId = request.DvdId;
            dvd.Title = request.Title;
            dvd.rating = request.rating;
            dvd.director = request.director;
            dvd.ReleaseYear1 = request.ReleaseYear1;
            dvd.Notes = request.Notes;

            RepositoryFactory.Create().Edit(dvd);
            return Ok(dvd);
        }

        [Route("dvds/delete/{dvdId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdId)
        {
            //create one instance, put null check in ado code

            //Dvd dvd = RepositoryFactory.Create().GetById(dvdId);

            //if(dvd == null)
            //{
            //    //why is this null??
            //    return NotFound();
            //}

            RepositoryFactory.Create().Delete(dvdId); //consider trycatch all exceptions
            return Ok();
        }


    }
}
