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
using System.Threading;
using System.Windows.Threading;

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
        private string default_profile_name = "Profile Name";

        public Controller()
        {
            InitializeComponent();
            controller_profile = new Profile("temp_user", 0 , -1);
            profile_name.Text = this.default_profile_name;
            //this.activateBlinker();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string msg;
            profile_name.Text = Static.profile_name;
            if (NavigationContext.QueryString.TryGetValue("profileid", out msg))
            {
                int id = Convert.ToInt32(msg);
                this.load_profile();
            }
            //this.activateBlinker();  
        }

        void activateBlinker()
        {
            string display_text = profile_name.Text;
            Boolean display = true;

            Timer timer = new Timer((obj) =>
            {
                Dispatcher pageDispatcher = obj as Dispatcher;
                
                pageDispatcher.BeginInvoke(() =>
                {
                    if (display)
                    {
                        profile_name.Text = display_text;
                        display = false;
                    }
                    else
                    {
                        profile_name.Text = display_text + "|";
                        display = true;
                    }
                });

            }, this.Dispatcher, 1000, 1000);
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
            this.update_profile();
            this.save_profile();
            this.list_box.Items.Remove(e);
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

        private Shape set_shape_property(Shape shape, int row = 0, int column = 0)
        {    
            shape.Fill = new SolidColorBrush(Colors.Brown);
            shape.Height = 200;
            shape.Width = 200;
            shape.SetValue(Grid.RowProperty, row);
            shape.SetValue(Grid.ColumnProperty, column);
            return shape;
        }

        private void update_profile() 
        {
            if (addition_shape != null)
            {
                controller_profile.add_Shapes(addition_shape,
                    addition_shape.RenderTransform,
                    'A');
            }
        }

        private void save_profile() 
        {
            controller_profile.save();
        }

        private Shape shape_clone(Shapes original)
        {
            Shape clone_shape = new Rectangle();
            Shape original_shape = original.load_shape();
            clone_shape.Fill = original_shape.Fill;
            clone_shape.Height = original_shape.Height;
            clone_shape.Width = original_shape.Width;
            clone_shape.SetValue(Grid.RowProperty, (int) original.get_row());
            clone_shape.SetValue(Grid.ColumnProperty, (int) original.get_column());
            clone_shape = set_shape_transform(clone_shape);
            clone_shape = set_shape_listener(clone_shape);
            clone_shape.RenderTransform = original.transform;
;
            return clone_shape;
        }

        private void load_profile()
        {
            this.controller_profile = Profile.get_profile(0, -1);
            if (this.controller_profile == null)
                return;
            foreach (Shapes profile_shapes in this.controller_profile.get_profile_shapes())
            {
                list_box.Items.Add(shape_clone(profile_shapes));
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
        private void Save_Click(object sender, EventArgs e)
        {
            if ((profile_name.Text == this.default_profile_name) ||
                    (profile_name.Text.Trim().Length == 0))
                MessageBox.Show("Enter a valid name for the profile");
            //Do work for your application here.
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

        void save_profile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pressed!");
        }

        public TransformGroup _addition_shape_transform { get; set; }

        public GestureListener shape_gesture { get; set; }

        public TransformGroup shape_transform { get; set; }

        public Shape profile_shape { get; set; }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void profile_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Static.profile_name = profile_name.Text;
        }
    }
}