namespace ISII.Web.Domain.ClienteAggregate.Specifications;

public sealed class ClienteByIdSpec : Specification<Cliente>
{
  public ClienteByIdSpec(ClienteId clienteId) =>
    Query.Where(cliente => cliente.Id == clienteId);
}