using Vogen;

namespace ISII.Web.Domain.ClienteAggregate;

[ValueObject<int>]
public readonly partial struct ClienteId
{
  public static ClienteId New => From(0);

  private static Validation Validate(int value) =>
    value >= 0
      ? Validation.Ok
      : Validation.Invalid("ClienteId must be non-negative.");
}