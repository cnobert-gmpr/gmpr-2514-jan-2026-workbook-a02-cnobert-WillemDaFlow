using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson7Pong;

public class Blocker
{
    private Vector2 _position, _dimensions;
    private Color _color;

    public Rectangle BoundingBox => new Rectangle(_position.ToPoint(), _dimensions.ToPoint());

    internal void Initialize(Vector2 position, Vector2 dimensions, Color color)
    {
        _position = position;
        _dimensions = dimensions;
        _color = color;
    }

    internal void Update(Ball ball)
    {
        // If the ball hits this blocker, tell the ball to bounce
        if (BoundingBox.Intersects(ball.BoundingBox))
        {
            // We pass 'this' (the blocker) into a collision processor
            ball.ProcessCollision(this.BoundingBox);
        }
    }

    internal void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        spriteBatch.Draw(pixelTexture, BoundingBox, _color);
    }
}