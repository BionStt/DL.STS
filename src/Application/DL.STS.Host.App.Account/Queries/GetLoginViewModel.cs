using MediatR;

namespace DL.STS.Host.App.Account.Queries
{
    public class GetLoginViewModel : IRequest<LoginViewModel>
    {
        public string ReturnUrl { get; set; }
    }
}
