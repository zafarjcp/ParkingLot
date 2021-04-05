using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotCore.Models
{
    public class Parking
    {
        public Parking()
        {
            car = new Car();
            cars = new HashSet<Car>();
        }
        public int slot_number { get; set; }
        public Car car { get; set; }
        public ICollection<Car> cars { get; set; }
    }
}
