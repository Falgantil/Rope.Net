using MonoTouch.Dialog;

using UIKit;

namespace Rope.Net.iOS.Sample.Ui.ViewControllers
{
    public class RootViewController : DialogViewController
    {
        public RootViewController() : base(UITableViewStyle.Grouped, null)
        {
            this.Root = new RootElement("Samples")
            {
                new Section
                {
                    new StringElement(
                        "Create Account",
                        () => this.NavigationController.PushViewController(new CreateAccountViewController(), true)),
                    new StringElement(
                        "Samples",
                        () => this.NavigationController.PushViewController(new SampleViewController(), true))
                }
            };
        }
    }
}