namespace DotNet8WebApi.ODataSample.Features.Blog;

public class BlogsController : ODataController
{
    private readonly AppDbContext _context;

    public BlogsController(AppDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IQueryable<BlogModel> Get()
    {
        return _context.Blogs;
    }

    [EnableQuery]
    public SingleResult<BlogModel> Get([FromODataUri] int key)
    {
        var result = _context.Blogs.Where(b => b.BlogId == key);
        return SingleResult.Create(result);
    }

    public async Task<IActionResult> Post([FromBody] BlogModel blog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        return Created(blog);
    }

    public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<BlogModel> blog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingBlog = await _context.Blogs.FindAsync(key);
        if (existingBlog == null)
        {
            return NotFound();
        }

        blog.Patch(existingBlog);
        await _context.SaveChangesAsync();

        return Updated(existingBlog);
    }

    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var blog = await _context.Blogs.FindAsync(key);
        if (blog == null)
        {
            return NotFound();
        }

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
