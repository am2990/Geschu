using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GestureClient
{
    public class ShapeProperty
    {
        Shape shape;
        public enum ShapeType {Rectangle, Circle};
        ShapeType type;
        Brush color;
        double row, column;
        double width, height;
        string value;
        public Transform transform;
        
        public ShapeProperty(Shape shape , double pos_x, double pos_y, string value ) 
        {
            this.shape = shape;
            this.color = shape.Fill;
            this.width = shape.Width;
            this.height = shape.Height;
            this.row = pos_x;
            this.column = pos_y;
            this.value = value;
            this.type = (shape is Rectangle) ? ShapeType.Rectangle : ShapeType.Circle;
        }

        public ShapeProperty(Shape shape,Transform transform, string value)
        {
            this.shape = shape;
            this.color = shape.Fill;
            this.width = shape.Width;
            this.height = shape.Height;
            this.transform = transform;
            this.value = value;
            this.type = (shape is Rectangle) ? ShapeType.Rectangle : ShapeType.Circle;
        }
        public double GetRow()
        {
            return this.row;
        }
        public double GetColumn()
        {
            return this.column;
        }
        public ShapeType GetType()
        {
            return this.type;
        }
        public Shape GetShape()
        {
            return this.shape;
        }

        
        public void Save() { }

        
    }
}
