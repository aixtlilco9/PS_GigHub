using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Dtos;
using GigHub.Core.IRepository;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        //used alt and enter to create private readonly and _context = context
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }

        //public Attendance AlreadyAttending(AttendanceDto dto, string userId)
        //{
        //    return _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == dto.GigId);
        //} simmilar to getattendance so will use that in dto method

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);

        }
    }
}