using UnityEngine;

public class CameraV1 : MonoBehaviour
{
    public GameObject target;
    public float xRotateSpeed = 5;
    public float yRotateSpeed = 3;

    Vector3 offset;

    void Start()
    {
        offset = new Vector3(target.transform.position.x, target.transform.position.y - 4.0f, target.transform.position.z + 5.0f);
    }

    void LateUpdate()
    {
        // Rotate the target (player) using the mouse
        float horizontal = Input.GetAxis("Mouse X") * xRotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        //float yRotation = Input.GetAxis("Mouse Y") * yRotateSpeed;
        //offset = Quaternion.AngleAxis(yRotation, Vector3.left) * offset;
        //transform.Rotate(yRotation, 0, 0);


        // Rotate the camera to look in the same direction as the player at the correct offset
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}