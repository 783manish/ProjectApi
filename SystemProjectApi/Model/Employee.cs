namespace SystemProjectApi.Model
{

    public class Employee
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> Department { get; set; }
        public List<Project> Projects { get; set; }
    }

}

