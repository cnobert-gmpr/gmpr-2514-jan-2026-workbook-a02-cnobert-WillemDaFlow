using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson7Pong;

public class Brick
{
    private Vector2 _position, _dimensions;
    private Color _color;
    private bool _isActive;

    public bool IsActive => _isActive;

    internal Rectangle BoundingBox
    {
        get => new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
    }

    internal void Initialize(Vector2 position, Vector2 dimensions, Color color)
    {
        _position = position;
        _dimensions = dimensions;
        _color = color;
        _isActive = true;
    }

    internal void Update(Ball ball)
    {
        if (_isActive && BoundingBox.Intersects(ball.BoundingBox))
        {
            
            ball.ProcessCollision(BoundingBox);
            
            _isActive = false;
        }
    }

    internal void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        if (_isActive)
        {
            spriteBatch.Draw(pixelTexture, BoundingBox, _color);
        }
    }
}