using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    private HitPoints hp = new HitPoints();
    private BossStates state = new BossStates();
    private Vector2 startingPosition;
    private const int BOSS_HEALTH = 100;

    void Start()
    {
        startingPosition = transform.position;
        //state = BossStates.Idle;
        SetBossHealth(1);
        transform.position += (Vector3)new Vector2(50, 50);
        StartCoroutine(ReturnToSpawn());
    }

    private IEnumerator ResetState()
    {
        state=BossStates.Idle;
        int randomTime = Random.Range(1, 5);
        while(hp.Health() > 0)
        {
            
            yield return new WaitUntil(AttackState());
        }

    }

    private void AttackState()
    {
        switch(state)
        {
            case BossStates.Shoot:

            break;
            case BossStates.Charge:
            
            break; 
            case BossStates.Swoosh:

            break;
            case BossStates.Split:

            break;
            case BossStates.Return:

            break;
        }
    }
    */

    public void SetBossHealth(int playerValue)
    {
        hp.SetHealth(playerValue * BOSS_HEALTH);
    }

    private IEnumerator ReturnToSpawn()
    {
        Vector2 currentPosition = transform.position;
        while(Vector2.Distance(currentPosition, startingPosition) > 0)
        {
        currentPosition = Vector2.MoveTowards(currentPosition, startingPosition, moveSpeed*Time.deltaTime);
        transform.position = currentPosition;
        Debug.Log("moving...");
        yield return new WaitForEndOfFrame();
        }
    ResetState();
    }
}
