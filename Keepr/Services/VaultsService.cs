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

    public Vault GetVaultById(int vaultId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"No vault at id: {vaultId}");
        }
        return vault;
    }

    public Vault UpdateVault(Vault updateData)
    {
        Vault original = GetVaultById(updateData.Id);
        if (original.CreatorId != updateData.CreatorId) throw new Exception("You may only modify a vault you have created");
        original.Name = updateData.Name ?? original.Name;
        original.Description = updateData.Description ?? original.Description;
        original.Img = updateData.Img ?? original.Img;
        original.IsPrivate = updateData.IsPrivate ?? original.IsPrivate;

        _repo.UpdateVault(original);
        return original;
    }
}
