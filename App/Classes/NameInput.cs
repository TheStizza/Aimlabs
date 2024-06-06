using KWEngine3;
using KWEngine3.GameObjects;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.ComponentModel.Design;

namespace Aimlabs.App.Classes
{
    public class NameInput
    {
        private HUDObjectText _prompt;
        private HUDObjectText _text;
        private HUDObjectText _cursor;
        private float _cursorOpacity = 0.0f;
        private float _height;
        private bool _hasName = false;
        private bool _done = false;

        public bool HasName()
        {
            return _hasName;
        }

        public bool IsDone()
        {
            return _done;
        }

        public NameInput()
        {
            _height = KWEngine.Window.Height * 0.75f;

            _prompt = new HUDObjectText("Enter your name:");
            _prompt.SetPosition(KWEngine.Window.Width * 0.25f, _height);
            _prompt.SetColor(1.0f, 0.67f, 0.25f);
            _prompt.SetScale(20f);

            _text = new HUDObjectText("");
            _text.SetPosition(KWEngine.Window.Width * 0.5f, _height);
            _text.SetColor(1.0f, 0.75f, 0.5f);
            _text.SetScale(20f);

            _cursor = new HUDObjectText("|");
            _cursor.SetPosition(_text.Position.X + _text.CharacterDistanceFactor * _text.Scale.X * _text.Text.Length - _text.CharacterDistanceFactor * _text.Scale.X * 0.25f, _height);
            _cursor.SetColor(1.0f, 0.75f, 0.5f);
            _cursor.SetColorEmissive(1.0f, 1.0f, 1.0f);
            _cursor.SetScale(20f);
        }

        public string GetNameEntered()
        {
            _done = true;
            return _text.Text;
        }

        public void Update(KeyboardExt state)
        {
            if (_hasName == false)
            {
                string pressedKey = GetPressedKey(state);
                if (pressedKey != null)
                {
                    if (pressedKey == "BACKSPACE")
                    {
                        if (_text.Text.Length > 0)
                        {
                            string textNew = _text.Text.Substring(0, _text.Text.Length - 1);
                            _text.SetText(textNew, false);
                        }
                    }
                    else if (pressedKey == "ENTER" && _text.Text.Length > 0)
                    {
                        _hasName = true;
                    }
                    else
                    {
                        if (_text.Text.Length < 12)
                        {
                            _text.SetText(_text.Text + pressedKey, false);
                        }
                    }

                }

                _cursor.SetPosition(_text.Position.X + _text.CharacterDistanceFactor * _text.Scale.X * _text.Text.Length - _text.CharacterDistanceFactor * _text.Scale.X * 0.25f, _height);
                float cursorOpacity = MathF.Sin(KWEngine.CurrentWorld.WorldTime * 20) * 0.5f + 0.5f;
                _cursor.SetColorEmissiveIntensity(cursorOpacity);
                _cursor.SetOpacity(cursorOpacity);
            }
        }

        public void AddToWorld()
        {
            KWEngine.CurrentWorld.AddHUDObject(_prompt);
            KWEngine.CurrentWorld.AddHUDObject(_text);
            KWEngine.CurrentWorld.AddHUDObject(_cursor);
        }

        public void RemoveHUDObjects()
        {
            KWEngine.CurrentWorld.RemoveHUDObject(_prompt);
            KWEngine.CurrentWorld.RemoveHUDObject(_text);
            KWEngine.CurrentWorld.RemoveHUDObject(_cursor);
        }

        private string GetPressedKey(KeyboardExt state)
        {
            string result = null;

            if (state.IsKeyPressed(Keys.Backspace))
            {
                result = "BACKSPACE";
            }
            else if (state.IsKeyPressed(Keys.Enter) || state.IsKeyPressed(Keys.KeyPadEnter))
            {
                result = "ENTER";
            }
            else if (state.IsKeyPressed(Keys.Space))
            {
                result = " ";
            }
            else
            {
                for (int i = 65; i < 91; i++)
                {
                    if (state.IsKeyPressed((Keys)i))
                    {
                        if (i == 89)
                            i = 90;
                        else if (i == 90)
                            i = 89;
                        if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
                        {
                            result = "" + (char)i;
                        }
                        else
                        {
                            result = "" + (char)(i + 32);
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}