using UnityEngine;

public class CursorInvisible : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }
}
