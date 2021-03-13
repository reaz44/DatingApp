using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]

    //"[controller]" - this is a placeholder which gets replaced 
    //by the term infront of Controller below, Eg- Car in this case
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private static readonly string[] CarNames = new[]
        {
            "Toyota", "Honda", "BMW", "Ford"
        };

            
        List<Car> CarTypes = new List<Car>()
                    {
                        new Car() { Id=1, Type="Sedan", Model="Toyota", Date=DateTime.Now },
                        new Car() { Id=2, Type="TUV", Model="Honda", Date=DateTime.Now }
                    };
        

        //Any GET request that goes out API with the Car Route will get sent to this particular end-point
        // nad the data is returned as an Array
        //Controller Endpoint
        [HttpGet]
        public IEnumerable<Car> Get(){

            return CarTypes;
        }


    }
}