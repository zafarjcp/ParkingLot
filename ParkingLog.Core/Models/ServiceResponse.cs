using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotCore.Models
{
    public class ServiceResponse
    {
        public bool isSuccessful { get; set; }
        public string message { get; set; }
    }

    public class SlotInformationResponse
    {
        public bool isSuccessful { get; set; }
        public string message { get; set; }
        public int slot_number { get; set; }
        public string car_number { get; set; }
    }
}
