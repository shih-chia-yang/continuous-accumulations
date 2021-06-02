using System;
using System.Threading.Tasks;

namespace marketplace.domain.kernel.commands
{
    // public class RetryingCommandHandler<T> : ICommandHandler<T>
    // {
    //     static RetryPolicy _policy = Policy.Handle<InvalidOperationException>().Retry();

    //     private ICommandHandler<T> _next;

    //     public RetryingCommandHandler(ICommandHandler<T> next)
    //     {
    //         _next = next;
    //     }

    //     public Task Handle(T command)
    //     {
    //         await _policy.ExecteAsync(() =>_next.Handle(command));
    //     }
    // }
}