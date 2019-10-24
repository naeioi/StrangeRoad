using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCrossingControl : MonoBehaviour
{
    public ArrowUnit arrow;
    PlayerControl ball;
    BoxCollider collider;
    RoadControl roadCtrl;

    // Start is called before the first frame update
    void Start()
    {
        ball = MainObjControl.Instant.playerCtrl;
        collider = GetComponent<BoxCollider>();
        roadCtrl = MainObjControl.Instant.roadCtrl;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GuideBall());

        // Remove paths on the other direction
        GameDefine.Direction oppDirection = arrow.GetOppositeDirection();
        foreach (RoadUnit road in arrow.road.next)
            if (road.direction == oppDirection)
            {
                roadCtrl.RemovePath(road, 1);
                break;
            }
    }

    IEnumerator GuideBall()
    {
        // By here the ball will give into arrow.Direction()

        // Wait for the wall to roll to the center of the crossing
        // Because Collider has a volume
        float rollToCenterTime = (collider.transform.position - ball.transform.position).magnitude / ball.speed;
        yield return new WaitForSeconds(rollToCenterTime);

        // Change ball direction
        GameDefine.Direction direction = arrow.GetDirection();
        ball.SetDirection(direction);        
    }
}
