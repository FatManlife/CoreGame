using Api;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Repository;

public class ImageRepository : IImageRepository
{
    private readonly AppDbContext _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Image>> GetImages()
    {
        return await _context.Images.ToListAsync();
    }
    
}