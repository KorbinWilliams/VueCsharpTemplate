using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get(string userId)
    {
      // string sql = "IF isPrivate = 0 OR userId = @UserId SELECT * FROM keeps END IF";
      string sql = @"SELECT * FROM keeps WHERE isPrivate = 0";
      return _db.Query<Keep>(sql, userId);
    }

    internal Keep GetById(int id)
    {
      string sql = @"SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal Keep Create(Keep KeepData)
    {
      string sql = @"INSERT INTO keeps (userId, name, description, img, isPrivate, views, shares, keeps) VALUES (@UserId, @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, KeepData);
      KeepData.Id = id;
      return KeepData;
    }

    internal void Edit(Keep update)
    {
      string sql = @"UPDATE keeps SET name = @Name, description = @Description, img = @img, isPrivate = @IsPrivate, views = @Views, shares = @Shares, keeps = @Keeps WHERE id = @Id";
      _db.Execute(sql, update);
    }

    internal void Delete(int id)
    {
      string sql = @"DELETE FROM keeps WHERE id = @id";
      _db.Execute(sql, new { id });
    }

  }
}

// //     public int Id { get; set; }
//     public string UserId { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public string Img { get; set; }
//     public bool IsPrivate { get; set; }
//     public int Views { get; set; }
//     public int Shares { get; set; }
//     public int Keeps { get; set; }
