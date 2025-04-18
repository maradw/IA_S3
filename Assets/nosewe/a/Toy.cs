using UnityEngine;

public class Toy : MonoBehaviour
{
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
}
