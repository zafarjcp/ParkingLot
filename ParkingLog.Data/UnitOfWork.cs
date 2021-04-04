using ParkingLot.Core;
using ParkingLotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, int>parkings;
        private ParkingRepository parkingRepository;
        int parkingCapacity = 0;
        
        public UnitOfWork(Dictionary<string, int> _parkings, int _parkingCapacity)
        {
            this.parkings = _parkings;
            this.parkingCapacity = _parkingCapacity;
        }

        public IParkingRepository Parkings => parkingRepository = parkingRepository ?? new ParkingRepository(parkings, parkingCapacity);
    }
}
