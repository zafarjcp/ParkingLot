using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLot.Api.Models;
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

        [RequestRateLimit]
        [HttpPost]
        [Route("park")]
        public IActionResult Park([FromBody] ParkRequest request)
        {
            Parking parking = new Parking();

            parking.car.car_number = request.car_number;
            
            ServiceResponse response = unitOfWork.ParkingRepository.ParkCar(parking);
            
            _logger.LogInformation("Request received for Car Parking");

            if (response.isSuccessful)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [RequestRateLimit]
        [HttpDelete]
        [Route("unpark")]
        public IActionResult UnPark(int slot_number)
        {
            Parking parking = new Parking();

            parking.slot_number = slot_number;

            ServiceResponse response = unitOfWork.ParkingRepository.UnParkCar(parking);

            _logger.LogInformation("Request received for Unpark Car");

            if (response.isSuccessful)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [RequestRateLimit]
        [HttpGet]
        [Route("get-info")]
        public IActionResult SlotInformation(string car_number, int slot_number)
        {
            SlotInformationResponse response = new SlotInformationResponse();
            Parking parking = new Parking();
            if (!string.IsNullOrEmpty(car_number))
            {
                parking.car.car_number = car_number;
                response = unitOfWork.ParkingRepository.GetSlotInformationByCarNumber(parking);
            }
            else
            {
                parking.slot_number = slot_number;
                response = unitOfWork.ParkingRepository.GetSlotInformationBySlotNumber(parking);
            }

            _logger.LogInformation("Slot information provided.");

            if (response.isSuccessful)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
