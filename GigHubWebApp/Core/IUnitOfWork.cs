using GigHubWebApp.Core.Repositories;

namespace GigHubWebApp.Core {
    public interface IUnitOfWork {
        IGigsRepositories GigsRepo { get; }
        IAttendancesRepository AttendancesRepo { get; }
        IFollowingRepository FollowingRepo { get; }
        IGenreRepository GenreRepo { get; }
        void Complete();
    }
}