using MediatR;

namespace Philomela.Application.Queries.User.V1.AllUsers
{
    public class GetManyUsersQuery : IRequest<List<GetUserResponse>>
    {
    }
}
