using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainScene");
    }
	

}
