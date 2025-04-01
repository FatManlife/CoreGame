using Api.Identity;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class DetailsGameRepository: IDetailsGameRepository
{
    private readonly AppDbContext _context;

    public DetailsGameRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Platform>> getPlatforms(ICollection<int> platformIds)
    {
        return await _context.Platforms.Where(p => platformIds.Contains(p.Id)).ToListAsync();
    }

    public async Task<ICollection<Mode>> getModes(ICollection<int> modeIds)
    {
        return await _context.Modes.Where(p => modeIds.Contains(p.Id)).ToListAsync();
    }

    public async Task<ICollection<Genre>> getGeneres(ICollection<int> generesIds)
    {
        return await _context.Genres.Where(p => generesIds.Contains(p.Id)).ToListAsync();
    }

    public async Task AddImage(Image image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteImage(Image image)
    {
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
    }

    public async Task<Image> getImageById(int id)
    {
        return await _context.Images.FindAsync(id);
    }
    
    public async Task<Spec> getSpecById(int id)
    {
        return await _context.Specs.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task updateSpec(Spec spec)
    {
        _context.Specs.Update(spec);
        await _context.SaveChangesAsync();
    }


}