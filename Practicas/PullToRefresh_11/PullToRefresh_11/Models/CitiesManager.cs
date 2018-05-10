using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace PullToRefresh_11.Models
{
    public class CitiesManager
    {

        #region Singleton
        //No se pueden instanciar de ninguna manera
        //Para hacer privado al constructor, simplemente el constructor tiene que ser privado
        //lazy solo se instancia una ves 
        static readonly Lazy<CitiesManager> lazy = new Lazy<CitiesManager>(()=> new CitiesManager());
        public static CitiesManager SharedInstance { get => lazy.Value; }
        #endregion

        #region Class Variables
        HttpClient httpClient;
        //El valor de la llave cities ahora se vuelve el string
        Dictionary<string, List<string>> cities;
        #endregion

        #region Events
        public event EventHandler<CitiesEventArgs> CitiesFetched;
        public event EventHandler<EventArgs> FetchCitiesFailed;
        #endregion



        #region Constructos


        CitiesManager()
        {
            httpClient = new HttpClient();

        }
        #endregion

        #region Public Functionality

        public Dictionary<string, List<string>> GetDefaultCities()
        {
            var citiesJson = File.ReadAllText("/Users/ISSC611/Desktop/ManoloMiranda/Parcial2/citites-incomplete.json");
            // Vamos a parsear el Json, usando Json.Net

            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
        }
        //Metodos sincronos pero que usan metodos asincronos
        public void FetchCities (){

            Task.Factory.StartNew(FetchCitiesAsync);
            async Task FetchCitiesAsync(){
                try
                {
                    var citiesJson = await httpClient.GetStringAsync("https://dl.dropbox.com/s/div7s568y92a80q/citites-incomplete.json?dl=0");
                    cities = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(citiesJson);
                    //Cuando se termina de descargar las ciudade ahora se mandan al evento
                    if (CitiesFetched == null)
                    {
                        return;
                    }
                    //Es avisarle al controller que ya estan disponibles los datos
                    //Hay 3 maneras:
                    //A traves de eventos(Events/Delegate)
                    //A traves de notificaciones(NSNotificationCenter)
                    //(Solo cuando estas dentro de un ViewController) A traves de Unwind Segues

                    var argumentos = new CitiesEventArgs(cities);
                    Console.WriteLine("realizado con exito");
                     CitiesFetched(this, argumentos);


                }
                catch (Exception ex)
                {
                    //Es avisarle al Controller de que algo fallo(con acento)
                    //A traves de eventos(Events/Delegate)
                    //A traves de notificaciones(NSNotificationCenter)
                    //(Solo cuando estas dentro de un ViewController) A traves de Unwind Segues
                    if (FetchCitiesFailed == null)
                        return;
                    

                    FetchCitiesFailed(this,new EventArgs());

                }
            }
        }



        #endregion
    }

    public class CitiesEventArgs : EventArgs{
        public Dictionary<string, List<string>> Cities { get; set; }

        public CitiesEventArgs(Dictionary<string, List<string>> cities){
            Cities = cities;
        }
    }
}
