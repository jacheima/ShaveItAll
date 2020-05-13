using UnityEngine;


public class CameraController : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform piviotTransform;

    //stores the ongoing rotation of the camera
    private Vector3 localRotation;

    //the distance the camera will maintain from the object
    public float distance;

    public float mouseSensitivity;
    public float scrollSensitivity;

    //controls how long it takes the camera to get to the new position
    public float orbitDampening;
    public float scrollDampening;

    //disable camera controls
    public bool cameraDisable = true;

    private void Start()
    {
        cameraTransform = this.transform;
        piviotTransform = this.transform.parent;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cameraDisable = false;
        }

        if(Input.GetMouseButtonUp(1))
        {
            cameraDisable = true;
        }

        if (!cameraDisable)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                //clamp the y rotatiton
                localRotation.y = Mathf.Clamp(localRotation.y, -20f, 90f);
               
            }
        }

        //scrolling input from the mouse scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

            //makes the scroll more drastic when you are zoomed out
            //makes the scroll more subtle when you are zoomed in
            scrollAmount *= (distance * 0.3f);

            distance += scrollAmount * -1;

            //the camera is not able to go closer than 5m and no further than 100m
            distance = Mathf.Clamp(distance, 5f, 100f);
        }

        //Setting the camera transformations
        Quaternion quaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        piviotTransform.rotation = Quaternion.Lerp(piviotTransform.rotation, quaternion, Time.deltaTime * orbitDampening);

        if(cameraTransform.localPosition.z != distance * -1f)
        {
            cameraTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(cameraTransform.localPosition.z, distance * -1, Time.deltaTime * scrollDampening));

        }

    }
}
