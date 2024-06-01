using System.Reflection;

namespace Con2D;

public class EntityManager
{
    private readonly EntityGenerator God;
    private List<Entity> _entities;
    private Player _hero;

    public EntityManager()
    {
        God = new EntityGenerator();
        _entities = [_hero = CreateHero()];

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
        return entity;
    }

    public void Remove(Entity entity) => _entities.Remove(entity);

    public Player Hero => _hero;

    public List<Entity> ActiveEntities => _entities.FindAll(x => x.IsActive).ToList();

    public List<Entity> Entities => _entities;

    public List<Entity> Active => _entities.FindAll(x => x.IsActive).ToList();

    public List<Entity> InActive => _entities.FindAll(x => !x.IsActive).ToList();

    public List<Enemy> Enemy => _entities.FindAll(x => x is Enemy).Cast<Enemy>().ToList();

    private Player CreateHero()
    {
        var hero = CreatePlayer();
        hero.PosX = 5;
        hero.PosY = 5;
        hero.Body = '@';
        return hero;
    }

    private Player CreatePlayer(int posX = 0, int posY = 0, char body = '@')
    {
        var entity = God.CreatePlayer();
        entity.PosX = posX;
        entity.PosY = posY;
        entity.Body = body;
        return entity;
    }

    private Enemy CreateEnemy(int posX = 0, int posY = 0, char body = 'E')
    {
        var entity = God.CreateEnemy();
        entity.PosX = posX;
        entity.PosY = posY;
        entity.Body = body;
        return entity;
    }
}
