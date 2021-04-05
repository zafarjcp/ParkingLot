using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLot.Data;
using ParkingLot.Models;
using ParkingLotCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLot.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;
        private UnitOfWork unitOfWork;

        public ParkingController(ILogger<ParkingController> logger, UnitOfWork _unitOfWork)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
        }

        [HttpGet]
        [Route("park")]
        public ServiceResponse Park(string car_number)
        {
            Parking parking = new Parking();
            parking.car.car_number = car_number;
            ServiceResponse response = unitOfWork.ParkingRepository.ParkCar(parking);
            
            _logger.LogInformation("Request received for Car Parking");
            
            return response;
        }
    }
}
