namespace Keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (creatorId, name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, vaultData);
        vaultData.Id = id;
        return vaultData;
    }

    public Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.id = @vaultId
        ";
        Vault vault = _db.Query<Vault, Account, Vault>(sql, (v, a) =>
        {
            v.Creator = a;
            return v;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    public List<Vault> GetAllProfileVaults(string profileId)
    {
        string sql = @"
        SELECT
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON v.creatorId = a.id
        WHERE v.creatorId = @profileId
        ";
        List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }

    public bool UpdateVault(Vault update)
    {
        string sql = @"
        UPDATE vaults SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id
        ";
        int rows = _db.Execute(sql, update);
        return rows > 0;
    }

    public bool RemoveVault(int vaultId)
    {
        string sql = @"
        DELETE from vaults
        WHERE id = @vaultId
        ";
        int rows = _db.Execute(sql, new { vaultId });
        return rows > 0;
    }
}
