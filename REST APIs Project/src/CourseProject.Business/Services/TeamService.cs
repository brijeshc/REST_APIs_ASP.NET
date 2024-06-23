using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Teams;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;
using System.Linq.Expressions;

namespace CourseProject.Business.Services;

public class TeamService : ITeamService
{
    private IMapper Mapper { get; }
    private TeamCreateValidatior TeamCreateValidator { get; }
    private TeamUpdateValidatior TeamUpdateValidator { get; }
    private IGenericRepository<Team> TeamRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }

    public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper,
        TeamCreateValidatior teamCreateValidator, TeamUpdateValidatior teamUpdateValidator)
    {
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        Mapper = mapper;
        TeamCreateValidator = teamCreateValidator;
        TeamUpdateValidator = teamUpdateValidator;
    }
    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        await TeamCreateValidator.ValidateAsync(teamCreate);
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] {employeeFilter}, null, null);
        var missingEmployees = teamCreate.Employees.Where((id) => !employees.Any(existing => existing.Id == id));
        if (missingEmployees.Any())
        {
            throw new EmployeesNotFoundException(missingEmployees.ToArray());
        }
        var entity = Mapper.Map<Team>(teamCreate);
        entity.Employees = employees;
        await TeamRepository.InsertAsync(entity);
        await TeamRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var entity = await TeamRepository.GetByIdAsync(teamDelete.Id);
        if (entity == null)
        {
            throw new TeamNotFoundException(teamDelete.Id);
        }
        TeamRepository.Delete(entity);
        await TeamRepository.SaveChangesAsync();
    }

    public async Task<TeamGet> GetTeamAsync(int id)
    {
       var entity = await TeamRepository.GetByIdAsync(id, (team) => team.Employees);
        if (entity == null)
        {
            throw new TeamNotFoundException(id);
        }
        return Mapper.Map<TeamGet>(entity);
    }

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var entities = await TeamRepository.GetAsync(null, null, (team) => team.Employees);
        return Mapper.Map<List<TeamGet>>(entities);
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await TeamUpdateValidator.ValidateAsync(teamUpdate);
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);

        var missingEmployees = teamUpdate.Employees.Where((id) => !employees.Any(existing => existing.Id == id));
        if (missingEmployees.Any())
        {
            throw new EmployeesNotFoundException(missingEmployees.ToArray());
        }

        var existingEntity = await TeamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);

        if (existingEntity == null)
        {
            throw new TeamNotFoundException(teamUpdate.Id);
        }
        Mapper.Map(teamUpdate, existingEntity);
        existingEntity.Employees = employees;
        TeamRepository.Update(existingEntity);
        await TeamRepository.SaveChangesAsync();
    }
}
