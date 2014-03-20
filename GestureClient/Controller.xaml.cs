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

namespace GestureClient
{
    public partial class Controller : PhoneApplicationPage
    {
        private TranslateTransform translateTransform;

        public Controller()
        {
            InitializeComponent();
            translateTransform = new TranslateTransform();
            myRectangle.RenderTransform = translateTransform;
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            translateTransform.X += e.HorizontalChange;
            translateTransform.Y += e.VerticalChange;

        }

        private void Add_Button(object sender, System.EventArgs e)
        {

        }
    }
}