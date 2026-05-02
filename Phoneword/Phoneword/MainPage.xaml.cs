namespace Phoneword;

public partial class MainPage : ContentPage
{
    string? translatedNumber;

    public MainPage()
    {
        InitializeComponent();
        CallBtn.IsEnabled = false;
    }

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text ?? "";

        translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
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

    private async void OnCall(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(translatedNumber))
            return;

        bool answer = await DisplayAlertAsync(
            "Dial Number",
            $"Would you like to call {translatedNumber}?",
            "Yes",
            "No");

        if (answer)
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                {
                    PhoneDialer.Default.Open(translatedNumber);
                }
            }
            catch (Exception)
            {
                await DisplayAlertAsync(
                    "Unable to dial",
                    "Phone dialing failed.",
                    "OK");
            }
        }
    }
}