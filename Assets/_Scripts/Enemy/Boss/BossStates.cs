/// <summary>
/// All of the Boss' possible actions or moves.
/// </summary>
public enum BossStates
{
Intro, //Goes down from above the screen;
Swing, //Swings from side to side;
Return, //Returns to the starting point;
Shoot, //Shoots projectiles at the player;
Charge, //Charges towards the player;
Sweep, //Sweeps from one side of the screen to the other in order to hit the player;
Spawn, //Spawns reinforcement;
Split, //Splits into 2 smaller bosses;
}
