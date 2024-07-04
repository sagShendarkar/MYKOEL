using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;

using Newtonsoft.Json;
using MyKoel_Domain.DTOs;

namespace Industry4.TPAIntegrations
{
    public class AssetDetails
    {
        private readonly IConfiguration _configuration;

        public AssetDetails(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public  async Task<bool> CheckADCredentials(string userName, string password)
       {
        using (var client = new HttpClient())
        {
            try
            {
                string apiUrl = $"https://mykoelpreprod.kirloskar.com/WorkPermit/Employee/IsADValid?UserName={userName}&Password={password}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    bool isValid = bool.Parse(jsonResponse); 
                    return isValid;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


         public async Task<EmployeeDetails> GetEmployeeDetailsAsync(string ticketNumber)
{
            using (var client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://mykoelpreprod.kirloskar.com/WorkPermit/Employee/GetHCMEmployeeDetails?TicketNumber={ticketNumber}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        EmployeeDetails employeeDetails = JsonConvert.DeserializeObject<EmployeeDetails>(jsonResponse);
                        return employeeDetails;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
}



    }
}
