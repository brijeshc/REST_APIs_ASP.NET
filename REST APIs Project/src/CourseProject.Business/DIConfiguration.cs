using CourseProject.Business.Services;
using CourseProject.Business.Validation;
using CourseProject.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.Business;
public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITeamService, TeamService>();

        services.AddScoped<AddressCreateValidatior>();
        services.AddScoped<AddressUpdateValidatior>();
        services.AddScoped<EmployeeCreateValidatior>();
        services.AddScoped<EmployeeUpdateValidatior>();
        services.AddScoped<JobCreateValidatior>();
        services.AddScoped<JobUpdateValidatior>();
        services.AddScoped<TeamCreateValidatior>();
        services.AddScoped<TeamUpdateValidatior>();
    }
}
