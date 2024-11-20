using Microsoft.Maui.Controls;

namespace dxclusive
{
    public partial class ClassPage3 : ContentPage
    {
        private readonly string _term;
        private readonly List<string> _classData;

        public ClassPage3(string term, List<string> classData)
        {
            InitializeComponent();
            _term = term;
            _classData = classData;

            // Example: Use the data to display on this page
            TermLabel.Text = $"Selected Term: {_term}";
            foreach (var item in _classData)
            {
                ClassesContainer.Children.Add(new Label
                {
                    Text = item,
                    FontSize = 18,
                    TextColor = Colors.Black
                });
            }
        }
    }
}
