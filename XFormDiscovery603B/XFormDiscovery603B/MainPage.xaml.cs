using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFormDiscovery603B
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();

            //TextDemo();
            //FormattedTextDemo();
            //StackLayoutDemo();
            ScrollingDemo();
        }

        private void StackLayoutDemo()
        {
            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5); double fontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    new Label {
                    Text = "White", TextColor = Color.White, FontSize = fontSize
                    },
                    new Label {
                    Text = "Silver", TextColor = Color.Silver, FontSize = fontSize
                    }, new Label {
                    Text = "Fuchsia", TextColor = Color.Fuchsia, FontSize = fontSize
                    },
                    new Label {
                    Text = "Purple", TextColor = Color.Purple, FontSize = fontSize
                    }
                }
            };
        }

        private void FormattedTextDemo()
        {
            Content = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans = {
                            new Span {
                            Text = "I "},
                            new Span {
                            Text = "love", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), FontAttributes = FontAttributes.Bold
                            },
                            new Span {
                            Text = " Xamarin.Forms!" } }
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
        }

        void TextDemo()
        {
            Content = new Label
            {
                Text = "Greetings, Xamarin.Forms!",

                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

                BackgroundColor = Color.Yellow,
                TextColor = Color.Blue,

            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold | FontAttributes.Italic


            };

            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
        }
        void ScrollingDemo()
        {

            StackLayout stackLayout = new StackLayout();
            // Loop through the Color structure fields. 
            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                // Skip the obsolete (i.e. misspelled) colors. 
            
                if (info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorLabel((Color)info.GetValue(null), info.Name));
                }
            }
            // Loop through the Color structure properties.
            foreach (PropertyInfo info in typeof(Color).GetRuntimeProperties())
            {
                MethodInfo methodInfo = info.GetMethod;
                if (methodInfo.IsPublic && methodInfo.IsStatic && methodInfo.ReturnType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorLabel((Color)info.GetValue(null), info.Name));
                }
            }
            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            // Put the StackLayout in a ScrollView. 
            Content = new ScrollView
            {
                Content = stackLayout
            };

        }
        Label CreateColorLabel(Color color, string name)
        {
            Color backgroundColor = Color.Default;
            if (color != Color.Default)
            {
                // Standard luminance calculation.
                double luminance = 0.30 * color.R + 0.59 * color.G + 0.11 * color.B;
                backgroundColor = luminance > 0.5 ? Color.Black : Color.White;
            }
            // Create the Label.
            return new Label
            {
                Text = name,
                TextColor = color,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                BackgroundColor = backgroundColor
            };
        }
        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = XForm.Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "  " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    dialer.Dial(translatedNumber);
            }
        }
    }
}
