using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBlockCtrl : MonoBehaviour
{
    public enum BlockType { Barrier, Bonus };
    public BlockType blockType;
    public bool doExtendRoad;

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
        Transform boomTrans = transform;
        boomTrans.eulerAngles =
            unit.direction == GameDefine.Direction.Forward ? Vector3.zero :
            unit.direction == GameDefine.Direction.Left ? new Vector3(0, 90f, 0) : new Vector3(0, -90f, 0);
        MainObjControl.Instant.boomCtrl.ShowBoom(boomTrans);
        unit.SetVisible(false);

        // Increment score
        if (blockType == BlockType.Bonus || player.smashing)
            MainCanvas.Instance.inGameScript.IncrScore();
        else
        {
            // TODO: GameOver
            MainCanvas.Instance.lostScript.GameOver();
        }

        if (doExtendRoad)
        {
            // Create future path
            MainObjControl.Instant.roadCtrl.ExtendPath(unit.road, 2);

            // TODO: Is recollection necessary?
            MainObjControl.Instant.blockCtrl.FreeBlock(unit);

            player.levelCounter += 1;
            if (player.levelCounter == 10)
                player.levelUp();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
