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

        public ServiceResponse ParkCar(Parking parking)
        {
            ServiceResponse response = new ServiceResponse();
            string message = ValidateInput(parking);
            if (message != string.Empty)
            {
                response.isSuccessful = false;
                response.message = message;
                return response;
            }

            if (parkings.Count >= parkingCapacity)
            {
                response.isSuccessful = false;
                response.message = "Parking is full";
                return response;
            }

            parkings.Add(parking.car.car_number, parking.slot_number);

            response.isSuccessful = true;
            response.message = "Car parked successfully";
            return response;
        }

        public ServiceResponse UnParkCar(Parking parking) 
        {
            ServiceResponse response = new ServiceResponse();
            int slotNumber = 0;
            string message = ValidateInput(parking);
            if (message != string.Empty)
            {
                response.isSuccessful = false;
                response.message = message;
                return response;
            }

            parkings.TryGetValue(parking.car.car_number, out slotNumber);
            if (slotNumber > 0)
            {
                parkings.Remove(parking.car.car_number);
                response.isSuccessful = true;
                response.message = "Car unparked successfully.";
            }
            else
            {
                response.isSuccessful = false;
                response.message = "Unable to unpark Car. Invalid Slot Number.";
            }

            return response;
        }

        private string ValidateInput(Parking parking)
        {
            if (parking == null)
            {                
                return $"Bad Request, parking information can't be null";
            }
            else if (parking.car.car_number == null)
            {
                return $"Bad Request, Car information not provided";
            }
            else if (parking.car.car_number != null && parkings.ContainsKey(parking.car.car_number))
            {
                return $"Car with number {parking.car.car_number} is already parked";
            }
            //else if (parkings.ContainsValue(parking.slot_number))
            //{
            //    return $"Car with slot number {parking.slot_number} is already parked";
            //}
            return string.Empty;
        }

        public KeyValuePair<string, int> GetSlotInformationByCarNumber(Parking parking)
        {

            return parkings.FirstOrDefault(x => x.Key == parking.car.car_number);
        }

        public KeyValuePair<string, int> GetSlotInformationBySlotNumber(Parking parking)
        {
            return parkings.FirstOrDefault(x => x.Value == parking.slot_number);
        }
    }
}
