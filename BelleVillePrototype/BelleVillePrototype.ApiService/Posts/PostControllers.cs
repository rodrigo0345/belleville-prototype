using BelleVillePrototype.ApiService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelleVillePrototype.ApiService.Posts;

namespace BelleVillePrototype.ApiService.Posts;

[Route("posts")]
public class PostControllers: Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    public PostControllers(ILogger logger, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostGetDto>>> GetPosts(
        [FromQuery]PostQueryDto query, CancellationToken ct)
    {
        this._logger.LogInformation("Querying posts with {query}", query);
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var postsDb = _dbContext.Posts.AsQueryable();
        
        if(query.Id is not null)
            postsDb = postsDb.Where(p => p.Id.Value == query.Id);
        
        if(query.Author is not null)
            postsDb = postsDb.Where(p => (p.Author != null) && p.Author.Contains(query.Author));

        if (query.Title is not null)
            postsDb = postsDb.Where(p => p.Title.Contains(query.Title));
        
        var posts = await postsDb.ToListAsync(ct);
        return Ok(posts);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PostGetDto>> GetPost(
        [FromRoute]Guid id, CancellationToken ct)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id, ct);
        if(post is null)
            return NotFound();
        
        return Ok(post);
    }
    
    [HttpPost]
    public async Task<ActionResult<PostGetDto>> CreatePost(
        [FromBody]PostCreateDto postDto, CancellationToken ct)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var post = new PostModel
        {
            Id = PostId.NewPostId,
            Title = postDto.Title,
            Author = postDto.Author
        };
        
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync(ct);
        
        var postId = post.Id.Value;
        return CreatedAtAction(nameof(GetPost), new { id = postId }, post);
    }
}