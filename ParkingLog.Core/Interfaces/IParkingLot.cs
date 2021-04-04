using System;
using System.Collections.Generic;
using System.Text;
using ParkingLog.Core.Models;
using ParkingLog.Core.Enums;

namespace ParkingLog.Core.Interfaces
{
    public interface IParkingLot
    {
        bool ParkCar(Car car);
        bool UnParkCar(Car car);
        ParkingSpotStatus GetParkingStatus(Parking slot);
    }
}
