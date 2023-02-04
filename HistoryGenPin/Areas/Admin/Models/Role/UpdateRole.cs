namespace HistoryGenPin.Areas.Admin.Models.Role
{
    public class UpdateRole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DetailRole DetailRole { get; set; }
    }
}
