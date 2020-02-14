using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      var exists = _repo.Get();
      if (exists == null)
      {
        throw new Exception("There are no keeps");
      }
      return exists;
    }

    public Keep Create(Keep newKeep)
    {
      var newerKeep = _repo.Create(newKeep);
      return newerKeep;
    }

    internal Keep GetById(int id)
    {
      var exists = _repo.GetById(id);
      if (exists == null)
      {
        throw new Exception("Invalid id");
      }
      return exists;
    }

    internal string Delete(int id, string userId)
    {
      var exists = GetById(id);
      if (exists == null)
      {
        throw new Exception("Invalid id");
      }
      else if (userId != exists.UserId)
      {
        throw new Exception("You do not own this Keep peasant!");
      }
      _repo.Delete(id);
      return "Successfully Destroyed";
    }

    internal IEnumerable<Keep> GetMyKeeps(string userId)
    {
      var exists = _repo.GetMyKeeps(userId);
      if (exists == null)
      {
        throw new Exception("You have no keeps");
      }
      return exists;
    }

    internal string Edit(Keep update)
    {
      var exists = GetById(update.Id);
      if (exists == null)
      {
        throw new Exception("Invalid id");
      }
      else if (update.UserId != exists.UserId)
      {
        throw new Exception("You do not own this Keep peasant!");
      }
      _repo.Edit(update);
      return "Successfully Updated";
    }
  }
}