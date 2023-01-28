namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
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

    internal List<Keep> GetAllKeeps()
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

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id = @keepId
        ";
        Keep keep = _db.Query<Keep>(sql, new { keepId }).FirstOrDefault();
        return keep;
    }
}
