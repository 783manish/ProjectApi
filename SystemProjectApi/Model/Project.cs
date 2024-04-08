namespace SystemProjectApi.Model
{
    public class Project
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public string ProjectManagerName { get; set; }
        public string ProjectManagerEmail { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

