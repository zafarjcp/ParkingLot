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
        Dictionary<int, string> parkings;
        int parkingCapacity = 0;

        public ParkingRepository(Dictionary<int, string> _parkings, int _parkingCapacity): base(_parkings)
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
            else if(!ValidateCarNumber(parking.car.car_number))
            {
                response.isSuccessful = false;
                response.message = "Car number must be Alpha numeric in upper case with first letter as Alphabet. Special characters are not allowed too.";
                return response;
            }
            else if (parkings.Count >= parkingCapacity)
            {
                response.isSuccessful = false;
                response.message = "Parking is full";
                return response;
            }

            int slotIndex = 0;
            var slotInfo = parkings.Where(x => x.Value == "").FirstOrDefault();
            if (slotInfo.Key == 0)
                parkings.Add(parkings.Count, parking.car.car_number);
            else
                parkings[slotInfo.Key] = parking.car.car_number;
            
            response.isSuccessful = true;
            response.message = "Car parked successfully";
            return response;
        }

        public ServiceResponse UnParkCar(Parking parking) 
        {
            ServiceResponse response = new ServiceResponse();
            int slotNumber = 0;
            if (parking == null)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, parking information can't be null";
            }
            else if (parking.slot_number <= 0)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, Slot information not provided";
            }

            var slotInfo = parkings.Where(x => x.Key == parking.slot_number).FirstOrDefault();
            if (slotInfo.Key > 0)
            {                
                parkings.Remove(slotInfo.Key);
                parkings.Add(slotInfo.Key, "");//Once slot created will not be removed.just will be set for empty.
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
            else if (parking.car.car_number != null && parkings.ContainsValue(parking.car.car_number))
            {
                return $"Car with number {parking.car.car_number} is already parked";
            }
            return string.Empty;
        }

        private bool ValidateCarNumber(string car_number)
        {
            if (car_number.Length > 0 && car_number[0] >= 65 && car_number[0] <= 90)
            {
                for (int i = 1; i < car_number.Length; i++)
                {
                    if ((car_number[i] >= 48 && car_number[i] <= 57) || (car_number[i] >= 65 && car_number[i] <= 90))
                        continue;
                    else
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        public SlotInformationResponse GetSlotInformationByCarNumber(Parking parking)
        {
            SlotInformationResponse response = new SlotInformationResponse();
            if (parking == null)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, parking information can't be null";
            }
            else if (parking.car.car_number == null)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, Car information not provided";
            }
            var slotInfo = parkings.FirstOrDefault(x => x.Value == parking.car.car_number);
            if (slotInfo.Key > 0)
            {
                response.isSuccessful = true;
                response.message = "Slot Information";
                response.car_number = slotInfo.Value;
                response.slot_number = slotInfo.Key;
            }
            else
            {
                response.isSuccessful = false;
                response.message = "No records found against given car number"+ parking.car.car_number;
            }

            return response;

        }

        public SlotInformationResponse GetSlotInformationBySlotNumber(Parking parking)
        {
            SlotInformationResponse response = new SlotInformationResponse();
            if (parking == null)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, parking information can't be null";
            }
            else if (parking.slot_number == null)
            {
                response.isSuccessful = false;
                response.message = $"Bad Request, Slot information not provided";
            }

            var slotInfo = parkings.FirstOrDefault(x => x.Key == parking.slot_number);
            if (slotInfo.Key > 0)
            {
                response.isSuccessful = true;
                response.message = "Slot Information";
                response.car_number = slotInfo.Value;
                response.slot_number = slotInfo.Key;
            }
            else
            {
                response.isSuccessful = false;
                response.message = "No records found against given slot number : " + parking.slot_number;
            }
            return response;
        }
    }
}
