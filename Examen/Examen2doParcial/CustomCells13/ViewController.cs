using System;

using UIKit;

namespace CustomCells13
{
    public partial class ViewController : TableViewController
    {
        
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

           
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        /*
        public async System.Threading.Tasks.Task generarQueryAsync(){
            await lq.twitterAsync("hola");
        }*/
    }
}
