using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliders : MonoBehaviour
{
    void Start()
    {
        foreach (Transform c in transform)
        {
            c.gameObject.AddComponent<MeshCollider>();
        }
    }
}