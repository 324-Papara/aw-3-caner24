using Para.Data.Domain;

namespace Para.Api.Model
{
    public class CustomerReport
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public CustomerDetail CustomerDetail { get; set; }
        public List<CustomerAddress> CustomerAddresses { get; set; }
        public List<CustomerPhone> CustomerPhones { get; set; }
    }
}
