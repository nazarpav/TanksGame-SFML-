using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tank
{
    class TemplateMenu : Drawable
    {
        private readonly bool _PlaySounds;
        private byte _choise;

        private Vector2f _VMenu;
        private readonly Vector2f _VMenuSave;
        private readonly byte _fontSize;
        private readonly byte _indentBetweenFonts;
        private readonly Color _colorAllItem;
        private readonly Color _colorAllItemShadow;
        private readonly Color _colorSelectedItem;

        private readonly Font _font;
        private readonly Sound _selectItem;
        private readonly Sound _itemSelected;
        private readonly Sprite _backgroundSprite;

        private List<Text> _menu;
        private Clock _indentBetweenСhangeItem;
        public byte Choise { get => _choise;}

        public TemplateMenu(Vector2f Vmenu, Font font, byte fontSize, byte indentBetweenFonts, Color colorAllItem, Color colorSelectedItem, bool PlaySounds, Texture background = null)
        {
            _indentBetweenСhangeItem = new Clock();
            _indentBetweenСhangeItem.Restart();
            _PlaySounds = PlaySounds;

            _colorAllItem = colorAllItem;
            _colorSelectedItem = colorSelectedItem;
            _colorAllItemShadow = new Color(0,30,30);

            _choise = 0;
            _font = font;
            _fontSize = fontSize;
            _indentBetweenFonts = indentBetweenFonts;

            _menu = new List<Text>(0);
            _VMenu = Vmenu;
            _VMenuSave = Vmenu;

            _selectItem = new Sound(new SoundBuffer(@"Content\Sounds\SoundMenu1.wav"));
            _selectItem.Pitch=0.1f;
            _selectItem.Volume=100;
            _itemSelected = new Sound(new SoundBuffer(@"Content\Sounds\SoundShotTank.wav"));

            _backgroundSprite = new Sprite(background);
            SetFontDefaulth();
        }
        private void SetFontDefaulth()
        {
            _VMenu = _VMenuSave;
            for (int i = 0; i < _menu.Count; i++)
            {
                _menu[i].CharacterSize = _fontSize;
                _menu[i].Position = _VMenu;
                _menu[i].FillColor = _colorAllItem;
            }
        }

        public void AddMenuItem(string text)
        {
            _menu.Add(new Text(text, _font, _fontSize));
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            while (true)
            {

                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    _selectItem.Stop();
                    break;
                }
                KeyPressedInMenu();
                _backgroundSprite.Draw(target, states);
                for (int i = 0; i < _menu.Count; i++)
                {
                    _menu[i].Position = _VMenu;
                    _VMenu.Y += _indentBetweenFonts;
                    if (i == Choise)_menu[i].FillColor = _colorSelectedItem;
                    else _menu[i].FillColor = _colorAllItemShadow;
                    _menu[i].CharacterSize = _fontSize + 1u;
                    _menu[i].Draw(target, states);
                    _menu[i].FillColor = _colorAllItem;
                    _menu[i].CharacterSize = _fontSize - 1u;
                    _menu[i].Draw(target, states);
                }
                SetFontDefaulth();

            }
        }
        private void KeyPressedInMenu()
        {
            if (_indentBetweenСhangeItem.ElapsedTime.AsMilliseconds() < 100)
                return;
            _indentBetweenСhangeItem.Restart();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if (_PlaySounds) _selectItem.Play();
                if (_choise <= 0)
                {
                    _choise = (byte)(_menu.Count - 1);
                    return;
                }
                _choise--;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                if (_PlaySounds) _selectItem.Play();
                if (_choise + 1 >= _menu.Count)
                {
                    _choise = 0;
                    return;
                }
                _choise++;
            }
            return;
        }
    }
}
