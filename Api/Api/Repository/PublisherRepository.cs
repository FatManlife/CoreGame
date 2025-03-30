using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly AppDbContext _context;

    public PublisherRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Publisher>> GetAllPublishers()
    {
        return await _context.Publishers.ToListAsync();
    }

    public async Task<Publisher> GetPublisherById(int id)
    {
        return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreatePublisher(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
            
    }

    public async Task UpdatePublisher(Publisher publisher)
    {
       _context.Publishers.Update(publisher);
       await _context.SaveChangesAsync();
    }

    public async Task DeletePublisher(Publisher publisher)
    {
        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }

    public async Task<Publisher> GetPublisherByName(string name)
    {
        return await _context.Publishers.FirstOrDefaultAsync(p => p.Name == name);
    }
}