using JwtStore.Core.Contexts.SharedContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; } = Guid.NewGuid().ToString("N").ToUpper()[0..6];
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null; 

    public void Verify(string code)
    {
        if (IsActive)
            throw new Exception("Este item já foi ativado");

        if(DateTime.UtcNow > ExpiresAt)
            throw new Exception("O código expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código inválido");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}