using LibraryManagement.Application.Dtos;

namespace LibraryManagement.Application.Commands.Loans.Notify
{
    public interface INotifyDelayService
    {
        Task<ResultViewModel> Execute(CancellationToken stoppingToken);
    }
}
