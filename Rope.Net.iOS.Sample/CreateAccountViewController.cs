using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using CoreGraphics;

using MonoTouch.Dialog;

using Rope.Net.iOS.Sample.Annotations;

using UIKit;

namespace Rope.Net.iOS.Sample
{
    public class CreateAccountViewController : DialogViewController
    {
        public CreateAccountViewController()
            : base(UITableViewStyle.Grouped, null, true)
        {
            this.ViewModel = new CreateAccountViewModel();

            var eleAcceptTos = new BooleanElement("Accept", this.ViewModel.ToS);
            eleAcceptTos.BindValue(this.ViewModel, vm => vm.ToS);

            var eleFirstName = new EntryElement("First name", "Enter first name", this.ViewModel.FirstName);
            eleFirstName.BindText(this.ViewModel, vm => vm.FirstName);

            var eleLastName = new EntryElement("Last name", "Enter last name", this.ViewModel.LastName);
            eleLastName.BindText(this.ViewModel, vm => vm.LastName);

            var eleBirthday = new DateElement("Birthday", this.ViewModel.Birthday);
            eleBirthday.BindDate(this.ViewModel, vm => vm.Birthday);

            var btnLogin = new UIButton { BackgroundColor = UIColor.White, Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 64) };
            btnLogin.SetTitle("Create Account", UIControlState.Normal);
            btnLogin.SetTitleColor(UIColor.Green, UIControlState.Normal);
            btnLogin.SetTitleColor(UIColor.DarkGray, UIControlState.Disabled);
            btnLogin.BindEnable(this.ViewModel, vm => vm.ToS);
            btnLogin.TouchUpInside += (sender, args) => this.CreateAccount();

            var eleUsername = new EntryElement("Login", "Enter desired login", this.ViewModel.Username);
            eleUsername.BindText(this.ViewModel, vm => vm.Username);

            var elePassword = new EntryElement("Password", "Enter desired password", this.ViewModel.Password, true);
            elePassword.BindText(this.ViewModel, vm => vm.Password);

            const string NavTitle = "Bindings";

            this.Root = new RootElement(NavTitle)
            {
                new Section("Personal")
                {
                    eleFirstName,
                    eleLastName,
                    eleBirthday
                },
                new Section("Account")
                {
                    eleUsername,
                    elePassword
                },
                new Section("Read the Terms of Service")
                {
                    eleAcceptTos
                },
                new Section
                {
                    new UIViewElement(string.Empty, btnLogin, true)
                }
            };

            Action<CreateAccountViewController, string> updateTitle = (c, text) =>
                {
                    if (this.NavigationItem == null)
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(this.ViewModel.FirstName)
                        && !string.IsNullOrEmpty(this.ViewModel.LastName))
                    {
                        this.NavigationItem.Title = $"{this.ViewModel.FirstName} {this.ViewModel.LastName}";
                    }
                    else
                    {
                        this.NavigationItem.Title = NavTitle;
                    }
                };
            BindingCore.CreateBinding(this, this.ViewModel, vm => vm.FirstName, updateTitle);
            BindingCore.CreateBinding(this, this.ViewModel, vm => vm.LastName, updateTitle);
        }

        private void CreateAccount()
        {
            var msg = string.Empty;
            msg += $"First name: {this.ViewModel.FirstName}\n";
            msg += $"Last name: {this.ViewModel.LastName}\n";
            msg += $"Birthday: {this.ViewModel.Birthday.ToShortDateString()}\n";
            msg += $"Username: {this.ViewModel.Username}\n";
            msg += $"Password: {this.ViewModel.Password}\n";
            new UIAlertView("Create Account", msg, null, "Continue").Show();
        }

        public CreateAccountViewModel ViewModel { get; }
    }

    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool ToS { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; } = DateTime.Now;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}