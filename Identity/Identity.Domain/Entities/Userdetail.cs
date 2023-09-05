using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Userdetail
    {
        public Guid UserDetailId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public Guid? UpdatedBy { get; set; }
        public int? UpdatedOn { get; set; }
        public string UserId { get; set; }
        public string? ImagePath { get; set; }

        // Parameterless constructor
        public Userdetail()
        {
            // Initialize properties here if needed
            this.CreatedOn = DateTime.UtcNow;
            this.Status = 1;
        }

        // Constructor for initializing properties
        public void Initialize(ApplicationUser model)
        {
            //this.UserId = Guid.TryParse(model.Id, out var userIdGuid) ? userIdGuid : Guid.Empty;
            this.UserId = model.Id;
            this.UserDetailId = Guid.NewGuid();
        }
    }

}
