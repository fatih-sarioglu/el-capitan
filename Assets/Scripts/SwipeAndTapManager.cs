using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeAndTapManager : MonoBehaviour
{
    private CharacterControlActions characterControlActions;

    #region Events
    public delegate void StartTouch(Vector3 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector3 position, float time);
    public event EndTouch OnEndTouch;
    public delegate void Tap(bool isTapped);
    public event Tap OnTap;
    #endregion

    private Camera mainCamera;

    private void Awake()
    {
        characterControlActions = new CharacterControlActions();

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        characterControlActions.Enable();
    }

    private void OnDisable()
    {
        characterControlActions.Disable();
    }

    void Start()
    {
        characterControlActions.Touch.PrimaryContact.started += ctx => StartPrimaryTouch(ctx);
        characterControlActions.Touch.PrimaryContact.canceled += ctx => EndPrimaryTouch(ctx);
        characterControlActions.Touch.TapAction.started += ctx => Tapped(ctx);
    }

    private void StartPrimaryTouch(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(Utils.ScreenToWorld(mainCamera, characterControlActions.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndPrimaryTouch(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(Utils.ScreenToWorld(mainCamera, characterControlActions.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    private void Tapped(InputAction.CallbackContext context)
    {

        OnTap?.Invoke(context.started);
    }
}
