using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson08MosquitoAttack;

public class Cannon
{
    private const int _NumCannonBalls = 12;
    private SimpleAnimation _animationAlive, _animationPoofing;
    private Vector2 _position, _direction;
    private Point _dimensions;
    private float _speed;
    private Rectangle _gameBoundingBox;
    private CannonBall[] _cBalls;
    private enum State { Alive, Poofing, Dead}
    private State _state;
    internal bool Alive { get => _state == State.Alive; }
    internal Vector2 Direction
    {
        set
        {
            // cannon should only move horizontally
            value.Y = 0;
            _direction = value;
            if(_direction.X < 0)
                _animationAlive.Reverse = true;
            else
                _animationAlive.Reverse = false;
        }
    }

    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)_animationAlive.FrameDimensions.X,
                (int)_animationAlive.FrameDimensions.Y
            );
        }
    }

    internal void Initialize(Vector2 position, float speed, Rectangle gameBoundingBox)
    {
        _position = position;
        _speed = speed;
        _gameBoundingBox = gameBoundingBox;
        _state = State.Alive;

        _cBalls = new CannonBall[_NumCannonBalls];
        for(int c = 0; c < _NumCannonBalls; c++)
        {
            _cBalls[c] = new CannonBall();
            _cBalls[c].Initialize(50, _gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D texture = content.Load<Texture2D>("Cannon");
        _dimensions = new Point(texture.Width / 4, texture.Height);
        _animationAlive = new SimpleAnimation(texture, _dimensions.X, _dimensions.Y, 4, 2);
        texture = content.Load<Texture2D>("Poof");
        _animationPoofing = new SimpleAnimation(texture, texture.Width / 8, texture.Height, 8, 8);
        foreach(CannonBall c in _cBalls)
            c.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        


        switch(_state)
        {
            case State.Alive:
                _position += _direction * _speed * dt;
                _animationAlive.Update(gameTime);
                if(_direction != Vector2.Zero)
                _animationAlive.Update(gameTime);
                foreach(CannonBall c in _cBalls)
                c.Update(gameTime);
                break;
            case State.Poofing:
                _animationPoofing.Update(gameTime);
                if (_animationPoofing.DonePlayingOnce)
                {
                    _state = State.Dead;
                }
                break;
            case State.Dead:
                break;
        }

        _animationAlive.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            
            case State.Alive:
                _animationAlive.Draw(spriteBatch, _position, SpriteEffects.None);
                if(_animationAlive != null)
                _animationAlive.Draw(spriteBatch, _position, SpriteEffects.None);
                foreach(CannonBall c in _cBalls)
                c.Draw(spriteBatch);
                break;
            case State.Poofing:
                _animationPoofing.Draw(spriteBatch, _position, SpriteEffects.None);
                break;
            case State.Dead:
                break;
        }
        
    }
    internal void Shoot()
    {
        foreach(CannonBall c in _cBalls)
        {
            if(c.Launchable)
            {
                float cannonBallPositionY = BoundingBox.Top - c.BoundingBox.Height;
                float cannonBallPositionX = BoundingBox.Center.X - c.BoundingBox.Width / 2;
                Vector2 cannonBallPosition = new Vector2(cannonBallPositionX, cannonBallPositionY);
                c.Launch(cannonBallPosition, new Vector2(0, -1));
                return; //break;
            }
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        foreach(CannonBall c in _cBalls)
        {
            if (c.ProcessCollision(boundingBox))
            {
                // with this code, only 1 cannonball can hit something each call to update
                return true;
            }
        }
        return false;
    }
    internal void Die()
    {
        if (Alive)
        {
            _state = State.Poofing;
            _animationPoofing.Looping = false;
        }
    }
}