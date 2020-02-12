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
        INSERT INTO vaultkeeps (vaultId, keepId)
        VALUES (@VaultId, @KeepId);
        SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVK);
      newVK.Id = id;
      return newVK;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"SELECT * FROM vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaultkeeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}