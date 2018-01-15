using System.Collections.Generic;
using GigHubWebApp.Core.Models;

namespace GigHubWebApp.Core.Repositories {
    public interface IGenreRepository {
        List<Genre> GetGenres();
    }
}