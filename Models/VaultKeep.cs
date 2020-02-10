namespace Keepr.Models
{
  class VaultKeep
  {
    public VaultKeep()
    { }
    int Id { get; set; }

    int VaultId { get; set; }

    int KeepId { get; set; }

    int UserId { get; set; }
  }
}