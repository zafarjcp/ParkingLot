using ParkingLotCore.Enums;
using ParkingLotCore.Models;
using ParkingLotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Data
{
    public class ParkingRepository : Repository<Parking>, IParkingRepository
    {
        Dictionary<string, int> parkings;
        int parkingCapacity = 0;

        public ParkingRepository(Dictionary<string, int> _parkings, int _parkingCapacity): base(_parkings)
        {
            parkings = _parkings;
            parkingCapacity = _parkingCapacity;
        }

        public bool ParkCar(Parking parking)
        {
            ValidateInput(parking);
            
            if (parkings.Count >= parkingCapacity)
            {
                throw new InvalidOperationException($"Parking is full");
            }

            parkings.Add(parking.car.car_number, parking.slot_number);
            
            return true;
        }

        public bool UnParkCar(Parking parking) 
        {
            int slotNumber = 0;
            ValidateInput(parking);
            
            parkings.TryGetValue(parking.car.car_number, out slotNumber);
            if (slotNumber > 0)
                parkings.Remove(parking.car.car_number);
            else
                return false;

            return true;
        }

        private void ValidateInput(Parking parking)
        {
            if (parking == null)
            {
                throw new InvalidOperationException($"Bad Request, parking information can't be null");
            }
            else if (parking.car == null)
            {
                throw new InvalidOperationException($"Bad Request, Car information not provided");
            }
            else if (parking.car != null && parkings.ContainsKey(parking.car.car_number))
            {
                throw new InvalidOperationException($"Car with number {parking.car.car_number} is already parked");
            }
            else if (parkings.ContainsValue(parking.slot_number))
            {
                throw new InvalidOperationException($"Car with slot number {parking.slot_number} is already parked");
            }
        }

        public KeyValuePair<string, int> GetSlotInformationByCarNumber(string car_number)
        {
            return parkings.FirstOrDefault(x => x.Key == car_number);
        }

        public KeyValuePair<string, int> GetSlotInformationBySlotNumber(int slot_number)
        {
            return parkings.FirstOrDefault(x => x.Value == slot_number);
        }
    }
}
