using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GestureClient
{
    public class Shapes
    {
        Shape shape;
        public enum ShapeType {Rectangle, Circle};
        ShapeType type;
        Brush color;
        double row, column;
        double width, height;
        char value;
        public Transform transform;
        
        public Shapes(Shape shape , double pos_x, double pos_y, char value ) 
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

        public Shapes(Shape shape,Transform transform, char value)
        {
            this.shape = shape;
            this.color = shape.Fill;
            this.width = shape.Width;
            this.height = shape.Height;
            this.transform = transform;
            this.value = value;
            this.type = (shape is Rectangle) ? ShapeType.Rectangle : ShapeType.Circle;
        }
        public double get_row()
        {
            return this.row;
        }
        public double get_column()
        {
            return this.column;
        }
        public ShapeType get_type()
        {
            return this.type;
        }
        public Shape load_shape()
        {
            return this.shape;
        }

        
        public void save() { }

        
    }
}
