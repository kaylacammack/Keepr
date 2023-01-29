namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (creatorId, name, description, img)
        VALUES
        (@creatorId, @name, @description, @img);
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, keepData);
        keepData.Id = id;
        return keepData;
    }

    public List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        ";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (k, a) =>
        {
            k.Creator = a;
            return k;
        }).ToList();
        return keeps;
    }

    public Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        COUNT (vaultId) AS kept,
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id = @keepId
        ";
        Keep keep = _db.Query<Keep, Account, Keep>(sql, (k, a) =>
        {
            k.Creator = a;
            return k;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }
    public List<Keep> GetKeepsByVault(int vaultId)
    {
        string sql = @"
        SELECT
        k.*,
        a.*,
        vk.*
        FROM vaultkeeps vk
        JOIN keeps k ON vk.keepId = k.id
        JOIN accounts a ON k.creatorId = a.id
        WHERE vk.vaultId = @vaultId;
        ";
        List<Keep> keeps = _db.Query<Keep, Account, VaultKeep, Keep>(sql, (k, a, vk) =>
        {
            k.VaultKeepId = vk.Id;
            k.Creator = a;
            return k;
        }, new { vaultId }).ToList();
        return keeps;
    }

    public List<Keep> GetAllProfileKeeps(string profileId)
    {
        string sql = @"
        SELECT
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON k.creatorId = a.id
        WHERE k.creatorId = @profileId
        ";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    public bool UpdateKeep(Keep update)
    {
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img,
        views = @views
        WHERE id = @id;
        ";
        int rows = _db.Execute(sql, update);
        return rows > 0;
    }

    public bool RemoveKeep(int keepId)
    {
        string sql = @"
        DELETE from keeps
        WHERE id = @keepId
        ";
        int rows = _db.Execute(sql, new { keepId });
        return rows > 0;
    }
}
