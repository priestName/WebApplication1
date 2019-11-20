using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            new Class1().Main();

            Console.ReadLine();
        }
        public class HouseData
        {
            public float Size { get; set; }
            public float Price { get; set; }
        }

        public class Prediction
        {
            [ColumnName("Score")]
            public float Price { get; set; }
        }

        static void Main1()
        {
            MLContext mlContext = new MLContext();

            // 1. Import or create training data
            HouseData[] houseData = {
               new HouseData() { Size = 0.1F, Price = 6.548F },
               new HouseData() { Size = 5F, Price = 6.248F },
               new HouseData() { Size = 50F, Price = 6.048F },
               new HouseData() { Size = 100F, Price = 5.548F } };
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseData);

            // 2. Specify data preparation and model training pipeline
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 100));

            // 3. Train model
            var model = pipeline.Fit(trainingData);

            // 4. Make a prediction
            var size = new HouseData() { Size = 94 };
            var price = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(model).Predict(size);

            Console.WriteLine($"Predicted price for size: {size.Size * 1000} sq ft= {price.Price * 100:C}k");

            // Predicted price for size: 2500 sq ft= $261.98k

            Console.ReadLine();
        }
    }
}
