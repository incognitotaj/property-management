using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IEFRepository
    {
        Task AddAsync(object entity);
        Task DeleteAsync(object entity);
        Task<bool> SaveChangesAsync();
        Task UpdateAsync(object entity);

        Task<IEnumerable<Service>> GetServicesAsync(bool includeDetails = false);
        Task<Service> GetServiceByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<Blog>> GetBlogsAsync(int? categoryId, bool includeDetails = false);
        Task<Blog> GetBlogByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<Category>> GetCategoriesAsync(bool includeDetails = false);
        Task<Category> GetCategoryByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<Comment>> GetCommentsAsync(int blogid, bool includeDetails = false);
        Task<Comment> GetCommentByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosAsync(bool includeDetails = false);
        Task<PropertyPhoto> GetPropertyPhotoByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<Property>> GetPropertiesAsync(bool includeDetails = false);
        Task<Property> GetPropertyByIdAsync(int id, bool includeDetails = false);
        Task<IEnumerable<Purpose>> GetPurposesAsync(bool includeDetails = false);
        Task<Purpose> GetPurposeByIdAsync(int id, bool includeDetails = false);
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync(bool includeDetails = false);
        Task<PropertyType> GetPropertyTypeByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<City>> GetCitiesAsync(bool includeDetails = false);
        Task<City> GetCityByIdAsync(int id, bool includeDetails = false);
        Task<IEnumerable<AreaUnit>> GetAreaUnitsAsync(bool includeDetails = false);
        Task<AreaUnit> GetAreaUnitByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<ServiceRequest>> GetServiceRequestsAsync(int serviceId, bool includeDetails = false);
        Task<ServiceRequest> GetServiceRequestByIdAsync(int id, bool includeDetails = false);
        Task<IEnumerable<Contact>> GetContactsAsync(bool includeDetails = false);
        Task<Contact> GetContactByIdAsync(int id, bool includeDetails = false);

        Task<IEnumerable<PropertyRequest>> GetPropertyRequestsAsync(int propertyId, bool includeDetails = false);
        Task<PropertyRequest> GetPropertyRequestByIdAsync(int id, bool includeDetails = false);
    }
}