using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Features.Identity.Commands.RegisterUser;

namespace Identity.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        //[Required(ErrorMessage = "value for PasswordSalt is required")]
        //[Column("PasswordSalt")]
        public string PasswordSalt { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string MobileNumber { get; set; }
        //public string Photo { get; set; }
        public ApplicationUser()
        {
            // Initialize properties here if needed
            this.IsSuperAdmin = false;
            this.MobileNumber= string.Empty;
            this.PasswordHash= string.Empty;
        }
        public ApplicationUser(RegisterUserCommands model, string salt)
        {
            this.IsSuperAdmin =false;
            this.PasswordSalt = salt;
            this.MobileNumber = model.MobileNumber;
            this.PasswordHash = model.Password;
            this.Email = model.Email;
            this.UserName = model.FirstName + model.LastName;
        }

    }
}
