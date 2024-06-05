namespace Crawly;

public class EntityGenerator
{
    private Map _map;

    public EntityGenerator(Map map)
    {
        _map = map;
    }

    public Player Createplayer() => 
        CreatePlayer(new Coordinate(-1, -1));

    public Player CreatePlayer(Coordinate position, char? body = null)
    {
        if (position.X == -1 && position.Y == -1)
            position = GenerateRandomPosition(_map);

        char b = body ?? 'E';
        return new Player(position, b);
    }

    public Enemy CreateEnemy() => 
        CreateEnemy(new Coordinate(-1, -1));

    public Enemy CreateEnemy(Coordinate position, char? body = null)
    {
        if (position.X == -1 && position.Y == -1)
            position = GenerateRandomPosition(_map);
        
        char b = body ?? 'E';

        return new Enemy(position, b);
    }

    private Coordinate GenerateRandomPosition(Map map)
    {
        var random = new Random();
        var posX = random.Next(0, map.EndX);
        var posY = random.Next(0, map.EndY);

        return new Coordinate(posX, posY);
    }
}
