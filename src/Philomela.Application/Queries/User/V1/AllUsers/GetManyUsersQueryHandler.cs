using MediatR;

namespace Philomela.Application.Queries.User.V1.AllUsers
{
    public class GetManyUsersQueryHandler : IRequestHandler<GetManyUsersQuery, List<GetUserResponse>>
    {
        public Task<List<GetUserResponse>> Handle(GetManyUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
