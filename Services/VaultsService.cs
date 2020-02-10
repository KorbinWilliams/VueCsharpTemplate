using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }

    public Vault Create(Vault newVault)
    {
      var newerVault = _repo.Create(newVault);
      return newerVault;
    }

    internal Vault GetById(int id, string userId)
    {
      var exists = _repo.GetById(id);
      if (exists == null)
      {
        throw new Exception("That is not a vault id plebian.");
      }
      else if (userId != exists.UserId)
      {
        throw new Exception("You don't own any vaults or are not logged in.");
      }
      return exists;
    }

    internal string Delete(int id, string userId)
    {
      var exists = GetById(id, userId);
      if (exists == null)
      {
        throw new Exception("Invalid id");
      }
      else if (userId != exists.UserId)
      {
        throw new Exception("You do not own this Vault peasant!");
      }
      _repo.Delete(id);
      return "Successfully Destroyed";
    }

    internal string Edit(Vault update)
    {
      var exists = GetById(update.Id, update.UserId);
      if (exists == null)
      {
        throw new Exception("Invalid id");
      }
      else if (update.UserId != exists.UserId)
      {
        throw new Exception("You do not own this Vault peasant!");
      }
      _repo.Edit(update);
      return "Successfully Updated";
    }
  }
}