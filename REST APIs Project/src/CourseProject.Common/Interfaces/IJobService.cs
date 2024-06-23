using CourseProject.Common.Dtos.Job;

namespace CourseProject.Common.Interfaces;

public interface IJobService
{
    Task<int> CreateJobAsync(JobCreate jobCreate);
    Task UpdateJobAsync(JobUpdate jobUpdate);
    Task<List<JobGet>> GetJobsAsync();
    Task<JobGet> GetJobAsync(int jobId);
    Task DeleteJobAsync(JobDelete jobDelete);
}
