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

        [Route("dvds/add")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(AddDvdRequest request)
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
                ReleaseYear = request.ReleaseYear,
                DirectorName = request.DirectorName,
                RatingName = request.RatingName,
                Notes = request.Notes

            };

            //pass the dvd information to the repository 
            RepositoryFactory.Create().Add(dvd);
            return Created($"dvds/get/{dvd.DvdId}", dvd);

        }

        [Route ("dvds/update")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(UpdateDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dvd dvd = RepositoryFactory.Create().GetById(request.DvdId);

            if(dvd == null)
            {
                return NotFound();
            }

            dvd.Title = request.Title;
            dvd.RatingName = request.Rating;

            RepositoryFactory.Create().Edit(dvd);
            return Ok(dvd);
        }

        [Route("dvds/delete")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdId)
        {
            Dvd dvd = RepositoryFactory.Create().GetById(dvdId);

            if(dvd == null)
            {
                return NotFound();
            }

            RepositoryFactory.Create().Delete(dvdId);
            return Ok();
        }


    }
}
