using UnityEngine;
using Cinemachine;

public class CameraToggle : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPerson;
    public CinemachineVirtualCamera firstPerson;
    public CinemachineVirtualCamera rearCamera;

    private bool firstPersonActive = false;
    private bool rearViewActive = false;

    void Start()
    {
        // starts with the third person cam
        thirdPerson.Priority = 10;
        firstPerson.Priority = 0;
        rearCamera.Priority = 0;
    }

    void Update()
    {
        // switch between third and first
        if (Input.GetKeyDown(KeyCode.T) && !rearViewActive)
        {
            firstPersonActive = !firstPersonActive;

            if (firstPersonActive)
            {
                firstPerson.Priority = 10;
                thirdPerson.Priority = 0;
            }
            else
            {
                thirdPerson.Priority = 10;
                firstPerson.Priority = 0;
            }
        }

        // toggle if the b button is pressed to the rear cam
        if (Input.GetKeyDown(KeyCode.B))
        {
            rearViewActive = !rearViewActive;

            if (rearViewActive)
            {
                rearCamera.Priority = 20;
                firstPerson.Priority = 0;
                thirdPerson.Priority = 0;
            }
            else
            {
                rearCamera.Priority = 0;

                if (firstPersonActive)
                {
                    firstPerson.Priority = 10;
                    thirdPerson.Priority = 0;
                }
                else
                {
                    thirdPerson.Priority = 10;
                    firstPerson.Priority = 0;
                }
            }
        }
    }
}