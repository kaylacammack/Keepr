namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
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

    public Keep GetKeepById(int keepId)
    {
        Keep keep = _repo.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"No keep at id:{keepId}");
        }
        return keep;
    }

    public List<Keep> GetKeepsByVault(int vaultId)
    {
        List<Keep> keeps = _repo.GetKeepsByVault(vaultId);
        return keeps;
    }

    public List<Keep> GetAllProfileKeeps(string profileId)
    {
        return _repo.GetAllProfileKeeps(profileId);
    }

    public Keep UpdateKeep(Keep updateData)
    {
        Keep original = GetKeepById(updateData.Id);
        if (original.CreatorId != updateData.CreatorId) throw new Exception("You may only modify keeps you have created");
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
