using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using ISII.Web.Domain.ClienteAggregate;

namespace ISII.Web.ClienteFeatures.Create;

public sealed class CreateClienteRequest
{
  public string Nombre { get; init; } = string.Empty;
}

public sealed class CreateEndpoint(IRepository<Cliente> repository)
  : Endpoint<CreateClienteRequest,
      Results<Created<ClienteRecord>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/clientes");
    AllowAnonymous();
    Tags("Clientes");

    Summary(s =>
    {
      s.Summary = "Crear un cliente";
      s.Description = "Crea un cliente con el nombre indicado.";
      s.ExampleRequest = new CreateClienteRequest { Nombre = "Juan Pérez" };
      s.ResponseExamples[201] = new ClienteRecord(1, "Juan Pérez");
      s.Responses[201] = "Cliente creado correctamente";
      s.Responses[400] = "Datos inválidos";
    });

    Description(builder => builder
      .Accepts<CreateClienteRequest>("application/json")
      .Produces<ClienteRecord>(201, "application/json")
      .ProducesProblem(400));
  }

  public override async Task<
    Results<Created<ClienteRecord>, ValidationProblem, ProblemHttpResult>>
    ExecuteAsync(CreateClienteRequest request, CancellationToken cancellationToken)
  {
    var cliente = Cliente.Create(ClienteId.New, request.Nombre);

    await repository.AddAsync(cliente, cancellationToken);
   

    var response = new ClienteRecord(cliente.Id.Value, cliente.Nombre);

    return TypedResults.Created($"/clientes/{cliente.Id.Value}", response);
  }
}

public sealed class CreateClienteValidator : Validator<CreateClienteRequest>
{
  public CreateClienteValidator()
  {
    RuleFor(request => request.Nombre)
      .NotEmpty()
      .WithMessage("El nombre es obligatorio")
      .MaximumLength(100)
      .WithMessage("El nombre no puede superar los 100 caracteres");
  }
}