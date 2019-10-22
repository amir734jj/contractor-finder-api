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
    public class DriverHostMappingLogic : IDriverHostMappingLogic
    {
        private readonly IDriverLogic _driverLogic;

        private readonly IHostLogic _hostLogic;

        private readonly IEmailServiceApi _emailServiceApi;

        /// <summary>
        /// Driver-Host mapping logic
        /// </summary>
        /// <param name="driverLogic"></param>
        /// <param name="hostLogic"></param>
        /// <param name="emailServiceApi"></param>
        public DriverHostMappingLogic(IDriverLogic driverLogic, IHostLogic hostLogic, IEmailServiceApi emailServiceApi)
        {
            _driverLogic = driverLogic;
            _hostLogic = hostLogic;
            _emailServiceApi = emailServiceApi;
        }

        /// <summary>
        /// Logic to handle the mapping
        /// </summary>
        /// <param name="newDriverHostMappingViewModel"></param>
        /// <returns></returns>
        public async Task<bool> MapDriverToHost(NewDriverHostMappingViewModel newDriverHostMappingViewModel)
        {
            var host = await _hostLogic.Get(newDriverHostMappingViewModel.HostId);

            // Save changes to driver
            return _driverLogic.Update(newDriverHostMappingViewModel.DriverId, x =>
            {
                // Add map
                x.Host = host;
                x.HostRefId = host.Id;
            }) != null;
        }

        /// <summary>
        /// Un-Map student from driver
        /// </summary>
        /// <param name="newDriverHostMappingViewModel"></param>
        /// <returns></returns>
        public async Task<bool> UnMapDriverToHost(NewDriverHostMappingViewModel newDriverHostMappingViewModel)
        {
            // Save changes to driver
            return await _driverLogic.Update(newDriverHostMappingViewModel.DriverId, x =>
            {
                // Remove map
                x.Host = null;
                x.HostRefId = null;
            }) != null;
        }

        /// <summary>
        /// Returns the status of mappings
        /// </summary>
        /// <returns></returns>
        public async Task<DriverHostMappingViewModel> MappingStatus()
        {
            var hosts = (await _hostLogic.GetAll()).ToList();
            var drivers = (await _driverLogic.GetAll()).Where(x => x.Role == RolesEnum.Driver).ToList();

            // TODO: add check to return only students that are present
            return new DriverHostMappingViewModel
            {
                AvailableHosts = hosts,
                AvailableDrivers = drivers.Where(x => x.Host == null),
                MappedDrivers = drivers.Where(x => x.Host != null),
                MappedHosts = hosts.Where(x => x.Drivers != null && x.Drivers.Any())
            };
        }

        /// <summary>
        /// Emails the mappings to hosts
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EmailMappings()
        {
            string MessageFunc(Host host)
            {
                return $@"
        <p> **This is an automatically generated email** </p>                      
        <br />
        <p> Hello {host.Fullname}</p>                                                 
        {(host.Drivers != null && host.Drivers.Any() ? $@"
            <p>List of drivers and students assigned to your home</p>
            <ul>
                {string.Join(Environment.NewLine, host.Drivers?.Select(driver => $@"
                    <li>
                        <p>Driver: {driver.Fullname}</p>
                        {(!string.IsNullOrEmpty(driver.Navigator) ? $"<p>Navigator: {driver.Navigator}</p>" : string.Empty)}
                        <ul>
                            {string.Join(Environment.NewLine, driver.Students?.Select(student => $@"
                                <li>{student.Fullname} ({student.Country})</li>
                            ") ?? new List<string> { "<li>No student assigned to this driver yet.</li>"})}
                        </ul>
                    </li>
                "))}
            </ul>
        " : "<p>No driver is assigned to your home.</p>")}
        <br />                                                                     
        <br />                                                                     
        <p> Thank you for helping with the tour this year. Reply to this email will be sent automatically to the team.</p>      
        <p> For questions, comments and feedback, please contact Asher Imtiaz (414-499-5360) or Marie Wilke (414-852-5132).</p> 
        ";
            }

            var hosts = await _hostLogic.GetAll(DateTime.UtcNow.Year);

            // Send the email to hosts
            var tasks = hosts
                .Select(x => _emailServiceApi.SendEmailAsync(x.Email, "Tour of Milwaukee - Assigned Drivers", MessageFunc(x)));

            await Task.WhenAll(tasks);
            
            // Return true
            return true;
        }
    }
}