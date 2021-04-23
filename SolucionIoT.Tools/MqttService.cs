using OpenNETCF.MQTT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SolucionIoT.Tools
{
    public static class MqttService
    {
        static MQTTClient mqtt;
        public static event EventHandler<MensajeRecibido> MensajeRecibido;
        public static event EventHandler<string> Conectado;
        public static event EventHandler<string> Error;
        public static event EventHandler<string> Mensaje;
        private static string NombreCliente;

        public static void Conectar(string nombreCliente, string server, int puerto=1883)
        {
            mqtt = new MQTTClient(server, puerto);
            NombreCliente = nombreCliente; 
            mqtt.Connected += Mqtt_Connected;
            mqtt.MessageReceived += Mqtt_MessageReceived;
            mqtt.Disconnected += Mqtt_Disconnected;
            mqtt.Subscriptions.SubscriptionAdded += Subscriptions_SubscriptionAdded;
            mqtt.Subscriptions.SubscriptionRemoved += Subscriptions_SubscriptionRemoved;
            Connect();
        }

        private static void Subscriptions_SubscriptionRemoved(object sender, OpenNETCF.GenericEventArgs<string> e)
        {
            Mensaje(null, $"Se elimino la suscripcion");
        }

        private static void Subscriptions_SubscriptionAdded(object sender, OpenNETCF.GenericEventArgs<Subscription> e)
        {
            Mensaje(null, $"Se realizo la suscripcion a {e.Value.TopicName}");
        }

        private static void Mqtt_Disconnected(object sender, EventArgs e)
        {
            Task.Delay(2000);
            Connect();
        }

        private static void Mqtt_MessageReceived(string topic, QoS qos, byte[] payload)
        {
            Debug.WriteLine("<-" + System.Text.Encoding.UTF8.GetString(payload));
            MensajeRecibido(null, new Tools.MensajeRecibido(){
                Mensaje=System.Text.Encoding.UTF8.GetString(payload),
                Topico=topic 
            });
        }

        private static void Mqtt_Connected(object sender, EventArgs e)
        {
            Debug.WriteLine($"Conectado a MQTT");
            Conectado(null, "Conectado como "+NombreCliente);
        }

        private static void Connect()
        {
            _ = Task.Run(() =>
            {
                mqtt.Connect(NombreCliente);
            }); 
        }

        public static void Suscribir(string topic)
        {
            if (mqtt.IsConnected)
            {
                mqtt.Subscriptions.Add(new Subscription(topic));
            }
            else
            {
                Error(null, "No hay conexion al servidor MQTT");
            }
        }

        public static void Publicar(string topic, string mensaje)
        {
            if (mqtt.IsConnected)
            {
                Debug.WriteLine($"-> [{topic}] {mensaje}");
                mqtt.Publish(topic, mensaje, QoS.FireAndForget, false);
            }
            else
            {
                Error(null, "No hay conexion al servidor MQTT, no se puede enviar el mensaje");
            }
            
        }

        public static void QuitarSuscripcion(string topic)
        {
            mqtt.Subscriptions.Remove(topic);
        }
    }
}
