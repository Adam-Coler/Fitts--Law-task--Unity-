using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
[DisallowMultipleComponent]
public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    public Material color2;
    public Material wall;
    private Material baseColor;
    

    void Start()
    {
        Debug.Log("Test script loaded");

        Renderer rend = this.gameObject.GetComponent<Renderer>();
        if (rend != null && color2 != null)
        {
            baseColor = rend.material;
            rend.material = color2;
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            Debug.Log(child.name);

            if (child.name.Contains("W"))
            {
                Renderer childRend = child.GetComponent<Renderer>();
                if (childRend != null && wall != null)
                {
                    childRend.material = wall;
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
