// GENERATED AUTOMATICALLY FROM 'Assets/Input System/OctoGoneControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @OctoGoneControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @OctoGoneControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""OctoGoneControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e6e0f1cd-b42c-43f4-af55-6e9503652273"",
            ""actions"": [
                {
                    ""name"": ""HorizontalTranslate"",
                    ""type"": ""Value"",
                    ""id"": ""36140bf7-350c-4c3a-8a75-2712def37ea5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CraftHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""1cbd281f-f5ef-46fd-92e4-f4d80b105341"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RefuelHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""6b1b0ad6-0daa-4f5d-b77c-ff3031345272"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NaniteHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""bb8ee8f4-9c75-4032-a528-26d28a9ab5cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RepairScreenHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""ef8498aa-3188-4acb-9d73-a110d8d2dd82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VerticalTranslate"",
                    ""type"": ""Value"",
                    ""id"": ""5ca766d4-8d7a-4b76-80e1-5d18a0713ceb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3547c4a0-9251-4fc5-be42-d8361d6deb46"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectTool1"",
                    ""type"": ""Button"",
                    ""id"": ""7617e1fd-e8ce-44e1-8191-d7038b038524"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectTool2"",
                    ""type"": ""Button"",
                    ""id"": ""de729249-7347-46b6-8270-ea4e5d473062"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectTool3"",
                    ""type"": ""Button"",
                    ""id"": ""4d5faf53-315b-424f-a565-22f20e56a5b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectTool4"",
                    ""type"": ""Button"",
                    ""id"": ""f61765b4-67a0-4cbb-8851-9012b74799e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectTool5"",
                    ""type"": ""Button"",
                    ""id"": ""a59cb421-381a-4b6e-928b-0c3832fe4a6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActivateTool"",
                    ""type"": ""Button"",
                    ""id"": ""bec71fa9-280b-41ea-94c3-79f0f8f7bb9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.5,behavior=2)""
                },
                {
                    ""name"": ""DeactivateTool"",
                    ""type"": ""Button"",
                    ""id"": ""87aa9818-cbb1-48dc-ad69-947041970967"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""e4ac7d6a-38a8-4657-bad9-0fa3e74ef490"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScanSalvage"",
                    ""type"": ""Button"",
                    ""id"": ""e5436b97-2b24-47a3-b3ef-081fdb927c0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Value"",
                    ""id"": ""db646e0b-05ba-4cb7-ad5f-26957fb2f818"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InventoryHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""50d85bad-425f-42dd-8df9-51fe118f560b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""db377cda-cbc0-4855-816a-8afe25fb9220"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1f7a91b-d0fd-4a62-997e-7fb9b69bf235"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c8e490b-c610-4785-884f-f04217b23ca4"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse;Touch"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e5f5442-8668-4b27-a940-df99bad7e831"",
                    ""path"": ""<Joystick>/{Hatswitch}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6f888016-df4e-4294-8cf3-adb6faf53d2e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalTranslate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""90e52cb5-b689-4717-af9c-2dc6d79f5f72"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""HorizontalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b33ae2e2-d774-4eee-94b2-9c9cc00dd360"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""HorizontalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""96db03e8-ccef-47da-8d6b-0124f51bfa68"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""HorizontalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9ba00599-0512-445f-bda3-88bc04559e49"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""HorizontalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Space-Shift"",
                    ""id"": ""dcc4fe66-fbe7-43c6-bdd3-f2cefade125a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalTranslate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3f7b3753-5827-4c9a-bf57-5de6f2c45fb4"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""VerticalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""68350635-07dd-4d2f-80e0-dd2987bb94b4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""VerticalTranslate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c378334c-af18-4692-9cd9-acfdc8399095"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectTool2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1453139b-c650-40b8-9db7-dcc7530f9ba6"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectTool3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d707538b-12dd-4b9e-8519-39b8c37d47fe"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectTool4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d39665f8-b9ac-409f-b228-2b0dd108dc92"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectTool5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ad3f37e-fa9e-4c69-95b7-a9c2627e3a11"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SelectTool1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87894c10-168c-4a6e-91a0-c0940ff4e2b2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateTool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e38d6fa-8785-491f-a4e9-db125b722c26"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeactivateTool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""375ec6cb-e843-4f0d-99fd-297e3aef2bcb"",
                    ""path"": ""<Pointer>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Touch"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59df1274-0b5d-4712-bfa8-c3a26af5219f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScanSalvage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6248be99-a288-49d1-a2d1-e38d8a88d075"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""92dec995-c63b-406f-8dcc-945750e16101"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""972c07ff-bbef-4651-99b9-2a21628f3c11"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""49e5c688-ff45-4f00-a8ab-85ef27c1f134"",
                    ""path"": ""<Keyboard>/C"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""CraftHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beed9d93-3163-47f5-a7da-8e1d5d30ffec"",
                    ""path"": ""<Keyboard>/F"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RefuelHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99bdcb14-9f70-4871-89c6-3e0314dc2cc7"",
                    ""path"": ""<Keyboard>/V"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""InventoryHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee412187-1354-4d2b-844e-82375ca0317f"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84f07d21-f0ad-4c28-99a0-d77dd3fbe034"",
                    ""path"": ""<Keyboard>/R"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RepairScreenHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca97dd01-0c19-492b-86f9-a001df78f6a1"",
                    ""path"": ""<Keyboard>/T"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""NaniteHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3cf7a443-ba34-4a3b-883b-4eeac954fca3"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""6ba5cd02-b83e-4b3f-849d-fc60784de81f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CraftHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""034a7873-6505-479d-9dc2-97484d37fbe0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RepairScreenHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""27ae20cf-20ed-4d9e-91eb-d95149cbca17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""6be71688-cbc0-433c-8fbd-69273d7c0d5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""cd684b73-5d50-43ed-b8d3-7f8d71f28809"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""62c8fbfd-51a3-4e80-98c7-1707f3957c5b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f2ecc7bc-cda1-4663-872d-4b8dc358201c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0d63b5f5-6d68-405d-b620-9c58541cc149"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""65016869-16f4-404d-a569-b000c4969a16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""404c76be-69d7-4cf5-a143-6407dd49a583"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a241dfc5-614c-4e4c-b70b-c95207d0722e"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bcb3e040-c195-46d7-9170-4c8f83ba2bf3"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""45bbc98d-d3f9-40dc-8935-6d046ff847d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Debug"",
                    ""type"": ""Button"",
                    ""id"": ""63319c69-2f7a-4b8b-8518-229419aaccf5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InventoryHotkey"",
                    ""type"": ""Button"",
                    ""id"": ""a501e0fb-193e-4dd9-b961-8f70cf3168a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""809f371f-c5e2-4e7a-83a1-d867598f40dd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9144cbe6-05e1-4687-a6d7-24f99d23dd81"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2db08d65-c5fb-421b-983f-c71163608d67"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""58748904-2ea9-4a80-8579-b500e6a76df8"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8ba04515-75aa-45de-966d-393d9bbd1c14"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""712e721c-bdfb-4b23-a86c-a0d9fcfea921"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fcd248ae-a788-4676-a12e-f4d81205600b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1f04d9bc-c50b-41a1-bfcc-afb75475ec20"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb8277d4-c5cd-4663-9dc7-ee3f0b506d90"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""e25d9774-381c-4a61-b47c-7b6b299ad9f9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3db53b26-6601-41be-9887-63ac74e79d19"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0cb3e13e-3d90-4178-8ae6-d9c5501d653f"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0392d399-f6dd-4c82-8062-c1e9c0d34835"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""942a66d9-d42f-43d6-8d70-ecb4ba5363bc"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ff527021-f211-4c02-933e-5976594c46ed"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""563fbfdd-0f09-408d-aa75-8642c4f08ef0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eb480147-c587-4a33-85ed-eb0ab9942c43"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2bf42165-60bc-42ca-8072-8c13ab40239b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""85d264ad-e0a0-4565-b7ff-1a37edde51ac"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74214943-c580-44e4-98eb-ad7eebe17902"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cea9b045-a000-445b-95b8-0c171af70a3b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8607c725-d935-4808-84b1-8354e29bab63"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4cda81dc-9edd-4e03-9d7c-a71a14345d0b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e92bb26-7e3b-4ec4-b06b-3c8f8e498ddc"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82627dcc-3b13-4ba9-841d-e4b746d6553e"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c52c8e0b-8179-41d3-b8a1-d149033bbe86"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1394cbc-336e-44ce-9ea8-6007ed6193f7"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5693e57a-238a-46ed-b5ae-e64e6e574302"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4faf7dc9-b979-4210-aa8c-e808e1ef89f5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d66d5ba-88d7-48e6-b1cd-198bbfef7ace"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47c2a644-3ebc-4dae-a106-589b7ca75b59"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb9e6b34-44bf-4381-ac63-5aa15d19f677"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38c99815-14ea-4617-8627-164d27641299"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24066f69-da47-44f3-a07e-0015fb02eb2e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c191405-5738-4d4b-a523-c6a301dbf754"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7236c0d9-6ca3-47cf-a6ee-a97f5b59ea77"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23e01e3a-f935-4948-8d8b-9bcac77714fb"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a0234bc-e4cc-429a-9c14-09c147acdca0"",
                    ""path"": ""<Keyboard>/Escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a44b9745-dcd3-4cbe-926c-2d82c766b5f3"",
                    ""path"": ""<Keyboard>/F1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Debug"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ab2bedc-00a9-441f-85af-941efc95bdc7"",
                    ""path"": ""<Keyboard>/C"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""CraftHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76e3a6e9-8d39-4dfa-b68c-fdf535070ad8"",
                    ""path"": ""<Keyboard>/V"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""InventoryHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89780cf3-91d2-40f1-aca6-8210f5b4c083"",
                    ""path"": ""<Keyboard>/R"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RepairScreenHotkey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_HorizontalTranslate = m_Player.FindAction("HorizontalTranslate", throwIfNotFound: true);
        m_Player_CraftHotkey = m_Player.FindAction("CraftHotkey", throwIfNotFound: true);
        m_Player_RefuelHotkey = m_Player.FindAction("RefuelHotkey", throwIfNotFound: true);
        m_Player_NaniteHotkey = m_Player.FindAction("NaniteHotkey", throwIfNotFound: true);
        m_Player_RepairScreenHotkey = m_Player.FindAction("RepairScreenHotkey", throwIfNotFound: true);
        m_Player_VerticalTranslate = m_Player.FindAction("VerticalTranslate", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_SelectTool1 = m_Player.FindAction("SelectTool1", throwIfNotFound: true);
        m_Player_SelectTool2 = m_Player.FindAction("SelectTool2", throwIfNotFound: true);
        m_Player_SelectTool3 = m_Player.FindAction("SelectTool3", throwIfNotFound: true);
        m_Player_SelectTool4 = m_Player.FindAction("SelectTool4", throwIfNotFound: true);
        m_Player_SelectTool5 = m_Player.FindAction("SelectTool5", throwIfNotFound: true);
        m_Player_ActivateTool = m_Player.FindAction("ActivateTool", throwIfNotFound: true);
        m_Player_DeactivateTool = m_Player.FindAction("DeactivateTool", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        m_Player_ScanSalvage = m_Player.FindAction("ScanSalvage", throwIfNotFound: true);
        m_Player_Roll = m_Player.FindAction("Roll", throwIfNotFound: true);
        m_Player_InventoryHotkey = m_Player.FindAction("InventoryHotkey", throwIfNotFound: true);
        m_Player_Scroll = m_Player.FindAction("Scroll", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_CraftHotkey = m_UI.FindAction("CraftHotkey", throwIfNotFound: true);
        m_UI_RepairScreenHotkey = m_UI.FindAction("RepairScreenHotkey", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
        m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
        m_UI_Esc = m_UI.FindAction("Esc", throwIfNotFound: true);
        m_UI_Debug = m_UI.FindAction("Debug", throwIfNotFound: true);
        m_UI_InventoryHotkey = m_UI.FindAction("InventoryHotkey", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_HorizontalTranslate;
    private readonly InputAction m_Player_CraftHotkey;
    private readonly InputAction m_Player_RefuelHotkey;
    private readonly InputAction m_Player_NaniteHotkey;
    private readonly InputAction m_Player_RepairScreenHotkey;
    private readonly InputAction m_Player_VerticalTranslate;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_SelectTool1;
    private readonly InputAction m_Player_SelectTool2;
    private readonly InputAction m_Player_SelectTool3;
    private readonly InputAction m_Player_SelectTool4;
    private readonly InputAction m_Player_SelectTool5;
    private readonly InputAction m_Player_ActivateTool;
    private readonly InputAction m_Player_DeactivateTool;
    private readonly InputAction m_Player_Zoom;
    private readonly InputAction m_Player_ScanSalvage;
    private readonly InputAction m_Player_Roll;
    private readonly InputAction m_Player_InventoryHotkey;
    private readonly InputAction m_Player_Scroll;
    public struct PlayerActions
    {
        private @OctoGoneControls m_Wrapper;
        public PlayerActions(@OctoGoneControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalTranslate => m_Wrapper.m_Player_HorizontalTranslate;
        public InputAction @CraftHotkey => m_Wrapper.m_Player_CraftHotkey;
        public InputAction @RefuelHotkey => m_Wrapper.m_Player_RefuelHotkey;
        public InputAction @NaniteHotkey => m_Wrapper.m_Player_NaniteHotkey;
        public InputAction @RepairScreenHotkey => m_Wrapper.m_Player_RepairScreenHotkey;
        public InputAction @VerticalTranslate => m_Wrapper.m_Player_VerticalTranslate;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @SelectTool1 => m_Wrapper.m_Player_SelectTool1;
        public InputAction @SelectTool2 => m_Wrapper.m_Player_SelectTool2;
        public InputAction @SelectTool3 => m_Wrapper.m_Player_SelectTool3;
        public InputAction @SelectTool4 => m_Wrapper.m_Player_SelectTool4;
        public InputAction @SelectTool5 => m_Wrapper.m_Player_SelectTool5;
        public InputAction @ActivateTool => m_Wrapper.m_Player_ActivateTool;
        public InputAction @DeactivateTool => m_Wrapper.m_Player_DeactivateTool;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputAction @ScanSalvage => m_Wrapper.m_Player_ScanSalvage;
        public InputAction @Roll => m_Wrapper.m_Player_Roll;
        public InputAction @InventoryHotkey => m_Wrapper.m_Player_InventoryHotkey;
        public InputAction @Scroll => m_Wrapper.m_Player_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @HorizontalTranslate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalTranslate;
                @HorizontalTranslate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalTranslate;
                @HorizontalTranslate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalTranslate;
                @CraftHotkey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCraftHotkey;
                @CraftHotkey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCraftHotkey;
                @CraftHotkey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCraftHotkey;
                @RefuelHotkey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRefuelHotkey;
                @RefuelHotkey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRefuelHotkey;
                @RefuelHotkey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRefuelHotkey;
                @NaniteHotkey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNaniteHotkey;
                @NaniteHotkey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNaniteHotkey;
                @NaniteHotkey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNaniteHotkey;
                @RepairScreenHotkey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRepairScreenHotkey;
                @RepairScreenHotkey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRepairScreenHotkey;
                @RepairScreenHotkey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRepairScreenHotkey;
                @VerticalTranslate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalTranslate;
                @VerticalTranslate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalTranslate;
                @VerticalTranslate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalTranslate;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @SelectTool1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool1;
                @SelectTool1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool1;
                @SelectTool1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool1;
                @SelectTool2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool2;
                @SelectTool2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool2;
                @SelectTool2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool2;
                @SelectTool3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool3;
                @SelectTool3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool3;
                @SelectTool3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool3;
                @SelectTool4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool4;
                @SelectTool4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool4;
                @SelectTool4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool4;
                @SelectTool5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool5;
                @SelectTool5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool5;
                @SelectTool5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectTool5;
                @ActivateTool.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivateTool;
                @ActivateTool.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivateTool;
                @ActivateTool.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActivateTool;
                @DeactivateTool.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivateTool;
                @DeactivateTool.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivateTool;
                @DeactivateTool.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDeactivateTool;
                @Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @ScanSalvage.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScanSalvage;
                @ScanSalvage.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScanSalvage;
                @ScanSalvage.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScanSalvage;
                @Roll.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRoll;
                @InventoryHotkey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventoryHotkey;
                @InventoryHotkey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventoryHotkey;
                @InventoryHotkey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventoryHotkey;
                @Scroll.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScroll;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalTranslate.started += instance.OnHorizontalTranslate;
                @HorizontalTranslate.performed += instance.OnHorizontalTranslate;
                @HorizontalTranslate.canceled += instance.OnHorizontalTranslate;
                @CraftHotkey.started += instance.OnCraftHotkey;
                @CraftHotkey.performed += instance.OnCraftHotkey;
                @CraftHotkey.canceled += instance.OnCraftHotkey;
                @RefuelHotkey.started += instance.OnRefuelHotkey;
                @RefuelHotkey.performed += instance.OnRefuelHotkey;
                @RefuelHotkey.canceled += instance.OnRefuelHotkey;
                @NaniteHotkey.started += instance.OnNaniteHotkey;
                @NaniteHotkey.performed += instance.OnNaniteHotkey;
                @NaniteHotkey.canceled += instance.OnNaniteHotkey;
                @RepairScreenHotkey.started += instance.OnRepairScreenHotkey;
                @RepairScreenHotkey.performed += instance.OnRepairScreenHotkey;
                @RepairScreenHotkey.canceled += instance.OnRepairScreenHotkey;
                @VerticalTranslate.started += instance.OnVerticalTranslate;
                @VerticalTranslate.performed += instance.OnVerticalTranslate;
                @VerticalTranslate.canceled += instance.OnVerticalTranslate;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @SelectTool1.started += instance.OnSelectTool1;
                @SelectTool1.performed += instance.OnSelectTool1;
                @SelectTool1.canceled += instance.OnSelectTool1;
                @SelectTool2.started += instance.OnSelectTool2;
                @SelectTool2.performed += instance.OnSelectTool2;
                @SelectTool2.canceled += instance.OnSelectTool2;
                @SelectTool3.started += instance.OnSelectTool3;
                @SelectTool3.performed += instance.OnSelectTool3;
                @SelectTool3.canceled += instance.OnSelectTool3;
                @SelectTool4.started += instance.OnSelectTool4;
                @SelectTool4.performed += instance.OnSelectTool4;
                @SelectTool4.canceled += instance.OnSelectTool4;
                @SelectTool5.started += instance.OnSelectTool5;
                @SelectTool5.performed += instance.OnSelectTool5;
                @SelectTool5.canceled += instance.OnSelectTool5;
                @ActivateTool.started += instance.OnActivateTool;
                @ActivateTool.performed += instance.OnActivateTool;
                @ActivateTool.canceled += instance.OnActivateTool;
                @DeactivateTool.started += instance.OnDeactivateTool;
                @DeactivateTool.performed += instance.OnDeactivateTool;
                @DeactivateTool.canceled += instance.OnDeactivateTool;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @ScanSalvage.started += instance.OnScanSalvage;
                @ScanSalvage.performed += instance.OnScanSalvage;
                @ScanSalvage.canceled += instance.OnScanSalvage;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @InventoryHotkey.started += instance.OnInventoryHotkey;
                @InventoryHotkey.performed += instance.OnInventoryHotkey;
                @InventoryHotkey.canceled += instance.OnInventoryHotkey;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_CraftHotkey;
    private readonly InputAction m_UI_RepairScreenHotkey;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Cancel;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_ScrollWheel;
    private readonly InputAction m_UI_MiddleClick;
    private readonly InputAction m_UI_RightClick;
    private readonly InputAction m_UI_TrackedDevicePosition;
    private readonly InputAction m_UI_TrackedDeviceOrientation;
    private readonly InputAction m_UI_Esc;
    private readonly InputAction m_UI_Debug;
    private readonly InputAction m_UI_InventoryHotkey;
    public struct UIActions
    {
        private @OctoGoneControls m_Wrapper;
        public UIActions(@OctoGoneControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @CraftHotkey => m_Wrapper.m_UI_CraftHotkey;
        public InputAction @RepairScreenHotkey => m_Wrapper.m_UI_RepairScreenHotkey;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;
        public InputAction @Esc => m_Wrapper.m_UI_Esc;
        public InputAction @Debug => m_Wrapper.m_UI_Debug;
        public InputAction @InventoryHotkey => m_Wrapper.m_UI_InventoryHotkey;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @CraftHotkey.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCraftHotkey;
                @CraftHotkey.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCraftHotkey;
                @CraftHotkey.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCraftHotkey;
                @RepairScreenHotkey.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRepairScreenHotkey;
                @RepairScreenHotkey.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRepairScreenHotkey;
                @RepairScreenHotkey.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRepairScreenHotkey;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @Esc.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEsc;
                @Debug.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDebug;
                @Debug.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDebug;
                @Debug.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDebug;
                @InventoryHotkey.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryHotkey;
                @InventoryHotkey.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryHotkey;
                @InventoryHotkey.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInventoryHotkey;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @CraftHotkey.started += instance.OnCraftHotkey;
                @CraftHotkey.performed += instance.OnCraftHotkey;
                @CraftHotkey.canceled += instance.OnCraftHotkey;
                @RepairScreenHotkey.started += instance.OnRepairScreenHotkey;
                @RepairScreenHotkey.performed += instance.OnRepairScreenHotkey;
                @RepairScreenHotkey.canceled += instance.OnRepairScreenHotkey;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
                @Debug.started += instance.OnDebug;
                @Debug.performed += instance.OnDebug;
                @Debug.canceled += instance.OnDebug;
                @InventoryHotkey.started += instance.OnInventoryHotkey;
                @InventoryHotkey.performed += instance.OnInventoryHotkey;
                @InventoryHotkey.canceled += instance.OnInventoryHotkey;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnHorizontalTranslate(InputAction.CallbackContext context);
        void OnCraftHotkey(InputAction.CallbackContext context);
        void OnRefuelHotkey(InputAction.CallbackContext context);
        void OnNaniteHotkey(InputAction.CallbackContext context);
        void OnRepairScreenHotkey(InputAction.CallbackContext context);
        void OnVerticalTranslate(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSelectTool1(InputAction.CallbackContext context);
        void OnSelectTool2(InputAction.CallbackContext context);
        void OnSelectTool3(InputAction.CallbackContext context);
        void OnSelectTool4(InputAction.CallbackContext context);
        void OnSelectTool5(InputAction.CallbackContext context);
        void OnActivateTool(InputAction.CallbackContext context);
        void OnDeactivateTool(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnScanSalvage(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnInventoryHotkey(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnCraftHotkey(InputAction.CallbackContext context);
        void OnRepairScreenHotkey(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
        void OnDebug(InputAction.CallbackContext context);
        void OnInventoryHotkey(InputAction.CallbackContext context);
    }
}
