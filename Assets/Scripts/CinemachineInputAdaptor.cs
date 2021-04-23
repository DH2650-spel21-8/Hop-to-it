using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class CinemachineInputAdaptor : MonoBehaviour
{
    private CinemachineFreeLook _cam;
    
    private void Start()
    {
        if (TryGetComponent(out CinemachineFreeLook cam))
        {
            _cam = cam;
        }
        else
        {
            GracefulExit("Missing CinemachineFreeLook component", this);
        }
    }

    public void OnInput(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        _cam.m_XAxis.m_InputAxisValue = value.x;
        _cam.m_YAxis.m_InputAxisValue = value.y;
    }
    private void GracefulExit(string message, Object context = null)
    {
        Debug.LogWarning(message, context);
        enabled = false;
    }
}
