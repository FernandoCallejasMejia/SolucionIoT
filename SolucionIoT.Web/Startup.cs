using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolucionIoT.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SolucionIoT.Web
{
    public class Startup
    {
        readonly Random r = new Random();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MqttService.Conectar("WEBKobra" + r.Next(0, 10000),"broker.hivemq.com");
            MqttService.Conectado += MqttService_Conectado;
            MqttService.MensajeRecibido += MqttService_MensajeRecibido;
        }

        private void MqttService_Conectado(object sender, string e)
        {
            Debug.WriteLine("MQTT-->Conectado");
        }

        private void MqttService_MensajeRecibido(object sender, MensajeRecibido e)
        {
            Debug.WriteLine($"MQTT<--[{e.Topico}] {e.Mensaje}");
        }

        //private void MqttService_Conectado(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("MQTT-->Conectado"); 
        //}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddAuthentication("Seguridad").AddCookie("Seguridad", options =>
            {
                options.LoginPath = new PathString("/Index");
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
