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
    public class VehicleController : ControllerBase
    {
        private readonly IMapperSession session;
        public VehicleController(IMapperSession session)
        {
            this.session = session;
        }


        [HttpGet]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = session.Vehicles.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            Vehicle result = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            try
            {
                session.BeginTransaction();
                session.Save(vehicle);
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
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == request.Id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();

                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;

                session.Update(vehicle);

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
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                session.Delete(vehicle);
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
