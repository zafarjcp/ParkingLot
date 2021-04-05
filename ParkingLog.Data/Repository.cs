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
        protected readonly Dictionary<int, string> Context;

        public Repository(Dictionary<int, string> context)
        {
            this.Context = context;
        }

        public void Add(Parking parking)
        {
            lock (Context)
            {
                Context.Add(parking.slot_number, parking.car.car_number);
            }            
        }

        public Dictionary<int, string>.ValueCollection GetAllAsync()
        {
            return Context.Values;
        }

        public void Remove(int key)
        {
            Context.Remove(key);
        }
    }
}
