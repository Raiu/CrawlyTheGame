﻿namespace Con2D;

public enum TileType
{
    None,
    Floor,
    Wall,
    EntityNone,
    EntityFloor,
}

public class Tile
{
    public TileType Type { get; set; }

    public char Body { get; set; }


    public Tile() : this(TileType.None) { }

    public Tile(TileType tileType)
    {
        Type = tileType;
    }

    public void UpdateEnity(char body)
    {
        Body = body;
        if (Type == TileType.Floor)
            Type = TileType.EntityFloor;
        else
            Type = TileType.EntityNone;
    }

}
