namespace Con2D;

public class EntityGenerator
{
    public Player CreatePlayer()
    {
        var player = new Player(0, 0);
        player.Body = '@';
        return player;
    }

    public Enemy CreateEnemy()
    {
        var enemy = new Enemy(0,0);
        enemy.Body = 'E';
        return enemy;
    }


    /* private Player CreatePlayer(Map map, List<Entity> entities, char body)
    {
        var (posX, posY) = GenerateRandomPosition(map);
        var player = new Player(posX, posY);
        player.Body = body;
        return player;
    }

    private Enemy CreateEnemey(Map map, List<Entity> entities, char body)
    {
        var (posX, posY) = GenerateRandomPosition(map);
        var enemy = new Enemy(posX, posY);
        enemy.Body = body;
        return enemy;
    } 
    */
}
