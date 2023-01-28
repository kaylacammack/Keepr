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
}
