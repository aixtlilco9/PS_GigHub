using System.Collections.Generic;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        Attendance GetAttendance(int gigId, string userId);
        //Attendance AlreadyAttending(AttendanceDto dto, string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);

    }

}