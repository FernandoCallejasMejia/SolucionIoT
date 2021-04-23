using SolucionIoT.BIZ.API;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SolucionIoT.ClienteMovil
{
    public partial class MainPage : ContentPage
    {
        LoginModel model;
        public MainPage()
        {
            InitializeComponent();
            model = this.BindingContext as LoginModel;
        }

        private void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            Usuario u = FactoryManager.UsuarioManager().Login(model.Correo, model.Password);
            if (u!=null)
            {
                //enviar al panel de usuario
                DisplayAlert("Solucion IoT", $"Biemvenido {u.Nombre}", "Ok");
                Navigation.PushAsync(new PanelUsuario(u));
            }
            else
            {
                DisplayAlert("Solucion IoT", "Usuario y/o contraseña incorrecta", "Ok");

            }
        }
    }
}
