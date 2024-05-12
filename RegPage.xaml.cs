using CommunityToolkit.Maui.Extensions;
using static Microsoft.Maui.Animations.AnimationLerpingExtensions;

namespace winui_cooler
{
    using System;
    using CommunityToolkit.Maui;
    using InputKit.Shared.Validations;
    using Mopups.Services;
    using UraniumUI.Material.Controls;

    public partial class RegPage : ContentPage
    {
        private List<bool> validFields;
        private Color dirtyYellow = MyColors.dirtyYellow;
	    private Color darkGray = MyColors.darkGray;
        private EmailValidation email_check;
        private OverrideRequiredValidation name_check;
        private PasswordValidation passval;
        private OverrideRequiredValidation email_req_check;
        private OverrideLenValidation pass_check;
        public Command SubmitCommand {get; private set;}
        public RegPage()
        {
            InitializeComponent();
            
            pass_check = new OverrideLenValidation() { Message = "This password is too short", MinLength=4};
            passval = new PasswordValidation() { Message = "Your passwords don't match!"};
            email_req_check = new OverrideRequiredValidation() { Message = "You must have an e-mail"};
            email_check = new EmailValidation() { Message = "Your email doesn't look like an email"};
            name_check = new OverrideRequiredValidation() { Message="You must have a name"};

            invalid_name.Text = name_check.Message;
            invalid_password.Text = pass_check.Message;
            invalid_confirm_password.Text = passval.Message;
            invalid_email.Text = email_check.Message;

            g_logo.Source = ImageSource.FromFile("google.png");
            SubmitCommand = new Command(Submit);
            validFields = new List<bool>{ false, false, false, false };
            // name_entry.TextChanged += (sender, args) => { validFields[0] = name_check.Validate(name_entry.Text);};
            // email_entry.TextChanged += (sender, args) => { validFields[1] = email_check.Validate(email_entry.Text);};
            // pass_entry.TextChanged += ValidatePass();
            // confirm_entry.TextChanged += ConfirmPass();
        }

        private EventHandler<TextChangedEventArgs> ValidatePass()
        {
            return (sender, args) =>
            {
                validFields[2] = pass_check.Validate(pass_entry.Text);
            };
        }
        private EventHandler<TextChangedEventArgs> ConfirmPass()
        {
            return (sender, args) =>
            {
                validFields[3] = passval.Validate(pass_entry.Text, confirm_entry.Text);
            };
        }
        public bool IsAllValid()
        {
            return validFields.All(f => f == true);
        }

        private async void Submit()
        {
            await Navigation.PopAsync();
        }


        private async void AgreeOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.TextColorBlink(agree_btn, Colors.LightYellow, Colors.White);
        }

        private async void RegisterOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            
            await Animations.BackColorAndScaleBlink(reg_btn, Colors.LightYellow, Colors.White, 1.0, 1.4);
            
            
            if (!name_check.Validate(name_entry.Text))
            {
                name_invalid_image.IsVisible = true;
                invalid_name.IsVisible = true;
                validFields[0] = false;
            }
            else
            {
                name_invalid_image.IsVisible = false;
                invalid_name.IsVisible = false;
                validFields[0] = true;
            }

            if (!email_check.Validate(email_entry.Text))
            {
                email_invalid_image.IsVisible = true;
                invalid_email.IsVisible = true;
                validFields[1] = false;
            }
            else
            {
                email_invalid_image.IsVisible = false;
                invalid_email.IsVisible = false;
                validFields[1] = true;
            }

            if (!pass_check.Validate(pass_entry.Text))
            {
                password_invalid_image.IsVisible = true;
                invalid_password.IsVisible = true;
                validFields[2] = false;
            }
            else
            {
                password_invalid_image.IsVisible = false;
                invalid_password.IsVisible = false;
                validFields[2] = true;
            }
            
            if (!passval.Validate(pass_entry.Text, confirm_entry.Text))
            {
                confirm_invalid_image.IsVisible = true;
                invalid_confirm_password.IsVisible = true;
                validFields[3] = false;
            }
            else
            {   
                //await DisplayAlert("passvalid", pass_entry.Text, confirm_entry.Text);
                confirm_invalid_image.IsVisible = false;
                invalid_confirm_password.IsVisible = false;
                validFields[3] = true;
            }
            if(IsAllValid()) 
            {
                if(await ConnectionManager.TryRegister(name_entry.Text, email_entry.Text, pass_entry.Text))
                {
                    Submit();
                }
                else 
                {
                    await MopupService.Instance.PushAsync(new CustomMopupAlert());
                    invalid_email.Text = "This email is occupied by somebody else.";
                    email_invalid_image.IsVisible = true;
                    invalid_email.IsVisible = true;
                }
            }
            
            
        }
        private async void LogOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.TextColorBlink(log_btn, Colors.LightYellow, Colors.White);
            await Navigation.PopAsync();
        }
        private async void GogleOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.BackColorBlink(gogle, Colors.Black, dirtyYellow);
        }
        
        

    }
}