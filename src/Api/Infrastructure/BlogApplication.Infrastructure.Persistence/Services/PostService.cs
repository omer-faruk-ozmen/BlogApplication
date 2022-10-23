using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Services;
using BlogApplication.Api.Domain.Models;
using Newtonsoft.Json;

namespace BlogApplication.Infrastructure.Persistence.Services
{
    public class PostService : IPostService
    {
        public async Task AddToPhysicalFilePath(Post post)
        {
            var jsonString = JsonConvert.SerializeObject(post, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            string fileName = post.Title.Replace(" ", "");

            string? filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

            fileName = $"{filePath}\\Desktop\\Software\\Notes\\BlogPosts\\{fileName}.json";

            if (File.Exists(fileName))
            {
                throw new Exception("Dosya yolu dolu");
            }

            StreamWriter write = new StreamWriter(fileName, append: true);

            await write.WriteAsync(jsonString);
            write.Close();

            await Task.FromResult(true);
        }
    }
}
