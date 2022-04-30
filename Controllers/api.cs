using Conversion.Class;
using Conversion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class api : ControllerBase
    {
        private readonly Context _context;

        private readonly ConvertServices _convertservices;

        private readonly ILogger<api> _logger;

        private readonly UsersRepository _usersrepository;

        private readonly ConvertRepository _convertrepository;

        private readonly UsersService _userservice;

        public api(ILogger<api> logger, Context context, ConvertServices convertservices, UsersRepository usersrepository, UsersService usersservice, ConvertRepository convertrepository)
        {
            _logger = logger;

            _context = context;

            _convertservices = convertservices;

            _usersrepository = usersrepository;

            _convertrepository = convertrepository;

            _userservice = usersservice;
        }

        [HttpGet("ConsultUser/{IDUser}")] //Query the user in the bank and bring in a json the name, email, phone, date of birth
        public async Task<IActionResult> GetUser([FromServices] Context context, int IDUser)
        {
            var user = await context.Users.AsNoTracking().Where(w => w.IDUser == IDUser).ToListAsync();

            var usuario = new Users();

            return Ok(user);
        }


        [HttpPost("RegisterUser")] //Register a new user using json
        public async Task<IActionResult> PostRegisterUser([FromServices] Context context, [FromBody] Users users)
        {
            try
            {

                var verifica = _userservice.Check(users);

                if (verifica)
                {
                    return BadRequest("This user already exists.");
                }

                var register = await _usersrepository.InsertAsync(users);


                return Ok("Registered user.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem(ex.Message, "Unable to register the user. ", 500); // new Exception(ex.Message);
            }

        }




        [HttpGet("Consult/User/{IDUser}/{CurrencyOrigin}/{orvalue}/{DestCurrency}/{destvalue}")] //Check the coin bringing its rate, this part still needs to be finished
        public async Task<IActionResult> GetCoin(int IDUser, string CurrencyOrigin, decimal orvalue, string DestCurrency, decimal destvalue)
        {
            try
            {
                var retorno = await _convertservices.GetRateCoin();

                //1.054463


                var users = await _context.Users.AsNoTracking().Where(w => w.IDUser == IDUser).ToListAsync();

                if (users.ToList().Count == 0)
                {
                    return NotFound("User does not exist.");
                }


                var registercoin = new Converts();

                var indice = retorno["rates"]["USD"];

                registercoin.IDUser = IDUser;

                registercoin.CurrencyOrigin = CurrencyOrigin;

                registercoin.Rate = indice;

                registercoin.OriginValue = orvalue;

                registercoin.DestCurrency = DestCurrency;

                registercoin.DestValue = destvalue;

                registercoin.Date_Time = DateTime.Now;



                var register = await _convertrepository.InsertAsync(registercoin);


                return Ok(registercoin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem(ex.Message, "Error performing currency conversion. ", 500); // new Exception(ex.Message);
            }

        }
    }
}
