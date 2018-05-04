using System;
using System.Threading.Tasks;
using UIKit;
using Photos;
using Foundation;
using AVFoundation;
using CoreGraphics;

namespace PhotoPicker09
{
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {
        #region Class Variables
        UITapGestureRecognizer profileTapGesture;
        UITapGestureRecognizer coverTapGesture;

        #endregion
        #region Constructors

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }


        #endregion
        #region Controller Life Cycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            InitializeComponents();
            btnEdit.Clicked += HandleEventHandler;
        }
        #endregion

        #region User Interactions

        void ShowOptions(UITapGestureRecognizer gesture)
        {
            
            var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create("Ir a la galeria", UIAlertActionStyle.Default, action => TryOpenLibrary()));
            alert.AddAction(UIAlertAction.Create("Tomar Foto", UIAlertActionStyle.Default, action => TryOpenCamera()));
            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

            PresentViewController(alert, animated: true, completionHandler: null);
        }
        #region Photo Library
        void TryOpenLibrary()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {

                return;
            }



            CheckPhotoLibraryAutorizationStatus(PHPhotoLibrary.AuthorizationStatus);

        }

        void CheckPhotoLibraryAutorizationStatus(PHAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    //TODO: VAMOS A PEDIR EL PERMISO PARA ACCEDER LA GALERIA
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAutorizationStatus);
                    break;
                case PHAuthorizationStatus.Restricted:
                    //TODO: MOSTRAR UN MSG DICIENDO QUE ESTA RESTRINGIDO
                    InvokeOnMainThread(() => ShowMessage("Acceso a Fotos Restringido", "Chale weee no que si la querias ver krnal??? Te quieres morir??", NavigationController));
                    break;
                case PHAuthorizationStatus.Denied:
                    //TODO: MOSTRAR UN MSG DICIENDO QUE ESTA DENEGADO
                    InvokeOnMainThread(() => ShowMessage("Acceso a Fotos Denegado", "Por que no muestras los packs??? Te quieres morir??", NavigationController));

                    break;
                case PHAuthorizationStatus.Authorized:
                    //TODO: VAMOS A ABRIR LA GALERIA
                    InvokeOnMainThread(() =>
                    {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                    });


                    break;
                default:
                    break;
            }
        }

        void TryOpenCamera()
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
                CheckCamaraAutorizationStatus(authStatus);
                return;
            }



        }


        async private void CheckCamaraAutorizationStatus(AVAuthorizationStatus authorizationStatus)
        {
            switch (authorizationStatus)
            {
                case AVAuthorizationStatus.NotDetermined:
                    await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
                    CheckCamaraAutorizationStatus(authorizationStatus);
                    break;
                case AVAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() => ShowMessage("Acceso a Camara Restringido", "Por que no muestras los packs??? Te quieres morir??", NavigationController));
                    break;
                case AVAuthorizationStatus.Denied:
                    InvokeOnMainThread(() => ShowMessage("Acceso a Camara Denegado", "Por que no muestras los packs??? Te quieres morir??", NavigationController));
                    break;
                case AVAuthorizationStatus.Authorized:
                    InvokeOnMainThread(() =>
                    {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                    });

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region UIImage Picker Controller Delegate

        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, Foundation.NSDictionary info)
        {
            UIImage imagen = new UIImage();
            var image = info[UIImagePickerController.OriginalImage] as UIImage;
            //Le damos una nuevo tamanio a la imagen
            var images = ResizeUIImage(image, 128, 128);
            imgProfile.ContentMode = UIViewContentMode.ScaleAspectFit;

            imgProfile.Image = images;
            var imagec2 = ResizeUIImage(image, 359, 259);
            imgCover.Image = imagec2;
            picker.DismissViewController(true, null);
        }



        [ExportAttribute("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true, null);
        }

        #endregion

        #endregion

        #region Internal Functionality
        void InitializeComponents()
        {
            lblProfile.Hidden = lblCover.Hidden = true;

            profileTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
            profileView.AddGestureRecognizer(profileTapGesture);

            coverTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
            CoverView.AddGestureRecognizer(coverTapGesture);

        }

        void HandleEventHandler(object sender, EventArgs e)
        {
            if (lblCover.Hidden == true)
            {
                lblCover.Hidden = false;
                lblProfile.Hidden = false;
                profileTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = true };
                profileView.AddGestureRecognizer(profileTapGesture);

                coverTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = true };
                CoverView.AddGestureRecognizer(coverTapGesture);
            }
            else
            {
                lblCover.Hidden = true;
                lblProfile.Hidden = true;
                profileTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
                profileView.AddGestureRecognizer(profileTapGesture);

                coverTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = false };
                CoverView.AddGestureRecognizer(coverTapGesture);
            }
        }

        void ShowMessage(string title, string message, UIViewController fromViewController)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            fromViewController.PresentViewController(alert, true, null);
        }
        #endregion

        #region Image resize
        public UIImage ResizeUIImage(UIImage sourceImage, float widthToScale, float heightToScale)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Max(widthToScale / sourceSize.Width, heightToScale / sourceSize.Height);
            if (maxResizeFactor > 1) return sourceImage;
            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;
            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
        #endregion
    }
}
