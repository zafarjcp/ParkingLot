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

            int slotIndex = 0;

            parkings.Add(parking.car.car_number, parkings.Count+1);

            
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

            var slotInfo = parkings.Where(x => x.Value == parking.slot_number).FirstOrDefault();
            if (slotInfo.Key != null)
            {                
                parkings.Remove(slotInfo.Key);
                //parkings[]
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
            return string.Empty;
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
            var slotInfo = parkings.FirstOrDefault(x => x.Key == parking.car.car_number);
            if (slotInfo.Key != null)
            {
                response.isSuccessful = true;
                response.message = "Slot Information";
                response.car_number = slotInfo.Key;
                response.slot_number = slotInfo.Value;
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

            var slotInfo = parkings.FirstOrDefault(x => x.Value == parking.slot_number);
            if (slotInfo.Key != null)
            {
                response.isSuccessful = true;
                response.message = "Slot Information";
                response.car_number = slotInfo.Key;
                response.slot_number = slotInfo.Value;
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
