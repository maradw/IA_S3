using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum Entity { Child, Toy }
public class Toy : MonoBehaviour
{

    public Entity Entity;
    public Transform AimOffSet;
    public string type;
    public Color color;
    public int value;

    public void Initialize(string type, Color color, int value)
    {
        this.type = type;
        this.color = color;
        this.value = value;

        GetComponent<Renderer>().material.color = color;
    }
    private void Start()
    {
         if (AimOffSet == null)
        {
            AimOffSet = this.transform;
        }
    }
}
