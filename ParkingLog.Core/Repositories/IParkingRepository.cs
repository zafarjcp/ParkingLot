﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParkingLotCore.Enums;
using ParkingLotCore.Models;

namespace ParkingLotCore.Repositories
{
    public interface IParkingRepository : IRepository<Parking>
    {
        ServiceResponse ParkCar(Parking parking);
        ServiceResponse UnParkCar(Parking parking);
        KeyValuePair<string, int> GetSlotInformationByCarNumber(Parking parking);
        KeyValuePair<string, int> GetSlotInformationBySlotNumber(Parking parking);
    }
}
