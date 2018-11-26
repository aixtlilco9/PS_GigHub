using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.IRepository;
using GigHub.Core.Models;
using Microsoft.Owin.Security.Provider;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        //this IEnumerable used to be a list i dont quite understand why it went from a list to Ienumberable
        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
           return  _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);//used to be  (viewModel.Id && g.ArtistId == userId) instead of gigId
            //if we dnt have a gig with this id null will be returned?
        }

         public IEnumerable<Gig> GetUpComingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g => 
                g.ArtistId == userId  &&
                g.DateTime > DateTime.Now 
                && !g.IsCanceled
                )
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGig(int id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetUpcomingGigs(string query = null)
        {
            var upcomingGigs= _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)//this was included way after artist and date time as to prevent eagerloading in index span genre.
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }

            return upcomingGigs.ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public Gig GetGigWithAttendees(int id, string userId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == id && g.ArtistId == userId);
                //.SingleOrDefault(g => g.Id == id);
        }
    }
}