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

    public bool UpdateKeep(Keep update)
    {
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img
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
