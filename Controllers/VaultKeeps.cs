using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly VaultsService _vs;
    public VaultKeepsController(VaultKeepsService vks, VaultsService vs)
    {
      _vks = vks;
      _vs = vs;
    }

    [HttpGet("{vaultId}/keeps")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> GetKeepsByVaultId(int vaultId)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        Vault vault = _vs.GetById(vaultId, userId);
        return Ok(_vks.GetKeepsByVaultId(vault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<VaultKeep> Create([FromBody] VaultKeep newData)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newData.UserId = userId;
        _vks.Create(newData);
        return Ok("Success");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{vaultId}/keeps/{keepId}")]
    [Authorize]
    public ActionResult<String> Delete([FromBody] int vaultId, int keepId)
    {
      try
      {
        // getting 415 maybe drop tables?
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.Delete(vaultId, keepId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}

