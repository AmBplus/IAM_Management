using Base.Shared.ResultUtility;

namespace api.Services.AdminServices
{
    public interface IAddRoll
    {
        Task<ResultOperation<int>> Check(CancellationToken cancellationToken);
    }
}
