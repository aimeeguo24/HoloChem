using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOfObj : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.material.color = new Color(255,0,0,1);
        }
    }
}
