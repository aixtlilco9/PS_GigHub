using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface IGenreRepository
    {
        List<Genre> GetGenres();
    }
}