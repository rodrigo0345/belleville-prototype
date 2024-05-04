namespace BelleVillePrototype.Web;

public class PostsApi(HttpClient httpClient)
{
    public async Task<PostModel[]> GetPostsAsync(int maxItems = 10,
        CancellationToken cancellationToken = default)
    {
        List<PostModel>? posts = null;

        await foreach (var post in httpClient.GetFromJsonAsAsyncEnumerable<PostModel>("/posts",
                           cancellationToken))
        {
            if (posts?.Count >= maxItems)
            {
                break;
            }

            if (post is not null)
            {
                posts ??= [];
                posts.Add(post);
            }
        }

        return posts?.ToArray() ?? [];
    }

    public async Task AddPostAsync(CreatePostModel postModel)
    {
        await httpClient.PostAsJsonAsync("/posts", postModel);
    }
}

public record PostModel(Guid id, string Title, string Author);
public record CreatePostModel(string Title, string Author);
