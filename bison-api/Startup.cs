using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using bison_api.Api; // +
using Microsoft.EntityFrameworkCore; // +
using System.Reflection; // +
using Microsoft.OpenApi.Models; // +
using System.IO; // +
using Microsoft.AspNetCore.Authentication.JwtBearer; // +
using Microsoft.IdentityModel.Tokens; // +
using System.Text; // +
using bison_api.Interface; // +
using bison_api.Repository; // +

namespace bison_api
{
    public class Startup
    {
        // --------------------------------------------------
        // * significa que ya venia al crear el proyecto
        // / siginfica que ya venia y fue modificada
        // + significa que es nueva
        // --------------------------------------------------

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // *
            services.AddControllers();

            // --------------------------------------------------
            // + configurando sql con ef core (el dbcontext es para definir las tablas de la base de datos y ejecutar métodos CRUD)
            // --------------------------------------------------
            services.AddDbContext<Model.bisonContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(C.STR_CONNECTION_NAME));
            });

            // --------------------------------------------------
            // + agregando swagger (nos sirve para ver todos los métodos de los controladores en una interfaz gráfica y poder consumirlos)
            // --------------------------------------------------
            AgregarSwagger(services);

            // --------------------------------------------------
            // +
            // implementando mapper https://gavilanch.wordpress.com/2019/03/18/asp-net-core-2-2-objetos-de-transferencia-de-datos-y-automapper/
            // Ver la clase Helpers/AutoMapperProfiles.cs, aqui se indican los mapeados de las clases abstractas a los DTO
            // --------------------------------------------------
            services.AddAutoMapper(typeof(Startup));

            // --------------------------------------------------
            // + agregando cors
            // --------------------------------------------------
            services.AddCors(o =>
            {
                o.AddDefaultPolicy(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddCors();

            // --------------------------------------------------
            // + agregando servicios
            // --------------------------------------------------
            AgregarServicios(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // +
            ConfigurarSwagger(app);

            // *
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // *
            app.UseHttpsRedirection();

            // +
            app.UseStaticFiles();

            // *
            app.UseRouting();

            // +
            app.UseCors();

            // *
            app.UseAuthorization();

            // +
            app.UseAuthentication();

            // *
            app.UseEndpoints(endpoints =>
            {
                // *
                endpoints.MapControllers();
            });
        }

        private void AgregarSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(C.SWAGGER_VERSION, new OpenApiInfo()
                {
                    Version = C.SWAGGER_VERSION,
                    Title = C.SWAGGER_TITLE,
                    Description = C.SWAGGER_DESCRIPTION
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //if (!File.Exists(xmlPath))
                //{
                //    File.Create(xmlPath);
                //}

                o.IncludeXmlComments(xmlPath);

                // se agrega autenticacion por tokens
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Autorización encabezado usando el esquema Bearer. \r\n\r\n Ingresa 'Bearer' [space] y luego el token generado " +
                    "(el token se genera al logearse)." +
                    "\r\n\r\nEjemplo: \"Bearer aqui_va_el_token\"",
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // --------------------------------------------------
            // configurando tokens para autenticacion
            // --------------------------------------------------
            services.AddAuthentication(n =>
            {
                n.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                n.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(n =>
            {
                n.RequireHttpsMetadata = false;
                n.SaveToken = true;
                n.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    // IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[Constantes.JWT_SECRETKEY_NAME])),
                    // se puso hardcodedado porque al publicar el sitio en azure no podia obtener la llave de seguridad
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(C.JWT_SECRETKEY_VALUE)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private void ConfigurarSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint(C.SWAGGER_ENDP_URL_DEFAULT, C.SWAGGER_ENDP_NAME_DEFAULT);
            });
        }

        private void AgregarServicios(IServiceCollection services)
        {
            // services.AddTransient<IExampleRep, ExampleRep>();
            services.AddTransient(typeof(IUsuarioRep), typeof(UsuarioRep));
        }
    }
}
