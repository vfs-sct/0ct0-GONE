using UnityEngine;

public class PlacementVisualizer : MonoBehaviour
{
    [SerializeField] GameObject sphere = null;

    public void VisualizeArea()
    {
        sphere.SetActive(true);
    }

    public void DisableVisualizer()
    {
        sphere.SetActive(false);
    }
}
