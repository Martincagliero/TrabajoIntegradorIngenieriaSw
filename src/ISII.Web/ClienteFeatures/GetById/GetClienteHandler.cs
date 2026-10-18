using ISII.Web.Domain.ClienteAggregate;
using ISII.Web.Domain.ClienteAggregate.Specifications;

namespace ISII.Web.ClienteFeatures.GetById;

public record GetClienteQuery(ClienteId ClienteId)
  : IQuery<Result<ClienteDto>>;

public sealed class GetClienteHandler(IReadRepository<Cliente> repository)
  : IQueryHandler<GetClienteQuery, Result<ClienteDto>>
{
  public async ValueTask<Result<ClienteDto>> Handle(
    GetClienteQuery request,
    CancellationToken cancellationToken)
  {
    var specification = new ClienteByIdSpec(request.ClienteId);

    var cliente = await repository.FirstOrDefaultAsync(
      specification,
      cancellationToken);

    if (cliente is null)
      return Result.NotFound("Cliente no encontrado");

    return new ClienteDto(cliente.Id, cliente.Nombre);
  }
}