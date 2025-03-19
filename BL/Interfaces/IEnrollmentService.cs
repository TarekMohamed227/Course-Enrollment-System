using System;
using System.Collections.Generic;
using DAL.Models;

namespace BL.Interfaces
{
    public interface IEnrollmentService
    {
        IEnumerable<Enrollment> GetAllEnrollments();
        Enrollment? GetEnrollmentById(int id);
      
        void UnenrollStudent(int enrollmentId);
        bool IsAlreadyEnrolled(int studentId, int courseId);
        IEnumerable<Enrollment> GetEnrollmentsByCourseId(int courseId);
        void AddEnrollment(Enrollment enrollment);
        IEnumerable<Enrollment> GetAllEnrollmentsWithDetails();

    }
}
