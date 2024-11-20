using Microsoft.Maui.Controls;

namespace dxclusive
{
    public partial class ClassPage : ContentPage
    {
        private readonly FirestoreService _firestoreService;

        public ClassPage()
        {
            InitializeComponent();
            _firestoreService = new FirestoreService();
            LoadTermsAsync();
        }

        private async void LoadTermsAsync()
        {
            try
            {
                // Fetch the terms from Firestore
                var terms = await _firestoreService.GetTermListAsync();

                // Clear existing buttons (if any)
                ButtonsContainer.Children.Clear();

                // Dynamically add buttons for each term
                foreach (var term in terms)
                {
                    var button = new Button
                    {
                        Text = term,
                        FontSize = 20,
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black,
                        CornerRadius = 10,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };

                    // Add a click event handler for each button
                    button.Clicked += async (sender, args) =>
                    {
                        try
                        {
                            // Fetch class data for the selected term
                            var classData = await _firestoreService.GetClassDataAsync(term);

                            // Navigate to ClassPage3 with the term and class data
                            await Navigation.PushAsync(new ClassPage3(term, classData));
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", $"Failed to load class data: {ex.Message}", "OK");
                        }
                    };

                    // Add the button to the container
                    ButtonsContainer.Children.Add(button);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load terms: {ex.Message}", "OK");
            }
        }
    }
}
