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
            Dvd dvd = RepositoryFactory.Create().Get(dvdId);

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

            Dvd dvd = new Dvd()
            {
                Title = request.Title,
                Rating = request.Rating
            };

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

            Dvd dvd = RepositoryFactory.Create().Get(request.DvdId);

            if(dvd == null)
            {
                return NotFound();
            }

            dvd.Title = request.Title;
            dvd.Rating = request.Rating;

            RepositoryFactory.Create().Edit(dvd);
            return Ok(dvd);
        }

        [Route("dvds/delete")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdId)
        {
            Dvd dvd = RepositoryFactory.Create().Get(dvdId);

            if(dvd == null)
            {
                return NotFound();
            }

            RepositoryFactory.Create().Delete(dvdId);
            return Ok();
        }


    }
}
