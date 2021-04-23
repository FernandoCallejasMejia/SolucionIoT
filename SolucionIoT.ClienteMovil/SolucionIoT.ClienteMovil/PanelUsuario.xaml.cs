using SolucionIoT.BIZ.API;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolucionIoT.ClienteMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelUsuario : ContentPage
    {
        PanelUsuarioModel model;
        public PanelUsuario(Usuario usuario)
        {
            InitializeComponent();
            model = this.BindingContext as PanelUsuarioModel;
            model.Usuario = usuario;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lstDispositivos.ItemsSource = null;
            lstDispositivos.ItemsSource = FactoryManager.DispositivoManager().DispositivosDeUsuarioPorId(model.Usuario.Id);
        }

        private void lstDispositivos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            model.DispositivoSeleccionado = e.SelectedItem as Dispositivo;
            Navigation.PushAsync(new DispositivoTabbedPage(model));

        }
    }
}