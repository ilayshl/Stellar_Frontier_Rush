using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private Transform player;
    private HitPoints hp = new HitPoints();
    private BossStates _state = new BossStates();
    private Vector2 startingPosition;
    private int moveDir = 1;

    private Coroutine activeCoroutine;

    private const int BOSS_HEALTH = 100;
    private const float EDGEX = 7.5f;

    void Start()
    {
        startingPosition = transform.position;
        //state = BossStates.Idle;
        InitiateBoss(1);
    }

    public void InitiateBoss(int healthMultiplier)
    {
        hp.SetHealth(healthMultiplier * BOSS_HEALTH);
        transform.position += (Vector3)new Vector2(0, 10);
        activeCoroutine = StartCoroutine("ChangeState", BossStates.Intro);
    }

    private void ChangeState(BossStates state)
    {
        this._state = state;
        switch(state)
        {
            case BossStates.Intro:
            activeCoroutine = StartCoroutine(Intro());
            break;
            case BossStates.Swing:
            activeCoroutine = StartCoroutine(Swing());
            break;
            case BossStates.Return:

            break;
            case BossStates.Shoot:

            break;
            case BossStates.Charge:
            
            break; 
            case BossStates.Sweep:

            break;
            case BossStates.Spawn:

            break;
            case BossStates.Split:

            break;
        }
    }

    private IEnumerator Intro()
    {
        Vector2 currentPosition = transform.position;
        while(Vector2.Distance(currentPosition, startingPosition) > 0.05f)
        {
        //currentPosition = Vector2.MoveTowards(currentPosition, startingPosition, moveSpeed*Time.deltaTime);
        currentPosition = Vector2.Lerp(currentPosition, startingPosition, moveSpeed * Time.deltaTime);
        transform.position = currentPosition;
        yield return null;
        }
        Debug.Log(_state);
        activeCoroutine = StartCoroutine("ChangeState", BossStates.Swing);
    }

    private IEnumerator Swing()
    {
        int randomTime = Random.Range(3, 6);
        while(_state == BossStates.Swing)
        {
            transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
            CheckForScreenEdges();
            yield return null;
        }
        Debug.Log(_state);
    }

    //Changes direction when touching screen edges.
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= EDGEX && moveDir > 0)
        || (transform.position.x <= -EDGEX && moveDir < 0))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        moveDir *= -1;
    }


    private IEnumerator ReturnToSpawn()
    {
        _state = BossStates.Return;
        Vector2 currentPosition = transform.position;
        while(Vector2.Distance(currentPosition, startingPosition) > 0.05f)
        {
        //currentPosition = Vector2.MoveTowards(currentPosition, startingPosition, moveSpeed*Time.deltaTime);
        currentPosition = Vector2.Lerp(currentPosition, startingPosition, moveSpeed * Time.deltaTime);
        transform.position = currentPosition;
        yield return null;
        }
    Debug.Log(activeCoroutine);
    activeCoroutine = StartCoroutine(Swing());
    }
}
