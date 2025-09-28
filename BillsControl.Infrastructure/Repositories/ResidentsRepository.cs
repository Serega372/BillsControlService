using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;
using BillsControl.Infrastructure.RepositoryHelpers;
using Dapper;
using Npgsql;

namespace BillsControl.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a repository for managing residents. Implements the data access related to updating and retrieving data.
    /// Uses <see cref="NpgsqlConnection"/> with connection string for create connection.
    /// </summary>
    /// <param name="connectionString">Connection string that is used to create connection.</param>
    public class ResidentsRepository(string connectionString) : IResidentsRepository
    {
        private NpgsqlConnection CreateConnection() => new(connectionString);
        
        /// <inheritdoc/>
        public async Task<IEnumerable<ResidentEntity>> GetFiltered(ResidentsQueryFilterParams residentsQueryFilterParams)
        {
            var (page, pageSize, billId, billNumber) = residentsQueryFilterParams;
            var sqlConditionsList = new List<string>();
            
            await using var connection = CreateConnection();
            SqlQueryBuilder.AddConditionToList(sqlConditionsList, "personal_bill_id", billId);
            SqlQueryBuilder.AddConditionToList(sqlConditionsList, "personal_bill_number", billNumber);
            
            var sqlPaginationQuery = SqlQueryBuilder.AddPagination(page, pageSize);
            var sqlConditionsString = SqlQueryBuilder.BuildWhereQuery(sqlConditionsList);
            var sql = $"SELECT " +
                      $"id AS {nameof(ResidentEntity.Id)}, " +
                      $"personal_bill_number AS {nameof(ResidentEntity.PersonalBillNumber)}, " +
                      $"first_name AS {nameof(ResidentEntity.FirstName)}, " +
                      $"last_name AS {nameof(ResidentEntity.LastName)}, " +
                      $"middle_name AS {nameof(ResidentEntity.MiddleName)}, " +
                      $"personal_bill_id AS {nameof(ResidentEntity.PersonalBillId)}, " +
                      $"is_owner AS {nameof(ResidentEntity.IsOwner)} " +
                      $"FROM residents " +
                      $"{sqlConditionsString} " +
                      $"{sqlPaginationQuery}";
            
            return await connection.QueryAsync<ResidentEntity>(sql);
        }
        
        /// <inheritdoc/>
        public async Task<ResidentEntity?> GetByResidentId(Guid id)
        {
            await using var connection = CreateConnection();
            const string sql = $"SELECT " +
                               $"id AS {nameof(ResidentEntity.Id)}, " +
                               $"personal_bill_number AS {nameof(ResidentEntity.PersonalBillNumber)}, " +
                               $"first_name AS {nameof(ResidentEntity.FirstName)}, " +
                               $"last_name AS {nameof(ResidentEntity.LastName)}, " +
                               $"middle_name AS {nameof(ResidentEntity.MiddleName)}, " +
                               $"personal_bill_id AS {nameof(ResidentEntity.PersonalBillId)}, " +
                               $"is_owner AS {nameof(ResidentEntity.IsOwner)} " +
                               $"FROM residents WHERE id = @{nameof(ResidentEntity.Id)}";
            
            return await connection.QueryFirstOrDefaultAsync<ResidentEntity>(sql, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task<Guid> AddResident(ResidentEntity residentEntity)
        {
            await using var connection = CreateConnection();
            const string sql = $"INSERT INTO residents VALUES " +
                               $"(@{nameof(ResidentEntity.Id)}, " +
                               $"@{nameof(ResidentEntity.PersonalBillNumber)}, " +
                               $"@{nameof(ResidentEntity.FirstName)}, " +
                               $"@{nameof(ResidentEntity.LastName)}, " +
                               $"@{nameof(ResidentEntity.MiddleName)}, " +
                               $"@{nameof(ResidentEntity.PersonalBillId)}, " +
                               $"@{nameof(ResidentEntity.IsOwner)}) " +
                               $"RETURNING id";
            
            return await connection.QuerySingleAsync<Guid>(sql, residentEntity);
        }
        
        /// <inheritdoc/>v
        public async Task<Guid> UpdateResident(ResidentEntity updatedResidentEntity)
        {
            await using var connection = CreateConnection();
            const string sql = $"UPDATE residents SET " +
                               $"personal_bill_number = @{nameof(ResidentEntity.PersonalBillNumber)}, " +
                               $"first_name = @{nameof(ResidentEntity.FirstName)}, " +
                               $"last_name = @{nameof(ResidentEntity.LastName)}, " +
                               $"middle_name = @{nameof(ResidentEntity.MiddleName)}, " +
                               $"personal_bill_id = @{nameof(ResidentEntity.PersonalBillId)}, " +
                               $"is_owner = @{nameof(ResidentEntity.IsOwner)} " +
                               $"WHERE id = @{nameof(ResidentEntity.Id)} " +
                               $"RETURNING id";
            
            return await connection.QuerySingleAsync<Guid>(sql, updatedResidentEntity);
        }

        /// <inheritdoc/>
        public async Task<Guid> DeleteResident(Guid id)
        {
            await using var connection = CreateConnection();
            const string sql = "DELETE FROM residents " + 
                               $"WHERE id = @{nameof(ResidentEntity.Id)}" + 
                               "RETURNING id";
            
            return await connection.QuerySingleAsync<Guid>(sql, new { Id = id });
        }
    }
}
