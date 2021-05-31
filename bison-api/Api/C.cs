using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bison_api.Api
{
    /// <summary>
    /// Clase que contiene constantes generales de la web api
    /// </summary>
    public class C
    {
        public static readonly string APP_NAME = "bison";
        public static readonly string APP_VERSION = "1.0.0";
        public static readonly string APP_NAME_VERSION = $"{APP_NAME} - {APP_VERSION}";

        public static readonly string JWT_SECRETKEY_NAME = "SecretKey";
        public static readonly string JWT_SECRETKEY_VALUE = "_\"asdwda1d8a4sd8w4das8d*w8d*asd@#\"";

        public static readonly string STR_CONNECTION_NAME = "bison_connection";

        public static readonly string SWAGGER_TITLE = APP_NAME_VERSION;
        public static readonly string SWAGGER_VERSION = APP_VERSION;
        public static readonly string SWAGGER_DESCRIPTION = "Web API para aplicación bison";
        public static readonly string SWAGGER_ENDP_URL_DEFAULT = $"/swagger/{SWAGGER_VERSION}/swagger.json";
        public static readonly string SWAGGER_ENDP_NAME_DEFAULT = APP_NAME_VERSION;

        /// <summary>
        /// Expiración de token en minutos
        /// </summary>
        public static readonly int C_AUTH_EXPIRACION_TOKEN = 60;
    }
}
