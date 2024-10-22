using LibraryManagement.Application.Configuration;
using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Services.Mail;
using LibraryManagement.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Application.Commands.Loans.Notify
{
    public class NotifyDelayService : INotifyDelayService
    {
        
        private readonly ILogger<NotifyDelayService> _logger;
        private readonly INetMailEmailService _notificationService;
        private readonly int _returnDays;
        private readonly ILoanRepository _loanRepository;

        public NotifyDelayService(
            ILogger<NotifyDelayService> logger,
            INetMailEmailService notificationService,
            ApplicationConfig appConfig,
            ILoanRepository loanRepository
            )
        {
            _loanRepository = loanRepository;
            _logger = logger;
            _notificationService = notificationService;
            _returnDays = appConfig.ReturnDaysConfig.Default;
        }

        public async Task<ResultViewModel> Execute(CancellationToken stoppingToken)
        {
            var loansDelay = await _loanRepository.GetAllLoanDelay(_returnDays);

            foreach (var loan in loansDelay)
            {
                try
                {
                    TimeSpan date = DateTime.Now - loan.DateOfLoan;
                    var delayDays = date.Days;
                    
                    _notificationService.SendEMail(loan.User.Name, loan.User.Email, delayDays.ToString(), loan.Book.Title);
                }
                catch (Exception e)
                {
                    return ResultViewModel.Error(e.Message.ToString());
                }
            }
            return ResultViewModel.Sucess();
        }
    }
}
