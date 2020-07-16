using UnityEngine;

[CreateAssetMenu(menuName = "Systems/Satellites/Create Satellite")]
public class Satellite : ScriptableObject
{
    public Sprite satIcon;
    public GameObject PlacePrefab;   
    public GameObject PreviewPrefab;
}
