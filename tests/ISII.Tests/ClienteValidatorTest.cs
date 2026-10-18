using ISII.Web.ClienteFeatures.Create;
using ISII.Web.ClienteFeatures.GetById;

namespace ISII.Tests;

public class ClienteValidatorTest
{
  [Fact]
  public async Task CreateValidator_ShouldRejectEmptyName()
  {
    var validator = new CreateClienteValidator();
    var request = new CreateClienteRequest { Nombre = "" };

    var result = await validator.ValidateAsync(request);

    Assert.False(result.IsValid);
  }

  [Fact]
  public async Task CreateValidator_ShouldAcceptValidName()
  {
    var validator = new CreateClienteValidator();
    var request = new CreateClienteRequest { Nombre = "Ana García" };

    var result = await validator.ValidateAsync(request);

    Assert.True(result.IsValid);
  }

  [Fact]
  public async Task GetValidator_ShouldRejectNonPositiveId()
  {
    var validator = new GetClienteByIdValidator();
    var request = new GetClienteByIdRequest { ClienteId = 0 };

    var result = await validator.ValidateAsync(request);

    Assert.False(result.IsValid);
  }
}