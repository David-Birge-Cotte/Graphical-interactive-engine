using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameObjects
{
    public class RandomWalker : Entity
    {
        float Speed = 16;

        public RandomWalker(Vector2 position = new Vector2(), float rotation = 0, Vector2 scale = new Vector2()) : base(position, rotation, scale)
        {
            AddComponent<Sprite>(new Sprite());
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            Vector2 moveVector;
			int rngVal = Noise.Generate(4);

            switch (rngVal)
            {
                case 0:
                    moveVector = Vector2.UnitX;
                    break;
                case 1:
                    moveVector = -Vector2.UnitX;
                    break;
                case 2:
                    moveVector = Vector2.UnitY;
                    break;
                case 3:
                    moveVector = -Vector2.UnitY;
                    break;
                default:
                    moveVector = Vector2.Zero;
                    break;
            }
            Position += moveVector * Speed;
        }
    }
}
