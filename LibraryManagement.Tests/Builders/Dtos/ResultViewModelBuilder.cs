using Bogus;
using LibraryManagement.Application.Dtos;

namespace LibraryManagement.Tests.Builders.Dtos
{
    public class ResultViewModelBuilder
    {
        private readonly Faker<ResultViewModel> instance;

        public ResultViewModelBuilder()
        {
            instance = new AutoFaker<ResultViewModel>();
        }

        public ResultViewModelBuilder WithIsSuccess(bool isSuccess)
        {
            instance.RuleFor(x => x.IsSuccess, isSuccess);
            return this;
        }
        public ResultViewModelBuilder WithMessage(string message) 
        {
            instance.RuleFor(x => x.Message, message);
            return this;
        }

        public ResultViewModel Build() => instance.Generate();
    }

    public class ResultViewModelBuilder<T> : ResultViewModelBuilder
    {
        private readonly Faker<ResultViewModel<T>> instance;

        public ResultViewModelBuilder()
        {
            instance = new AutoFaker<ResultViewModel<T>>();
        }

        public ResultViewModelBuilder WithIsSuccess(T? data)
        {
            instance.RuleFor(x => x.Data, data);
            return this;
        }
        

        public ResultViewModel<T> Build() => instance.Generate();
    }
}
