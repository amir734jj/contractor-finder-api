using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Logic.Abstracts;
using Logic.Interfaces;
using Models.Constants;
using Models.Entities;
using Models.Enums;
using static Logic.Utilities.DisplayIdUtility;

namespace Logic
{
    public class DriverLogic : BasicCrudLogicAbstract<Driver>, IDriverLogic
    {
        private readonly IDriverDal _driverDal;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="driverDal"></param>
        public DriverLogic(IDriverDal driverDal)
        {
            _driverDal = driverDal;
        }

        /// <summary>
        /// Returns instance of driver DAL
        /// </summary>
        /// <returns></returns>
        protected override IBasicCrudDal<Driver> GetBasicCrudDal()
        {
            return _driverDal;
        }

        /// <summary>
        /// Make sure display ID is not null or empty
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override async Task<Driver> Save(Driver instance)
        {
            // If role is navigator then capaciy is 0
            if (instance.Role == RolesEnum.Navigator)
            {
                instance.Capacity = 0;
            }
            
            var count = (await base.GetAll(DateTime.UtcNow.Year)).Count();
          
            instance.DisplayId = "Null";

            // Set the year
            instance.Year = DateTime.UtcNow.Year;

            instance.DisplayId = GenerateDisplayId(instance, count);

            // Save the instance
            var retVal = await base.Save(instance);

            return retVal;
        }

        public override async Task<Driver> Update(int id, Driver instance)
        {
            // If role is navigator then capacity is 0
            if (instance.Role == RolesEnum.Navigator)
            {
                instance.Capacity = 0;
            }

            return await base.Update(id, instance);
        }

        public override async Task<IEnumerable<Driver>> GetAll()
        {
            return (await base.GetAll()).Where(x => x.Year == GlobalConfigs.YearValue);
        }
    }
}