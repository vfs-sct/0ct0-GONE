using UnityEngine;
using UnityEngine.UI;

public class OffscreenIndicator : MonoBehaviour
{
    [SerializeField] private GameObject spaceStation = null;
    [SerializeField] private Image indicator = null;
    [SerializeField] private Image shipHealthBar = null;

    private int skipFrame = 5;

    //uncomment if changing ship health bar to worldspace instead of camera overlay
    //private void Start()
    //{
    //    shipHealthBar.gameObject.transform.position = spaceStation.transform.position;
    //}

    // Update is called once per frame
    void Update()
    {
        UpdateShipHUD();
    }

    public void UpdateShipHUD()
    {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(spaceStation.transform.position);

        shipHealthBar.gameObject.transform.position = spaceStation.transform.position;

        if (screenpos.z > 0 &&
            screenpos.x > 0 &&
            screenpos.x < Screen.width &&
            screenpos.y > 0 &&
            screenpos.y < Screen.height)
        {
            if (!shipHealthBar.gameObject.activeSelf)
            {
                shipHealthBar.gameObject.SetActive(true);
                indicator.gameObject.SetActive(false);
            }
            shipHealthBar.transform.position = screenpos;
        }
        else
        {
            if (shipHealthBar.gameObject.activeSelf)
            {
                shipHealthBar.gameObject.SetActive(false);
                indicator.gameObject.SetActive(true);
            }

            indicator.gameObject.SetActive(true);

            if (screenpos.z < 0)
            {
                screenpos *= -1;
            }

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            screenpos -= screenCenter;

            float angle = Mathf.Atan2(screenpos.y, screenpos.x);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenpos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            //check up/down
            if (cos > 0)
            {
                screenpos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
            }
            else
            {
                screenpos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
            }
            //if out of bounds, get correct side
            if (screenpos.x > screenBounds.x)
            {
                screenpos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
            }
            else if (screenpos.x < -screenBounds.x)
            {
                screenpos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
            }

            screenpos += screenCenter;

            indicator.transform.position = screenpos;
            indicator.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }
}
