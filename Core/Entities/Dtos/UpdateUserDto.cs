namespace Core.Entities.Dtos
{
    public class UpdateUserDto : IDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int? OrganizationId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string MobilePhones { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }

    }
}
