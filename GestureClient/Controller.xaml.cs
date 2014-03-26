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
        private TranslateTransform move = new TranslateTransform();
        private ScaleTransform resize = new ScaleTransform();
        private Brush stationaryBrush;
        private Brush transformingBrush = new SolidColorBrush(Colors.Orange);
        private Shape addition_shape = null;

        private enum SHAPES_ALLOWED { circle, rectangle };
        private Profile controller_profile = null;

        private static SHAPES_ALLOWED default_shape = SHAPES_ALLOWED.rectangle;

        public Controller()
        {
            InitializeComponent();
            controller_profile = new Profile("temp_user", 0 , -1);
        }

        protected override void onNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string msg;
            if (NavigationContext.QueryString.TryGetValue("profileid", out msg))
            {
                int id = ToInt32(msg);
            }  
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
            NavigationService.Navigate(new Uri("/ShapesProperties.xaml", UriKind.Relative));
        }

        private Shape set_shape_transform(Shape shape)
        {
            move = new TranslateTransform();
            shape_transform = new TransformGroup();
            shape_transform.Children.Add(move);
            shape_transform.Children.Add(resize);
            shape.RenderTransform = shape_transform;
            return shape;
        }

        private Shape set_shape_listener(Shape shape)
        {
            shape_gesture = GestureService.GetGestureListener(shape);
            shape_gesture.Tap += new EventHandler<Microsoft.Phone.Controls.GestureEventArgs>(GestureListener_Tap);
            shape_gesture.DragDelta += new EventHandler<DragDeltaGestureEventArgs>(GestureListener_DragDelta);
            return shape;
        }

        private Shape set_shape_property(Shape shape)
        {    
            shape.Fill = new SolidColorBrush(Colors.Brown);
            shape.Height = 200;
            shape.Width = 200;
            shape.SetValue(Grid.RowProperty, 0);
            shape.SetValue(Grid.ColumnProperty, 0);
            return shape;
        }

        private void update_profile() 
        {
            if (addition_shape != null)
            {
                controller_profile.add_Shapes(addition_shape, 
                    addition_shape.TransformToVisual(list_box).Transform(new Point(0,0)),
                    'A');
            }
        }

        private void save_profile() 
        {
            controller_profile.save();
        }

        private void load_profile()
        {
            Profile current_profile = controller_profile.get_profile(0,-1);
            if (current_profile == null)
                return;
            foreach (Shapes profile_shapes in current_profile.get_profile_shapes())
            {
                profile_shape = profile_shapes.load_shape();
            }
        }

        private void Add_Button(object sender, System.EventArgs e)
        {
            this.update_profile();
            if (default_shape == SHAPES_ALLOWED.rectangle)
            {
                addition_shape = new Rectangle();
            }
            else if (default_shape == SHAPES_ALLOWED.circle)
            {
                addition_shape = new Ellipse();
            }
            addition_shape = set_shape_property(addition_shape);
            addition_shape = set_shape_transform(addition_shape);
            addition_shape = set_shape_listener(addition_shape);
            

          
        /*
            rectangle.ManipulationStarted +=
                new EventHandler<ManipulationStartedEventArgs>(Rectangle_ManipulationStarted);
            rectangle.ManipulationDelta +=
                new EventHandler<ManipulationDeltaEventArgs>(Rectangle_ManipulationDelta);
            rectangle.ManipulationCompleted +=
                new EventHandler<ManipulationCompletedEventArgs>(Rectangle_ManipulationCompleted); */
            list_box.Items.Add(addition_shape);
        }
        
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

        public TransformGroup _addition_shape_transform { get; set; }

        public GestureListener shape_gesture { get; set; }

        public TransformGroup shape_transform { get; set; }

        public Shape profile_shape { get; set; }
    }
}