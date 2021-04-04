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
        bool ParkCar(Car car);
        bool UnParkCar(Car car);
        ParkingStatus GetParkingSlotStatus(Parking slot);
        Task<IEnumerable<Parking>> GetParkingByStatusAsync(ParkingStatus parkingStatus);
        Task<IEnumerable<Parking>> GetSlotInformationByCarNumberAsync(string car_number);
        Task<IEnumerable<Parking>> GetSlotInformationBySlotNumberAsync(string car_number);
    }
}
