using ParkingLotCore.Enums;
using ParkingLotCore.Models;
using ParkingLotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Data
{
    public class ParkingRepository : Repository<Parking>, IParkingRepository
    {

        public ParkingRepository(Dictionary<string, string> context): base(context)
        {        
        }

        public bool ParkCar(Car car)
        {
            return true;
        }
        public bool UnParkCar(Car car) 
        {
            return true;
        }

        public ParkingStatus GetParkingSlotStatus(Parking slot)
        {
            return ParkingStatus.Vacant;
        }
        public Task<IEnumerable<Parking>> GetParkingByStatusAsync(ParkingStatus parkingStatus)
        {
            return null;
        }
        public Task<IEnumerable<Parking>> GetSlotInformationByCarNumberAsync(string car_number)
        {
            return null;
        }

        public Task<IEnumerable<Parking>> GetSlotInformationBySlotNumberAsync(string car_number)
        {
            return null;
        }
    }
}
