using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarketPlace.Application.Interfaces
{
    public interface ICommand<TRequest>
    {
        void Execute(TRequest request);
    }

    public interface ICommand<TRequest,TResponse> where TResponse : class
    {
        void Execute(TRequest request);
    }
}
