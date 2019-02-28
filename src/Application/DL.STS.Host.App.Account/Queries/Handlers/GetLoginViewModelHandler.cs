using IdentityServer4.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DL.STS.Host.App.Account.Queries.Handlers
{
    public class GetLoginViewModelHandler : IRequestHandler<GetLoginViewModel, LoginViewModel>
    {
        private readonly IIdentityServerInteractionService _interaction;

        public GetLoginViewModelHandler(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public async Task<LoginViewModel> Handle(GetLoginViewModel query, CancellationToken cancellationToken)
        {
            LoginViewModel vm = null;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var context = await _interaction.GetAuthorizationContextAsync(query.ReturnUrl);

                vm = new LoginViewModel
                {
                    Email = context?.LoginHint,
                    ReturnUrl = query.ReturnUrl
                };
            }
            catch (OperationCanceledException) { }

            return vm;
        }
    }
}
