using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLot.Api.Models
{
    public class ParkRequest
    {
        public string car_number { get; set; }
    }

    public class UnParkRequest
    {
        public int slot_number { get; set; }
    }

    public class ParkInfoRequest
    {
        public string car_number { get; set; }
        public int slot_number { get; set; }
    }
}
