using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Unlockable
{
    #region Enumerations

    public enum MouseButtons { Left, Right, Middle, XButton1, XButton2 }

    #endregion

    public class InputHandler 
    {
        #region Fields

        public delegate void ControllerDisconnectHandler(object o, int e);
        public static event ControllerDisconnectHandler OnControllerDisconnect;

        #endregion

        #region Properties

        public static KeyboardState KeyboardState { get; set; }
        public static KeyboardState LastKeyboardState { get; set; }

        public static MouseState MouseState { get; set; }
        public static MouseState LastMouseState { get; set; }

        public static GamePadState[] GamePadStates { get; set; }
        public static GamePadState[] LastGamePadStates { get; set; }

        #endregion

        #region Constructors

        public InputHandler() 
        {
            GamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            SetStates();
        }
        #endregion

        #region Methods

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && LastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static GamePadState GamePadState(PlayerIndex index)
        {
            return GamePadStates[(int)index];
        }

        public static GamePadState LastGamePadState(PlayerIndex index)
        {
            return LastGamePadStates[(int)index];
        }

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonUp(button) &&
                LastGamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonDown(button) &&
                   LastGamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonUp(Buttons button, PlayerIndex index)
        {
            return GamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool IsConnected(PlayerIndex index)
        {
            return GamePadStates[(int)index].IsConnected;
        }

        public static Vector2 LeftThumb(PlayerIndex index)
        {
            return GamePadStates[(int)index].ThumbSticks.Left;
        }

        public static Vector2 LastLeftThumb(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].ThumbSticks.Left;
        }

        public static Vector2 RightThumb(PlayerIndex index)
        {
            return GamePadStates[(int)index].ThumbSticks.Right;
        }

        public static Vector2 LastRightThumb(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].ThumbSticks.Right;
        }

        public static GamePadDPad DPad(PlayerIndex index)
        {
            return GamePadStates[(int)index].DPad;
        }

        public static GamePadDPad LastDPad(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].DPad;
        }

        public static GamePadTriggers Triggers(PlayerIndex index)
        {
            return GamePadStates[(int)index].Triggers;
        }

        public static GamePadTriggers LastTriggers(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].Triggers;
        }

        public static float LeftTrigger(PlayerIndex index)
        {
            return GamePadStates[(int)index].Triggers.Left;
        }

        public static float LastLeftTrigger(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].Triggers.Left;
        }

        public static float RightTrigger(PlayerIndex index)
        {
            return GamePadStates[(int)index].Triggers.Right;
        }

        public static float LastRightTrigger(PlayerIndex index)
        {
            return LastGamePadStates[(int)index].Triggers.Right;
        }

        public static Vector2 MousePositionAsVector2 { get { return new Vector2(MouseState.X, MouseState.Y); } }
        public static Vector2 LastMousePositionAsVector2 { get { return new Vector2(LastMouseState.X, LastMouseState.Y); } }
        public static Point MousePositionAsPoint { get { return new Point(MouseState.X, MouseState.Y); } }
        public static Point LastMousePositionAsPoint { get { return new Point(LastMouseState.X, MouseState.Y); } }

        public static bool MouseButtonReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Released &&
                           LastMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Released &&
                           LastMouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.XButton1:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.XButton2:
                    return MouseState.MiddleButton == ButtonState.Released &&
                           LastMouseState.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        public static bool MouseButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.XButton2:
                    return MouseState.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        public static bool MouseButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Released;
                case MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Released;
                case MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Released;
                case MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Released;
                case MouseButtons.XButton2:
                    return MouseState.XButton2 == ButtonState.Released;
            }

            return false;
        }

        public static bool MouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseState.LeftButton == ButtonState.Pressed &&
                           LastMouseState.LeftButton == ButtonState.Released;
                case MouseButtons.Right:
                    return MouseState.RightButton == ButtonState.Pressed &&
                           LastMouseState.RightButton == ButtonState.Released;
                case MouseButtons.Middle:
                    return MouseState.MiddleButton == ButtonState.Pressed &&
                           LastMouseState.MiddleButton == ButtonState.Released;
                case MouseButtons.XButton1:
                    return MouseState.XButton1 == ButtonState.Pressed &&
                           LastMouseState.XButton1 == ButtonState.Released;
                case MouseButtons.XButton2:
                    return MouseState.XButton1 == ButtonState.Pressed &&
                           LastMouseState.XButton1 == ButtonState.Released;
            }

            return false;
        }

        public static bool MousePresent(Rectangle rectangle)
        {
            return rectangle.Contains(MousePositionAsPoint);
        }

        public static bool LastMousePresent(Rectangle rectangle)
        {
            return rectangle.Contains(LastMousePositionAsPoint);
        }

        public static bool MouseScrollUp()
        {
            return MouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }

        public static bool MouseScrollDown()
        {
            return MouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }



        #endregion

        #region XNA Methods

        void SetStates()
        {
            LastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            LastMouseState = MouseState;
            MouseState = Mouse.GetState();

            LastGamePadStates = (GamePadState[])GamePadStates.Clone();

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                if (!GamePadStates[(int)index].IsConnected)
                    ControllerDisconnect((int)index);

                GamePadStates[(int)index] = GamePad.GetState(index);
            }
                
        }


        #endregion

        #region Events

        private void ControllerDisconnect(int e)
        {
            if (OnControllerDisconnect != null)
                OnControllerDisconnect(this, e);
        }

        #endregion
    }
}
