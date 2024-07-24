using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Para.Api.Model;
using Para.Data.Domain;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController:ControllerBase
    {
        private readonly IConfiguration _config;

        public CustomerReportController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports() 
        {
            var result =await GetCustomerReportsAsync();
            return Ok(result);
        }


        private async Task<IEnumerable<CustomerReport>> GetCustomerReportsAsync()
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("MsSqlConnection")))
            {
                // SQL sorgusu ile müşteri ve ilişkili verileri al
                var sql = @"
                SELECT * FROM Customer c
                LEFT JOIN CustomerDetail cd ON c.Id = cd.CustomerId
                LEFT JOIN CustomerAddress ca ON c.Id = ca.CustomerId
                LEFT JOIN CustomerPhone cp ON c.Id = cp.CustomerId";

                var customerDictionary = new Dictionary<long, CustomerReport>();

                var customerReports = await connection.QueryAsync<Customer, CustomerDetail, CustomerAddress, CustomerPhone, CustomerReport>(
                    sql,
                    (customer, detail, address, phone) =>
                    {
                        if (!customerDictionary.TryGetValue(customer.Id, out var customerReport))
                        {
                            customerReport = new CustomerReport
                            {
                                Id = customer.Id,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                IdentityNumber = customer.IdentityNumber,
                                Email = customer.Email,
                                CustomerNumber = customer.CustomerNumber,
                                DateOfBirth = customer.DateOfBirth,
                                CustomerDetail = detail,
                                CustomerAddresses = new List<CustomerAddress>(),
                                CustomerPhones = new List<CustomerPhone>()
                            };
                            customerDictionary.Add(customer.Id, customerReport);
                        }

                        if (address != null)
                        {
                            customerReport.CustomerAddresses.Add(address);
                        }

                        if (phone != null)
                        {
                            customerReport.CustomerPhones.Add(phone);
                        }

                        return customerReport;
                    },
                    splitOn: "Id,CustomerId,CustomerId");

                return customerReports.Distinct().ToList();
            }
        }

    }
}
