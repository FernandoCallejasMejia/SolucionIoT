using SolucionIoT.BIZ.API;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolucionIoT.ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = this.DataContext as LoginModel;
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = FactoryManager.UsuarioManager().Login(model.Correo, model.Password);
            if (u != null)
            {
                //envar al panel del usuario
                MessageBox.Show($"Bienvenido {u.Nombre}", "Solucion IoT", MessageBoxButton.OK, MessageBoxImage.Information);
                PanelUsuario panel = new PanelUsuario(u);
                panel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrecta","Solucion IoT",MessageBoxButton.OK,MessageBoxImage.Error);

            }
        }
    }
}
