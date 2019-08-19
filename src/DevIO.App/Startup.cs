using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevIO.Data.Context;
using AutoMapper;
using DevIO.App.Configurations;

namespace DevIO.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);

            services.AddDbContext<MeuDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Quando passa o Startup aqui, ele diz para procurar qualquer classe dentro do assembly DevIO.App, que possua o
            // Profile como herança (porque é uma classe de configuração de perfil de mapeamento do AutoMapper); e na classe resolve os mapeamentos criados;
            services.AddAutoMapper(typeof(Startup));

            services.AddMvcConfiguration();

            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // se o seu ambiente for desenvolvimento, ele irá utilizar as páginas de erro para desenvolvedor;

                // no caso de uma exception;
                app.UseDeveloperExceptionPage();
                // no caso de erro de banco, inclusive quando falta migrations ele pede para aplicá-las;
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithReExecute("/erro/{0}");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // Esse é um middleware que adiciona o strict transport security dentro do seu header;
                // ao verificar os requests, irá ver que ele irá passar o strict transport security;
                // O Hsts é uma implementação de segurança que os browsers modernos já suportam, onde se você tentar uma conexão não segura, 
                // ele vai te forçar seguir por uma conexão segura. Então se você não tiver uma conexão segura, irá receber um erro;
                app.UseHsts(); 
            }

            // O UseHttpsRedirection faz a mesma coisa (que o Hsts); a função dele aqui é redirecionar Http para Https, assim como o Hsts;
            // só que tem um detalhe, o strict transport security header precisa fazer uma primeira negociação com o browser;
            // quando o browser chamar a sua app pela primeira vez, a app irá devolver o header com o Hsts (informando que só irá conversar dentro de um protoloco seguro Https),
            // e o browser irá guardar essa informação no cache, dentro do browser (ficará lá até você limpar o cache); Isto é para ter certeza que aquele domínio, só se comunica
            // através de uma conexão segura, qualquer coisa que vier de forma insegura, ele irá redirecionar automaticamente;
            // Só que para o Hsts entrar em vigor com o browser, ele precisa dessa primeira negociação, e ai que entra o redirecionamento do Https, porque se a primeira
            // vez que o browser chamar a aplicação via Http, e não tiver o redirecionamento Https, ele irá responder Http, e ai o Hsts nunca vai ser configurado no browser - o browser
            // nunca irá se lembrar do seu domínio porque o Hsts nunca chegou até lá através do strict transport security header que tem que ser passado;
            // Então para garantir essa primeira conexão com o browser e passar o header, é necessário então forçar o redirecionamento; por isto que os dois trabalham sempre juntos;
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseGlobalizationConfig();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
