using ISII.Web.Domain.ClienteAggregate;

namespace ISII.Tests;

public class ClienteTest
{
  [Fact]
  public void Create_ShouldAssignProvidedValues()
  {
    var cliente = Cliente.Create(ClienteId.New, "Juan Pérez");

    Assert.Equal(ClienteId.New, cliente.Id);
    Assert.Equal("Juan Pérez", cliente.Nombre);
  }

  [Fact]
  public void Create_ShouldRejectEmptyName()
  {
    Assert.Throws<ArgumentException>(() =>
      Cliente.Create(ClienteId.New, ""));
  }

  [Fact]
  public void Create_ShouldRejectWhitespaceName()
  {
    Assert.Throws<ArgumentException>(() =>
      Cliente.Create(ClienteId.New, "   "));
  }

  [Fact]
  public void Create_ShouldRejectNameLongerThan100Characters()
  {
    var nombre = new string('A', 101);

    Assert.Throws<ArgumentException>(() =>
      Cliente.Create(ClienteId.New, nombre));
  }

  [Fact]
  public void UpdateNombre_ShouldChangeName()
  {
    var cliente = Cliente.Create(ClienteId.New, "Nombre anterior");

    var result = cliente.UpdateNombre("Nombre nuevo");

    Assert.Same(cliente, result);
    Assert.Equal("Nombre nuevo", cliente.Nombre);
  }
}