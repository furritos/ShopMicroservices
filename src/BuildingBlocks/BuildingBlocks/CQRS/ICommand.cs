using MediatR;

namespace BuildingBlocks.CQRS
{
    /*
     * <Unit> is the equivalent of void, no TResponse is expected
     */
    public interface ICommand: ICommand<Unit> { }

    /*
     * Returns a TReponse generic
     */
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
