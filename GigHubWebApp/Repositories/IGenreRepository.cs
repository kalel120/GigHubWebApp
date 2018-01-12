using System.Collections.Generic;
using GigHubWebApp.Models;

namespace GigHubWebApp.Repositories {
    public interface IGenreRepository {
        List<Genre> GetGenres();
    }
}