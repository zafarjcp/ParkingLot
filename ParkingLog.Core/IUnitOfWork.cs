using ParkingLotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Core
{
    public interface IUnitOfWork
    {
        IParkingRepository Parkings { get; }
    }
}
