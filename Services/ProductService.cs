using System.Net.Http.Json;
using System.Text.Json;

namespace DilanBlazor;

public class ProductService : IProductService 
{
    public readonly HttpClient client;

    public readonly JsonSerializerOptions options;

    public ProductService(HttpClient httpclient)
    {
        client = httpclient;
        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Product>?> GetProductsAsync()
    {
        var response = await client.GetAsync("https://api.escuelajs.co/api/v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Product>>(content, options);
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var response = await client.GetAsync($"https://api.escuelajs.co/api/v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<Product>(content, options);
    }

    public async Task Add(Product product)
    {
        var response = await client.PostAsync("https://api.escuelajs.co/api/v1/products/", JsonContent.Create(product));
        var Content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(Content);
        }
    }

    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"https://api.escuelajs.co/api/v1/products/{productId}");
        var Content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(Content);
        }
    }

    public async Task Update(Product product)
    {
        var response = await client.PutAsync($"https://api.escuelajs.co/api/v1/products/{product.id}", JsonContent.Create(product));
        var Content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(Content);
        }
    }
}

public interface IProductService
{
    Task<List<Product>?> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task Add(Product product);
    Task Delete(int productId);
    Task Update(Product product);
}

