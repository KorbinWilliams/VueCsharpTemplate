using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal void Create(VaultKeep newData)
    {
      VaultKeep exists = _repo.GetById(newData.Id);
      if (exists != null)
      {
        throw new Exception("Relationship already exists");
      }
      // Pretty sure I don't need this on create...
      // else if (exists.UserId != newData.UserId)
      // {
      //   throw new Exception("");
      // }
      _repo.Create(newData);
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(Vault vault)
    {
      var exists = _repo.GetKeepsByVaultId(vault);
      // if (exists == null)
      // {
      //   throw new Exception("This is not the VaultKeep you are looking for");
      // }
      // else if (exists.UserId != userId)
      // {
      //   throw new Exception("Get yer own VaultKeep ya ninnie!");
      // }
      return exists;
    }

    internal string Delete(int keepId, int vaultId, string userId)
    {
      VaultKeep exists = _repo.FindByIds(keepId, vaultId);
      if (exists == null)
      {
        throw new Exception("Invalid Id Combination");
      }
      else if (exists.UserId != userId)
      {
        throw new Exception("No tienes estes");
      }
      _repo.Delete(exists.Id);
      return "Successfully Deleted";
    }
  }
}

