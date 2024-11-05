using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

public class Stress_Exits: MonoBehaviour
{
    
    [UnityTest]
    public IEnumerator RapidSceneTransition()
    {
        int i = 0;
        float transitions = 10.0f;
        while(true)
        {
            SceneManager.LoadScene("PlayerRoom");
            yield return new WaitForSeconds((float)(1.0 - i/transitions));
            SceneManager.LoadScene("Town");
            yield return new WaitForSeconds((float)(1.0 - i/transitions));
            Debug.Log("Loading Iteration:" + i + " Waited " + ((float)(1.0 - i/ transitions)) + " inbetween Transitions");
            i++;
            yield return null;
            if (i > transitions)
            {
                break;
            }
        }
    }
}
