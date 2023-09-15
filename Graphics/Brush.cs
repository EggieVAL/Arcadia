using System;

namespace Arcadia.Graphics
{
    /// <summary>
    /// The <see cref="Brush"/> class is a representation of a brush. A brush paints a digital canvas
    /// with ink in some shape. Inks are represented by an integer value, where different inks have
    /// different values. Shapes are represented by a 2D integer array. Negative values in the 2D
    /// array are ignored, and are considered not part of the shape.
    /// </summary>
    public sealed class Brush
    {
        /// <summary>
        /// Inks are represented by an integer value, where different inks have different values.
        /// </summary>
        public int[] Inks
        {
            get => (int[]) Inks.Clone();
        }

        /// <summary>
        /// Weights determine how frequent an ink will appear when painting. A higher weight value,
        /// the more the ink will be painted.
        /// </summary>
        public int[] Weights
        {
            get => (int[]) Weights.Clone();
        }

        /// <summary>
        /// Shapes are represented by a 2D integer array. Negative values in the 2D array are ignored,
        /// and are considered not part of the shape.
        /// </summary>
        public int[,] Shape
        {
            get => (int[,]) _shape.Clone();
        }

        /// <summary>
        /// Constructs a brush that paints with one ink one pixel at a time.
        /// </summary>
        /// <param name="ink">An integer representation of an ink.</param>
        public Brush(int ink)
        {
            _inks = new int[1];
            _inks[0] = ink;

            _weights = new int[1];
            _weights[0] = 1;

            _shape = new int[1, 1];
            _shape[0, 0] = 1;

            CheckArguments();
        }

        /// <summary>
        /// Constructs a brush that paints with one ink in some shape.
        /// </summary>
        /// <param name="ink">An integer representation of an ink.</param>
        /// <param name="shape">A 2D integer array representation of a shape.</param>
        public Brush(int ink, int[,] shape)
        {
            _inks = new int[1];
            _inks[0] = ink;

            _weights = new int[1];
            _weights[0] = 1;

            _shape = (int[,]) shape.Clone();

            CheckArguments();
        }

        /// <summary>
        /// Constructs a brush that paints with multiple inks one pixel at a time.
        /// </summary>
        /// <param name="inks">A list of integers representing inks.</param>
        public Brush(int[] inks)
        {
            _inks = (int[]) inks.Clone();

            int numberOfInks = _inks.Length;
            _weights = new int[numberOfInks];
            for (int i = 0; i < numberOfInks; ++i)
            {
                _weights[i] = 1;
            }

            _shape = new int[1, 1];
            _shape[0, 0] = 1;

            CheckArguments();
        }

        /// <summary>
        /// Constructs a brush that paints with multiple inks one pixel at a time.
        /// </summary>
        /// <param name="inks">A list of integers representing inks.</param>
        /// <param name="weights">The weights of each ink.</param>
        public Brush(int[] inks, int[] weights)
        {
            _inks = (int[]) inks.Clone();
            _weights = (int[]) weights.Clone();

            _shape = new int[1, 1];
            _shape[0, 0] = 1;

            CheckArguments();
        }

        /// <summary>
        /// Constructs a brush that paints with multiple inks in some shape.
        /// </summary>
        /// <param name="inks">A list of integers representing inks.</param>
        /// <param name="weights">The weights of each ink.</param>
        /// <param name="shape">A 2D integer array representation of a shape.</param>
        public Brush(int[] inks, int[] weights, int[,] shape)
        {
            _inks = (int[]) inks.Clone();
            _weights = (int[]) weights.Clone();
            _shape = (int[,]) shape.Clone();

            CheckArguments();
        }

        /// <summary>
        /// Simulates painting with this brush.
        /// </summary>
        /// <returns>Returns what the brush painted in a 2D integer array.</returns>
        public int[,] Paint()
        {
            int[,] shape = Shape;
            int[] thresholds = GetWeightThresholds();

            for (int x = 0; x < shape.GetLength(0); ++x)
            {
                for (int y = 0; y < shape.GetLength(1); ++y)
                {
                    HandleInkDistribution(shape, thresholds, x, y);
                }
            }

            return shape;
        }

        private void HandleInkDistribution(int[,] shape, int[] thresholds, int x, int y)
        {
            if (shape[x, y] < 0)
            {
                return;
            }

            int numberOfInks = _inks.Length;
            int numberGenerated = UniversalRandom.Next(1, thresholds[numberOfInks - 1]);

            for (int i = 0; i < numberOfInks; ++i)
            {
                if (numberGenerated <= thresholds[i])
                {
                    shape[x, y] = _inks[i];
                }
            }
        }

        private int[] GetWeightThresholds()
        {
            int numberOfWeights = _weights.Length;
            int[] thresholds = new int[numberOfWeights];

            thresholds[0] = _weights[0];
            for (int i = 1; i < numberOfWeights; ++i)
            {
                thresholds[i] = thresholds[i - 1] + _weights[i];
            }

            return thresholds;
        }

        private void CheckArguments()
        {
            if (_inks.Length != _weights.Length)
            {
                throw new ArgumentException("There must be the same number of inks and weights.");
            }
            if (_shape.GetLength(0) == 0 || _shape.GetLength(1) == 0)
            {
                throw new ArgumentException("The shape cannot be empty");
            }
        }

        private readonly int[] _inks;
        private readonly int[] _weights;

        private readonly int[,] _shape;
    }
}
