using System;
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
        SlotInformationResponse GetSlotInformationByCarNumber(Parking parking);
        SlotInformationResponse GetSlotInformationBySlotNumber(Parking parking);
    }
}
