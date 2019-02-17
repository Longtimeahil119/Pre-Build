using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public bool open = false;

    public float doorOpenAngle = 90f;
    public float doorClosedAngle = 0f;

    public float smooth = 2f;

    public void ChangeDoorState()
    {
        open = !open;
    }

    private void Update()
    {
        if (open)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(0, doorClosedAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
    }

}
