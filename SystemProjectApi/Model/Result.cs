namespace SystemProjectApi.Model
{
    public class Result
            {
                public IEnumerable<Project> Data { get; set; }
                public int Page { get; set; }
                public int PageSize { get; set; }
                public Boolean HasMore { get; set; }
                public int TotalItems { get; set; }
            }
    
    }

