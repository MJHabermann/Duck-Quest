using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneswitch : MonoBehaviour
{
    public void SceneChanger(string entranceFlag)
    {
        SceneManager.LoadScene(entranceFlag);

    }
}
