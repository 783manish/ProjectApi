using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemProjectApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SystemProjectApi.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class ProjectsController : Controller
        {
            private readonly IConfiguration _configuration;
            private readonly ProjectDbContext _context;

            public ProjectsController(IConfiguration configuration ,ProjectDbContext context)
            {
                _configuration = configuration;
                _context = context;
            }           

            [Authorize]
            [HttpGet]
            public async Task<ActionResult> GetProjectDetails(int page = 1, int pageSize = 3)
            {
                try
                {
                    var resultSet = _context.Projects
                    .Join(
                    _context.Employees,
                    project => project.ProjectManagerId,
                    employee => employee.Id,
                    (project, manager) => new { Project = project, Manager = manager }
                )
                .Join(
                    _context.Departments,
                    j => j.Manager.DepartmentId,
                    department => department.Id,
                    (j, department) => new { j.Project, j.Manager, Department = department }
                )
                .Select(j => new Project
                {
                    Id = j.Project.Id,
                    Name = j.Project.Name,
                    StartDate = j.Project.StartDate,
                    EndDate = j.Project.EndDate,
                    ProjectManagerName = j.Project.ProjectManagerName,
                    ProjectManagerEmail = j.Project.ProjectManagerEmail,
                    Employees = _context.Employees
                        .Where(e => e.Projects.Any(p => p.Id == j.Project.Id))
                        .Select(e => new Employee
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Email = e.Email,
                            DateOfJoining = e.DateOfJoining,
                            Department = e.Department.Select(d => new Department
                            {
                                Id = d.Id,
                                Name = d.Name
                            }).ToList()
                        }).ToList()
                });
                  
                        int totalItems = await resultSet.CountAsync();
                        bool hasMore = totalItems > page * pageSize;

                        var dtSet = resultSet.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                        var response = new Result
                        {
                            Data = resultSet,
                            Page = page,
                            PageSize = pageSize,
                            HasMore = hasMore,
                            TotalItems = totalItems
                        };

                        return Ok(new { result = response });
                    

                }

                catch(Exception ex)
                {
                    return NotFound("Result not found");
                }
            
            }

       
        }
    }

