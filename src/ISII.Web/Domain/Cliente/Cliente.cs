namespace ISII.Web.Domain.ClienteAggregate;

public class Cliente : EntityBase<Cliente, ClienteId>, IAggregateRoot
{
  private Cliente() { }

  public Cliente(ClienteId id, string nombre)
  {
    Id = id;
    Nombre = nombre;
  }

  public string Nombre { get; private set; } = string.Empty;

  public void UpdateNombre(string nombre)
  {
    Nombre = nombre;
  }
}