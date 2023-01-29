namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }

    public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Vault vault)
    {
        if (vaultKeepData.CreatorId != vault.CreatorId)
        {
            throw new Exception("You cannot add keeps to vaults you do not own");
        }
        VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    public VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"Unable to find vaultkeep at id: {vaultKeepId}");
        }
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("Unauthorized");
        }
        return vaultKeep;
    }

    public string RemoveVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null) throw new Exception($"No vault at id: {vaultKeepId}");
        if (vaultKeep.CreatorId != userId) throw new Exception("You cannot delete a vaultkeep you did not create");
        bool result = _repo.RemoveVaultKeep(vaultKeepId);
        if (result == false) throw new Exception($"No vaultkeep at id: {vaultKeepId}");
        return "Vaultkeep has been deleted";
    }
}
