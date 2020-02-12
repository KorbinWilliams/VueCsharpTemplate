using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      string sql = @"
        INSERT INTO vaultkeeps (vaultId, keepId, userId)
        VALUES (@VaultId, @KeepId, @UserId);
        SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVK);
      newVK.Id = id;
      return newVK;
    }

    internal VaultKeep FindByIds(int keepId, int vaultId)
    {
      string sql = @"SELECT * FROM vaultkeeps WHERE 
      (vaultId = @VaultId AND keepId = @KeepId)";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId });
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"SELECT * FROM vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault(sql, new { id });
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(Vault vault)
    {
      string sql = @"SELECT k.* FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId
        WHERE (vaultId = @Id AND vk.userId = @userId)";
      return _db.Query<Keep>(sql, vault);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}