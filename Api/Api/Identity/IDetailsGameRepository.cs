using Api.Models;

namespace Api.Identity;

public interface IDetailsGameRepository
{
    public Task<ICollection<Platform>> getPlatforms(ICollection<int> platformIds);
    public Task<ICollection<Mode>> getModes(ICollection<int> modeIds);
    public Task<ICollection<Genre>> getGeneres(ICollection<int> genresIds);
    public Task AddImage(Image image);
    public Task DeleteImage(Image image);
    public Task<Image> getImageById(int id);
    public Task<Spec> getSpecById(int id);
    public Task updateSpec(Spec spec);
    
}