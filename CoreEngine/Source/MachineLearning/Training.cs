using System;

namespace CoreEngine.MachineLearning
{
    public class TrainingPoint
    {
        public float x, y;
        public int label;

        float bias = 1;

        public TrainingPoint(float x_, float y_)
        {
            x = x_;
            y = y_;
            Initialize();
        }

        public TrainingPoint()
        {
            x = Noise.Generate(-1f, 1f);
            y = Noise.Generate(-1f, 1f);
            Initialize();
        }

        void Initialize()
        {
            label = (y > FLineY(x) ? 1 : -1);
        }

        float FLineY(float x_)
        {
            float y_ = 2 * x_ - 1;
            return (y_);
        }

        public float[] GetData()
        {
            float[] ret = { x, y, bias };
            return ret;
        }
    }
}
