using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SolucionIoT.BIZ.API;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Modelos;
using SolucionIoT.Tools;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SolucionIoT.ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para PanelUsuario.xaml
    /// </summary>
    public partial class PanelUsuario : Window
    {
        readonly DispatcherTimer timer;
        //private List<ComandosModel> opciones;
        readonly PanelUsuarioModel model;
        readonly Random r;
        private string mensaje;
        private MensajeRecibido mensajeRecibido;
        string topico;
        public PanelUsuario(Usuario usuario )
        {
            InitializeComponent();

            this.SizeChanged += PanelUsuario_SizeChanged;
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 100)
            };
            timer.Start();
            timer.Tick += Timer_Tick;
            r = new Random();
            MqttService.Conectar("SolucionIoTWPF" + r.Next(0, 10000).ToString(), "broker.hivemq.com");
            MqttService.Conectado += MqttService_Conectado;
            MqttService.MensajeRecibido += MqttService_MensajeRecibido;
            MqttService.Error += MqttService_Error;
            MqttService.Mensaje += MqttService_Mensaje;
            model = this.DataContext as PanelUsuarioModel;
            model.Usuario = usuario;
            model.Dispositivos = FactoryManager.DispositivoManager().DispositivosDeUsuarioPorId(usuario.Id).ToList();
            lstDispositivos.ItemsSource = null;
            lstDispositivos.ItemsSource = model.Dispositivos;
            topico = "";
        }

        private void MqttService_Mensaje(object sender, string e)
        {
            mensaje = e;
        }

        private void MqttService_Error(object sender, string e)
        {
            mensaje = "Error: " + e;
        }

        private void MqttService_MensajeRecibido(object sender, MensajeRecibido e)
        {
            mensajeRecibido = e;
        }

        private void MqttService_Conectado(object sender, string e)
        {
            mensaje = e;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //if (mensaje !="")
            //{
            //    lstMensajes.Items.Add($"{DateTime.Now.ToShortTimeString()}:{mensaje}");
            //    mensaje = "";
            //}
            //if (mensajeRecibido != null)
            //{
            //    if (mensajeRecibido.Topico == topico)
            //    {
            //        lstMensajes.Items.Add($"[{mensajeRecibido.Topico}]>{mensajeRecibido.Mensaje}");
            //        if (mensajeRecibido.Mensaje.Contains("="))
            //        {
            //            string[] parte = mensajeRecibido.Mensaje.Split('=');
            //            switch (parte[0])
            //            {
            //                case "B":
            //                    lblEBuzzer.Content = parte[1] == "1" ? "Encendido" : "Apagado";
            //                    break;
            //                case "R1":
            //                    lblER1.Content = parte[1] == "1" ? "Encendido" : "Apagado";
            //                    break;
            //                case "R2":
            //                    lblER2.Content = parte[1] == "1" ? "Encendido" : "Apagado";
            //                    break;
            //                case "R3":
            //                    lblER3.Content = parte[1] == "1" ? "Encendido" : "Apagado";
            //                    break;
            //                case "R4":
            //                    lblER4.Content = parte[1] == "1" ? "Encendido" : "Apagado";
            //                    break;
            //                case "R":
            //                    lblER1.Content = parte[1][0] == '1' ? "Encendido" : "Apagado";
            //                    lblER2.Content = parte[1][1] == '1' ? "Encendido" : "Apagado";
            //                    lblER3.Content = parte[1][2] == '1' ? "Encendido" : "Apagado";
            //                    lblER4.Content = parte[1][3] == '1' ? "Encendido" : "Apagado";
            //                    break;
            //                case "M":
            //                    lblEPIR.Content = DateTime.Now.ToShortTimeString();
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }
            //    }
            //    mensajeRecibido = null;
            //}
        }

        private void PanelUsuario_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            plotGrafica.Width = e.NewSize.Width / 2 * .95;
        }

        private void lstDispositivos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarDatos();
            topico = $"SolucionIoT/{model.DispositivoSeleccionado.Id}";
            MqttService.Suscribir(topico);
            btnActualizar.IsEnabled = true;


            //model.DispositivoSeleccionado = lstDispositivos.SelectedItem as Dispositivo;
            //model.LecturasDelDispositivo = FactoryManager.LecturaManager().LecturasDelDispositivo(model.DispositivoSeleccionado.Id).ToList();

            //dtgLecturas.ItemsSource = null;
            //dtgLecturas.ItemsSource = model.LecturasDelDispositivo;
        }

        private void ActualizarDatos()
        {
            this.Cursor = Cursors.Wait;
            model.DispositivoSeleccionado = lstDispositivos.SelectedItem as Dispositivo;

            model.LecturasDelDispositivo = FactoryManager.LecturaManager().LecturasDelDispositivo(model.DispositivoSeleccionado.Id).OrderBy(e => e.FechaHora).ToList();
            Graficar();
            dtgLecturas.ItemsSource = null;
            dtgLecturas.ItemsSource = model.LecturasDelDispositivo;
            //lstMensajes.Items.Add($"{DateTime.Now.ToShortTimeString()}: Mostrando datos del dispositivo{model.DispositivoSeleccionado.Ubicacion}");
            //opciones = new List<ComandosModel>()
            //{
            //    new ComandosModel("R11",$"Encender"+model.DispositivoSeleccionado.UsoRelevador1),
            //    new ComandosModel("R10",$"Apagar"+model.DispositivoSeleccionado.UsoRelevador1),
            //    new ComandosModel("R21",$"Encender"+model.DispositivoSeleccionado.UsoRelevador2),
            //    new ComandosModel("R20",$"Apagar"+model.DispositivoSeleccionado.UsoRelevador2),
            //    new ComandosModel("R31",$"Encender"+model.DispositivoSeleccionado.UsoRelevador3),
            //    new ComandosModel("R30",$"Apagar"+model.DispositivoSeleccionado.UsoRelevador3),
            //    new ComandosModel("R41",$"Encender"+model.DispositivoSeleccionado.UsoRelevador4),
            //    new ComandosModel("R40",$"Apagar"+model.DispositivoSeleccionado.UsoRelevador4),
            //    new ComandosModel("B1",$"Encender"+model.DispositivoSeleccionado.UsoBuzzer),
            //    new ComandosModel("B0",$"Apagar"+model.DispositivoSeleccionado.UsoBuzzer),
            //};
            //lblR1.Content = "Estado de " + model.DispositivoSeleccionado.UsoRelevador1;
            //lblR2.Content = "Estado de " + model.DispositivoSeleccionado.UsoRelevador2;
            //lblR3.Content = "Estado de " + model.DispositivoSeleccionado.UsoRelevador3;
            //lblR4.Content = "Estado de " + model.DispositivoSeleccionado.UsoRelevador4;
            //lblBuzzer.Content = "Estado de " + model.DispositivoSeleccionado.UsoBuzzer;
            //lblPIR.Content = "Ultimo movimiento en " + model.DispositivoSeleccionado.UbicacionPIR;
            //lblER1.Content = "";
            //lblER2.Content = "";
            //lblER3.Content = "";
            //lblER4.Content = "";
            //lblEBuzzer.Content = "";
            //lblPIR.Content = "";
            //cmbOpciones.ItemSource = null;
            //cmbOpciones.ItemSource = opciones;
            //this.Cursor = Cursors.Arrow;
        }

        private void Graficar()
        {
            PlotModel grafica = new PlotModel();
            DateTimeAxis ejeTiempo = new DateTimeAxis();
            LineSeries temperatura = new LineSeries();
            LineSeries humeadadAmbiental = new LineSeries();
            LineSeries luminosidad = new LineSeries();
            foreach (var item in model.LecturasDelDispositivo)
            {
                temperatura.Points.Add(DateTimeAxis.CreateDataPoint(item.FechaHora, item.Temperatura));
                humeadadAmbiental.Points.Add(DateTimeAxis.CreateDataPoint(item.FechaHora, item.Humedad));
                luminosidad.Points.Add(DateTimeAxis.CreateDataPoint(item.FechaHora, item.Luminosidad));
            }
            temperatura.Title = "Temperatura °C";
            humeadadAmbiental.Title = "Humedad ambiental %";
            luminosidad.Title = "Luminosidad";
            grafica.Axes.Add(ejeTiempo);
            grafica.Series.Add(temperatura);
            grafica.Series.Add(humeadadAmbiental);
            grafica.Series.Add(luminosidad);
            plotGrafica.Model = grafica;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MqttService.Publicar(topico, "?R");
            Task.Delay(2500);
            MqttService.Publicar(topico, "?B");
        }
    }
}
