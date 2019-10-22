using System;
using System.Threading.Tasks;
using DAL.Interfaces;
using Logic.Interfaces;
using Models.Entities;
using Models.Enums;

namespace Logic
{
    public class RegistrationLogic : IRegistrationLogic
    {
        private readonly IStudentLogic _studentLogic;

        private readonly IDriverLogic _driverLogic;

        private readonly IHostLogic _hostLogic;
        
        private readonly IEventLogic _eventLogic;
        
        private readonly IEmailServiceApi _emailServiceApiApi;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="studentLogic"></param>
        /// <param name="driverLogic"></param>
        /// <param name="hostLogic"></param>
        /// <param name="emailServiceApiApi"></param>
        public RegistrationLogic(IStudentLogic studentLogic, IDriverLogic driverLogic, IHostLogic hostLogic, IEventLogic eventLogic, IEmailServiceApi emailServiceApiApi)
        {
            _studentLogic = studentLogic;
            _driverLogic = driverLogic;
            _hostLogic = hostLogic;
            _eventLogic = eventLogic;
            _emailServiceApiApi = emailServiceApiApi;
        }

        /// <summary>
        /// Register driver
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public async Task<bool> RegisterDriver(Driver driver)
        {
            driver = await _driverLogic.Save(driver);

            // If save was successful
            if (driver != null)
            {
                switch (driver.Role)
                {
                    case RolesEnum.Driver:
                        await _emailServiceApiApi.SendEmailAsync(driver.Email, "Tour of Milwaukee: Driver registration", $@"
                    <p> This is an automatically generated email. </p>
                    <p> ----------------------------------------- </p>
                    <p> Name: {driver.Fullname}</p>
                    <p> Role: {driver.Role}</p>
                    <p> Phone: {driver.Phone}</p>
                    <p> Capacity: {driver.Capacity}</p>
                    <p> Display Id: {driver.DisplayId}</p>
                    <p> Require Navigator: {(driver.RequireNavigator ? "Yes, navigator will be assigned to you" : $"No, my navigator is: {driver.Navigator}")}</p>
                    <br>
                    <p> 2019 Tour of Milwaukee</p>
                    <p> Date: August 31, 2019</p>
                    <p> Time: 12:30 pm (Brief orientation only for drivers and navigators) </p>
                    <p> Address: 3202 N Maryland Ave, Milwaukee, WI 53211 </p>
                    <p> Location: Lubar Hall (Business Building, UWM) </p>
                    <br>
                    <p> Thank you for helping with the tour this year. Reply to this email will be sent automatically to the team.</p>
                    <p> For questions, comments and feedback, please contact Asher Imtiaz (414-499-5360) or Marie Wilke (414-852-5132).</p>
                    <br>
                    <p> Blessings, </p>
                ");
                        break;
                    case RolesEnum.Navigator:
                        await _emailServiceApiApi.SendEmailAsync(driver.Email, "Tour of Milwaukee: Driver registration", $@"
                    <p> This is an automatically generated email. </p>
                    <p> ----------------------------------------- </p>
                    <p> Name: {driver.Fullname}</p>
                    <p> Role: {driver.Role}</p>
                    <p> Phone: {driver.Phone}</p>
                    <p> Display Id: {driver.DisplayId}</p>
                    <br>
                    <p> 2019 Tour of Milwaukee</p>
                    <p> Date: August 31, 2019</p>
                    <p> Time: 12:30 pm (Brief orientation only for drivers and navigators) </p>
                    <p> Address: 3202 N Maryland Ave, Milwaukee, WI 53211 </p>
                    <p> Location: Lubar Hall (Business Building, UWM) </p>
                    <br>
                    <p> Thank you for helping with the tour this year. Reply to this email will be sent automatically to the team.</p>
                    <p> For questions, comments and feedback, please contact Asher Imtiaz (414-499-5360) or Marie Wilke (414-852-5132).</p>
                    <br>
                    <p> Blessings, </p>
                ");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return true;
        }

        /// <summary>
        /// Registers student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<bool> RegisterStudent(Student student)
        {
            var result = await _studentLogic.Save(student);

            // If save was successful
            if (result != null)
            {
                await _emailServiceApiApi.SendEmailAsync(student.Email, "Tour of Milwaukee Registration Confirmation", $@"
                    <p>Name: {student.Fullname}</p>
                    <p>Email: {student.Email}</p>
                    <p>University: {student.University}</p>
                    <p>Major: {student.Major}</p>
                    <p>Phone: {student.Phone}</p>
                    <hr>
                    <p>See you at the Tour of Milwaukee</p>
                    <br>
                    <p> 2019 Tour of Milwaukee Registration</p>
                    <p> Date: August 31, 2019</p>
                    <p> Time: 12:00 noon</p>
                    <p> Address: 3202 N Maryland Ave, Milwaukee, WI 53211 </p>
                    <p> Location: Lubar Hall (Business Building, UWM) </p>
                    <p> Thank you for registering for this event. Please share this with other new international friends.</p>
                    <p> If you need any sort of help (furniture, moving, etc.), please contact Asher Imtiaz (414-499-5360) or Prayag (646-226-4330) on campus.</p>
                ");
            }

            return true;
        }

        /// <summary>
        /// Registers host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<bool> RegisterHost(Host host)
        {
            var result = await _hostLogic.Save(host);

            // If save was successful
            if (result != null)
            {
                await _emailServiceApiApi.SendEmailAsync(host.Email, "Tour of Milwaukee: Host registration", $@"
                    <p> This is an automatically generated email. </p>
                    <p> ----------------------------------------- </p>
                    <p>Name: {host.Fullname}</p>
                    <p>Email: {host.Email}</p>
                    <p>Address: {host.Address}</p>
                    <hr>
                    <p>Thank you for helping with hosting and welcoming Internationals to Milwaukee.</p>
                    <br>
                    <p> Date: August 31, 2019</p>
                    <p> Time: 5:00 pm</p>
                    <p> We will send you more details once we have them.</p>
                    <p> For questions, any change in plans, please contact Asher Imtiaz (414-499-5360) or Marie Wilke (414-852-5132).</p>
                    <p> Blessings,</p>
                ");
            }

            return true;
        }

        public async Task<bool> RegisterEvent(Event @event)
        {
            var savedEvent = await _eventLogic.Save(@event);

            return savedEvent != null;
        }
    }
}
