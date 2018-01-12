using GigHubWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHubWebApp.Repositories {
    public class GenreRepository {
        private readonly ApplicationDbContext _dbContext;

        public GenreRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public List<Genre> GetGenres() {
            return _dbContext.Genres.ToList();
        }
    }
}