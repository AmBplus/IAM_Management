using Base.Shared.ResultUtility;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace api.Services
{
    public class AddToRoleCommandRequest : IRequest<ResultOperation<int>>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }

    }
    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommandRequest, ResultOperation<int>>
    {
        public AddToRoleCommandHandler()
        {
                
        }
        public async Task<ResultOperation<int>> Handle(AddToRoleCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
