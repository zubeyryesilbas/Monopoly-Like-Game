using UnityEngine;

public class RandomColorHSV : MonoBehaviour
{
    private void Start()
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            // Make sure we're not affecting shared materials
            Material newMat = new Material(renderer.material);
            newMat.color = Random.ColorHSV(); // You can use custom ranges here if you want
            renderer.material = newMat;
        }
    }
}