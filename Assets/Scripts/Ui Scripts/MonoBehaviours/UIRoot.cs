using UnityEngine;

public class UIRoot : MonoBehaviour
{
    //grab a screen by the component - so if the name of a screen changes the code doesn't break
    public T GetScreen<T>() where T : MonoBehaviour
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.TryGetComponent<T>(out var screen))
            {
                return screen;
            }
        }

        throw new System.Exception($"Could not find screen of type {typeof(T).Name}");
    }

    //use a string to get the screen
    public GameObject GetScreen(string screenName)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.name == screenName)
            {
                return child.gameObject;
            }
        }

        throw new System.Exception($"Could not find screen with name {screenName}");
    }
}
