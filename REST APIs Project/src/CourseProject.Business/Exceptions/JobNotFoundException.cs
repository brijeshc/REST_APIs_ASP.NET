﻿using CourseProject.Common.Dtos.Job;
using System.Runtime.Serialization;

namespace CourseProject.Business.Exceptions;

[Serializable]
public class JobNotFoundException : Exception
{
    public int Id { get; }

    public JobNotFoundException()
    {
    }

    public JobNotFoundException(int id)
    {
        Id = id;
    }

    public JobNotFoundException(string? message) : base(message)
    {
    }

    public JobNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected JobNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}