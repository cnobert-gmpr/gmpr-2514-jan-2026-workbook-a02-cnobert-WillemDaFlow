using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment1;

public class DisplayingContentGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _gameBackround, _shuriken, _player, _starSprite;
    private Vector2 _shurikenPosition;
    private Vector2 _shurikenVelocity;
    private Vector2 _playerPosition;
    private float _playerSpeed;
    private SpriteFont _font;
    private string _output = "Yo wassup Conrad.";

    private SimpleAnimation _walkingAnimation;
    private SimpleAnimation _walkingAnimation2;
    public DisplayingContentGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1078;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        _playerPosition = new Vector2(400, 600);
        _playerSpeed = 250f;
        _shurikenPosition = new Vector2(500, 0);
        _shurikenVelocity = new Vector2(6f, 7f);


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameBackround = Content.Load<Texture2D>("NarutoBackround");
        _shuriken = Content.Load<Texture2D>("Shuriken2");
        _player = Content.Load<Texture2D>("playerSprite2");
        _playerPosition = new Vector2(400, 600);
        _font = Content.Load<SpriteFont>("SystemArialFont");
        _starSprite = Content.Load<Texture2D>("starSprite");
        Texture2D walkingSpriteSheet = Content.Load<Texture2D>("NarutoWalking2");
        int width = walkingSpriteSheet.Width;
        int height = walkingSpriteSheet.Height;
        _walkingAnimation = new SimpleAnimation(walkingSpriteSheet, width / 6, height, 6, 6);

        Texture2D walkingSpriteSheet2 = Content.Load<Texture2D>("BartWalking");
        int width2 = walkingSpriteSheet2.Width;
        int height2 = walkingSpriteSheet2.Height;
        _walkingAnimation2 = new SimpleAnimation(walkingSpriteSheet2, width2 / 6, height2, 6, 6);
        
    }

    protected override void Update(GameTime gameTime)
    {
        
        _walkingAnimation.Update(gameTime);
        _walkingAnimation2.Update(gameTime);
        _shurikenPosition += _shurikenVelocity;
        var kstate = Keyboard.GetState();
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (kstate.IsKeyDown(Keys.Left))
            _playerPosition.X -= _playerSpeed * delta;
        if (kstate.IsKeyDown(Keys.Right))
            _playerPosition.X += _playerSpeed * delta;
        if (kstate.IsKeyDown(Keys.Up))
            _playerPosition.Y -= _playerSpeed * delta;
        if (kstate.IsKeyDown(Keys.Down))
            _playerPosition.Y += _playerSpeed * delta;
        float playerScale = 1.0f;
        _playerPosition.X = MathHelper.Clamp(_playerPosition.X, 0, _graphics.PreferredBackBufferWidth - _player.Width * playerScale);
        _playerPosition.Y = MathHelper.Clamp(_playerPosition.Y, 0, _graphics.PreferredBackBufferHeight - _player.Height * playerScale);



        if (_shurikenPosition.X <= 0 || _shurikenPosition.X + _shuriken.Width * 0.5f >= _graphics.PreferredBackBufferWidth)
        {
            _shurikenVelocity.X *= -1;
        }
        if (_shurikenPosition.Y <= 0 || _shurikenPosition.Y + _shuriken.Height * 0.5f >= _graphics.PreferredBackBufferHeight)
        {
            _shurikenVelocity.Y *= -1;
        }
        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        float playerScale = 1f;
        _spriteBatch.Draw(_gameBackround, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_starSprite, new Vector2(800, 300), Color.White);
        _spriteBatch.Draw(_shuriken, _shurikenPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        _spriteBatch.DrawString(_font, _output, new Vector2(20, 20), Color.Beige);
        _spriteBatch.Draw(_player, _playerPosition, null, Color.White, 0f, Vector2.Zero, playerScale, SpriteEffects.None, 0f);
        _walkingAnimation.Draw(_spriteBatch, new Vector2(20, 0), SpriteEffects.None);
        _walkingAnimation2.Draw(_spriteBatch, new Vector2(700, 0), SpriteEffects.None);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}