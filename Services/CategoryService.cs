using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace DilanBlazor;

    public class CategoryService : ICategoryService
    {
        private readonly HttpClient client;

        private readonly JsonSerializerOptions options;

        public CategoryService(HttpClient client)
        {
           this.client = client;
           options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Category>?> GetCategoriesAsync()
        {
            var response = await client.GetAsync("https://api.escuelajs.co/api/v1/products");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            return JsonSerializer.Deserialize<List<Category>>(content, options);
        }
   
    }

public interface ICategoryService
{
    Task<List<Category>?> GetCategoriesAsync();
}

