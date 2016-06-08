using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XFormDiscovery603B
{
    public class ColorView : ContentView
    {
        

        string colorName;
        ColorTypeConverter colorTypeConv = new ColorTypeConverter();
        public ColorView()
        {

        }
        public void ReSet()
        {
            //InitializeComponent();
            Color color = (Color)colorTypeConv.ConvertFrom(colorName);
            Content = new Frame
            {
                OutlineColor = Color.Accent,
                Content = new StackLayout
                {
                    //Orientation = HorizontalOptions.
                    Children = {

                          new BoxView {
                             Color = color,
                             WidthRequest = 70, HeightRequest = 70
                        },
                          new StackLayout {
                        Children =
                            {
                                new Label {
                                    Text = colorName,
                                 FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                   // HorizontalOptions = LayoutOptions.StartAndExpand
                                },new Label {
                                    Text=String.Format("{0:X2}-{1:X2}-{2:X2}",
                                                     (int)(255 * color.R),
                                                     (int)(255 * color.G),
                                                     (int)(255 * color.B)),
                                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                    FontAttributes = FontAttributes.Bold,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                   // HorizontalOptions = LayoutOptions.StartAndExpand
                                },

                            }
                        }

                     }
                }

            };

            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
        }

        public string ColorName
        {
            set
            {
                // Set the name.
                colorName = value;

                ReSet();

                //colorNameLabel.Text = value;

                //// Get the actual Color and set the other views.
                //Color color = (Color)colorTypeConv.ConvertFrom(colorName);
                //boxView.Color = color;
                //colorValueLabel.Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                //                                     (int)(255 * color.R),
                //                                     (int)(255 * color.G),
                //                                     (int)(255 * color.B));
            }
            get
            {
                return colorName;
            }
        }
    }
}
