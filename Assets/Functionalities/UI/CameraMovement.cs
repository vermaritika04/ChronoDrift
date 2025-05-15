//using UnityEngine;
//using System.Collections;

//public class CameraMovement : MonoBehaviour
//{
//    public Transform carTransform;
//    [SerializeField] private Transform[] fixedViews; // 0: Top, 1: Front, 2: Back
//    [SerializeField] private float rotationSpeed = 2f;
//    [SerializeField] private float zoomSpeed = 2f;
//    [SerializeField] private float minZoom = 3f;
//    [SerializeField] private float maxZoom = 10f;
//    [SerializeField] private float verticalSpeed = 2f;

//    private int currentViewIndex = -1; // Start with main view
//    private Transform currentFixedView;
//    private bool isEngineStarted = false;
//    private float rotationAngleY = 0f;
//    private float currentZoom;
//    private Vector3 cameraOffset;

//    void Start()
//    {
//        //if (playerCarTransform == null)
//        //{
//        //    Debug.LogError("CameraMovement: Player Car Transform is not assigned!");
//        //    return;
//        //}

//        SetCameraToMainView();
//    }

//    void LateUpdate()
//    {
//        if (!isEngineStarted)
//        {
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                isEngineStarted = true;
//                Debug.Log("Engine Started! Camera Controls Activated.");
//            }
//            return;
//        }

//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            CycleViews();
//        }

//        if (currentFixedView == null) // Only allow controls in main view
//        {
//            HandleMainCameraControls();
//        }
//    }

//    void HandleMainCameraControls()
//    {
//        // 1️⃣ Rotate Camera with Left Mouse Button
//        if (Input.GetMouseButton(0))
//        {
//            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
//            rotationAngleY += horizontalInput;
//        }

//        // 2️⃣ Move Camera Up/Down with Right Mouse Button
//        if (Input.GetMouseButton(1))
//        {
//            float verticalInput = -Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;
//            cameraOffset.y = Mathf.Clamp(cameraOffset.y + verticalInput, 1f, 5f);
//        }

//        // 3️⃣ Zoom In/Out with Scroll Wheel
//        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
//        currentZoom = Mathf.Clamp(currentZoom - scrollInput * zoomSpeed, minZoom, maxZoom);

//        // Apply updated rotation and position
//        Quaternion rotation = Quaternion.Euler(0, rotationAngleY, 0);
//        Vector3 zoomedOffset = cameraOffset.normalized * currentZoom;
//        transform.position = carTransform.position + rotation * zoomedOffset;
//        transform.LookAt(carTransform.position + Vector3.up * 1.5f);
//    }

//    void CycleViews()
//    {
//        currentViewIndex = (currentViewIndex + 1) % (fixedViews.Length + 1);

//        if (currentViewIndex == fixedViews.Length)
//        {
//            SetCameraToMainView(); // Return to default (Main) view
//        }
//        else
//        {
//            SwitchToView(fixedViews[currentViewIndex]);
//        }
//    }

//    void SwitchToView(Transform targetView)
//    {
//        currentFixedView = targetView;
//        transform.position = targetView.position;
//        transform.rotation = targetView.rotation;
//    }

//    void SetCameraToMainView()
//    {
//        currentFixedView = null;
//        cameraOffset = new Vector3(0, 3, -5);
//        currentZoom = Mathf.Clamp(cameraOffset.magnitude, minZoom, maxZoom);
//        rotationAngleY = carTransform.eulerAngles.y;
//    }
//}

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform carTransform;
    [SerializeField] private Transform[] fixedViews; // 0: Top, 1: Front, 2: Back
    [SerializeField] private float rotationSpeed = 0.5f; // Slower rotation for realism
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float verticalSpeed = 2f;

    private int currentViewIndex = -1; // Start with main view
    private Transform currentFixedView;
    private bool isEngineStarted = false;
    private float currentZoom;
    private Vector3 cameraOffset;

    void Start()
    {
        SetCameraToMainView();
    }

    void LateUpdate()
    {
        if (!isEngineStarted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isEngineStarted = true;
                Debug.Log("Engine Started! Camera Controls Activated.");
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CycleViews();
        }

        if (currentFixedView == null) // Only allow controls in main view
        {
            HandleMainCameraControls();
        }
    }

    void HandleMainCameraControls()
    {
        // Update the camera's position based on the car's position and rotation
        Vector3 desiredPosition = carTransform.position + carTransform.TransformDirection(cameraOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f); // Smoothly interpolate to the desired position

        // Rotate the camera to look at the car
        transform.LookAt(carTransform.position + Vector3.up * 1.5f);

        // 1️⃣ Rotate Camera with Left Mouse Button
        if (Input.GetMouseButton(0))
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            cameraOffset = Quaternion.Euler(0, horizontalInput, 0) * cameraOffset; // Rotate the offset
        }

        // 2️⃣ Move Camera Up/Down with Right Mouse Button
        if (Input.GetMouseButton(1))
        {
            float verticalInput = -Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;
            cameraOffset.y = Mathf.Clamp(cameraOffset.y + verticalInput, 1f, 5f);
        }

        // 3️⃣ Zoom In/Out with Scroll Wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom = Mathf.Clamp(currentZoom - scrollInput * zoomSpeed, minZoom, maxZoom);
        cameraOffset = cameraOffset.normalized * currentZoom; // Adjust the camera offset based on zoom
    }

    void CycleViews()
    {
        currentViewIndex = (currentViewIndex + 1) % (fixedViews.Length + 1);

        if (currentViewIndex == fixedViews.Length)
        {
            SetCameraToMainView(); // Return to default (Main) view
        }
        else
        {
            SwitchToView(fixedViews[currentViewIndex]);
        }
    }

    void SwitchToView(Transform targetView)
    {
        currentFixedView = targetView;
        transform.position = targetView.position;
        transform.rotation = targetView.rotation;
    }

    void SetCameraToMainView()
    {
        currentFixedView = null;
        cameraOffset = new Vector3(0, 3, -5); // Set the initial offset
        currentZoom = cameraOffset.magnitude; // Set the current zoom based on the offset
    }
}