namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public KeepsService(KeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    public Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repo.CreateKeep(keepData);
        return keep;
    }
    public List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    public Keep GetKeepById(int keepId, string userId)
    {
        Keep keep = _repo.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"No keep at id:{keepId}");
        }
        if (keep.CreatorId != userId)
        {
            keep.Views++;
            _repo.UpdateKeep(keep);
        }
        return keep;
    }

    public List<Keep> GetKeepsByVault(int vaultId, string userId)
    {
        Vault vault = _vaultsService.GetVaultById(vaultId, userId);

        return _repo.GetKeepsByVault(vaultId);
    }

    public List<Keep> GetAllProfileKeeps(string profileId)
    {
        return _repo.GetAllProfileKeeps(profileId);
    }

    public Keep UpdateKeep(Keep updateData, string userId)
    {
        Keep original = GetKeepById(updateData.Id, userId);
        if (original.CreatorId != userId) throw new Exception("You may only modify keeps you have created");
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;

        _repo.UpdateKeep(original);
        return original;
    }

    public string RemoveKeep(int keepId, string userId)
    {
        Keep keep = _repo.GetKeepById(keepId);
        if (keep == null) throw new Exception($"No keep at id {keepId}");
        if (keep.CreatorId != userId) throw new Exception("You cannot delete a keep you did not create");
        bool result = _repo.RemoveKeep(keepId);
        if (result == false) throw new Exception($"No keep at id: {keepId}");
        return "Keep has been deleted.";
    }
}
