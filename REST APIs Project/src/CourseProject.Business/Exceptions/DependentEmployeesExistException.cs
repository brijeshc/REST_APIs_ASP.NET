using CourseProject.Common.Model;
using System.Runtime.Serialization;

namespace CourseProject.Business.Exceptions;

[Serializable]
public class DependentEmployeesExistException : Exception
{
    public List<Employee> employees { get; }

    public DependentEmployeesExistException()
    {
    }

    public DependentEmployeesExistException(List<Employee> employees)
    {
        this.employees = employees;
    }

    public DependentEmployeesExistException(string? message) : base(message)
    {
    }

    public DependentEmployeesExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentEmployeesExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}