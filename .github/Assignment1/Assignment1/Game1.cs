using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardState _kbPreviousState, _kbCurrentState;
    private string _message = "";
    private SpriteFont _font;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("SystemArialFont");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        _kbCurrentState = Keyboard.GetState();
        _message = "";

        #region arrow keys
        
        if(_kbCurrentState.IsKeyDown(Keys.Up))
        {
            _message += "Up ";
        }

        if(_kbCurrentState.IsKeyDown(Keys.Down))
        {
            _message += "Down ";
        }

        if(_kbCurrentState.IsKeyDown(Keys.Left))
        {
            _message += "Left ";
        }

        if(_kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += "Right ";
        }

        #endregion arrow keys
        if(IsKeyHeld(Keys.Space))
        {
            _message += "\n";
            _message += "Space pressed\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
            _message += "----------------------------------------\n";
        }

        else if(_kbPreviousState.IsKeyUp(Keys.Space))
        {
            _message += "\n";
            _message += "Space not pressed";
        }

        _kbPreviousState = _kbCurrentState;
        base.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
    private bool IsKeyHeld(Keys key)
    {
        return _kbCurrentState.IsKeyDown(key);
    }
}
