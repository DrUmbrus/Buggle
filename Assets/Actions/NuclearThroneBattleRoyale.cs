// GENERATED AUTOMATICALLY FROM 'Assets/Actions/NuclearThroneBattleRoyale.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NuclearThroneBattleRoyale : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NuclearThroneBattleRoyale()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""NuclearThroneBattleRoyale"",
    ""maps"": [
        {
            ""name"": ""PlayerCon"",
            ""id"": ""d9873699-d81e-41e6-ba85-4204a451848c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ad55b621-88b2-46c6-a919-ac67746e0b91"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""345cfc50-5df4-471f-b76a-9f80db009811"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""39b1cd80-e62f-4549-9f10-0a3257f0e23b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.75)""
                },
                {
                    ""name"": ""AltFire"",
                    ""type"": ""Button"",
                    ""id"": ""bea2fd69-bb89-485c-bcdd-021af6f21b89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.75)""
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Button"",
                    ""id"": ""cda3350b-e3e1-46eb-a28b-cb7da45ecd5a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""AltFire1"",
                    ""type"": ""Button"",
                    ""id"": ""ba2d8de4-74b4-4211-9773-f5ee460ce09f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""5fb4de15-131b-40eb-b61f-81190fb2d7b8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell"",
                    ""type"": ""Button"",
                    ""id"": ""853fba1f-cefa-4ccf-b8e4-cfd5ab49f6d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AltSpell"",
                    ""type"": ""Button"",
                    ""id"": ""29e4eea7-eccc-4df3-a464-da64e643dcf5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0067ba75-6f54-4442-85f6-4ae4b77061e8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""f6722d2c-2326-4ec0-8313-ffbc1552de90"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ShowChar"",
                    ""type"": ""Button"",
                    ""id"": ""ada26aa3-68b7-462a-9c3a-39cf01bb3c13"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""978bfe49-cc26-4a3d-ab7b-7d7a29327403"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cae3d0b-96a2-4033-b905-350d40ab4ad6"",
                    ""path"": ""<SwitchProControllerHID>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1635d3fe-58b6-4ba9-a4e2-f4b964f6b5c8"",
                    ""path"": ""<XRController>/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""685ee14f-a09b-409d-924f-a9effc386fc1"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0170520b-1e04-43f7-a920-b61fc8f3f9cc"",
                    ""path"": ""<DualShock4GampadiOS>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Dualshock;Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
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
                    ""id"": ""863dbe7a-5072-428c-a483-8bccefaf4042"",
                    ""path"": ""<SwitchProControllerHID>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
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
                    ""name"": """",
                    ""id"": ""aad89e86-ec72-4925-ba86-9819c9817d7d"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2aaa2a9c-30b2-4387-81ef-2d474becf7b0"",
                    ""path"": ""<DualShock4GampadiOS>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Dualshock;Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd424b33-dc87-4d73-82a4-a6ba2e68c81c"",
                    ""path"": ""<SwitchProControllerHID>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee3d0cd2-254e-47a7-a8cb-bc94d9658c54"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8255d333-5683-4943-a58a-ccb207ff1dce"",
                    ""path"": ""<XRController>/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""886e731e-7071-4ae4-95c0-e61739dad6fd"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9aae7977-6dbd-4b4f-a523-9bd995370e04"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60146b73-ecff-4aa6-b8b7-aca97a056b19"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bb3a7bd-0219-4d4b-a7c1-d94f4940168b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""802bd57a-8de9-4706-972b-870ae693a04d"",
                    ""path"": ""<SwitchProControllerHID>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b81e5d5-5590-456d-98c1-867bb8df03e2"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23c86528-a756-43b2-8923-aa38c9d6de0f"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Dualshock;Gamepad"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f2224c6-3cae-42c0-9961-8bab413c7227"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02416ab7-9849-4998-b3d9-face5f689fe2"",
                    ""path"": ""<SwitchProControllerHID>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c3ecd1a-9859-412e-b5a3-37d7ccf8dccc"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""286d5e50-b2cc-46d6-bf44-a1722216d7c0"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11c7b1a5-43d2-44df-b251-3bc9d52db920"",
                    ""path"": ""<SwitchProControllerHID>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfffa513-e7b5-44ee-8e5a-3b7518c10738"",
                    ""path"": ""<Gamepad>/startButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c92ac11e-555c-4f13-a517-1b62d7045168"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99c32b70-3d68-4f2e-9b2f-1f84a57a8c78"",
                    ""path"": ""<SwitchProControllerHID>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d47cbb5-040b-4017-ac9c-965154b6a6cf"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67f38258-3cd5-4d6f-a067-2c58d368fa12"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a335eee-c30c-48a7-b0d9-82e38706c491"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3072711d-e7db-40f9-80b9-0b6958beee37"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3eda855e-553e-42e4-b0bc-edfdc27df878"",
                    ""path"": ""<SwitchProControllerHID>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a04602d1-a96c-45e3-ac99-e7f0433a9ca7"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Dualshock;Gamepad"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d2c497c-9470-4926-8da6-35309175e8fc"",
                    ""path"": ""<SwitchProControllerHID>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98f9aa0b-a902-4da9-ab46-657532792ca4"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3751dc41-7e3a-4c3f-8f74-d0ec94e9a9ee"",
                    ""path"": ""<XRController>/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3571a85-575f-443b-8e7d-fb6afea6da1b"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e196107a-7ac3-4723-994b-f5cb5f76408e"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b4746c7-a51e-4bef-929b-4eb90608a241"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""822df69f-a178-494f-b24d-ef1c97fac99f"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""ShowChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4b0d0a1-b787-4b1f-a1bd-198596a1fea6"",
                    ""path"": ""<SwitchProControllerHID>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""ShowChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f4a0c0c-c905-4d36-b85f-d53fb35d50b0"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""ShowChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""304786f2-fae7-41a8-94aa-876fcdef5772"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""335e141a-196b-4f1e-b429-c36b231604c0"",
                    ""path"": ""<SwitchProControllerHID>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78594700-ef99-4ee8-bf48-4a415145a84b"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e19b8ea3-9318-4082-85e3-39855669c538"",
                    ""path"": ""<XRController>/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a03ed93-8fb3-409e-95e3-3c4e7ce851d7"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed72cf00-3547-49af-9483-e8ae7392134f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd0b4667-9126-48e6-ad38-f26fc28ff0b2"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47a62e29-8541-405e-9613-812e1524178f"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""AltFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""318dde0e-c992-451a-9d80-95d18adaf6f5"",
                    ""path"": ""<SwitchProControllerHID>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e181e9a-42e2-4a8a-8c30-bc15508eabb9"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3b8c3fa-470d-4bc4-b76f-4b3d28e4538d"",
                    ""path"": ""<XRController>/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eec42822-1823-4376-9ffd-ef57106702fa"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Touch"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b17251bb-0c2b-4a59-8035-a15752b8a00c"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bded3fb4-a08d-4504-a18d-07ffaccf977e"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""AltFire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""150f3f68-e04a-4daf-a750-1792e6a6f84b"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": ""Press(pressPoint=1)"",
                    ""processors"": """",
                    ""groups"": ""X360;Gamepad"",
                    ""action"": ""AltSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e56c5d13-0086-4b01-8286-b379bdfa3717"",
                    ""path"": ""<SwitchProControllerHID>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Switch"",
                    ""action"": ""AltSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f1fd9cb-126b-4e3f-8c66-2c2901f14162"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Dualshock"",
                    ""action"": ""AltSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
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
        },
        {
            ""name"": ""Switch"",
            ""bindingGroup"": ""Switch"",
            ""devices"": [
                {
                    ""devicePath"": ""<SwitchProControllerHID>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""X360"",
            ""bindingGroup"": ""X360"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Dualshock"",
            ""bindingGroup"": ""Dualshock"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShock4GampadiOS>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        }
    ]
}");
        // PlayerCon
        m_PlayerCon = asset.FindActionMap("PlayerCon", throwIfNotFound: true);
        m_PlayerCon_Move = m_PlayerCon.FindAction("Move", throwIfNotFound: true);
        m_PlayerCon_Look = m_PlayerCon.FindAction("Look", throwIfNotFound: true);
        m_PlayerCon_Fire = m_PlayerCon.FindAction("Fire", throwIfNotFound: true);
        m_PlayerCon_AltFire = m_PlayerCon.FindAction("AltFire", throwIfNotFound: true);
        m_PlayerCon_Fire1 = m_PlayerCon.FindAction("Fire1", throwIfNotFound: true);
        m_PlayerCon_AltFire1 = m_PlayerCon.FindAction("AltFire1", throwIfNotFound: true);
        m_PlayerCon_Grab = m_PlayerCon.FindAction("Grab", throwIfNotFound: true);
        m_PlayerCon_Spell = m_PlayerCon.FindAction("Spell", throwIfNotFound: true);
        m_PlayerCon_AltSpell = m_PlayerCon.FindAction("AltSpell", throwIfNotFound: true);
        m_PlayerCon_Pause = m_PlayerCon.FindAction("Pause", throwIfNotFound: true);
        m_PlayerCon_Back = m_PlayerCon.FindAction("Back", throwIfNotFound: true);
        m_PlayerCon_ShowChar = m_PlayerCon.FindAction("ShowChar", throwIfNotFound: true);
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

    // PlayerCon
    private readonly InputActionMap m_PlayerCon;
    private IPlayerConActions m_PlayerConActionsCallbackInterface;
    private readonly InputAction m_PlayerCon_Move;
    private readonly InputAction m_PlayerCon_Look;
    private readonly InputAction m_PlayerCon_Fire;
    private readonly InputAction m_PlayerCon_AltFire;
    private readonly InputAction m_PlayerCon_Fire1;
    private readonly InputAction m_PlayerCon_AltFire1;
    private readonly InputAction m_PlayerCon_Grab;
    private readonly InputAction m_PlayerCon_Spell;
    private readonly InputAction m_PlayerCon_AltSpell;
    private readonly InputAction m_PlayerCon_Pause;
    private readonly InputAction m_PlayerCon_Back;
    private readonly InputAction m_PlayerCon_ShowChar;
    public struct PlayerConActions
    {
        private @NuclearThroneBattleRoyale m_Wrapper;
        public PlayerConActions(@NuclearThroneBattleRoyale wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerCon_Move;
        public InputAction @Look => m_Wrapper.m_PlayerCon_Look;
        public InputAction @Fire => m_Wrapper.m_PlayerCon_Fire;
        public InputAction @AltFire => m_Wrapper.m_PlayerCon_AltFire;
        public InputAction @Fire1 => m_Wrapper.m_PlayerCon_Fire1;
        public InputAction @AltFire1 => m_Wrapper.m_PlayerCon_AltFire1;
        public InputAction @Grab => m_Wrapper.m_PlayerCon_Grab;
        public InputAction @Spell => m_Wrapper.m_PlayerCon_Spell;
        public InputAction @AltSpell => m_Wrapper.m_PlayerCon_AltSpell;
        public InputAction @Pause => m_Wrapper.m_PlayerCon_Pause;
        public InputAction @Back => m_Wrapper.m_PlayerCon_Back;
        public InputAction @ShowChar => m_Wrapper.m_PlayerCon_ShowChar;
        public InputActionMap Get() { return m_Wrapper.m_PlayerCon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerConActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerConActions instance)
        {
            if (m_Wrapper.m_PlayerConActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnLook;
                @Fire.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire;
                @AltFire.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire;
                @AltFire.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire;
                @AltFire.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire;
                @Fire1.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnFire1;
                @AltFire1.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire1;
                @AltFire1.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire1;
                @AltFire1.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltFire1;
                @Grab.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnGrab;
                @Spell.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSpell;
                @Spell.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSpell;
                @Spell.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnSpell;
                @AltSpell.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltSpell;
                @AltSpell.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltSpell;
                @AltSpell.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnAltSpell;
                @Pause.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnPause;
                @Back.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnBack;
                @ShowChar.started -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnShowChar;
                @ShowChar.performed -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnShowChar;
                @ShowChar.canceled -= m_Wrapper.m_PlayerConActionsCallbackInterface.OnShowChar;
            }
            m_Wrapper.m_PlayerConActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @AltFire.started += instance.OnAltFire;
                @AltFire.performed += instance.OnAltFire;
                @AltFire.canceled += instance.OnAltFire;
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @AltFire1.started += instance.OnAltFire1;
                @AltFire1.performed += instance.OnAltFire1;
                @AltFire1.canceled += instance.OnAltFire1;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Spell.started += instance.OnSpell;
                @Spell.performed += instance.OnSpell;
                @Spell.canceled += instance.OnSpell;
                @AltSpell.started += instance.OnAltSpell;
                @AltSpell.performed += instance.OnAltSpell;
                @AltSpell.canceled += instance.OnAltSpell;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @ShowChar.started += instance.OnShowChar;
                @ShowChar.performed += instance.OnShowChar;
                @ShowChar.canceled += instance.OnShowChar;
            }
        }
    }
    public PlayerConActions @PlayerCon => new PlayerConActions(this);
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
    private int m_SwitchSchemeIndex = -1;
    public InputControlScheme SwitchScheme
    {
        get
        {
            if (m_SwitchSchemeIndex == -1) m_SwitchSchemeIndex = asset.FindControlSchemeIndex("Switch");
            return asset.controlSchemes[m_SwitchSchemeIndex];
        }
    }
    private int m_X360SchemeIndex = -1;
    public InputControlScheme X360Scheme
    {
        get
        {
            if (m_X360SchemeIndex == -1) m_X360SchemeIndex = asset.FindControlSchemeIndex("X360");
            return asset.controlSchemes[m_X360SchemeIndex];
        }
    }
    private int m_DualshockSchemeIndex = -1;
    public InputControlScheme DualshockScheme
    {
        get
        {
            if (m_DualshockSchemeIndex == -1) m_DualshockSchemeIndex = asset.FindControlSchemeIndex("Dualshock");
            return asset.controlSchemes[m_DualshockSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerConActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnAltFire(InputAction.CallbackContext context);
        void OnFire1(InputAction.CallbackContext context);
        void OnAltFire1(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnSpell(InputAction.CallbackContext context);
        void OnAltSpell(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnShowChar(InputAction.CallbackContext context);
    }
}
