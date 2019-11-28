using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ConsoleApp1
{
    class Class3
    {
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

        public void main()
        {
            MLContext mlContext = new MLContext();

            // 1. 导入或创建培训数据
            HouseData[] houseData = {
               new HouseData() { Size = 1F, Price = 5F },
               new HouseData() { Size = 2F, Price = 4.7F },
               new HouseData() { Size = 3F, Price = 4.2F },
               new HouseData() { Size = 4F, Price = 3.4F } };
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseData);

            // 2. 指定数据准备模型和培训对象
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Size" })
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Price", maximumNumberOfIterations: 1));

            // 3. 陈列模型
            var model = pipeline.Fit(trainingData);

            // 4. 做出预测
            var size = new HouseData() { Size = 2.5F };
            var price = mlContext.Model.CreatePredictionEngine<HouseData, Prediction>(model).Predict(size);

            Console.WriteLine($"Predicted price for size: {size.Size * 1000} sq ft= {price.Price * 100:C}k");

            // Predicted price for size: 2500 sq ft= $261.98k
        }
    }
}

