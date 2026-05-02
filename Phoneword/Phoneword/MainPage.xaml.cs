using Microsoft.Maui.Controls;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;

            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                CallBtn.IsEnabled = true;
                CallBtn.Text = "Call " + translatedNumber;
            }
            else
            {
                CallBtn.IsEnabled = false;
                CallBtn.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                "Dial Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Number was not valid.", "OK");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}