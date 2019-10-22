using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Logic.Interfaces;
using Models.Entities;
using Models.Enums;
using Models.ViewModels;

namespace Logic
{
    public class StudentDriverMappingLogic : IStudentDriverMappingLogic
    {
        private readonly IStudentLogic _studentLogic;
        
        private readonly IDriverLogic _driverLogic;
        
        private readonly IEmailServiceApi _emailServiceApi;

        /// <summary>
        /// Student-Driver mapping logic
        /// </summary>
        /// <param name="studentLogic"></param>
        /// <param name="driverLogic"></param>
        /// <param name="emailServiceApi"></param>
        public StudentDriverMappingLogic(IStudentLogic studentLogic, IDriverLogic driverLogic, IEmailServiceApi emailServiceApi)
        {
            _studentLogic = studentLogic;
            _driverLogic = driverLogic;
            _emailServiceApi = emailServiceApi;
        }

        /// <summary>
        /// Logic to handle the mapping
        /// </summary>
        /// <param name="newStudentDriverMappingViewModel"></param>
        /// <returns></returns>
        public async Task<bool> MapStudentToDriver(NewStudentDriverMappingViewModel newStudentDriverMappingViewModel)
        {
            var driver = await _driverLogic.Get(newStudentDriverMappingViewModel.DriverId);
            
            // Save changes to driver
            return await _studentLogic.Update(newStudentDriverMappingViewModel.StudentId, x =>
            {
                // Add map
                x.Driver = driver;
                x.DriverRefId = driver.Id;
            }) != null;
        }

        /// <summary>
        /// Un-Map student from driver
        /// </summary>
        /// <param name="newStudentDriverMappingViewModel"></param>
        /// <returns></returns>
        public async Task<bool> UnMapStudentToDriver(NewStudentDriverMappingViewModel newStudentDriverMappingViewModel)
        {            
            // Save changes to driver
            return await _studentLogic.Update(newStudentDriverMappingViewModel.StudentId, x =>
            {
                // Remove map
                x.Driver = null;
                x.DriverRefId = null;
            }) != null;
        }

        /// <summary>
        /// Returns the status of mappings
        /// </summary>
        /// <returns></returns>
        public async Task<StudentDriverMappingViewModel> MappingStatus()
        {
            var students = (await _studentLogic.GetAll()).ToList();
            var drivers = (await _driverLogic.GetAll()).Where(x => x.Role == RolesEnum.Driver).ToList();
            
            // TODO: add check to return only students that are present
            return new StudentDriverMappingViewModel
            {
                AvailableStudents = students.Where(x => x.Driver == null),
                AvailableDrivers = drivers.ToDictionary(x => x, x => 
                {
                    // Count = to 1 + FamilySize
                    var cnt = (x.Students ?? new HashSet<Student>()).Select(st => 1 + st.FamilySize)
                        .DefaultIfEmpty(0)
                        .Sum();

                    return x.Capacity > cnt;
                }).ToList(),
                MappedDrivers = drivers.Where(x => x.Students != null && x.Students.Any()),
                MappedStudents = students.Where(x => x.Driver != null)
            };
        }
        
        /// <summary>
        /// Emails the mappings to drivers
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EmailMappings()
        {
            string MessageFunc(Driver driver)
            {
                return $@"
        <p> **This is an automatically generated email** </p>                      
        <br />                                                                    
        <p> Hello {driver.Fullname},</p>
        <p> Your Driver ID:<strong> {driver.DisplayId} </strong></p> 
        <p> Students: </p>                       
        <ul>                                                                    
            {string.Join(Environment.NewLine, driver.Students?.Select(student =>
                                                  $"<li>{student.Fullname} ({student.Country})</li>")
                                              ?? new List<string> { "<p>No student is assigned to you yet.</p>"})}                                                    
        </ul>
        <br />                                                                   
            {string.Join(Environment.NewLine, driver.Host != null ? new List<string>
                {
                    $"<p> Host Name: {driver.Host?.Fullname} </p>",
                    $"<p> Host Contact: {driver.Host?.Phone} </p>",
                    $"<p> Host Address: {driver.Host?.Address} </p>"
                } : new List<string> { "<p>You are not assigned to a host home yet.</p>" })}
        <br />                                                                   
        <br />                                                                
        <p> Thank you for helping with the tour this year. Reply to this email will be sent automatically to the team.</p>      
        <p> For questions, comments and feedback, please contact Asher Imtiaz (414-499-5360) or Marie Wilke (414-852-5132).</p> 
        ";
            }

            var drivers = await _driverLogic.GetAll(DateTime.UtcNow.Year);
            
            // Send the email to drivers
            var tasks = drivers.Select(x =>
                _emailServiceApi.SendEmailAsync(x.Email, "Tour of Milwaukee - Assigned Students", MessageFunc(x)));

            await Task.WhenAll(tasks);
            
            // Return true
            return true;
        }
    }
}