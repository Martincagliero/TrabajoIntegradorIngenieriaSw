using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using ISII.Web.Domain.ClienteAggregate;
using ISII.Web.Extensions;

namespace ISII.Web.ClienteFeatures.GetById;

public sealed class GetClienteByIdRequest
{
  public const string Route = "/clientes/{ClienteId}";
  public int ClienteId { get; init; }
}

public sealed class GetByIdEndpoint(IMediator mediator)
  : Endpoint<GetClienteByIdRequest,
      Results<Ok<ClienteRecord>, NotFound, ProblemHttpResult>,
      GetClienteByIdMapper>
{
  public override void Configure()
  {
    Get(GetClienteByIdRequest.Route);
    AllowAnonymous();
    Tags("Clientes");

    Summary(s =>
    {
      s.Summary = "Consultar cliente por ID";
      s.Description = "Obtiene un cliente por su identificador.";
      s.ExampleRequest = new GetClienteByIdRequest { ClienteId = 1 };
      s.ResponseExamples[200] = new ClienteRecord(1, "Juan Pérez");
      s.Responses[200] = "Cliente encontrado";
      s.Responses[404] = "Cliente no encontrado";
      s.Responses[400] = "Identificador inválido";
    });

    Description(builder => builder
      .Produces<ClienteRecord>(200, "application/json")
      .ProducesProblem(400)
      .ProducesProblem(404));
  }

  public override async Task<
    Results<Ok<ClienteRecord>, NotFound, ProblemHttpResult>>
    ExecuteAsync(GetClienteByIdRequest request, CancellationToken cancellationToken)
  {
    var query = new GetClienteQuery(ClienteId.From(request.ClienteId));
    var result = await mediator.Send(query, cancellationToken);

    return result.ToGetByIdResult(Map.FromEntity);
  }
}

public sealed class GetClienteByIdValidator : Validator<GetClienteByIdRequest>
{
  public GetClienteByIdValidator()
  {
    RuleFor(request => request.ClienteId)
      .GreaterThan(0)
      .WithMessage("El ID del cliente debe ser mayor que cero");
  }
}

public sealed class GetClienteByIdMapper
  : Mapper<GetClienteByIdRequest, ClienteRecord, ClienteDto>
{
  public override ClienteRecord FromEntity(ClienteDto cliente) =>
    new(cliente.Id.Value, cliente.Nombre);
}