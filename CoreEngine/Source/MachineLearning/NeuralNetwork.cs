using System;
using System.Collections.Generic;

namespace CoreEngine.MachineLearning
{
    public class NeuralNetwork
    {
        #region Members
        int inputs; // number of inputs of the neural net
        int outputs; // number of output of the neural net
        int hiddenNodes; // number of neurons in hidden layer
        int numHiddenLayers = 1;
        List<MatrixF> weightMatrixes;
        List<MatrixF> biasMatrixes;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Neural Network
        /// </summary>
        /// <param name="_inputs">number of inputs</param>
        /// <param name="_outputs">number of output</param>
        /// <param name="_hiddenNodes">number of neurons per hidden layer</param>
        public NeuralNetwork(int _inputs, int _hiddenNodes, int _outputs, int _numHiddenLayers = 1)
        {
            inputs = _inputs;
            outputs = _outputs;
            hiddenNodes = _hiddenNodes;
            numHiddenLayers = _numHiddenLayers;

            // matrix of weights to use with layers
            weightMatrixes = new List<MatrixF>();
            weightMatrixes.Add(MatrixF.RandomMatrix(hiddenNodes, inputs));
            for (int i = 0; i < _numHiddenLayers - 1; i++)
                weightMatrixes.Add(MatrixF.RandomMatrix(hiddenNodes, hiddenNodes));
            weightMatrixes.Add(MatrixF.RandomMatrix(outputs, hiddenNodes));

            // very tall matrix of bias to use with layers
            biasMatrixes = new List<MatrixF>();
            for (int i = 0; i < _numHiddenLayers; i++)
                biasMatrixes.Add(MatrixF.RandomMatrix(hiddenNodes, 1));
            biasMatrixes.Add(MatrixF.RandomMatrix(outputs, 1));
        }
        #endregion

        /// <summary>
        /// Returns outputs based on inputs going through the neural network
        /// </summary>
        /// <param name="_inputs"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] _inputs)
        {
            // create matrix from inputs
            List<MatrixF> m = new List<MatrixF>();
            m.Add(MatrixF.FromArray(_inputs));

            // calculate hidden layers outputs
            for (int i = 0; i < numHiddenLayers; i++)
            {
                m.Add(MatrixF.DotProduct(weightMatrixes[i], m[i]));
                m[i + 1].Add(biasMatrixes[i]);
                Activate(m[i + 1]);
            }

            // calculate output layer output
            m.Add(MatrixF.DotProduct(weightMatrixes[weightMatrixes.Count - 1], m[m.Count - 1]));
            m[m.Count - 1].Add(biasMatrixes[biasMatrixes.Count - 1]);
            Activate(m[m.Count - 1]);

            return (MatrixF.ToArray(m[m.Count - 1]));
        }

        /// <summary>
        /// Trains the model with a complex algorithm and a shit load of data
        /// </summary>
        /// <param name="_inputs"></param>
        /// <param name="_outputs"></param>
        public void Train(float[] _inputs, float[] _outputs)
        {
            // No need for training when there's Darwinian evolution ;)
        }

        /// <summary>
        /// Maps the matrix to the sigmoid activation function
        /// </summary>
        /// <param name="_m"></param>
        void Activate(MatrixF _m)
        {
            for (int i = 0; i < _m.n; i++)
                for (int j = 0; j < _m.m; j++)
                    _m.data[i, j] = Sigmoid(_m.data[i, j]) * 2f - 1f; // final value between -1f and +1f
        }

        /// <summary>
        /// Does a simple mutation of each weight by a small value
        /// </summary>
        public void SimpleMutate(int _percentage = 30)
        {
            for (int i = 0; i < weightMatrixes.Count; i++)
                weightMatrixes[i] = MatrixF.Mutate(weightMatrixes[i], _percentage);
            for (int i = 0; i < biasMatrixes.Count; i++)
                biasMatrixes[i] = MatrixF.Mutate(biasMatrixes[i], _percentage);
        }

        #region Static Functions
        /// <summary>
        /// Sigmoid activation function, returns a value between 0f and 1f
        /// </summary>
        /// <param name="_x"></param>
        /// <returns></returns>
        public static float Sigmoid(float _x)
        {
            return (1 / (1 + (float)Math.Exp(-_x)));
        }

        /// <summary>
        /// Creates a new NeuralNetwork based on an instance. 
        /// </summary>
        /// <param name="_nn"></param>
        /// <returns></returns>
        public static NeuralNetwork Copy(NeuralNetwork _nn)
        {
            NeuralNetwork nn2 = new NeuralNetwork(
                _nn.inputs, _nn.hiddenNodes, _nn.outputs, _nn.numHiddenLayers);
            List<MatrixF> weights = new List<MatrixF>();
            List<MatrixF> bias = new List<MatrixF>();

            for (int i = 0; i < _nn.weightMatrixes.Count; i++)
                weights.Add(MatrixF.Copy(_nn.weightMatrixes[i]));
            for (int i = 0; i < _nn.biasMatrixes.Count; i++)
                bias.Add(MatrixF.Copy(_nn.biasMatrixes[i]));
            nn2.weightMatrixes = weights;
            nn2.biasMatrixes = bias;
            return nn2;
        }
        #endregion
    }
}
