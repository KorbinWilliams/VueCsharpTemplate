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
        throw new Exception("Student already enrolled");
      }
      // Pretty sure I don't need this on create...
      // else if (exists.UserId != newData.UserId)
      // {
      //   throw new Exception("No eres este estudiante.");
      // }
      _repo.Create(newData);
    }

    internal VaultKeep GetById(int id, string userId)
    {
      VaultKeep exists = _repo.GetById(id);
      if (exists == null)
      {
        throw new Exception("This is not the VaultKeep you are looking for");
      }
      else if (exists.UserId != userId)
      {
        throw new Exception("Get yer own VaultKeep ya ninnie!");
      }
      return exists;
    }

    internal string Delete(VaultKeep vk)
    {
      VaultKeep exists = _repo.GetById(vk.Id);
      if (exists == null)
      {
        throw new Exception("Invalid Id Combination");
      }
      else if (exists.UserId != vk.UserId)
      {
        throw new Exception("No eres este estudiante.");
      }
      _repo.Delete(exists.Id);
      return "Successfully Deleted";
    }
  }
}

