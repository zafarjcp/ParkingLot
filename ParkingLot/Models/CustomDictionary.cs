using Microsoft.Extensions.Configuration;
using ParkingLot.Data;
using ParkingLotCore.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ParkingLot.Models
{
    public class UnitOfWork
    {
        private int parkingCapacity = 0;
        
        public static Dictionary<string, int> parkings { get; set; }
        
        public IParkingRepository ParkingRepository;

        public UnitOfWork(IConfiguration configuration) 
        {
            if(parkings == null)
                parkings = new Dictionary<string, int>();
            parkingCapacity = configuration.GetValue<int>("parkingCapacity");
            
            ParkingRepository = new ParkingRepository(parkings, parkingCapacity);
        }        
    }
}
