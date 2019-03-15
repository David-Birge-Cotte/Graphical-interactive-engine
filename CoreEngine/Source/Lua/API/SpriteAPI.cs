using System;
using CoreEngine.ECS;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class SpriteAPI
    {
        [MoonSharpHidden]
        public Sprite Sprite;
        
        [MoonSharpHidden]
        public SpriteAPI(Sprite spr)
        {
            Sprite = spr;
        }

        public void RandomizeColor()
        {
            Sprite.Color = Noise.RandomGaussianColor();
        }
    }
}