using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Game1.Engine;
using Game1.Engine.MachineLearning;
using Game1.GameObjects;

namespace Game1
{
    class Scene02 : Scene
    {
        int step = 512;
        bool isSpacePressed = false; // TODO make a real InputControllerManagerThing
        Perceptron brain;
        Entity[,] pointsInSpace;
        int n = 0;
        int viewSize = 60;

        public Scene02() : base()
        {
            Camera.Zoom = 0.05f;
            Global.BackgroundColor = Noise.RandomGaussianColor();

            pointsInSpace = new Entity[viewSize, viewSize];
            brain = new Perceptron(3);
            InstantiateViewGrid();
            DrawGrid();
        }

        void InstantiateViewGrid()
        {
            for (int x = 0; x < viewSize; x++)
            {
                for (int y = 0; y < viewSize; y++)
                {
                    Entity en = Instantiate(new Entity());
                    en.AddComponent(new Sprite());
                    en.Position = new Vector2(-viewSize / 2 + x, viewSize / 2 - y);
                    en.Scale = new Vector2(0.75f, 0.75f);
                    pointsInSpace[x, y] = en;
                }
            }
        }

        public void DrawGrid()
        {
            Console.WriteLine($"Draw Grid nb {n}");
            for (int x = 0; x < viewSize; x++)
            {
                for (int y = 0; y < viewSize; y++)
                {
                    float[] data = { (x - viewSize / 2f) / 10f, (y - viewSize / 2f) / 10f, 1 };
                    int guess = brain.Process(data);
                    pointsInSpace[x, y].GetComponent<Sprite>().Color = (guess > 0 ? Color.DarkRed : Color.LightBlue);

                    if (x - viewSize / 2 == 0 || y - viewSize / 2 == 0)
                    {
                        pointsInSpace[x, y].GetComponent<Sprite>().Color = Color.AliceBlue;
                        if(x % 10 == 0 && y % 10 == 0)
                            pointsInSpace[x, y].GetComponent<Sprite>().Color = Color.Black;
                        if(x == viewSize - 1 || y == viewSize - 1)
                            pointsInSpace[x, y].GetComponent<Sprite>().Color = Color.Red;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Quick Quit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Global.Game.Exit();

            CameraMovement((float)gameTime.ElapsedGameTime.TotalSeconds);

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && isSpacePressed == false)
            {
                isSpacePressed = true;
                for (int i = 0; i < step; i++)
                {
                    TrainingPoint pt = new TrainingPoint(
                        Noise.Generate(-viewSize / 2f, +viewSize / 2f),
                        Noise.Generate(-viewSize / 2f, +viewSize / 2f));
                    brain.Train(pt.GetData(), pt.label);
                    n++;
                }
                DrawGrid();
                //brain.DebugWeights();
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
                isSpacePressed = false;

            base.Update(gameTime);
        }

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
    }
}
