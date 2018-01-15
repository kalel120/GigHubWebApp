using GigHubWebApp.Core;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Repositories;

namespace GigHubWebApp.Persistence {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _dbContext;
        public IGigsRepositories GigsRepo { get; }
        public IAttendancesRepository AttendancesRepo { get; }
        public IFollowingRepository FollowingRepo { get; }
        public IGenreRepository GenreRepo { get; }

        public UnitOfWork(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
            GigsRepo = new GigsRepositories(_dbContext);
            AttendancesRepo = new AttendancesRepository(_dbContext);
            FollowingRepo = new FollowingRepository(_dbContext);
            GenreRepo = new GenreRepository(_dbContext);
        }

        public void Complete() {
            _dbContext.SaveChanges();
        }
    }
}