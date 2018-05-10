using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace PracticaMultiplicacion
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        #region Class Variables
        List<string> listaNum;
        nint numeroMultiplicado;
        #endregion

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            btnAdd.Clicked += BtnAdd_Clicked;


            tableView.DataSource = this;
            tableView.Delegate = this;
            listaNum = new List<string>();
        }



        public nint RowsInSection(UITableView tableView, nint section)
        {
            return listaNum.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("cityCell", indexPath);

            cell.TextLabel.Text = listaNum[indexPath.Row];
            return cell;
        }

        #region User Interaction
        void BtnAdd_Clicked(object sender, EventArgs e)
        {
            var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create("1", UIAlertActionStyle.Default, action => getMultiplier(1)));
            alert.AddAction(UIAlertAction.Create("2", UIAlertActionStyle.Default, action => getMultiplier(2)));
            alert.AddAction(UIAlertAction.Create("3", UIAlertActionStyle.Default, action => getMultiplier(3)));
            alert.AddAction(UIAlertAction.Create("4", UIAlertActionStyle.Default, action => getMultiplier(4)));
            alert.AddAction(UIAlertAction.Create("5", UIAlertActionStyle.Default, action => getMultiplier(5)));
            alert.AddAction(UIAlertAction.Create("6", UIAlertActionStyle.Default, action => getMultiplier(6)));
            alert.AddAction(UIAlertAction.Create("7", UIAlertActionStyle.Default, action => getMultiplier(7)));
            alert.AddAction(UIAlertAction.Create("8", UIAlertActionStyle.Default, action => getMultiplier(8)));
            alert.AddAction(UIAlertAction.Create("9", UIAlertActionStyle.Default, action => getMultiplier(9)));
            alert.AddAction(UIAlertAction.Create("10", UIAlertActionStyle.Default, action => getMultiplier(10)));
            alert.AddAction(UIAlertAction.Create("11", UIAlertActionStyle.Default, action => getMultiplier(11)));
            alert.AddAction(UIAlertAction.Create("12", UIAlertActionStyle.Default, action => getMultiplier(12)));

            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, animated: true, completionHandler: null);

        }


        #endregion

        #region Internal Functionability
        private void getMultiplier(nint number)
        {
            numeroMultiplicado = number;
            var alertNumber = UIAlertController.Create("Multiplicar", "Ingresa el numero de veces por el que tiene que ser multiplicado, si el valor no es valido se tomara como 0", UIAlertControllerStyle.Alert);
            UITextField testo = new UITextField();
            alertNumber.AddTextField((alertText) => {
                testo = alertText;
                testo.Placeholder = "Ingresa un numero";
                testo.KeyboardType = UIKeyboardType.NumberPad;
            });
            alertNumber.AddAction(UIAlertAction.Create("Aceptar", UIAlertActionStyle.Default, action => validarNumero(testo.Text.ToString())));

            alertNumber.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

            PresentViewController(alertNumber, animated: true, completionHandler: null);
            InvokeOnMainThread(() =>
            {
                tableView.ReloadData();
            });

        }



        private void validarNumero(string st)
        {
            listaNum = new List<string>();
            if (int.TryParse(st, out int numberOut))
            {
                //generamos la lista de resultados
                for (int i = 0; i < numberOut + 1; i++)
                {

                    listaNum.Add(numeroMultiplicado + " x " + i + " = " + (numeroMultiplicado * i));

                }


            }
            else
            {
                listaNum.Add(numeroMultiplicado + " x 0 = 0");
            }
            RefreshTableView();
        }

        public void RefreshTableView()
        {
            InvokeOnMainThread(() =>
            {
                tableView.ReloadData();
            });

        }
        #endregion
    }
}
