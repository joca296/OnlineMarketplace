using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Commands
{
    public interface IAuthenticateUserCommand : ICommand<LogInInfoDto, GetUserBasicDto>
    {
    }
}
