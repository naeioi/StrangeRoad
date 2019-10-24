using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBlockCtrl : MonoBehaviour
{
    BlockUnit unit;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<BlockUnit>();
        player = MainObjControl.Instant.playerCtrl;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Boom effect
        MainObjControl.Instant.boomCtrl.ShowBoom(transform);
        unit.SetVisible(false);

        // Increment score
        if (unit.color == true || player.smashing)
            MainCanvas.Instance.inGameScript.IncrScore();
        else
        {
            // TODO: GameOver
            MainCanvas.Instance.lostScript.GameOver();
        }

        // Create future path
        MainObjControl.Instant.roadCtrl.ExtendPath(unit.road, 2);

        // TODO: Is recollection necessary?
        MainObjControl.Instant.blockCtrl.FreeBlock(unit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
