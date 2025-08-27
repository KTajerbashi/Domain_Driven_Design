namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities;

[Table("RevokedTokens", Schema = "Identity")]
public class RevokedToken : Entity
{
    public string Token { get; set; } = string.Empty;
    public DateTime RevokedAt { get; set; }

    [ForeignKey("UserId")]
    public long UserId { get; set; }
    public virtual UserIdentity User { get; set; } = null!;
}
