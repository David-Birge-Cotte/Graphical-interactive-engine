using System;
using Microsoft.Xna.Framework;
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

        public void SetSortingOrder(float sortOrder)
        {
            Sprite.SortingOrder = sortOrder;
        }

        public float GetSortingOrder()
        {
            return Sprite.SortingOrder;
        }

        public void ChangeColor(ColorAPI color)
        {
            Sprite.Color = color.Color;
        }

        public void ChangeTextureData(ColorAPI[] pixels, int height, int width)
        {
            Color[] data = new Color[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
                data[i] = pixels[i].Color;
            Sprite.ChangeTextureData(data, height, width);
        }
    }
}