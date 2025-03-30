using System.Collections;
using UnityEngine;

public class Missile : Bullet
{
    private bool hasTarget = false;

    protected override void Start()
    {
        base.Start();
        StartCoroutine("CheckRaycast");
    }

    protected override void Update()
    {
        if(!hasTarget)
        {
            base.Update();
        }
    }

    private IEnumerator CheckRaycast(){
        RaycastHit2D hit = new();
        while(hit.collider == null)
        {
            hit = Physics2D.BoxCast(transform.position, new Vector2(3, 10), 5, transform.up, 0, LayerMask.GetMask("Enemy"));
            yield return new WaitForSeconds(0.3f);
        }
        hasTarget = true;
        StartCoroutine("MoveTowardsTarget", hit.collider.transform);
    }

    //Needs to check if target is dead. If it is, go to the last stored position and explode.
    private IEnumerator MoveTowardsTarget(Transform target)
    {
        while(Vector2.Distance(this.transform.position, target.position) > 0.05f)
        {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        yield return null;
        }
    }
}
