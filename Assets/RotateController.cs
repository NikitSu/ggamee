using UnityEngine;

public class RotateLayers : MonoBehaviour
{
    public Transform outerLayer;
    public Transform innerLayer;
    public float outerSpeed = 20f;
    public float innerSpeed = 20f;

    void Update()
    {
        if (outerLayer != null)
        {
            outerLayer.Rotate(0, 0, outerSpeed * Time.deltaTime);
        }

        if (innerLayer != null)
        {
            innerLayer.Rotate(0, 0, -innerSpeed * Time.deltaTime);
        }
    }
}
