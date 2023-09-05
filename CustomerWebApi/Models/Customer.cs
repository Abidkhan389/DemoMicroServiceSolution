using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Features.Customers.Quries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerWebApi.Models
{
    // [Table("Course", Schema = "Admin")]  if i have seprate schema then i will use it

    [Table("Customer")] // i have public schema so thats why i am using this syntax
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public Customer()
        {

        }
        public Customer(AddEditCommand model)
        {
            if(model.CustomerId == Guid.Empty || model.CustomerId == null)
                this.CustomerId= Guid.NewGuid();
            CustomerName= model.CustomerName;
            MobileNumber= model.MobileNumber;
            Email= model.Email;
        }
    }
}
