//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Scripts/InputController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputController"",
    ""maps"": [
        {
            ""name"": ""playerMovement"",
            ""id"": ""8dce818c-9107-45ca-917e-dd1dfc35ad01"",
            ""actions"": [
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""7865ab40-5406-4d25-a05c-e1db03fcfafb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""42f70c64-60ab-45cb-80ad-b37cc6c0391f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""51a35287-f03a-4de2-9dd5-2a650d055af6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""cef8e574-3a5a-4c9a-9768-380b416f6616"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AimTarget"",
                    ""type"": ""Button"",
                    ""id"": ""d4510f71-5713-44d9-9079-3d0fbef91294"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StopAimTarget"",
                    ""type"": ""Button"",
                    ""id"": ""a31ce334-f4ef-49b4-a55a-69f4b7f655ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1211c2a-5af1-4e42-a731-9149e570da7a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f12a0c02-cdc2-4e4f-a73c-ddf61c1de557"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""462497d1-0125-48b8-95d2-06a71786ab9b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c0a3466-bc5c-43e1-b9c8-dbcb7c3f326e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2af360e0-c4ce-4551-ae93-f96d5296feb3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6113b90-c076-414f-9580-2e8a4aef140c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopAimTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // playerMovement
        m_playerMovement = asset.FindActionMap("playerMovement", throwIfNotFound: true);
        m_playerMovement_MoveUp = m_playerMovement.FindAction("MoveUp", throwIfNotFound: true);
        m_playerMovement_MoveDown = m_playerMovement.FindAction("MoveDown", throwIfNotFound: true);
        m_playerMovement_MoveRight = m_playerMovement.FindAction("MoveRight", throwIfNotFound: true);
        m_playerMovement_MoveLeft = m_playerMovement.FindAction("MoveLeft", throwIfNotFound: true);
        m_playerMovement_AimTarget = m_playerMovement.FindAction("AimTarget", throwIfNotFound: true);
        m_playerMovement_StopAimTarget = m_playerMovement.FindAction("StopAimTarget", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // playerMovement
    private readonly InputActionMap m_playerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_playerMovement_MoveUp;
    private readonly InputAction m_playerMovement_MoveDown;
    private readonly InputAction m_playerMovement_MoveRight;
    private readonly InputAction m_playerMovement_MoveLeft;
    private readonly InputAction m_playerMovement_AimTarget;
    private readonly InputAction m_playerMovement_StopAimTarget;
    public struct PlayerMovementActions
    {
        private @InputController m_Wrapper;
        public PlayerMovementActions(@InputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveUp => m_Wrapper.m_playerMovement_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_playerMovement_MoveDown;
        public InputAction @MoveRight => m_Wrapper.m_playerMovement_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_playerMovement_MoveLeft;
        public InputAction @AimTarget => m_Wrapper.m_playerMovement_AimTarget;
        public InputAction @StopAimTarget => m_Wrapper.m_playerMovement_StopAimTarget;
        public InputActionMap Get() { return m_Wrapper.m_playerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @MoveUp.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveUp;
                @MoveDown.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveDown;
                @MoveRight.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMoveLeft;
                @AimTarget.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimTarget;
                @AimTarget.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimTarget;
                @AimTarget.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimTarget;
                @StopAimTarget.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnStopAimTarget;
                @StopAimTarget.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnStopAimTarget;
                @StopAimTarget.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnStopAimTarget;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @AimTarget.started += instance.OnAimTarget;
                @AimTarget.performed += instance.OnAimTarget;
                @AimTarget.canceled += instance.OnAimTarget;
                @StopAimTarget.started += instance.OnStopAimTarget;
                @StopAimTarget.performed += instance.OnStopAimTarget;
                @StopAimTarget.canceled += instance.OnStopAimTarget;
            }
        }
    }
    public PlayerMovementActions @playerMovement => new PlayerMovementActions(this);
    public interface IPlayerMovementActions
    {
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnAimTarget(InputAction.CallbackContext context);
        void OnStopAimTarget(InputAction.CallbackContext context);
    }
}