using DL.STS.Host.App.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DL.STS.Host.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _mediator.Send(new GetLoginViewModel
            {
                ReturnUrl = returnUrl
            });
            return View(vm);
        }
    }
}