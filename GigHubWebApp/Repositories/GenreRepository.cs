using System.Collections.Generic;
using System.Linq;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Persistence;

namespace GigHubWebApp.Repositories {
    public class GenreRepository : IGenreRepository {
        private readonly ApplicationDbContext _dbContext;

        public GenreRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public List<Genre> GetGenres() {
            return _dbContext.Genres.ToList();
        }
    }
}