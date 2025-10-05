namespace AH.Api;

public class Api
{
    public HttpClient _httpClient;

    public Api(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Article>> GetArticle()
    {
        return await _httpClient.GetFromJsonAsync<List<Article>>("http://localhost:5262/products");
    }


     /// Lägg till en produkt
    public async Task AddProductAsync(Article article)
    {
        await _httpClient.PostAsJsonAsync("https://localhost:7064/products", article);
    }
}
        
