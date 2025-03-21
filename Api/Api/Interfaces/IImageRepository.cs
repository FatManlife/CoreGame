using Api.Models;

namespace Api.Interfaces;

public interface IImageRepository
{
    Task<ICollection<Image>> GetImages();

}