// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""bf2f2042-8e4e-48f4-b056-f44e199fcf09"",
            ""actions"": [
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""fca1faf3-51bb-48de-9482-bb67c43cedf5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Button"",
                    ""id"": ""0203a786-bfd7-4a1e-9c0b-5a2226c438e6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Button"",
                    ""id"": ""7412f3a5-5286-4bc8-afad-381d868288a7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""778154ab-3f0e-4505-b85e-af53f610a4ef"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a467a32-68d1-4082-82fa-e6f411c4545f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cb2d93d-34b2-46c6-b7ef-53d15c51a720"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_X = m_ActionMap.FindAction("X", throwIfNotFound: true);
        m_ActionMap_LeftStick = m_ActionMap.FindAction("LeftStick", throwIfNotFound: true);
        m_ActionMap_RightStick = m_ActionMap.FindAction("RightStick", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private IActionMapActions m_ActionMapActionsCallbackInterface;
    private readonly InputAction m_ActionMap_X;
    private readonly InputAction m_ActionMap_LeftStick;
    private readonly InputAction m_ActionMap_RightStick;
    public struct ActionMapActions
    {
        private @InputControls m_Wrapper;
        public ActionMapActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @X => m_Wrapper.m_ActionMap_X;
        public InputAction @LeftStick => m_Wrapper.m_ActionMap_LeftStick;
        public InputAction @RightStick => m_Wrapper.m_ActionMap_RightStick;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @X.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnX;
                @LeftStick.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLeftStick;
                @RightStick.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightStick;
                @RightStick.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightStick;
                @RightStick.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnRightStick;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @RightStick.started += instance.OnRightStick;
                @RightStick.performed += instance.OnRightStick;
                @RightStick.canceled += instance.OnRightStick;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    public interface IActionMapActions
    {
        void OnX(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
    }
}
