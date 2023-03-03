using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Core
{
    /// <summary>
    /// Baseclass for Commands
    /// </summary>
    public abstract class BaseCommand<Req, Res> : IRequestHandler<Req, Res>
    where Res : IApplicationResponse
    where Req : class, IGenericApplicationRequest<Res>, IRequest
    {
        public abstract Task<Res> Handle(Req request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// MediatR Req/Res Representation
    /// </summary>
    public interface IGenericApplicationRequest<Res> : IRequest<Res>
    where Res : IApplicationResponse
    {
    }

    /// <summary>
    /// To mark the response of the commands
    /// </summary>
    public interface IApplicationResponse
    {
    }
}
