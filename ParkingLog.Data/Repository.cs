using ParkingLotCore.Models;
using ParkingLotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Dictionary<string,int> Context;

        public Repository(Dictionary<string, int> context)
        {
            this.Context = context;
        }
        public void Add(Parking parking)
        {
            Context.Add(parking.car.car_number, parking.slot_number);
        }

        public Dictionary<string, int>.ValueCollection GetAllAsync()
        {
            return Context.Values;
        }

        public void Remove(string key)
        {
            Context.Remove(key);
        }
    }
}
