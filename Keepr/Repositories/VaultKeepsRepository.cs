namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultkeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
        vaultKeepData.Id = id;
        return vaultKeepData;
    }

    public VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT
        vk.*
        FROM vaultkeeps vk
        WHERE vk.id = @vaultKeepId
        ";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }

    public bool RemoveVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE from vaultkeeps
        WHERE id = @vaultKeepId
        ";
        int rows = _db.Execute(sql, new { vaultKeepId });
        return rows > 0;
    }
}
