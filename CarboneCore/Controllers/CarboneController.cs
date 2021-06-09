using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Carbone.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carbone.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarboneController : ControllerBase
    {
        private readonly INodeServices _nodeServices;
        public CarboneController(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        [HttpPost]
        [Route("GetXlsxReport")]
        public async Task<IActionResult> GetXlsxReport([FromBody] ReportRequest data)
        {
            var result = await _nodeServices.InvokeExportAsync<Document>(
                       "./Script/carbone.js", // Path to our JavaScript file
                       "createXlsx", // Exported function name
                       new string[] { data.Data, data.NombreReporte }); // Arguments, in this case a json string

            return File(result.data, "application/vnd.ms-excel");
        }

        [HttpPost]
        [Route("GetPdfReport")]
        public async Task<IActionResult> GetPdfReport([FromBody] ReportRequest data)
        {
            var result = await _nodeServices.InvokeExportAsync<Document>(
                       "./Script/carbone.js", // Path to our JavaScript file
                       "createPdf", // Exported function name
                       new string[] { data.Data, data.NombreReporte }); // Arguments, in this case a json string

            return File(result.data, "application/pdf");

            //return await ConvertToPdf(result.data);
        }

        //private async Task<IActionResult> ConvertToPdf(byte[] data)
        //{
        //    Guid tempfilename = new Guid();
        //    var sourceFileName = $@"tmp\{tempfilename.ToString()}.tmp";

        //    if (!System.IO.Directory.Exists("tmp"))
        //    {
        //        System.IO.Directory.CreateDirectory("tmp");
        //    }

        //    using (var resultStream = new MemoryStream(data))
        //    {

        //        using (var sourceFile = System.IO.File.OpenWrite(sourceFileName))
        //        {
        //            await resultStream.CopyToAsync(sourceFile);
        //            await sourceFile.FlushAsync();
        //        }
        //    }

        //    // Convert file to PDF using libreoffice
        //    var pdfProcess = new Process();
        //    pdfProcess.StartInfo.FileName = @"libreoffice\program\soffice.exe";
        //    pdfProcess.StartInfo.Arguments = $"--norestore --nofirststartwizard --headless --convert-to pdf \"{Path.GetFileName(sourceFileName)}\"";
        //    pdfProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(sourceFileName); //This is really important
        //    pdfProcess.Start();

        //    // Wait while document is converting
        //    pdfProcess.WaitForExit();

        //    // Check if file was converted properly
        //    var destinationFileName = $"{Path.Combine(Path.GetDirectoryName(sourceFileName), Path.GetFileNameWithoutExtension(sourceFileName))}.pdf";
        //    if (!System.IO.File.Exists(destinationFileName))
        //    {
        //        return new BadRequestObjectResult("Error converting file to PDF");
        //    }


        //    byte[] buffer = await System.IO.File.ReadAllBytesAsync(destinationFileName);

        //    System.IO.File.Delete(destinationFileName);
        //    System.IO.File.Delete(sourceFileName);

        //    return File(buffer, "application/pdf");
        //}
    }
}
