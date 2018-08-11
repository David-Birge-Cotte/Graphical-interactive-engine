using System;

namespace Game1.Engine.MachineLearning
{
    class Perceptron
    {
        #region Members
        float[] weights; // Neuron Weights
        float learningRate = 0.002f; // Learning Rate
        #endregion

        #region Constructors
        /// <summary>
        /// New perceptron with n inputs
        /// </summary>
        /// <param name="n">number of weights</param>
        public Perceptron(int n)
        {
            weights = new float[n];
            // Initialize weights at random
            for (int i = 0; i < weights.Length; i++)
                weights[i] = Noise.Generate(-1f, 1f);
        }

        /// <summary>
        /// New perceptron with n inputs
        /// </summary>
        /// <param name="n">number of weights</param>
        /// <param name="lr">learning rate</param>
        public Perceptron(int n, float lr)
        {
            learningRate = lr;
            weights = new float[n];
            // Initialize weights at random
            for (int i = 0; i < weights.Length; i++)
                weights[i] = Noise.Generate(-1f, 1f);
        }
        #endregion

        public int Process(float[] inputs)
        {
            // TODO: what if inputs.lenght != weights.lenght ?
            float sum = FeedForward(inputs);
            return Activate(sum);
        }

        /// <summary>
        /// FeedForward algorithm
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float FeedForward(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Length; i++)
                sum += inputs[i] * weights[i];
            return sum;
        }

        /// <summary>
        /// Makes the output go through a custom function
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        int Activate(float sum)
        {
            return MathHelp.Sign(sum);
        }

        /// <summary>
        /// Trains the perceptron to give correct answer based on input data and correct answer
        /// </summary>
        /// <param name="inputs">Input data array. Must be the same size as the perceptrons weights array</param>
        /// <param name="target">Desired output from the perceptron. Needed to correct errors</param>
        public void Train(float[] inputs, int target)
        {
            int guess = Process(inputs);
            int error = target - guess;

            // Tune the weights according to errors
            for (int i = 0; i < weights.Length; i++)
                weights[i] += error * inputs[i] * learningRate;
        }

        public void PrintWeights()
        {
            Console.WriteLine("Perceptron weights: ");
            for (int i = 0; i < weights.Length; i++)
                Console.Write($"w[{i}]={weights[i]} ");
        }
    }
}
