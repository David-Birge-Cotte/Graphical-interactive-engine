using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Game1.Engine;
using Game1.Engine.MachineLearning;
using Game1.GameObjects;

namespace Game1
{
    class NeuralEntities_test01 : Scene
    {
        #region Members
        List<SteeringEntity> steeringEntities;
        uint nbrEntity = 60;
       
        public List<Entity> Food;
        int initialFood = 100;
        int maxFood = 100;

        float time;
        #endregion

        public NeuralEntities_test01() : base()
        {
            Camera.Zoom = 0.05f;
            Global.BackgroundColor = Noise.RandomGaussianColor();

            steeringEntities = new List<SteeringEntity>();
            AddSteeringEntities(nbrEntity);
            Food = new List<Entity>();
            for (int i = 0; i < initialFood; i++)
                InstantiateFood();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += dt;

            // -- Mecanics --
            // Refill food
            AddFood();
            // Core of the scene
            HandleSteeringLife();

            // -- Controls --
            // Camera
            CameraMovement(dt);
            // Spawn
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                steeringEntities.AddRange(InstantiateEntities(20));
            // Quick Quit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Global.Game.Exit();

            base.Update(gameTime);
        }

        #region instantiating steering entities
        void AddSteeringEntities(uint nb)
        {
            steeringEntities.AddRange(InstantiateEntities(nb));
        }

        List<SteeringEntity> InstantiateEntities(uint nb)
        {
            List<SteeringEntity> en = new List<SteeringEntity>();
            for (int i = 0; i < nb; i++)
                en.Add((SteeringEntity)Instantiate(new SteeringEntity()));
            return (en);
        }
        #endregion

        #region SteeringEntities, movement, life and things
        void HandleSteeringLife()
        {
            MoveAndCheckEnergy();
            Eating();
            Reproduction();
        }

        void MoveAndCheckEnergy()
        {
            for (int i = steeringEntities.Count - 1; i >= 0; i--)
            {
                steeringEntities[i].BrainMovement(ClosestFoodFrom(steeringEntities[i].Position));
                if (steeringEntities[i].energy <= 0)
                {
                    Destroy(steeringEntities[i]);
                    steeringEntities.RemoveAt(i);
                }
            }
        }

        void Eating()
        {
            foreach (var en in steeringEntities)
            {
                for (int i = Food.Count - 1; i >= 0; i--)
                {
                    if (Vector2.DistanceSquared(Food[i].Position, en.Position) < 1)
                    {
                        Destroy(Food[i]);
                        Food.RemoveAt(i);
                        en.energy += 5;
                    }
                }
            }
        }

        void Reproduction()
        {
            for (int i = 0; i < steeringEntities.Count; i++)
            {
                if (steeringEntities[i].energy > 10)
                {
                    steeringEntities[i].energy -= 5;

                    SteeringEntity en = (SteeringEntity)Instantiate(new SteeringEntity());
                    en.Brain = NeuralNetwork.Copy(steeringEntities[i].Brain);
                    en.Brain.SimpleMutate();
                    steeringEntities.Add(en);
                    en.Position = steeringEntities[i].Position + new Vector2(Noise.Generate(-1f, 1f));
                }
            }
        }
        #endregion

        #region Food related
        void AddFood()
        {
            while(Food.Count < maxFood)
                InstantiateFood();
        }

        void InstantiateFood()
        {
            Food.Add(Instantiate(new Entity()));
            Food[Food.Count - 1].AddComponent<Sprite>(new Sprite());
            Food[Food.Count - 1].Scale = new Vector2(0.5f, 0.5f);
            Food[Food.Count - 1].Position = new Vector2(
                Noise.Generate(Global.WinWidth) - (Global.WinWidth / 2),
                Noise.Generate(Global.WinHeight) - (Global.WinHeight / 2)) / 10f;
        }
        #endregion

        /// <summary>
        /// Get closest food from a position in the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        Entity ClosestFoodFrom(Vector2 pos)
        {
            float minDist = float.PositiveInfinity;
            int best = 0;

            for (int i = 0; i < Food.Count; i++)
            {
                float dist = Vector2.DistanceSquared(pos, Food[i].Position);
                if (dist < minDist)
                {
                    minDist = dist;
                    best = i;
                }
            }
            return Food[best];
        }

        #region Controls
        private void CameraMovement(float deltaTime)
        {
            if (InputManager.Up)
                Camera.Move(-Vector2.UnitY * deltaTime * 300);
            if (InputManager.Down)
                Camera.Move(Vector2.UnitY * deltaTime * 300);
            if (InputManager.Left)
                Camera.Move(-Vector2.UnitX * deltaTime * 300);
            if (InputManager.Right)
                Camera.Move(Vector2.UnitX * deltaTime * 300);

            if (InputManager.IsScrolling)
                Camera.Zoom += InputManager.ScrollValue * 0.0001f * Camera.Zoom;
        }
        #endregion
    }
}