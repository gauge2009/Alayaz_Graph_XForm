using System;
using System.Collections.Generic;
using System.IO;
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

            ///关于文本
            ///
            //TextDemo();
            //FormattedTextDemo();


            ///关于布局
            ///
            //StackLayoutDemo();
            // ScrollingDemo();
            //FrameDemo();
            //BoxViewDemo();
            //ScrollViewInStackLayoutDemo ();


            ///关于尺寸
            ///
            // 见 C:\T2_16\Graph\Xamarin\src\Charles_Petzold\Chapter05


            ///关于按钮
            ///
            // ButtonDemo();
            //KeypadPersistentDemo();


             
            /// Xaml 与 CodeBehind 混搭(综合实例)
            XamlDemo();
            /// 纯 Xaml  
            //见 ScaryColorListPage.xaml
            ///Code and XAML in harmony  ：  Custom XAML-based views  
            //见 ColorView.xaml
            /// Xaml 与 TapGestureRecognizer Tapped="OnBoxViewTapped" 
            //见Charles_Petzold\Chapter08\MonkeyTap\MonkeyTap.sln



            /// 平台项目（XFormDiscovery603B.Droid / XFormDiscovery603B.iOS / XFormDiscovery603B.Windows）中的类如何在PCL中使用？
            //见Charles_Petzold\Chapter09\MonkeyTapWithSound\MonkeyTapWithSound.sln


            ///使用静态资源 乃至 资源字典
            //见C:\T2_16\Graph\Xamarin\src\Charles_Petzold\Chapter10



            ///定义可绑定的元素/属性~类比WPF的可观察对象及改变通知机制
            //见C:\T2_16\Graph\Xamarin\src\Charles_Petzold\Chapter11\PointSizedText\PointSizedText.sln
            //另见C:\T2_16\Graph\Xamarin\src\Charles_Petzold\Libraries\Xamarin.FormsBook.Toolkit\Xamarin.FormsBook.Toolkit














            /// Xamarin.FormsBook.Toolkit  见C:\T2_16\Graph\Xamarin\src\Charles_Petzold\Libraries\Xamarin.FormsBook.Toolkit\Xamarin.FormsBook.Toolkit
            //较具有参考价值的架构元件
        }

        private void xxx()
        {



        }
        private void xxxx()
        {



        }
        private void XamlDemo()
        {

            // Create a vertical stack for the entire keypad.
            StackLayout mainStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            // First row is the Label.
            displayLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.End
            };
            mainStack.Children.Add(displayLabel);

            // Second row is the backspace Button.
            backspaceButton = new Button
            {
                Text = "\u21E6",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                IsEnabled = false
            };
            backspaceButton.Clicked += OnBackspaceButtonClicked;
            mainStack.Children.Add(backspaceButton);

            // Now do the 10 number keys.
            StackLayout rowStack = null;

            for (int num = 1; num <= 10; num++)
            {
                if ((num - 1) % 3 == 0)
                {
                    rowStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };
                    mainStack.Children.Add(rowStack);
                }

                Button digitButton = new Button
                {
                    Text = (num % 10).ToString(),
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                    StyleId = (num % 10).ToString()
                };
                digitButton.Clicked += OnDigitBtnClicked;

                // For the zero button, expand to fill horizontally.
                if (num == 10)
                {
                    digitButton.HorizontalOptions = LayoutOptions.FillAndExpand;
                }
                rowStack.Children.Add(digitButton);
            }


            #region Xaml 与 CodeBehind 混搭
            /*********************************************
             *********************************************
             *********************************************/
             (Content as StackLayout).Children.Insert(1, mainStack);
            /*********************************************
             *********************************************
             *********************************************/
            #endregion


            // New code for loading previous keypad text.
            App app = Application.Current as App;
            displayLabel.Text = app.DisplayLabelText;
            backspaceButton.IsEnabled = displayLabel.Text != null &&
                                        displayLabel.Text.Length > 0;

        


        }
        void OnDigitBtnClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            displayLabel.Text += (string)button.StyleId;
            backspaceButton.IsEnabled = true;

            // Save keypad text.
            App app = Application.Current as App;
            app.DisplayLabelText = displayLabel.Text;
        }
        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = XForm.Core.PhonewordTranslator.ToNumber(displayLabel.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "  " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "拨号";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "准备拨号",
                    "您是否继续拨打 " + translatedNumber + "?",
                    "当然",
                    "算了"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    dialer.Dial(translatedNumber);
            }
        }


        Label displayLabel;
        Button backspaceButton;

        public void KeypadPersistentDemo()
        {
            // Create a vertical stack for the entire keypad.
            StackLayout mainStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            // First row is the Label.
            displayLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.End
            };
            mainStack.Children.Add(displayLabel);

            // Second row is the backspace Button.
            backspaceButton = new Button
            {
                Text = "\u21E6",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                IsEnabled = false
            };
            backspaceButton.Clicked += OnBackspaceButtonClicked;
            mainStack.Children.Add(backspaceButton);

            // Now do the 10 number keys.
            StackLayout rowStack = null;

            for (int num = 1; num <= 10; num++)
            {
                if ((num - 1) % 3 == 0)
                {
                    rowStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };
                    mainStack.Children.Add(rowStack);
                }

                Button digitButton = new Button
                {
                    Text = (num % 10).ToString(),
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                    StyleId = (num % 10).ToString()
                };
                digitButton.Clicked += OnDigitButtonClicked;

                // For the zero button, expand to fill horizontally.
                if (num == 10)
                {
                    digitButton.HorizontalOptions = LayoutOptions.FillAndExpand;
                }
                rowStack.Children.Add(digitButton);
            }

            this.Content = mainStack;

            // New code for loading previous keypad text.
            App app = Application.Current as App;
            displayLabel.Text = app.DisplayLabelText;
            backspaceButton.IsEnabled = displayLabel.Text != null &&
                                        displayLabel.Text.Length > 0;

        }

        void OnDigitButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            displayLabel.Text += (string)button.StyleId;
            backspaceButton.IsEnabled = true; 
            
            // Save keypad text.
            App app = Application.Current as App;
            app.DisplayLabelText = displayLabel.Text;
        }

        void OnBackspaceButtonClicked(object sender, EventArgs args)
        {
            string text = displayLabel.Text;
            displayLabel.Text = text.Substring(0, text.Length - 1);
            backspaceButton.IsEnabled = displayLabel.Text.Length > 0;

            // Save keypad text.
            App app = Application.Current as App;
            app.DisplayLabelText = displayLabel.Text;
        }


        Button addButton, removeButton;
        StackLayout loggerLayout = new StackLayout();

        public void ButtonDemo()
        {
            // Create the Button views and attach Clicked handlers.
            addButton = new Button
            {
                Text = "Add",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            addButton.Clicked += OnButtonClicked;

            removeButton = new Button
            {
                Text = "Remove",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsEnabled = false
            };
            removeButton.Clicked += OnButtonClicked;

            this.Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0);

            // Assemble the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            addButton,
                            removeButton
                        }
                    },

                    new ScrollView
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Content = loggerLayout
                    }
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;

            if (button == addButton)
            {
                // Add Label to scrollable StackLayout.
                loggerLayout.Children.Add(new Label
                {
                    Text = "Button clicked at " + DateTime.Now.ToString("T")
                });
            }
            else
            {
                // Remove topmost Label from StackLayout
                loggerLayout.Children.RemoveAt(0);
            }

            // Enable "Remove" button only if children are present.
            removeButton.IsEnabled = loggerLayout.Children.Count > 0;
        }

        /*
#if __IOS__ 
        string resource = "BlackCatSap.iOS.Texts.TheBlackCat.txt"; 
#elif __ANDROID__ 
        string resource = "BlackCatSap.Droid.Texts.TheBlackCat.txt"; 
#elif WINDOWS_UWP 
        string resource = "BlackCatSap.UWP.Texts.TheBlackCat.txt"; 
#elif WINDOWS_APP 
        string resource = "BlackCatSap.Windows.Texts.TheBlackCat.txt"; 
#elif WINDOWS_PHONE_APP 
        string resource = "BlackCatSap.WinPhone.Texts.TheBlackCat.txt"; 
#endif
*/
        private void ScrollViewInStackLayoutDemo()
        {
            StackLayout mainStack = new StackLayout();
            StackLayout textStack = new StackLayout
            {
                Padding = new Thickness(5),
                Spacing = 10
            };

            // Get access to the text resource.
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            string resource = "XFormDiscovery603B.Texts.TheBlackCat.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    bool gotTitle = false;
                    string line;

                    // Read in a line (which is actually a paragraph).
                    while (null != (line = reader.ReadLine()))
                    {
                        Label label = new Label
                        {
                            Text = line,

                            // Black text for ebooks!
                            TextColor = Color.Black
                        };

                        if (!gotTitle)
                        {
                            // Add first label (the title) to mainStack.
                            label.HorizontalOptions = LayoutOptions.Center;
                            label.FontSize = Device.GetNamedSize(NamedSize.Medium, label);
                            label.FontAttributes = FontAttributes.Bold;
                            mainStack.Children.Add(label);
                            gotTitle = true;
                        }
                        else
                        {
                            // Add subsequent labels to textStack.
                            textStack.Children.Add(label);
                        }
                    }
                }
            }

            // Put the textStack in a ScrollView with FillAndExpand.
            ScrollView scrollView = new ScrollView
            {
                Content = textStack,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5, 0),
            };

            // Add the ScrollView as a second child of mainStack.
            mainStack.Children.Add(scrollView);

            // Set page content to mainStack.
            Content = mainStack;

            // White background for ebooks!
            BackgroundColor = Color.White;

            // Add some iOS padding for the page
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);


        }
        void BoxViewDemo()
        {

            StackLayout stackLayout = new StackLayout();
            // Loop through the Color structure fields. 
            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                // Skip the obsolete (i.e. misspelled) colors. 

                if (info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name));
                }
            }
            // Loop through the Color structure properties.
            foreach (PropertyInfo info in typeof(Color).GetRuntimeProperties())
            {
                MethodInfo methodInfo = info.GetMethod;
                if (methodInfo.IsPublic && methodInfo.IsStatic && methodInfo.ReturnType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorView((Color)info.GetValue(null), info.Name));
                }
            }
            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            // Put the StackLayout in a ScrollView. 
            Content = new ScrollView
            {
                Content = stackLayout
            };

        }
        View CreateColorView(Color color, string name)
        {
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children = {
                        new BoxView {
                            Color = color
                        },
                        new Label {
                            Text = name, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                FontAttributes = FontAttributes.Bold,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout {
                            Children = {
                                new Label {
                                    Text = String.Format("{0:X2}-{1:X2}-{2:X2}", (int)(255 * color.R), (int)(255 * color.G), (int)(255 * color.B)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = color != Color.Default
                                },
                                new Label {
                                    Text = String.Format("{0:F2}, {1:F2}, {2:F2}", color.Hue, color.Saturation, color.Luminosity),
                                    VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = color != Color.Default
                                }
                            },
                            HorizontalOptions = LayoutOptions.End
                        }
                    }
                }
            };
        }


        private void FrameDemo()
        {
            BackgroundColor = Color.Aqua;
            Content = new Frame
            {
                OutlineColor = Color.Black,
                BackgroundColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = new Label
                {
                    Text = "I've been framed!",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    FontAttributes = FontAttributes.Italic,
                    TextColor = Color.Blue,
                    //   HorizontalOptions = LayoutOptions.Center,
                    //VerticalOptions = LayoutOptions.Center
                }
            };
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
     
    }
}
