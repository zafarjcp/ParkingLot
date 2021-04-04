using ParkingLotCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotCore.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Dictionary<string, string>.ValueCollection GetAllAsync();
        void Add(Parking parking);
        void Remove(string key);
        
    }
}
