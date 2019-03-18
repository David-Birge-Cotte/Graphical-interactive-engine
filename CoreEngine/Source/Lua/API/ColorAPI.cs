using System;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class ColorAPI
    {
        [MoonSharpHidden]
        public Color Color;

        public static ColorAPI NewColor(byte r = 255, byte g = 255, byte b = 255, byte a = 255)
        {
            var newColor = new ColorAPI();
            newColor.Color = new Color(r, g, b, a);
            return newColor;
        }

        public static ColorAPI RandomColor()
        {
            var colAPI = ColorAPIFromColor(Noise.RandomColor());
            return (colAPI);
        }

        public static ColorAPI RandomGaussianColor()
        {
            var colAPI = ColorAPIFromColor(Noise.RandomGaussianColor());
            return (colAPI);
        }

        private static ColorAPI ColorAPIFromColor(Color color)
        {
            var colAPI = NewColor(color.R, color.G, color.B, color.A);
            return (colAPI);
        }
    }
}