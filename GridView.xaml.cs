using StudentLoginPage.Model;
using StudentLoginPage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace StudentLoginPage.View
{
    public partial class GridView : ContentPage
    {
        public static int NumberItems = 0;
        public MainViewModel VM;
        public List<Student> Studs;
        public int maxItems;
        public int CurrentRow;
        public int CurrentColumn;
        TapGestureRecognizer tapGesture;
        public GridView()
        {
            InitializeComponent();
            VM = new MainViewModel(this.Navigation);
            Studs = VM.Students;
            CurrentRow = 0;
            CurrentColumn = 0;
            maxItems = grid.ColumnDefinitions.Count * grid.RowDefinitions.Count;
            tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OnStudentSelected;


            //  for (int i = 0; i < 15; i++)
            // {
            //      AddToGrid();
            //  }     


            foreach (Student i in Studs)
            {
                AddToGrid(i);
            }
            
        }

        public void AddToGrid(Student X)

        {
           // Frame description = myFrame(X);
           // description.GestureRecognizers.Add(tapGesture);
    
            //adding a stack layout
            StackLayout description = AnotherLayout(X);
            description.GestureRecognizers.Add(tapGesture);

            if (NumberItems == maxItems)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                CurrentRow++;
                CurrentColumn = 0;



               // grid.Children.Add(new BoxView { Color = Color.Teal,HeightRequest=40, WidthRequest=40 }, CurrentColumn, CurrentRow);
                grid.Children.Add(description, CurrentColumn, CurrentRow);


                CurrentColumn++;
                //return;
            }

            else if (CurrentColumn == grid.ColumnDefinitions.Count)
            {
                CurrentRow++;
                CurrentColumn = 0;
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //grid.Children.Add(new BoxView { Color = Color.Teal }, CurrentColumn, CurrentRow);
                grid.Children.Add(description, CurrentColumn, CurrentRow);
                CurrentColumn++;
            }
            else
            {
                //grid.Children.Add(new BoxView { Color = Color.Teal }, CurrentColumn, CurrentRow);
                grid.Children.Add(description, CurrentColumn, CurrentRow);
                CurrentColumn++;
            }
            NumberItems++;
        }


        public Frame myFrame(Student X)
        {

            return new Frame()
            {
                Padding = new Thickness(10, 5),
                HasShadow = false,
                BackgroundColor = Color.Pink,
                OutlineColor = Color.Blue,
                ClassId = X.StudentName.ToString(),
                Content = new Label() {
                    FontSize = 25,
                    TextColor = Color.FromHex("#ddd"),
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = X.StudentName

                }, 
            };

        }

  

         void OnTapped(object sender, EventArgs args)
        {
            Frame frame = (Frame)sender;
            Student student= Studs.Find(x => x.StudentName == frame.ClassId);
            VM.ChangeSelectedItem(student);

        }


        void OnStudentSelected(object sender, EventArgs args)
        {
            StackLayout layout = (StackLayout)sender;
            Student student = Studs.Find(x => x.StudentName == layout.ClassId);
            VM.ChangeSelectedItem(student);

        }

        public StackLayout AnotherLayout(Student X)
        {

            Grid StudentGrid = new Grid
            {
               
                RowDefinitions =
                {
                      new RowDefinition { Height =  new GridLength(3,GridUnitType.Star) },
                      new RowDefinition { Height =  new GridLength(1,GridUnitType.Star) }

                },
                ColumnDefinitions =
                {
                     new ColumnDefinition { Width =  new GridLength(1,GridUnitType.Star) },

                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
               
            };


            var LblStudentName = new Label
            {
                Text = X.StudentName,
                FontSize = 30,
                FontFamily = "AvenirNext-DemiBold",
                TextColor = Color.Black
            };


            var image = new Image()
            {
                Source = ImageSource.FromFile("contact.png"),
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };

            StudentGrid.Children.Add(image, 0, 0);
            StudentGrid.Children.Add(LblStudentName, 0, 1);


            return new StackLayout()
            {
                ClassId = X.StudentName.ToString(),
                BackgroundColor = Color.FromHex("#e6ffff"),
                Children = {
                   StudentGrid
                }
            };
        }

    }
}
