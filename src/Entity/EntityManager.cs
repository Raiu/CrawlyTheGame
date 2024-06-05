using System.Reflection;
using System.Runtime.CompilerServices;

namespace Crawly;

public class EntityManager
{
    private readonly EntityGenerator God;
    private List<IEntity> _entities;
    private Player _hero;
    private Map _map;

    public Player Hero => _hero;
    public List<IEntity> ActiveEntities => _entities.FindAll(x => x.IsActive).ToList();
    public List<IEntity> Entities => _entities;
    public List<IEntity> Active => _entities.FindAll(x => x.IsActive).ToList();
    public List<IEntity> InActive => _entities.FindAll(x => !x.IsActive).ToList();
    public List<Enemy> Enemies => _entities.FindAll(x => x is Enemy).Cast<Enemy>().ToList();
    public List<IDrawable> Drawables => _entities.FindAll(x => x is IDrawable).Cast<IDrawable>().ToList();



    public EntityManager(Map map)
    {
        God = new EntityGenerator(map);
        _entities = new List<IEntity>();
        _map = map;

        _hero = (Player)Add(EntityType.Player);
    }

    public IEntity Add(EntityType type) =>
        Add(type, new Coordinate(-1, -1));

    public IEntity Add(EntityType type, Coordinate position , char? body = null)
    {
        IEntity entity;
        switch (type)
        {
            case EntityType.Player:
                entity = God.CreatePlayer(position, body);
                break;
            case EntityType.Enemy:
                entity = God.CreateEnemy(position, body);
                break;
            default:
                throw new Exception("Unknown entity type");
        }

        if (entity == null) throw new Exception("Failed to create entity");

        _entities.Add(entity);

        return entity;
    }

    public void Remove(IEntity entity) => _entities.Remove(entity);

    private bool IsPositionWithinMap(Coordinate pos, int mapWidth, int mapHeight) =>
            pos.X >= 0 && pos.X < mapWidth && pos.Y >= 0 && pos.Y < mapHeight;

    private bool ValidEntityPosition(List<IEntity> entities, Coordinate pos)
    {
        if (!IsPositionWithinMap(pos, _map.Width, _map.Height)) return false;

        if (_map.Tiles[pos.X, pos.Y].Type == TileType.Wall) return false;

        if (entities.Any(e => e.Position.X == pos.X && e.Position.Y == pos.Y && e.IsActive)) return false;

        return true;
    }
}
