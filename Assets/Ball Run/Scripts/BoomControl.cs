using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoomControl : MonoBehaviour
{
    public GameObject boomPrefab;
    public List<GameObject> listBoom;

    public GameObject GetBoom()
    {
        GameObject newBoom;
        if (listBoom.Count == 0)
        {
            newBoom = Instantiate(boomPrefab);
        }
        else
        {
            newBoom = listBoom[listBoom.Count - 1];
            listBoom.RemoveAt(listBoom.Count - 1);
        }
        return newBoom;
    }

    public void SetBoom(GameObject boom)
    {
        listBoom.Add(boom);
    }

    public void ShowBoom(Transform trans)
    {
        GameObject bom = GetBoom();
        bom.GetComponent<OrangeMest>().Boom(trans);
    }

    public void ShowPlayerBoom(bool isLeft, Material blockMat)
    {
        GameObject bom = GetBoom();
        bom.GetComponent<OrangeMest>().PlayerBoom(isLeft, blockMat);
    }

}
