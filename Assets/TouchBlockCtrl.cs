using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBlockCtrl : MonoBehaviour
{
    BlockUnit unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<BlockUnit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        MainObjControl.Instant.boomCtrl.ShowBoom(transform);
        MainObjControl.Instant.blockCtrl.FreeBlock(unit);
        unit.SetVisible(false);

        MainObjControl.Instant.roadCtrl.ExtendPath(unit.road, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
