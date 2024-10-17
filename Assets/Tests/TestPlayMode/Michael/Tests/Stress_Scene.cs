using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class Stress_Exits: MonoBehaviour
{   
    [UnityTest]
    public IEnumerator RapidSceneTransition()
    {
        float transitions = 10.0f;
        for (int i = 0; i < transitions; i++)
        {
            SceneManager.LoadScene("PlayerRoom");
            yield return new WaitForSeconds((float)(1.0 - i/transitions));
            SceneManager.LoadScene("Town");
            yield return new WaitForSeconds((float)(1.0 - i/transitions));

            Debug.Log("Loading Iteration:" + i + " Waited " + ((float)(1.0 - i/ transitions)) + " inbetween Transitions");
        }
    }
}
