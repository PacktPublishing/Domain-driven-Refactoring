using BrewUp.Shared.DomainModel;
using System.Linq.Expressions;

namespace BrewUp.Shared.ReadModel
{
	public interface IQueries<T> where T : DtoBase
	{
		Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
		Task<PagedResult<T>> GetByFilterAsync(Expression<Func<T, bool>>? query, int page, int pageSize, CancellationToken cancellationToken);
	}
}
