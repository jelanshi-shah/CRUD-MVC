namespace CRUDMVC.Models
{
    public class AddEmployeeViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long Salary { get; set; }
        public string Department { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
