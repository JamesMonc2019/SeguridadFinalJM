using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeguridadFinalJM.DTOs;
using SeguridadFinalJM.Entidades;
using SeguridadFinalJM.Utilerias;
using System.Text;

namespace SeguridadFinalJM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;

        public TarjetaController(DatabaseContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this._configuration = configuration;
        }
       

        [HttpGet]
        public async Task<ActionResult<List<tarjeta>>> Get()
        {
            try
            {
                var list = await context.tarjeta.ToListAsync();
                return Ok(list);
            }
            catch (Exception)
            {

                return BadRequest("Se presento un error en tu solicitud");
            }
        }

        [HttpPost]
        public async Task<ActionResult<tarjeta>> Post([FromBody] TarjetaDtoCreate dtoCreate)
        {
            try
            {
                var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Keyaes"]));

                string salt = llave.ToString() + dtoCreate.plastico;
                string esSha = new Utilerias.Utils().MuestraSha(salt);

                byte[] plasticoEncriptado = new Utils().Encrypt(dtoCreate.plastico);
                string plasticoDesencriptado = new Utils().Decrypt(plasticoEncriptado);
                string esShaDecrypt = new Utilerias.Utils().MuestraSha(llave.ToString() + plasticoDesencriptado);

                string tarjet = new Utilerias.Utils().MaskTarjetaCRedito(dtoCreate.plastico);

                if (esShaDecrypt != esSha)
                {
                    return BadRequest("los datos del plastico no son Iguales");
                }
                dtoCreate.shatarjeta = esSha;
                dtoCreate.plastico = tarjet;
                dtoCreate.encryptedtarjeta = Convert.ToBase64String(plasticoEncriptado);
                var tar = mapper.Map<tarjeta>(dtoCreate);
                context.Add(tar);
                await context.SaveChangesAsync();
                return tar; 
            }
            catch (Exception)
            {
                return BadRequest("NO fue posible guardar su información");
            }
            
        }
    }
}