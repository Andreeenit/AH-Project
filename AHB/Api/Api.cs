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
}