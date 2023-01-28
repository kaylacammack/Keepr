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
}
