using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SolucionIoT.BIZ.API;
using SolucionIoT.COMMON.Modelos;
using SolucionIoT.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolucionIoT.ClienteMovil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DispositivoTabbedPage : TabbedPage
    {
        PanelUsuarioModel model;
        MensajeRecibido mensajeRecibido;
        List<string> log;
        Random r;
        string topico;
        public DispositivoTabbedPage(PanelUsuarioModel model)
        {
            InitializeComponent();
            r = new Random();
            log = new List<string>();
            this.model = model;
            this.BindingContext = model;
            model.LecturasDelDispositivo = FactoryManager.LecturaManager().LecturasDelDispositivo(model.DispositivoSeleccionado.Id).ToList();
            MqttService.Conectar("SolucionIoTMovil" + r.Next(0, 1000).ToString(), "broker.hivemq.com");
            MqttService.Conectado += MqttService_Conectado;
            MqttService.Error += MqttService_Error;
            MqttService.Mensaje += MqttService_Mensaje;
            MqttService.MensajeRecibido += MqttService_MensajeRecibido;
            model.LecturasDelDispositivo = FactoryManager.LecturaManager().LecturasDelDispositivo(model.DispositivoSeleccionado.Id).OrderBy(e => e.FechaHora).ToList();            
            lstLecturas.ItemsSource = null;
            lstLecturas.ItemsSource = model.LecturasDelDispositivo;
            topico = "SolucionIoT/" + model.DispositivoSeleccionado.Id;
            LlenarComandos();
            Graficar();
            mensajeRecibido = null;

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                lstLog.ItemsSource = null;
                lstLog.ItemsSource = log;
                if (mensajeRecibido != null)
                {
                    if (mensajeRecibido.Topico == topico)
                    {
                        log.Add($"<{ mensajeRecibido.Mensaje}");
                        if (mensajeRecibido.Mensaje.Contains("="))
                        {
                            string[] parte = mensajeRecibido.Mensaje.Split('=');
                            switch (parte[0])
                            {
                                case "B":
                                    lblEB.Text = parte[1] == "1" ? "Encendido" : "Apagado";
                                    break;
                                case "R1":
                                    lblER1.Text = parte[1] == "1" ? "Encendido" : "Apagado";
                                    break;
                                case "R2":
                                    lblER2.Text = parte[1] == "1" ? "Encendido" : "Apagado";
                                    break;
                                case "R3":
                                    lblER3.Text = parte[1] == "1" ? "Encendido" : "Apagado";
                                    break;
                                case "R4":
                                    lblER4.Text = parte[1] == "1" ? "Encendido" : "Apagado";
                                    break;
                                case "R":
                                    lblER1.Text = parte[1][0] == '1' ? "Encendido" : "Apagado";
                                    lblER2.Text = parte[1][1] == '1' ? "Encendido" : "Apagado";
                                    lblER3.Text = parte[1][2] == '1' ? "Encendido" : "Apagado";
                                    lblER4.Text = parte[1][3] == '1' ? "Encendido" : "Apagado";
                                    break;
                                case "M":
                                    lblEM.Text = DateTime.Now.ToShortTimeString();
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    mensajeRecibido = null;
                }
                return true;
            });
            Thread.Sleep(2000);
            MqttService.Suscribir("SolucionIoT/" + model.DispositivoSeleccionado.Id);

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

        private void LlenarComandos()
        {
            lblR1.Text = model.DispositivoSeleccionado.UsoRelevador1;
            lblR2.Text = model.DispositivoSeleccionado.UsoRelevador2;
            lblR3.Text = model.DispositivoSeleccionado.UsoRelevador3;
            lblR4.Text = model.DispositivoSeleccionado.UsoRelevador4;
            lblB.Text = model.DispositivoSeleccionado.UsoBuzzer;
            lblM.Text = "Ultimo mov. en "+model.DispositivoSeleccionado.UbicacionPIR;
        }

        private void MqttService_MensajeRecibido(object sender, MensajeRecibido e)
        {
            mensajeRecibido = e;
        }

        private void MqttService_Mensaje(object sender, string e)
        {
            log.Add(e);
        }

        private void MqttService_Error(object sender, string e)
        {
            log.Add($"ERROR {e}");
        }

        private void MqttService_Conectado(object sender, string e)
        {
            log.Add(e);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "?R");
            Thread.Sleep(2000);
            MqttService.Publicar(topico, "?B");
        }

        private void btnR11_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R11");
        }

        private void btnR10_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R10");
        }

        private void btnR21_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R21");
        }

        private void btnR20_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R20");
        }

        private void btnR31_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R31");
        }

        private void btnR30_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R30");
        }

        private void btnR40_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R40");
        }

        private void btnR41_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "R41");
        }

        private void btnB1_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "B1");
        }

        private void btnB0_Clicked(object sender, EventArgs e)
        {
            MqttService.Publicar(topico, "B0");
        }
    }
}