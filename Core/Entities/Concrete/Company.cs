﻿using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete
{
    public class Company : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int? TenantId { get; set; } = 0;
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TaxNo { get; set; }
        public string WebSite { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
