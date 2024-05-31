﻿namespace Con2D;

public abstract class Entity
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int OldPosX { get; set; }
    public int OldPosY { get; set; }
    public bool IsActive { get; set; }
    public char Body { get; set; }

    public abstract void Move(int x, int y);

    public abstract void UpdateActive(bool status);
}
