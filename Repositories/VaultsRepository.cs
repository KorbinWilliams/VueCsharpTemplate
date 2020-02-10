using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string userId)
    {
      string sql = "SELECT * FROM Vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { userId });
    }

    internal Vault Create(Vault VaultData)
    {
      string sql = @"INSERT INTO vaults (userId, name, description) VALUES (@UserId, @Name, @Description); SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, VaultData);
      VaultData.Id = id;
      return VaultData;
    }

    internal Vault GetById(int id)
    {
      string sql = @"SELECT * FROM vaults WHERE id = @id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM vaults WHERE id = @id";
      _db.Execute(sql, new { id });
    }

    internal void Edit(Vault update)
    {
      string sql = @"UPDATE vaults SET name = @Name, description = @Description";
      _db.Execute(sql, update);
    }
  }
}