using OnlineMarketPlace.Application.DataTransfer;
using OnlineMarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OnlineMarketPlace.Application.Commands
{
    public interface ICreateRoleCommand : ICommand<RoleDto>
    {
    }
}
