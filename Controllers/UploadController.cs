using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
    [HttpPost]
    public async Task<IActionResult> PostAsync(List<IFormFile> Files)
    {
        long size = Files.Sum(f => f.Length);
        foreach (var formFile in Files)
        {
            if (formFile.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        return Ok(new { count = Files.Count, size });
    }

    public async Task<byte[]> GetAsync(IFormFile file)
    {
        using (var target = new MemoryStream())
        {
            await file.CopyToAsync(target);
            return target.ToArray();
        }
    }
}
}


