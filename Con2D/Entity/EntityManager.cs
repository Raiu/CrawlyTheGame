using System.Reflection;

namespace Con2D;

public class EntityManager
{
    private readonly EntityGenerator God;
    private List<Entity> _entities;
    private Player _hero;
    private Map _map;

    public Player Hero => _hero;
    public List<Entity> ActiveEntities => _entities.FindAll(x => x.IsActive).ToList();
    public List<Entity> Entities => _entities;
    public List<Entity> Active => _entities.FindAll(x => x.IsActive).ToList();
    public List<Entity> InActive => _entities.FindAll(x => !x.IsActive).ToList();
    public List<Enemy> Enemy => _entities.FindAll(x => x is Enemy).Cast<Enemy>().ToList();


    public EntityManager(Map map)
    {
        God = new EntityGenerator();
        _entities = [_hero = CreateHero()];
        _map = map;
    }

    public Entity Add(EntityType type, int posX = 0, int posY = 0, char body = ' ')
    {
        Entity entity;
        switch (type)
        {
            case EntityType.Player:
                entity = CreatePlayer(posX, posY, body);
                break;
            case EntityType.Enemy:
                entity = CreateEnemy(posX, posY, body);
                break;
            default:
                throw new Exception("Unknown entity type");
        }
        _entities.Add(entity);
        return entity;
    }

    public void Remove(Entity entity) => _entities.Remove(entity);

    public void SetMap(Map map) => _map = map;

    private Player CreateHero()
    {
        var hero = CreatePlayer();
        hero.PosX = 5;
        hero.PosY = 5;
        hero.Body = '@';
        return hero;
    }

    private Player CreatePlayer(int posX = -1, int posY = -1, char body = '@')
    {
        var ent = God.CreatePlayer();
        
        if (posX == -1 || posY == -1)
            (ent.PosX, ent.PosY) = (posX, posY);
        else
            (ent.PosX, ent.PosY) = GenerateRandomPosition(_map);

        return ent;
    }

    private Enemy CreateEnemy(int posX = -1, int posY = -1, char body = 'E')
    {
        var ent = God.CreateEnemy();
        //ent.Body = body;
        ent.Body = 'E';

        if (posX == -1 || posY ==-1)
            (ent.PosX, ent.PosY) = (posX, posY);
        else
            (ent.PosX, ent.PosY) = GenerateRandomPosition(_map);

        return ent;
    }

    private (int x, int y) GenerateRandomPosition(Map map)
    {
        int posX;
        int posY;

        do {
            var random = new Random();
            posX = random.Next(0, map.EndX);
            posY = random.Next(0, map.EndY);
        } while (!ValidEntityPosition(Active, posX, posY));

        return (posX, posY);
    }

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight) =>
            x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;

    private bool ValidEntityPosition(List<Entity> entities, int posX, int posY)
    {
        if (!IsPositionWithinMap(posX, posY, _map.Width, _map.Height)) return false;
        if (_map.Tiles[posX, posY].Type == TileType.Wall) return false;
        if (entities.Any(e => e.PosX == posX && e.PosY == posY && e.IsActive)) return false;
        return true;
    }
}
