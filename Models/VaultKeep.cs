namespace Keepr.Models
{
  public class VaultKeep
  {
    public VaultKeep()
    { }
    public int Id { get; set; }

    public int VaultId { get; set; }

    public int KeepId { get; set; }

    public string UserId { get; set; }
  }
}