using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void DestroyObject()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed Some Object");
    }
}
