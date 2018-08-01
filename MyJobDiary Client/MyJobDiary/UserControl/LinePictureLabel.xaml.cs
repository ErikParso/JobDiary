using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.UserControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinePictureLabel : ContentView
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            "Color", typeof(Color), typeof(LinePictureLabel), Color.Red);
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            "Icon", typeof(string), typeof(LinePictureLabel), string.Empty);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text", typeof(string), typeof(LinePictureLabel), string.Empty);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public LinePictureLabel()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}