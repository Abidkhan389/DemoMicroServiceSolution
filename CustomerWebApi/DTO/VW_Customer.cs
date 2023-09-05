using CustomerWebApi.Helpers.General;

namespace CustomerWebApi.DTO
{
    public class VW_Customer : ListingLogFields
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
