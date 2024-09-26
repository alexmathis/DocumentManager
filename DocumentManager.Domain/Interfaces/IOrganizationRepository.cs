using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;
public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(int id);  // Retrieves an Organization by Id, nullable if not found
    Task<IEnumerable<Organization>> GetAllAsync();  // Retrieves all Organizations
    void Insert(Organization organization);  // Adds a new Organization
    void Update(Organization organization);  // Updates an existing Organization
    void Delete(Organization organization);  // Deletes an Organization
    Task SaveChangesAsync();  // Saves changes to the database
}
