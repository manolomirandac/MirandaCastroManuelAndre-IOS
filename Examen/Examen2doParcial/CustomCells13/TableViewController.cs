// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using LinqToTwitter;
using System.Collections.Generic;
using CustomCells13.Objects;

namespace CustomCells13
{
    public partial class TableViewController : UITableViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchResultsUpdating
    {

        #region class variables
        public List<objTweet> lstSt;
        List<string> lista;
        linq2twitter lq;
        #endregion
        public TableViewController(IntPtr handle) : base(handle)
        {
        }
        async void getList(string query)
        {
            
            lq = new linq2twitter();
            lstSt = new List<objTweet>();
            lstSt =await lq.twitterAsync(query);
           
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            getList("hola");
            btnBusqueda.Clicked+= BtnBusqueda_Clicked;

            //Añadimos las propiedades a la celda
            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 40f;
            InvokeOnMainThread(() => {
                this.TableView.ReloadData();
            });
        }

        #region UITable View Data Source

        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return lstSt.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var tweet = lstSt[indexPath.Row];
            var cell = tableView.DequeueReusableCell(CustomTableViewCell.Key, indexPath) as CustomTableViewCell;
            cell.Label = tweet.text;
            cell.Like = tweet.likes;
            cell.imageLike = UIImage.FromFile("like.png");
            cell.IndexPath = indexPath;



            /*
            var cell = tableView.DequeueReusableCell(CustomTableViewCell.Key, indexPath) as CustomTableViewCell;
            cell.Label= $"{lista[indexPath.Row]}";*/
            return cell;

        }

        #endregion

        #region user Interaction

        #region Search
        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            //search
            var find = searchController.SearchBar.Text;
            if (searchController.SearchBar.Text!="")
            {
                getList(find);
            }
            else{
                var alertNumber = UIAlertController.Create("Error", "Ingresa el el parametro de busqueda", UIAlertControllerStyle.Alert);
                UITextField testo = new UITextField();

                alertNumber.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, action => getList(testo.Text.ToString())));

              

                PresentViewController(alertNumber, animated: true, completionHandler: null);
            }

        }
        #endregion

        void BtnBusqueda_Clicked(object sender, EventArgs e)
        {
            var alertNumber = UIAlertController.Create("Buscar", "Ingresa el el parametro de busqueda", UIAlertControllerStyle.Alert);
            UITextField testo = new UITextField();
            alertNumber.AddTextField((alertText) => {
                testo = alertText;
                testo.Placeholder = "Ingresa un parametro";
                testo.KeyboardType = UIKeyboardType.Default;
            });
            alertNumber.AddAction(UIAlertAction.Create("Aceptar", UIAlertActionStyle.Default, action => getList(testo.Text.ToString())));

            alertNumber.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));

            PresentViewController(alertNumber, animated: true, completionHandler: null);
            InvokeOnMainThread(() =>
            {
                TableView.ReloadData();
            });
        }

        #endregion





    }
}
