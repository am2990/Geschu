using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;

namespace GestureClient
{
    public partial class Controller : PhoneApplicationPage
    {
        //private TranslateTransform translateTransform;
        private TranslateTransform move = new TranslateTransform();
        private ScaleTransform resize = new ScaleTransform();
        private TransformGroup rectangleTransforms = new TransformGroup();
        private Brush stationaryBrush;
        private Brush transformingBrush = new SolidColorBrush(Colors.Orange);

        public Controller()
        {
            InitializeComponent();
            //myRectangle.RenderTransform = rectangleTransforms;
            rectangleTransforms.Children.Add(move);
            rectangleTransforms.Children.Add(resize);
        }

        void Rectangle_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            // Save the original color before changing the color.
            Rectangle rect = sender as Rectangle;
            stationaryBrush = rect.Fill;
            rect.Fill = transformingBrush;
        }

        void Rectangle_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            // Move the rectangle.

            move.X += e.DeltaManipulation.Translation.X;
            move.Y += e.DeltaManipulation.Translation.Y;

            // Resize the rectangle.
            if (e.DeltaManipulation.Scale.X > 0 && e.DeltaManipulation.Scale.Y > 0)
            {
                // Scale the rectangle.
                resize.ScaleX *= e.DeltaManipulation.Scale.X;
                resize.ScaleY *= e.DeltaManipulation.Scale.Y;
            }
        }

        void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            // Restore the original color.
            Rectangle rect = sender as Rectangle;
            rect.Fill = stationaryBrush;
        }

        
        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {

            move.X += e.HorizontalChange;
            move.Y += e.VerticalChange;

        }

        private void GestureListener_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            (sender as Rectangle).Fill = new SolidColorBrush(Colors.White);
        }

        private void Add_Button(object sender, System.EventArgs e)
        {

            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(Colors.Blue);
            rectangle.Height = 200;
            rectangle.Width = 200;
            rectangle.RenderTransform = rectangleTransforms;
            rectangle.SetValue(Grid.RowProperty, 0);
            rectangle.SetValue(Grid.ColumnProperty, 0);

            var gl = GestureService.GetGestureListener(rectangle);
            gl.Tap += new EventHandler<Microsoft.Phone.Controls.GestureEventArgs>(GestureListener_Tap);
            gl.DragDelta += new EventHandler<DragDeltaGestureEventArgs>(GestureListener_DragDelta);
  
    
            rectangle.ManipulationStarted +=
                new EventHandler<ManipulationStartedEventArgs>(Rectangle_ManipulationStarted);
            rectangle.ManipulationDelta +=
                new EventHandler<ManipulationDeltaEventArgs>(Rectangle_ManipulationDelta);
            rectangle.ManipulationCompleted +=
                new EventHandler<ManipulationCompletedEventArgs>(Rectangle_ManipulationCompleted);
            list_box.Items.Add(rectangle);
        }
        static int count;
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Button newButton = new Button();
            newButton.Height = 75;
            newButton.Width = 250;
            
            newButton.Foreground = new SolidColorBrush(Colors.Green);
            newButton.Content = "Dynamically added";
            newButton.Click += new RoutedEventHandler(newButton_Click);

            ContentPanel.Children.Add(newButton);
        }

        void newButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello phone!");
        }
    }
}