namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;
    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _vaultKeepsService = vaultKeepsService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userInfo.Id);
            VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, vault);
            return Ok(vaultKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> GetVaultKeepById(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            VaultKeep vaultKeep = _vaultKeepsService.GetVaultKeepById(vaultKeepId, userInfo.Id);
            return Ok(vaultKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo?.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
