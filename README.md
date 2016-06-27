# Rope.Net
Create simple bindings between your View and View Model (VM)

## Introduction 

Rope.Net is a library that assists in creating bindings between your View and your VM. Use the PCL assembly by itself, and optionally import the platform-specific libraries, for ease of binding. The platform-specific libraries will generally take care of issues such as binding between a platform textbox, and invoking UI changes on the main thread.

## Why? 

Generally when performing binding, it comes bundled with some huge framework that enforces an MVVM structure throughout your application. This component aims to eliminate this dependancy on a large MVVM framework, and will allow you to use as much or little of it as you wish.

## Notes

* All bindings that are specific to a user-modifiable UI control, will be TwoWay bindings. (Text Boxes, Sliders, Checkboxes, etc.). If you wish to make a SingleWay (VM to View), just do a generic binding, using either the platform-specific generic-binding.
* Using 'BindingCore.CreateBinding' directly is generally not recommended, since you'll have to manually invoke the changes on the UI thread. But if you have no UI involved, or you just want to be straight-up hard-core go right ahead!
* I highly suggest using [Fody](https://github.com/Fody/Fody) along with the [PropertyChanged addin](https://github.com/Fody/PropertyChanged), as it makes the binding and notification process a whole lot easier. Take a few minutes to read how to use it, and you'll never dread Auto properties again!
* Throughout this text, when I've said 'View', I'm not saying this library is a 'Relevant for UI apps'-only library. You can easily (for instance) make bindings between a config file and a service, without the needs for having a view.

## Sample
*(You can also take a look at the Samples in the source)*

    // This sample uses PropertyChanged.Fody, so the PropertyChanged event 
    // will automatically be invoked whenever its Properties are changed.

    public class LoginViewController : View
    {
        public LoginViewController()
        {
            this.InitializeUi();
            this.ViewModel = new LoginViewModel();
            
            this.txtUsername.BindText(this.ViewModel, vm => vm.Username);
            this.txtPassword.BindText(this.ViewModel, vm => vm.Password);
            this.btnLogin.BindEnable(this.ViewModel, vm => vm.LoginEnabled);
        }

        public LoginViewModel ViewModel { get; }
    }

    public class LoginViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }

        public string Password { get; set; }
        
        public bool LoginEnabled => !string.IsNullOrEmpty(this.Username) && !string.IsNullOrEmpty(this.Password);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

# Why should I care/use it?

In normal circumstances, when making a new application (be it mobile, desktop, or whatever other type that has components that needs to react to changes in other properties), you'd usually import some huge MVVM library, that usually takes care of UI navigation on the platform, as well as dependancy injection, and instantiating the VMs. All in all, a lot of (in my humble opinion) unnecessary hazzle. At least if you want to decide yourself precisely what library you use for each individual aspect of your application.

These assemblies will let you make bindings between your view and view model, without all that other stuff!
