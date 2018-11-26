using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpComingGigsByArtist(string userId);
        Gig GetGig(int id);
        IEnumerable<Gig> GetUpcomingGigs(string query = null);
        void Add(Gig gig);
        Gig GetGigWithAttendees(int id, string userId);
    }
}