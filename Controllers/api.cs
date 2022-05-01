using System;
using System.Linq;
using System.Threading.Tasks;
using Conversion.Class;
using Conversion.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Conversion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class api : ControllerBase
    {
        private readonly Context _context;

        private readonly ConvertRepository _convertrepository;

        private readonly ConvertServices _convertservices;

        private readonly ILogger<api> _logger;

        private readonly UsersService _userservice;

        private readonly UsersRepository _usersrepository;

        public api(ILogger<api> logger, Context context, ConvertServices convertservices,
            UsersRepository usersrepository, UsersService usersservice, ConvertRepository convertrepository)
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
                var contain = _userservice.Check(users);

                if (contain)
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


        [HttpPost(
            "Consult/User/{IDUser}/{CurrencyOrigin}/{orvalue}/{DestCurrency}")] //Check the currency by bringing your rate and convert it to the chosen currency ex:   2 *  1.054463 /  5.243579 = 0.4021920905549434 (BRL >> USD)
        public async Task<IActionResult> GetCoin(int IDUser, string CurrencyOrigin, double orvalue, string DestCurrency)
        {
            try
            {
                var coinrate = await _convertservices.GetRateCoin();


                var users = await _context.Users.AsNoTracking().Where(w => w.IDUser == IDUser).ToListAsync();

                if (users.ToList().Count == 0)
                {
                    return NotFound("User does not exist.");
                }


                var registercoin = new Converts();

                var valuei1 = 0.00;

                var valuei2 = 0.00;

                switch (CurrencyOrigin)
                {
                    case "EUR":
                        valuei1 = coinrate["rates"][CurrencyOrigin];
                        break;

                    case "BRL":
                        valuei1 = coinrate["rates"][CurrencyOrigin];
                        break;

                    case "USD":
                        valuei1 = coinrate["rates"][CurrencyOrigin];
                        break;

                    case "JPY":
                        valuei1 = coinrate["rates"][CurrencyOrigin];
                        break;
                }


                switch (DestCurrency)
                {
                    case "EUR":
                        valuei2 = coinrate["rates"][DestCurrency];
                        break;

                    case "BRL":
                        valuei2 = coinrate["rates"][DestCurrency];
                        break;

                    case "USD":
                        valuei2 = coinrate["rates"][DestCurrency];
                        break;

                    case "JPY":
                        valuei2 = coinrate["rates"][DestCurrency];
                        break;
                }


                registercoin.IDUser = IDUser;

                registercoin.CurrencyOrigin = CurrencyOrigin;

                registercoin.Rate = valuei1;

                registercoin.OriginValue = orvalue;

                registercoin.DestCurrency = DestCurrency;

                var calculation = orvalue * valuei2 / valuei1;

                registercoin.DestValue = calculation;

                registercoin.Date_Time = DateTime.Now;


                var register = await _convertrepository.InsertAsync(registercoin);


                return Ok(calculation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem(ex.Message, "Error performing currency conversion. ", 500); // new Exception(ex.Message);
            }
        }
    }
}