﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        public Point Center { get; set; }
        public List<Rectangle> Rectangles = new List<Rectangle>();
        private double _angle;
        private readonly int _step = 1;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0
                || rectangleSize.Height <= 0)
                throw new ArgumentException();

            var rectangle = new Rectangle(FindNewRectanglePosition(), rectangleSize);

            while (HaveIntersection(rectangle)) rectangle.Location = FindNewRectanglePosition();

            Rectangles.Add(rectangle);
            return rectangle;
        }

        public Point FindNewRectanglePosition(double deltaAngle = 0.1)
        {
            _angle += deltaAngle;
            var k = _step / (2 * Math.PI);
            var radius = k * _angle;

            var position = new Point(
                Center.X + (int)(Math.Cos(_angle) * radius),
                Center.Y + (int)(Math.Sin(_angle) * radius));

            return position;
        }

        public bool HaveIntersection(Rectangle newRectangle) =>
            Rectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle));
    }
}
