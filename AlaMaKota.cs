using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace _248993
{
    /// <summary>
    /// Klasa statyczna <c>AlaMaKota</c> zawiera metod� wy�wietlaj�c� po��czone zmienne napisowe z ��dania.
    /// <list type="bullet">
    /// <item>
    /// <term>Run </term>
    /// <description>Metoda o nazwie "AlaMaKota" wy�wietlaj�ca po��czone zmienne napisowe.</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>Klasa statyczna <c>AlaMaKota</c> zawiera metod� wy�wietlaj�c� po��czone zmienne napisowe z ��dania.</para>
    /// </remarks>
    public static class AlaMaKota
    {
        /// <summary>
        /// Metoda wy�wietlaj�ca po��czone zmienne napisowe.
        /// </summary>
        /// <remarks>
        /// <para>Metoda wy�wietlaj�ca po��czone zmienne napisowe lub pro�b� o podanie napis�w.</para>
        /// </remarks>
        /// <returns>Zmienna napisowa stanowi�ca po��czenie napis�w z ��dania lub pro�b� o ich podanie.</returns>
        [FunctionName("AlaMaKota")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string str1 = req.Query["str1"];
            string str2 = req.Query["str2"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            str1 += str2;
            str1 = str1 ?? data?.str1;

            string responseMessage = string.IsNullOrEmpty(str1)
                ? "This HTTP triggered function executed successfully. " +
                "Pass a str1 and str2 in the query string or in the request body for a response as a combination (concatenation) of both strings. " +
                "Request format: \"https://248993.azurewebsites.net/AlaMaKota?str1={str1}&str2={str2}\"."
                : $"{str1}";

            return new OkObjectResult(responseMessage);
        }
    }
}
