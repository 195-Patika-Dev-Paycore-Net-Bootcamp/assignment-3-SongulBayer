using Microsoft.AspNetCore.Mvc;
using PycApi.Context;
using PycApi.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IMapperSession session;
        public ContainerController(IMapperSession session)
        {
            this.session = session;
        }

        [HttpGet]
        public List<Container> Get()
        {
            List<Container> result = session.Container.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public Container Get(int id)
        {
            Container result = session.Container.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }
        [HttpPost]
        public void Post([FromBody] Container container)
        {
            try
            {
                session.BeginTransaction();
                session.Save(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Book Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }
        }
        [HttpPut]
        public ActionResult<Container> Put([FromBody] Container request)
        {
            Container container1 = session.Container.Where(x => x.Id == request.Id).FirstOrDefault();
            if (container1 == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                container1.Latitude = request.Latitude;
                container1.Longitude = request.Longitude;
                container1.VehicleId = request.VehicleId;
                container1.ContainerName = request.ContainerName;
                session.Update(container1);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Book Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = session.Container.Where(x => x.Id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(container);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                Log.Error(ex, "Book Insert Error");
            }
            finally
            {
                session.CloseTransaction();
            }

            return Ok();
        }
    }
}
