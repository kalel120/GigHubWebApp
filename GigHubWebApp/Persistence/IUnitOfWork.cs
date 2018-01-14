using GigHubWebApp.Repositories;

namespace GigHubWebApp.Persistence {
    public interface IUnitOfWork {
        IGigsRepositories GigsRepo { get; }
        IAttendancesRepository AttendancesRepo { get; }
        IFollowingRepository FollowingRepo { get; }
        IGenreRepository GenreRepo { get; }
        void Complete();
    }
}