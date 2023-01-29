namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    public Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repo.CreateVault(vaultData);
        return vault;
    }

    public Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"No vault at id: {vaultId}");
        }
        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception("This vault can only be seen by it's owner");
        }
        return vault;
    }

    public Vault UpdateVault(Vault updateData, string userId)
    {
        Vault original = GetVaultById(updateData.Id, userId);
        if (original.CreatorId != updateData.CreatorId) throw new Exception("You may only modify a vault you have created");
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        original.IsPrivate = updateData.IsPrivate ?? original.IsPrivate;

        _repo.UpdateVault(original);
        return original;
    }

    public string RemoveVault(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault == null) throw new Exception($"No vault at id: {vaultId}");
        if (vault.CreatorId != userId) throw new Exception("You cannot delete a vault you did not create");
        bool result = _repo.RemoveVault(vaultId);
        if (result == false) throw new Exception($"No vault at id: {vaultId}");
        return "Vault has been deleted";
    }
}
