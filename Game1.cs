using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Project3;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private Tribble[] _tribbles = new Tribble[4];
    private SpriteBatch _spriteBatch;
    private Color _backGroundColor = Color.Blue;
    private int[] _window = {1200, 1200};

    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = _window[0];
       _graphics.PreferredBackBufferHeight = _window[1];
       _graphics.ApplyChanges();
       this.Window.Title = "Tribbles";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tribbles[0]= new Tribble(Content.Load<Texture2D>("tribbleBrown"), 403, 399, _window);
        _tribbles[1] = new Tribble(Content.Load<Texture2D>("tribbleCream"), 332, 294, _window);
        _tribbles[2] = new Tribble(Content.Load<Texture2D>("tribbleGrey"), 419, 419, _window);
        _tribbles[3] = new Tribble(Content.Load<Texture2D>("tribbleOrange"), 339, 290, _window);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        foreach (Tribble tribble in _tribbles)
        {
            _backGroundColor = tribble.MoveTribble(_window, _backGroundColor);
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(_backGroundColor);
        _spriteBatch.Begin();
        foreach (Tribble tribble in _tribbles)
        {
            tribble.DrawTribble(_spriteBatch);
        } 
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
public class Tribble
    {
        private Texture2D _tribbleName;
        private int _width;
        private int _height;
        private int[] _cords;
        private int[] _speeds;
        private Random _random;

        public Tribble(Texture2D tribbleName, int width, int height, int[] window) 
        {

            _random = new Random();
            _tribbleName = tribbleName;
            _width = width;
            _height = height;
            _cords = PlaceAtRandomLocation(window);
            _speeds = RandomSpeed();
        }
        public int[] RandomSpeed()
        {
            int[] _speeds = {0, 0};
            while (_speeds[0] == 0 & _speeds[1] == 0)
            {
                _speeds[0] = _random.Next(-6, 7);
                _speeds[1] = _random.Next(-6, 7);
            }
            return _speeds;
        }
        public int[] PlaceAtRandomLocation(int[] window)
        {
            int[] _cords = {0, 0};
            _cords[0] = _random.Next(0, window[0] - _width);
            _cords[1] = _random.Next(0, window[1] - _height);
            return _cords;
        }
        public Color MoveTribble( int[] window, Color backGround)
        {
            if (_cords[0] + _width >= window[0] | _cords[0] <= 0 | _cords[1] + _height >= window[0] | _cords[1] <= 0)
            {
                backGround = new Color (_random.Next(0, 256), _random.Next(0, 256), _random.Next(0, 256));
                _cords = PlaceAtRandomLocation(window);
                _speeds = RandomSpeed();
            }
            _cords[0] += _speeds[0];
            _cords[1] += _speeds[1];
            return backGround;
        }
        public void DrawTribble(SpriteBatch draw)
        {
            draw.Draw(_tribbleName, new Vector2(_cords[0], _cords[1]), Color.White);
        }
    }
