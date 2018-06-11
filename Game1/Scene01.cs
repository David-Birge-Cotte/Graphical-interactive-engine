using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Game1.Engine;
using Game1.Engine.Physics;
using Game1.GameObjects;

namespace Game1
{
    class Scene01 : Scene
    {
        private int entityNb = 100;
        private List<PhysicsEntity> entities;

        public Scene01() : base()
        {
            camera = new Camera(Global.Game.GraphicsDevice.Viewport, Vector2.Zero, 0, 0.5f);
            entities = new List<PhysicsEntity>();
            RandomFlies();
        }

        public void RandomFlies()
        {
            for (int i = 0; i < entityNb; i++)
            {
                entities.Add((PhysicsEntity)Instantiate(new SteeringEntity()));
                entities[i].Position = new Vector2(Global.Noiser.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2) * 1.5f,
                    Global.Noiser.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2) * 1.5f);
                entities[i].Mass = Global.Noiser.Gaussian(10, 60);
                float scale = entities[i].Mass;
                entities[i].Scale = new Vector2(scale, scale);
            }
        }

        public override void Update(GameTime gameTime)
        {
            /*foreach (Entity phyEn in gameObjects)
            {
                PhysicsEntity en = phyEn as PhysicsEntity;
                if (en != null)
                    en.AddForce(Vector2.UnitX + -Vector2.UnitY * 0.25f);
            }*/

            base.Update(gameTime);
        }
    }
}