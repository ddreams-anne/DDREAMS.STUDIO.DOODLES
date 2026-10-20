using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class OpenFractureInput
{
    private static readonly HashSet<KeyCode> warnedKeyCodes = new HashSet<KeyCode>();

    public static bool IsKeyPressed(KeyCode keyCode)
    {
        return IsPressed(keyCode, false);
    }

    public static bool WasKeyPressedThisFrame(KeyCode keyCode)
    {
        return IsPressed(keyCode, true);
    }

    private static bool IsPressed(KeyCode keyCode, bool wasPressedThisFrame)
    {
        if (IsMouseButtonKey(keyCode))
        {
            var mouseButton = GetMouseButton(keyCode);
            return mouseButton != null &&
                (wasPressedThisFrame ? mouseButton.wasPressedThisFrame : mouseButton.isPressed);
        }

        if (!TryGetInputSystemKey(keyCode, out var key))
        {
            if (keyCode != KeyCode.None && warnedKeyCodes.Add(keyCode))
            {
                Debug.LogWarning($"OpenFracture does not support the configured key code '{keyCode}' with the Input System.");
            }

            return false;
        }

        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return false;
        }

        var keyControl = keyboard[key];
        return wasPressedThisFrame ? keyControl.wasPressedThisFrame : keyControl.isPressed;
    }

    private static bool IsMouseButtonKey(KeyCode keyCode)
    {
        return keyCode == KeyCode.Mouse0 ||
            keyCode == KeyCode.Mouse1 ||
            keyCode == KeyCode.Mouse2 ||
            keyCode == KeyCode.Mouse3 ||
            keyCode == KeyCode.Mouse4;
    }

    private static ButtonControl GetMouseButton(KeyCode keyCode)
    {
        var mouse = Mouse.current;
        if (mouse == null)
        {
            return null;
        }

        switch (keyCode)
        {
            case KeyCode.Mouse0:
                return mouse.leftButton;
            case KeyCode.Mouse1:
                return mouse.rightButton;
            case KeyCode.Mouse2:
                return mouse.middleButton;
            case KeyCode.Mouse3:
                return mouse.forwardButton;
            case KeyCode.Mouse4:
                return mouse.backButton;
            default:
                return null;
        }
    }

    private static bool TryGetInputSystemKey(KeyCode keyCode, out Key key)
    {
        var keyName = keyCode.ToString();
        if (keyName.StartsWith("Alpha", StringComparison.Ordinal))
        {
            keyName = "Digit" + keyName.Substring("Alpha".Length);
        }
        else if (keyName.StartsWith("Keypad", StringComparison.Ordinal))
        {
            keyName = "Numpad" + keyName.Substring("Keypad".Length);
        }
        else if (keyName == "Return")
        {
            keyName = "Enter";
        }

        return Enum.TryParse(keyName, true, out key) && key != Key.None;
    }
}
