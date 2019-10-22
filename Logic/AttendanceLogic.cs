using System;
using System.Threading.Tasks;
using DAL.Extensions;
using DAL.Interfaces;
using Logic.Interfaces;
using Models.Constants;
using Models.ViewModels;

namespace Logic
{
    public class AttendanceLogic : IAttendanceLogic
    {
        private readonly IStudentLogic _studentLogic;
        
        private readonly IDriverLogic _driverLogic;
        
        private readonly IEmailServiceApi _emailServiceApi;

        /// <summary>
        /// Constructor dependency injection
        /// </summary>
        /// <param name="studentLogic"></param>
        /// <param name="driverLogic"></param>
        /// <param name="emailServiceApi"></param>
        public AttendanceLogic(IStudentLogic studentLogic, IDriverLogic driverLogic, IEmailServiceApi emailServiceApi)
        {
            _studentLogic = studentLogic;
            _driverLogic = driverLogic;
            _emailServiceApi = emailServiceApi;
        }
        
        /// <summary>
        /// Set the attendance for student
        /// </summary>
        /// <param name="attendanceViewModel"></param>
        /// <returns></returns>
        public async Task<bool> StudentSetAttendance(AttendanceViewModel attendanceViewModel)
        {
            await _studentLogic.Update(attendanceViewModel.Id, x =>
            {
                // Set attendance
                x.IsPresent = attendanceViewModel.Attendance;
            });

            return true;
        }

        /// <summary>
        /// Set the attendance for driver
        /// </summary>
        /// <param name="attendanceViewModel"></param>
        /// <returns></returns>
        public async Task<bool> DriverSetAttendance(AttendanceViewModel attendanceViewModel)
        {
            // Set attendance
            await _driverLogic.Update(attendanceViewModel.Id, x =>
            {
                // Set attendance
                x.IsPresent = attendanceViewModel.Attendance;
            });

            return true;
        }

        /// <summary>
        /// Handles sending email to students so they check-in
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HandleStudentSendCheckIn()
        {
            (await _studentLogic.GetAll()).ForEach(x =>
            {
                var url = $"{ApiConstants.SiteUrl}/utility/EmailCheckIn/Student/{x.GetHashCode()}";
                
                _emailServiceApi.SendEmailAsync(x.Email, "Tour Check-In", $@"
                    <h4>Please use this link to check-in</h4>
                    <br>
                    <p><a href=""{url}"">Link</a> ({url})</p>
                    <br>
                    <p>Thank you</p>
                ");
            });
            
            return true;
        }

        /// <summary>
        /// Handles sending email to drivers so they check-in
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HandleDriverSendCheckIn()
        {
            (await _driverLogic.GetAll()).ForEach(x =>
            {
                var url = $"{ApiConstants.SiteUrl}/utility/EmailCheckIn/Driver/{x.GetHashCode()}";
                
                _emailServiceApi.SendEmailAsync(x.Email, $"Tour Driver Check-In and Host Info ({DateTime.UtcNow.Year})", $@"
                    <h4>Hello {x.Fullname},</h4>
                    <h4>Please use the following link to see details and to check-in</h4>
                    <p><a href=""{url}"">{url}</a></p>
                    <p>Most important thing to remember is your -Display ID-. Students are matched to this ID. The number next to your initials is unique.</p>
                    <p>The link has information about your host where you will go for dinner with students. </p>
                    <br>
                    <p>To save time when you arrive at UWM, just click on the button which says 'Check-In'. We will know that you are there and ready to drive students. Remember to pick up your Display ID when you arrive at the drivers area.</p>
                    <p>Reach out to us if there are issues.</p>
                    <p>Thank you</p>
                ");
            });

            return true;
        }
    }
}
