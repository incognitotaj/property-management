using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EFRepository : IEFRepository
    {
        private readonly AskDbContext _context;

        public EFRepository(AskDbContext context)
        {
            _context = context;
        }

        #region Generic Methods
        public async Task AddAsync(object entity)
        {
            await _context.AddAsync(entity);
        }

        public Task UpdateAsync(object entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(object entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Persistance Layer
        /// </summary>
        /// <returns>true in case of success</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Services
        public async Task<IEnumerable<Service>> GetServicesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Services
                    .Include(p => p.ServiceRequests)
                    .ToListAsync();
            }

            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Services
                    .Include(p => p.ServiceRequests)
                    .FirstOrDefaultAsync(p => p.ServiceId == id);
            }

            return await _context.Services
                .FirstOrDefaultAsync(p => p.ServiceId == id);
        }
        #endregion

        #region Blogs
        public async Task<IEnumerable<Blog>> GetBlogsAsync(int? categoryId, bool includeDetails = false)
        {
            if (includeDetails)
            {
                if (categoryId.HasValue)
                {
                    return await _context.Blogs
                        .Include(p => p.Category)
                        .Include(p => p.Comments)
                        .Where(p => p.CategoryId == categoryId)
                        .ToListAsync();
                }
                else
                {
                    return await _context.Blogs
                        .Include(p => p.Category)
                        .Include(p => p.Comments)
                        .ToListAsync();
                }
            }
            else
            {
                if (categoryId.HasValue)
                {
                    return await _context.Blogs
                        .Where(p => p.CategoryId == categoryId)
                        .ToListAsync();
                }
                else
                {
                    return await _context.Blogs
                        .ToListAsync();
                }
            }
        }

        public async Task<Blog> GetBlogByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Blogs
                    .Include(p => p.Category)
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.BlogId == id);
            }

            return await _context.Blogs
                .FirstOrDefaultAsync(p => p.BlogId == id);
        }
        #endregion

        #region Categories
        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Categories
                    .Include(p => p.Blogs)
                    .ToListAsync();
            }

            return await _context.Categories
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Categories
                    .Include(p => p.Blogs)
                    .FirstOrDefaultAsync(p => p.CategoryId == id);
            }

            return await _context.Categories
                .FirstOrDefaultAsync(p => p.CategoryId == id);
        }
        #endregion

        #region Comments
        public async Task<IEnumerable<Comment>> GetCommentsAsync(int blogid, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Comments
                    .Include(p => p.Blog)
                    .Where(p => p.BlogId == blogid)
                    .ToListAsync();
            }
            return await _context.Comments
                .Where(p => p.BlogId == blogid)
                .ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Comments
                    .Include(p => p.Blog)
                    .FirstOrDefaultAsync(p => p.CommentId == id);
            }

            return await _context.Comments
                .FirstOrDefaultAsync(p => p.CommentId == id);
        }
        #endregion

        #region Area Units
        public async Task<IEnumerable<AreaUnit>> GetAreaUnitsAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.AreaUnits
                    .Include(p => p.Properties)
                    .ToListAsync();
            }

            return await _context.AreaUnits
                .ToListAsync();
        }

        public async Task<AreaUnit> GetAreaUnitByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.AreaUnits
                    .Include(p => p.Properties)
                    .FirstOrDefaultAsync(p => p.AreaUnitId == id);
            }

            return await _context.AreaUnits
                .FirstOrDefaultAsync(p => p.AreaUnitId == id);
        }
        #endregion

        #region Cities
        public async Task<IEnumerable<City>> GetCitiesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Cities
                    .Include(p => p.Properties)
                    .ToListAsync();
            }

            return await _context.Cities
                .ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Cities
                    .Include(p => p.Properties)
                    .FirstOrDefaultAsync(p => p.CityId == id);
            }

            return await _context.Cities
                .FirstOrDefaultAsync(p => p.CityId == id);
        }
        #endregion

        #region Property Types
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyTypes
                    .Include(p => p.Properties)
                    .ToListAsync();
            }

            return await _context.PropertyTypes
                .ToListAsync();
        }

        public async Task<PropertyType> GetPropertyTypeByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyTypes
                    .Include(p => p.Properties)
                    .FirstOrDefaultAsync(p => p.PropertyTypeId == id);
            }

            return await _context.PropertyTypes
                .FirstOrDefaultAsync(p => p.PropertyTypeId == id);
        }
        #endregion

        #region Purposes
        public async Task<IEnumerable<Purpose>> GetPurposesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Purposes
                    .Include(p => p.Properties)
                    .ToListAsync();
            }

            return await _context.Purposes
                .ToListAsync();
        }

        public async Task<Purpose> GetPurposeByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Purposes
                    .Include(p => p.Properties)
                    .FirstOrDefaultAsync(p => p.PurposeId == id);
            }

            return await _context.Purposes
                .FirstOrDefaultAsync(p => p.PurposeId == id);
        }
        #endregion

        #region Properties
        public async Task<IEnumerable<Property>> GetPropertiesAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Properties
                    .Include(p => p.PropertyRequests)
                    .Include(p => p.AreaUnit)
                    .Include(p => p.City)
                    .Include(p => p.PropertyType)
                    .Include(p => p.Purpose)
                    .Include(p => p.PropertyPhotos)
                    .ToListAsync();
            }

            return await _context.Properties
                .ToListAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Properties
                    .Include(p => p.PropertyRequests)
                    .Include(p => p.AreaUnit)
                    .Include(p => p.City)
                    .Include(p => p.PropertyType)
                    .Include(p => p.Purpose)
                    .Include(p => p.PropertyPhotos)
                    .FirstOrDefaultAsync(p => p.PropertyId == id);
            }

            return await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == id);
        }
        #endregion

        #region Property Photos
        public async Task<IEnumerable<PropertyPhoto>> GetPropertyPhotosAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyPhotos
                    .Include(p => p.Property)
                    .ToListAsync();
            }

            return await _context.PropertyPhotos
                .ToListAsync();
        }

        public async Task<PropertyPhoto> GetPropertyPhotoByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyPhotos
                    .Include(p => p.Property)
                    .FirstOrDefaultAsync(p => p.PropertyPhotoId == id);
            }

            return await _context.PropertyPhotos
                .FirstOrDefaultAsync(p => p.PropertyPhotoId == id);
        }
        #endregion

        #region Property Requests
        public async Task<IEnumerable<PropertyRequest>> GetPropertyRequestsAsync(int propertyId, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyRequests
                    .Include(p => p.Property)
                    .Where(p => p.PropertyId == propertyId)
                    .ToListAsync();
            }

            return await _context.PropertyRequests
                    .Where(p => p.PropertyId == propertyId)
                    .ToListAsync();
        }

        public async Task<PropertyRequest> GetPropertyRequestByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.PropertyRequests
                    .Include(p => p.Property)
                    .FirstOrDefaultAsync(p => p.PropertyRequestId == id);
            }

            return await _context.PropertyRequests
                .FirstOrDefaultAsync(p => p.PropertyRequestId == id);
        }
        #endregion

        #region Service Requests
        public async Task<IEnumerable<ServiceRequest>> GetServiceRequestsAsync(int serviceId, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.ServiceRequests
                    .Include(p => p.Service)
                    .Where(p => p.ServiceId == serviceId)
                    .ToListAsync();
            }

            return await _context.ServiceRequests
                    .Where(p => p.ServiceId == serviceId)
                    .ToListAsync();
        }

        public async Task<ServiceRequest> GetServiceRequestByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.ServiceRequests
                    .Include(p => p.Service)
                    .FirstOrDefaultAsync(p => p.ServiceRequestId == id);
            }

            return await _context.ServiceRequests
                .FirstOrDefaultAsync(p => p.ServiceId == id);
        }
        #endregion

        #region Contacts
        public async Task<IEnumerable<Contact>> GetContactsAsync(bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Contacts
                    .ToListAsync();
            }

            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id, bool includeDetails = false)
        {
            if (includeDetails)
            {
                return await _context.Contacts
                    .FirstOrDefaultAsync(p => p.ContactId == id);
            }

            return await _context.Contacts
                .FirstOrDefaultAsync(p => p.ContactId == id);
        }
        #endregion
    }
}
