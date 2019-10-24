using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCrossingControl : MonoBehaviour
{
    public ArrowUnit arrow;
    PlayerControl ball;
    BoxCollider collider;
    RoadControl roadCtrl;
    PlayerControl playerCtrl;

    // Start is called before the first frame update
    void Start()
    {
        ball = MainObjControl.Instant.playerCtrl;
        collider = GetComponent<BoxCollider>();
        roadCtrl = MainObjControl.Instant.roadCtrl;
        playerCtrl = MainObjControl.Instant.playerCtrl;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GuideBall());

        // Remove paths on the other direction
        GameDefine.Direction oppDir = arrow.GetOppositeDirection();
        RoadUnit oppRoad = arrow.road.GetNextByDirection(oppDir);
        roadCtrl.RemovePath(oppRoad, 1);

        // Update player
        GameDefine.Direction aheadDir = arrow.GetDirection();
        RoadUnit aheadRoad = arrow.road.GetNextByDirection(aheadDir);
        playerCtrl.currentRoad = aheadRoad;
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
