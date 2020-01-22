using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cindy
{
    public static class VirtualInput
    {
        private static readonly Dictionary<string, ISet<VirtualAxis>> virtualAxes = new Dictionary<string, ISet<VirtualAxis>>();
        private static readonly Dictionary<string, ISet<VirtualButton>> virtualButtons = new Dictionary<string, ISet<VirtualButton>>();
        public static void Register(string name,VirtualAxis virtualAxis)
        {
            if (!virtualAxes.ContainsKey(name))
                virtualAxes.Add(name, new HashSet<VirtualAxis>());
            ISet<VirtualAxis> axes = virtualAxes[name];
            if (!axes.Contains(virtualAxis))
                axes.Add(virtualAxis);
        }

        public static void Remove(string name,VirtualAxis virtualAxis)
        {
            if (!virtualAxes.ContainsKey(name))
                return;
            ISet<VirtualAxis> axes = virtualAxes[name];
            if (axes.Contains(virtualAxis))
                axes.Remove(virtualAxis);
        }

        public static void Register(string name,VirtualButton virtualButton)
        {
            if (!virtualButtons.ContainsKey(name))
                virtualButtons.Add(name, new HashSet<VirtualButton>());
            ISet<VirtualButton> buttons = virtualButtons[name];
            if (!buttons.Contains(virtualButton))
                buttons.Add(virtualButton);
        }

        public static void Remove(string name,VirtualButton virtualButton)
        {
            if (!virtualButtons.ContainsKey(name))
                return;
            ISet<VirtualButton> buttons = virtualButtons[name];
            if (buttons.Contains(virtualButton))
                buttons.Remove(virtualButton);
        }

        public static float GetAxis(string name)
        {
            float valueA = 0, valueB = 0;
            try
            {
                float value = Input.GetAxis(name);
                if (value > 0)
                    valueA = value;
                else if(value < 0)
                    valueB = value;
            }catch(Exception e)
            {
            }
            if (!virtualAxes.ContainsKey(name))
                return valueA + valueB;
            ISet<VirtualAxis> axes = virtualAxes[name];
            foreach(VirtualAxis axis in axes)
            {
                if (axis == null)
                    continue;
                float value = axis();
                if (value > 0 && value > valueA)
                    valueA = value;
                else if (value < 0 && value < valueB)
                    valueB = value;
            }
            return valueA + valueB;
        }

        public static bool GetButton(string name)
        {
            try
            {
                if (Input.GetButton(name))
                    return true;
            }catch(Exception e)
            {

            }
            if (!virtualButtons.ContainsKey(name))
                return false;
            ISet<VirtualButton> buttons = virtualButtons[name];
            foreach(VirtualButton button in buttons)
            {
                if (button == null)
                    continue;
                if (button().value)
                    return true;
            }
            return false;
        }

        public static bool GetButtonDown(string name)
        {
            try
            {
                if (Input.GetButtonDown(name))
                    return true;
            }
            catch (Exception e)
            {

            }
            if (!virtualButtons.ContainsKey(name))
                return false;
            ISet<VirtualButton> buttons = virtualButtons[name];
            foreach (VirtualButton button in buttons)
            {
                if (button == null)
                    continue;
                if (button().down)
                    return true;
            }
            return false;
        }

        public static bool GetButtonUp(string name)
        {
            try
            {
                if (Input.GetButtonUp(name))
                    return true;
            }
            catch (Exception e)
            {

            }
            if (!virtualButtons.ContainsKey(name))
                return false;
            ISet<VirtualButton> buttons = virtualButtons[name];
            foreach (VirtualButton button in buttons)
            {
                if (button == null)
                    continue;
                if (button().up)
                    return true;
            }
            return false;
        }

        public static Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public static Vector3 GetMouseScrollDelta()
        {
            return Input.mouseScrollDelta;
        }
    }

    public class ButtonState
    {
        public bool down, up, value;
    }

    public delegate ButtonState VirtualButton();

    public delegate float VirtualAxis();

}