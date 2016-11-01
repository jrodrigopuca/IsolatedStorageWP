//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Data.Linq; //Esta implementación funciona en WP7/WP8, para versiones superiores hay alternativas de terceros.
using System.Data.Linq.Mapping;

namespace ISO
{
    //Agrego una clase actividad
    [Table]
    public class actividad
    {
        [Column(IsPrimaryKey = true)]
        public string id { get; set; }
        [Column]
        public string descripcion { get; set; }
        
        //Los [] sirven para mapear la tabla (sirven para cuando la clase se "convierta" en tabla. 

    }

    //Voy a crear un clase que herede DataContext (permite trabajar con una base de datos )
    public class actividadDC: DataContext
    {
        //Voy a crear la tabla objeto llamada oActividad (esto permitirá convertir la clase en una tabla)
        public Table<actividad> oActividad;

        //Agrego la cadena de conexión
        private actividadDC(string cadenaConexion) : base(cadenaConexion) { }

        //Inicialmente el DataContext debe ir vacío
        static actividadDC dc = null;

        //Agrego un acceso al DataContext
        public static actividadDC accesoDC
        {
            get
            { 
             //Si es la primera vez que inicia voy a crear la conexión, si no existe la Base de Datos voy a crear una:
                if (dc == null)
                {
                    dc = new actividadDC("isostore:/LugarBD.sdf");

                    if (!dc.DatabaseExists())
                    {
                        dc.CreateDatabase();
                    }
                }

                return dc;
            }

        }



    }


}
