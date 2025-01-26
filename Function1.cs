#region
// Mikayle Coetzee
// ST10023767
// CLDV6212 POE 2023
// Part 1
#endregion

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CLDV6212_POE_ST10023767
{
    public static class Function1
    {
        //・♫-------------------------------------------------------------------------------------------------♫・//
        [FunctionName("ST10023767")] //remove the "ST10023767" from the /id/{id?} = feedback 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "ST10023767/id/{id?}")] HttpRequest req,
            string id, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Initialize validation class
            ValidationClass validate = new ValidationClass();

            // Read the request body and deserialize JSON data
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Assign values from query parameters or request body
            id = id ?? data?.id;

            if (id == "{id")
            {
                id = null;
            }

            string report = string.Empty;

            // Validate input
            if (!string.IsNullOrEmpty(id) && !validate.ValidateInput(id).Item1)
            {
                return new BadRequestObjectResult(validate.ValidateInput(id).Item2);
            }
            else
            {
                if (id != null)
                {
                    report = validate.ValidateInput(id).Item2;
                }
            }

            // Construct response message
            string responseMessage = string.IsNullOrEmpty(id)
                ? "This HTTP triggered function executed successfully. Pass your id or passport number" +
                  " in the query string or in the request body for a personalized response."
                : report;

            // Return the result message 
            return new OkObjectResult(responseMessage);
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//





