//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/input system/playerAction.inputactions
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

public partial class @PlayerAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""playerAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""dfeee357-eacf-4454-9a10-382160582131"",
            ""actions"": [
                {
                    ""name"": ""Clicked"",
                    ""type"": ""Button"",
                    ""id"": ""84621980-9cfc-4655-a88f-bd964d86804f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""debugKey"",
                    ""type"": ""Button"",
                    ""id"": ""ba60707c-4f89-4a5a-ac1e-18d048296fc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d80a963c-3307-4b2b-b261-37e691e68123"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""179cfc0e-7239-4620-bfab-e825f7b93143"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""debugKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Clicked = m_Player.FindAction("Clicked", throwIfNotFound: true);
        m_Player_debugKey = m_Player.FindAction("debugKey", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Clicked;
    private readonly InputAction m_Player_debugKey;
    public struct PlayerActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Clicked => m_Wrapper.m_Player_Clicked;
        public InputAction @debugKey => m_Wrapper.m_Player_debugKey;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Clicked.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClicked;
                @Clicked.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClicked;
                @Clicked.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClicked;
                @debugKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugKey;
                @debugKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugKey;
                @debugKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugKey;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Clicked.started += instance.OnClicked;
                @Clicked.performed += instance.OnClicked;
                @Clicked.canceled += instance.OnClicked;
                @debugKey.started += instance.OnDebugKey;
                @debugKey.performed += instance.OnDebugKey;
                @debugKey.canceled += instance.OnDebugKey;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnClicked(InputAction.CallbackContext context);
        void OnDebugKey(InputAction.CallbackContext context);
    }
}
