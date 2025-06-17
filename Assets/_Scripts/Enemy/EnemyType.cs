public enum EnemyType
{
    Normal, //Nothing special
    Ant, //Small but faster
    Goliath, //Bigger with more health but slower
    Berserker, //Gains move speed when at 1 hp
    Explosive, //Explodes on death
    Mother, //Spawns more enemies on death
    Ghost, //Alternately transparent
}

public enum EnemyPattern
{
    Swing, //Normal side to side
    Direct, //Head on to the base
    Shooter, //Can shoot projectiles on the player
    Boss = 10, //Big fight, contains all 3 combined
}
