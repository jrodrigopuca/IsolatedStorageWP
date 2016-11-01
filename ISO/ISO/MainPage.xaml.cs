using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ISO.Resources;
using System.IO.IsolatedStorage;

namespace ISO
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            revisarNuevoUsuario();
            cargarDatos();

        }

        public void revisarNuevoUsuario()
        {
            //Buscamos si el dato persistido como "nuevousuario" es verdadero
            if ((bool)IsolatedStorageSettings.ApplicationSettings["nuevousuario"])
            {
                //Sis es verdadero mostramos un mensaje, y de paso confirmamos que ya no es un nuevo usuario
                MessageBox.Show("Hola nuevo usuario");
                IsolatedStorageSettings.ApplicationSettings["nuevousuario"] = false;
            }
        }


        public void cargarDatos()
        {
            //para traer los datos realizo una consulta (usando LINQ)
            var query = from actividad in actividadDC.accesoDC.oActividad
                        select actividad.descripcion;
            //al resultado de la consulta la muestro en un listbox
            LstActividades.ItemsSource = query;
            

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Creo un nuevo ID 
            Guid guid = Guid.NewGuid();
            string miGuid = guid.ToString();

            //Creo una instancia de actividad
            actividad nuevaActividad = new actividad();
            nuevaActividad.descripcion = TextActividad.Text;
            nuevaActividad.id = miGuid;
            
            //Guardo en la BD la nueva actividad
            actividadDC.accesoDC.oActividad.InsertOnSubmit(nuevaActividad);
            actividadDC.accesoDC.SubmitChanges();

            //Alternativa: una forma de guardar una clave/dato

            IsolatedStorageSettings.ApplicationSettings["datoguardado"] = true;

            //cargo nuevamente los datos
            cargarDatos();


            

            

        }
    }
}