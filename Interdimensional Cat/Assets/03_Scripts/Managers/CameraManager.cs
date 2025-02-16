using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineCamera[] allCinemachineCameras;

    [SerializeField] private float fallPanAmount = 0.25f;
    [SerializeField] private float fallYPanTime = 0.35f;
    public float fallSpeedYDampingChangeThreshold = -15f;

    public bool isLerpingYDamping { get; private set; }
    public bool lerpedFromPlayerFalling { get; set; }

    private Coroutine lerpYPanCoroutine;

    private CinemachineCamera currentCamera;
    private CinemachinePositionComposer positionComposer;

    private float normYPanAmount;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < allCinemachineCameras.Length; i++)
        {
            if (allCinemachineCameras[i].enabled)
            {
                currentCamera = allCinemachineCameras[i];

                positionComposer = currentCamera.GetComponent<CinemachinePositionComposer>();
            }
        }

        normYPanAmount = positionComposer.Damping.y;

    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        isLerpingYDamping = true;

        float startDampAmount = positionComposer.Damping.y;
        float endDampAmount = 0;

        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            lerpedFromPlayerFalling = true;
        } else
        {
            endDampAmount = normYPanAmount;
        }

        float elapsedTime = 0f;
        while (elapsedTime < fallYPanTime)
        {

            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallYPanTime));
            positionComposer.Damping.y = lerpedPanAmount;

            yield return null;
        }

        isLerpingYDamping = false;
    }
}
