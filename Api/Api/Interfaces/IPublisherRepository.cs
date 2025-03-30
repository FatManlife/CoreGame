using Api.Models;
using Api.Models.DTOs;

namespace Api.Interfaces;

public interface IPublisherRepository
{
    public Task<ICollection<Publisher>> GetAllPublishers();
    public Task<Publisher> GetPublisherById(int id);
    public Task CreatePublisher(Publisher publisher);
    public Task UpdatePublisher(Publisher publisher);
    public Task DeletePublisher(Publisher publisher);
    public Task<Publisher> GetPublisherByName(string name);
}