using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level : MonoBehaviour
{
    public void lv1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void lv2()
    {
        SceneManager.LoadSceneAsync(2);

    }
    public void lv3()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
