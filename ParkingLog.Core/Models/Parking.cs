using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLog.Core.Models
{
    public class Parking
    {
        public string slot_number { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
