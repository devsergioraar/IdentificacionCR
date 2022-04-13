// Implementación de Selenium para extraer información para completar datos de registro de persona.
// Fecha: 2022-04-12
// Desarrollador: Sergio Ramirez A.
//
// Este programa es software libre: puede redistribuirlo y / o modificarlo
// bajo los + términos de la Licencia Pública General Reducida de GNU publicada por
// la Free Software Foundation, ya sea la versión 3 de la licencia, o
// (a su opción) cualquier versión posterior.
//
// Este programa se distribuye con la esperanza de que sea útil,
// pero SIN NINGUNA GARANTÍA; sin siquiera la garantía implícita de
// COMERCIABILIDAD O IDONEIDAD PARA UN PROPÓSITO PARTICULAR. 
// Licencia pública general menor de GNU para más detalles.
//
// Deberías haber recibido una copia de la Licencia Pública General Reducida de GNU
// junto con este programa.Si no, vea http://www.gnu.org/licenses/.
//
// This program Is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program.If Not, see http://www.gnu.org/licenses/. 

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Globalization;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace IdentificacionCR
{
    

    /// <summary>
    /// Clase que emplea Selenium para extraer Información de identificación de personas TSE.
    /// </summary>    
    /// <see cref="https://servicioselectorales.tse.go.cr/chc/"/>
    public class IdService
    {
        private string tse_cedula = "https://servicioselectorales.tse.go.cr/chc/consulta_cedula.aspx";

        private string tse_nombre = "https://servicioselectorales.tse.go.cr/chc/consulta_nombres.aspx";

        private ChromeDriver _driver;

        /// <summary>
        /// En caso que se deba cambiar la URL del Servicio de búsqueda por Identificación.               
        /// </summary>
        public string TSE_cedula { get => tse_cedula; set => tse_cedula=value; }

        /// <summary>
        /// En caso que se deba cambiar la URL del Servicio de búsqueda por nombre.        
        /// </summary>
        public string TSE_nombre { get => tse_nombre; set => tse_nombre=value; }


        /// <summary>
        /// Constructor de Clase.        
        /// </summary>
        ///<param name="path_chrome_driver">Ruta de Driver de Chrome.</param>
        ///<returns>Instancia de Objeto para realizar consultas.</returns>
        public IdService( string path_chrome_driver = null )
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            //Se deja la posibilidad de cambiar el driver.
            if (string.IsNullOrEmpty(path_chrome_driver))
            {
                _driver=new ChromeDriver();
            }
            else
            {
                _driver=new ChromeDriver(path_chrome_driver);
            }
        }

        /// <summary>
        /// Método que realiza las consultas mediante el numero de cédula.   
        /// </summary>
        ///<param name="parm_cedula">Cédula de Persona.</param>
        ///<returns>JSON con información de persona.</returns>
        public string ConsultaCedula( string parm_cedula )
        {
            Persona consulta;            
            
            try
            {
                _driver.Navigate().GoToUrl(tse_cedula);

                IWebElement cedula = _driver.FindElement(By.Id("txtcedula"));
                cedula.SendKeys(parm_cedula);

                IWebElement buscar = _driver.FindElement(By.Id("btnConsultaCedula"));
                buscar.Click();
                _driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);




                if (_driver.Url=="https://servicioselectorales.tse.go.cr/chc/error_trans.aspx")
                {
                    throw new Exception(string.Format("Error: la persona que busca con la Identificación '{0}' no está en la base de datos", parm_cedula));
                }
                else
                {
                    try
                    {
                        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                        bool valor = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("lblcedula")));
                        if (valor)
                        {
                            throw new Exception(string.Format("Error: La persona que busca con la Identificación '{0}' no pudo ser localizada.", parm_cedula));
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("Error: La persona que busca con la Identificación '{0}' no pudo ser localizada.", parm_cedula));
                    }
                    consulta=ArmarPersona();
                }
            }            
            catch (Exception ex1)
            {
                throw ex1;
            }

            return JsonConvert.SerializeObject(consulta,Formatting.Indented);
        }

        /// <summary>
        /// Método que realiza las consultas mediante el nombre de persona.
        /// </summary>
        ///<param name="nombre">Nombre de Persona.</param>
        ///<param name="apellido1">Primer apellido de Persona.</param>
        ///<param name="apellido2">Segundo apellido de Persona.</param>
        ///<returns>Lista JSON con información de personas.</returns>
        public string ConsultaNombre( string nombre, string apellido1, string apellido2 )
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                _driver.Navigate().GoToUrl(tse_nombre);

                IWebElement nombreInput = _driver.FindElement(By.Id("txtnombre"));
                nombreInput.SendKeys(nombre);

                IWebElement apellido1Input = _driver.FindElement(By.Id("txtapellido1"));
                apellido1Input.SendKeys(apellido1);

                IWebElement apellido2Input = _driver.FindElement(By.Id("txtapellido2"));
                apellido2Input.SendKeys(apellido2);

                IWebElement buscar = _driver.FindElement(By.Id("btnConsultarNombre"));
                buscar.Click();
                _driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);                

                Console.WriteLine("URL EN CASOS DE NOMBRE: "+_driver.Url);
                if (_driver.Url=="https://servicioselectorales.tse.go.cr/chc/error_trans.aspx")
                {
                    throw new Exception(string.Format("Error: la persona que búsca con el nombre '{0} {1} {2}' no está en la base de datos", nombre,apellido1,apellido2));
                }

                try
                {
                    IWebElement marcarTodo = _driver.FindElement(By.XPath("//*[@id='chk2']"));
                    marcarTodo.Click();
                    _driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
                }
                catch (Exception)
                {

                    throw new Exception(string.Format("Error: la persona que búsca con el nombre '{0} {1} {2}' no está en la base de datos", nombre, apellido1, apellido2));
                }

                ReadOnlyCollection<IWebElement> items = _driver.FindElements(By.XPath("//*[@id='chk1']/tbody/tr"));

                int iteraciones = items.Count();
                List<string> cedula_str = new List<string>();

                for (int i = 0; i<iteraciones; i++)
                {
                    cedula_str.Add(items[i].Text.Split(' ')[1]);
                }

                for (int i = 0; i<iteraciones; i++)
                {
                    _driver.Navigate().GoToUrl(tse_cedula);

                    IWebElement cedula = _driver.FindElement(By.Id("txtcedula"));
                    cedula.SendKeys(cedula_str[i]);

                    IWebElement btn_buscar = _driver.FindElement(By.Id("btnConsultaCedula"));
                    btn_buscar.Click();
                    _driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);

                    personas.Add(ArmarPersona());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
            return JsonConvert.SerializeObject(personas, Formatting.Indented);
        }



        /// <summary>
        /// Método que realiza la extracción de la información.   
        /// </summary>        
        ///<returns>Retorna objeto Persona que contiene la información de la persona</returns>
        public Persona ArmarPersona()
        {
            Persona personaConsulta = new Persona();

            try
            {
                personaConsulta.Cedula=_driver.FindElement(By.Id("lblcedula")).Text;

                personaConsulta.Nombre_persona=_driver.FindElement(By.Id("lblnombrecompleto")).Text;

                personaConsulta.Cc_persona=_driver.FindElement(By.Id("lblconocidocomo")).Text;

                personaConsulta.Nombre_padre_persona=_driver.FindElement(By.Id("lblnombrepadre")).Text;

                personaConsulta.Cedula_padre_persona=_driver.FindElement(By.Id("lblid_padre")).Text;

                personaConsulta.Nombre_madre_persona=_driver.FindElement(By.Id("lblnombremadre")).Text;

                personaConsulta.Cedula_madre_persona=_driver.FindElement(By.Id("lblid_madre")).Text;

                personaConsulta.Fecha_nacimiento_persona=DateTime.ParseExact(_driver.FindElement(By.Id("lblfechaNacimiento")).Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                personaConsulta.Nacionalidad_persona=_driver.FindElement(By.Id("lblnacionalidad")).Text;

                personaConsulta.Edad_persona=_driver.FindElement(By.Id("lbledad")).Text;

                personaConsulta.Marginal_persona=_driver.FindElement(By.Id("lblLeyendaMarginal")).Text;

                try
                {
                    personaConsulta.Fecha_fallecido_persona=DateTime.ParseExact(_driver.FindElement(By.XPath("//*[@id='lbldefuncion2']")).Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    personaConsulta.Fallecido=true;
                }
                catch (Exception)
                {
                    personaConsulta.Fallecido=false;
                    personaConsulta.Fecha_fallecido_persona=DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                      
            return personaConsulta;
        }


        /// <summary>
        /// Método que cierra el Driver de Chrome.   
        /// </summary>
        public void CerrarCliente()
        {
            _driver.Quit();
        }        
    }
}
