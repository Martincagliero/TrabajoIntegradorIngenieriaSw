namespace ISII.Web.Domain.ClienteAggregate;

public class Cliente : EntityBase<Cliente, ClienteId>, IAggregateRoot
{
  private Cliente() { }

  public Cliente(ClienteId id, string nombre)
  {
    Id = id;
    SetNombre(nombre);
  }

  public string Nombre { get; private set; } = string.Empty;

  public static Cliente Create(ClienteId id, string nombre) =>
    new(id, nombre);

  public Cliente UpdateNombre(string nombre)
  {
    SetNombre(nombre);
    return this;
  }

  private void SetNombre(string nombre)
  {
    if (string.IsNullOrWhiteSpace(nombre))
      throw new ArgumentException("El nombre del cliente es obligatorio.", nameof(nombre));

    if (nombre.Length > 100)
      throw new ArgumentException(
        "El nombre del cliente no puede superar los 100 caracteres.",
        nameof(nombre));

    Nombre = nombre.Trim();
  }
}