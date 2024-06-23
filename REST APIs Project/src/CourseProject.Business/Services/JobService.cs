using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validation;
using CourseProject.Common.Dtos.Job;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;

namespace CourseProject.Business.Services;

public class JobService : IJobService
{
    private IMapper Mapper
    {
        get;
    }
    private IGenericRepository<Job> JobRepository { get; }
    private JobCreateValidatior JobCreateValidator { get; }
    private JobUpdateValidatior JobUpdateValidator { get; }

    public JobService(IMapper mapper, IGenericRepository<Job> jobRepository,
        JobCreateValidatior jobCreateValidator, JobUpdateValidatior jobUpdateValidator)
    {
        Mapper = mapper;
        JobRepository = jobRepository;
        JobCreateValidator = jobCreateValidator;
        JobUpdateValidator = jobUpdateValidator;
    }


    public async Task<int> CreateJobAsync(JobCreate jobCreate)
    {
        await JobCreateValidator.ValidateAsync(jobCreate);
        var entity = Mapper.Map<Job>(jobCreate);
        await JobRepository.InsertAsync(entity);
        await JobRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteJobAsync(JobDelete jobDelete)
    {
        var entity = await JobRepository.GetByIdAsync(jobDelete.Id, (job) => job.Employees);
        if (entity == null)
        {
            throw new JobNotFoundException(jobDelete.Id);
        }
        if(entity.Employees.Count > 0)
        {
            throw new DependentEmployeesExistException(entity.Employees);
        }
       
        JobRepository.Delete(entity);
        await JobRepository.SaveChangesAsync();
    }

    public async Task<JobGet> GetJobAsync(int id)
    {
        var entity = await JobRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new JobNotFoundException(id);
        }
        return Mapper.Map<JobGet>(entity);
    }

    public async Task<List<JobGet>> GetJobsAsync()
    {
        var entities = await JobRepository.GetAsync(null, null);
        return Mapper.Map<List<JobGet>>(entities);
    }

    public async Task UpdateJobAsync(JobUpdate jobUpdate)
    {
        await JobUpdateValidator.ValidateAsync(jobUpdate);

        var existingJob = await JobRepository.GetByIdAsync(jobUpdate.id);
        if ( existingJob == null)
        {
            throw new JobNotFoundException(jobUpdate.id);
        }
        var entity = Mapper.Map<Job>(jobUpdate);
        JobRepository.Update(entity);
        await JobRepository.SaveChangesAsync();
    }
}
