using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class Stress_Exits: MonoBehaviour
{   
    [UnityTest]
    public IEnumerator RapidSceneTransition()
    {
        int transitions = 50;
        for (int i = 0; i < transitions; i++)
        {
            SceneManager.LoadScene("PlayerRoom");
            yield return new WaitForSeconds((float)(1- transitions/50));
            SceneManager.LoadScene("Town");
            yield return new WaitForSeconds((float)(1 - transitions / 50));
            Debug.Log("Loading Iteration:" + i);
        }
    }
}
