using FirstWeb.API.Model.Domain;

namespace FirstWeb.API.Repositories.ADO.Net
{
    public interface IProductRepositoryADO
    {
        Product? getByName(string name);
    }
}
