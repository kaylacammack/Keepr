namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;

    public ProfilesController(AccountService accountService, KeepsService keepsService, Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _accountService = accountService;
        _keepsService = keepsService;
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _accountService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetAllProfileKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetAllProfileKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]


    public async Task<ActionResult<List<Vault>>> GetAllProfileVaults(string profileId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetAllProfileVaults(profileId, userInfo?.Id);
            return Ok(vaults);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
