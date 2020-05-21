using UnityEngine;
using UnityEngine.InputSystem;

public class CameraComponent : MonoBehaviour
{
    [Header("Camera Control")]
    [SerializeField]
    public GameFrameworkManager GameManager;
    [SerializeField]
    private Transform _CamPivot = null;
    [SerializeField]
    private Transform _MainCamera = null;
    [SerializeField]
    private float _MaxZoom = 1.7f;
    [SerializeField]
    private float _MinZoom = -3f;
    [SerializeField]
    private float _ZoomSpeed = 2f;
    [SerializeField]
    private float _CameraZoomEase = 3f;
    [SerializeField]
    private float _CameraSpeed = 0.3f;

    private float _ZoomTarget;
    private Vector2 mouseAxis;
    private float zoomAmount;

    public Transform GetCamera()
    {
        return _MainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPaused)
        {
            return;
        }

        UpdateZoom();
        UpdateRotations();
    }

    //OnZoom is the scroll wheel input from the input system
    void OnZoom(InputValue input)
    {
        zoomAmount += input.Get<Vector2>().y;
    }

    void UpdateZoom()
    {
        //grab the current position of the camera
        var ZAxis = _CamPivot.localPosition.z;

        //read scrollwheel input and give it our desired zoom speed
        ZAxis += zoomAmount * _ZoomSpeed / 120f;

        zoomAmount = 0;

        //clamp: if the value create above falls outside of our maximum or our minimum, force it to change
        if (ZAxis < _MinZoom) //minzoom is -3 by default
        {
            ZAxis = _MinZoom;
        }
        else if (ZAxis > _MaxZoom) //maxzoom is 1.7 by default
        {
            ZAxis = _MaxZoom;
        }

        //our new target to lerp to
        _ZoomTarget = ZAxis;

        //update camera with the new position values, lerp
        var newCamPos = _CamPivot.localPosition;
        newCamPos.z = Mathf.Lerp(newCamPos.z, _ZoomTarget, _CameraZoomEase * Time.deltaTime);
        _CamPivot.localPosition = newCamPos;

        var playerToCam = _MainCamera.transform.position - transform.position;

        //TODO the collision's v. bad
        foreach (var hit in Physics.RaycastAll(transform.position, playerToCam.normalized, playerToCam.magnitude))
        {
            if (hit.collider.gameObject != gameObject)
            {
                //Debug: uncomment to see what the camera is hitting
                //Debug.Log(hit.collider.name);
                playerToCam = playerToCam.normalized;
                _CamPivot.transform.position = hit.point;
                break;
            }
        }
    }

    void OnLook(InputValue value)
    {
        mouseAxis = value.Get<Vector2>();
    }

    //TODO: use vectors to make looking in a direction (more dramatically) change your rotation (with lerp delay)
    void UpdateRotations()
    {
        //read mouse inputs and feed them into the camera rotation
        var mouseX = mouseAxis.x;
        var mouseY = mouseAxis.y;
        //rotate character, multiplied by cam speed. Should be kept slow-ish for a space-y feel
        transform.Rotate(-mouseY * _CameraSpeed, mouseX * _CameraSpeed, 0f);
        //rotate camera
        _CamPivot.Rotate(-mouseY * _CameraSpeed, 0f, 0f);

        //grab the current rotation of the camera (after above input is applied)
        var pitch = _CamPivot.eulerAngles.x;

        //clamp how high/low character can look. Possibly not necessary in space if we want the char to do a vertical 360 just by looking upward/downward?
        //if (pitch > 40 && pitch < 180)
        //{
        //    pitch = 40;
        //}
        //else if (pitch < 325 && pitch > 180)
        //{
        //    pitch = 325;
        //}

        //store new pitch in a variable, because you can't change just the X position on the camera directly
        //var euler_angles = _CamPivot.eulerAngles;
        //euler_angles.x = pitch;

        //update camera rotation with new value
        //_CamPivot.eulerAngles = euler_angles;

        //uncomment to see numerical value of pitch angle change in real time
        //Debug.Log(pitch);
    }
}

