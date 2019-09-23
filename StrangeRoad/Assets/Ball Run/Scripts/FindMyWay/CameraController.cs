using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float speed;
    Vector3 direct;
    Vector3 target;

    Transform playerTrans;
    public bool allow;

    void Start()
    {
//        playerTrans = MainObjControl.Instant.playerCrt.transform;
        direct = transform.position - new Vector3(0, 2.5f, 0);
        target = direct + new Vector3(0, 0, 10);
    }

    public void NewTarget(Vector3 newTarget)
    {
        target = newTarget + direct;
    }


    void Update()
    {
        if (allow)
        {
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }
    }
}
