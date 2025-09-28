using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;
using BillsControl.Infrastructure.RepositoryHelpers;
using Dapper;
using Npgsql;

namespace BillsControl.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a repository for managing personal bills. Implements the data access related to updating and retrieving data.
    /// Uses <see cref="NpgsqlConnection"/> with connection string for create connection.
    /// </summary>
    /// <param name="connectionString">Connection string that is used to create connection.</param>
    public class PersonalBillsRepository(string connectionString) : IPersonalBillsRepository
    {
        private NpgsqlConnection CreateConnection() => new(connectionString);
        
        /// <inheritdoc/>
        public async Task<IEnumerable<PersonalBillEntity>> GetFiltered(
            PersonalBillsQueryFilterParams personalBillsQueryFilterParams, FullNameDto fullNameDto)
        {
            var (page, pageSize, withResidents, address, billNumber, openDate, _) = personalBillsQueryFilterParams;
            var sqlConditionsList = new List<string>();
            var sqlJoinLine = "";
            
            await using var connection = CreateConnection();
            SqlQueryBuilder.AddConditionToList(sqlConditionsList, "bill_number", billNumber);
            SqlQueryBuilder.AddConditionToList(sqlConditionsList, "address", address);
            SqlQueryBuilder.AddConditionToList(sqlConditionsList, "open_date", openDate);

            if (SqlQueryBuilder.NeedsJoin(fullNameDto, withResidents))
            {
                sqlJoinLine = "JOIN residents r ON r.personal_bill_id = pb.id ";
                SqlQueryBuilder.AddConditionToList(sqlConditionsList, "r.last_name", fullNameDto.LastName);
                SqlQueryBuilder.AddConditionToList(sqlConditionsList, "r.first_name", fullNameDto.FirstName);
                SqlQueryBuilder.AddConditionToList(sqlConditionsList, "r.middle_name", fullNameDto.MiddleName);
            }

            var sqlConditionsString = SqlQueryBuilder.BuildWhereQuery(sqlConditionsList);
            var sqlPaginationQuery = SqlQueryBuilder.AddPagination(page, pageSize);
            var sql = "SELECT " + 
                      $"pb.id AS {nameof(PersonalBillEntity.Id)}, " + 
                      $"pb.bill_number AS {nameof(PersonalBillEntity.BillNumber)}, " + 
                      $"pb.open_date AS {nameof(PersonalBillEntity.OpenDate)}, " +
                      $"pb.close_date AS {nameof(PersonalBillEntity.CloseDate)}, " +
                      $"pb.is_closed AS {nameof(PersonalBillEntity.IsClosed)}, " +
                      $"pb.address AS {nameof(PersonalBillEntity.Address)}, " +
                      $"pb.place_area AS {nameof(PersonalBillEntity.PlaceArea)} " +
                      $"FROM personal_bills pb " +
                      $"{sqlJoinLine} " +
                      $"{sqlConditionsString} " +
                      $"{sqlPaginationQuery}";
            
            return await connection.QueryAsync<PersonalBillEntity>(sql);
        }

        /// <inheritdoc/>
        public async Task<PersonalBillEntity?> GetByBillId(Guid? id)
        {
            await using var connection = CreateConnection();
            const string sql = $"SELECT " +
                               $"id AS {nameof(PersonalBillEntity.Id)}, " +
                               $"bill_number AS {nameof(PersonalBillEntity.BillNumber)}, " +
                               $"open_date AS {nameof(PersonalBillEntity.OpenDate)}, " +
                               $"close_date AS {nameof(PersonalBillEntity.CloseDate)}, " +
                               $"is_closed AS {nameof(PersonalBillEntity.IsClosed)}, " +
                               $"address AS {nameof(PersonalBillEntity.Address)}, " +
                               $"place_area AS {nameof(PersonalBillEntity.PlaceArea)} " +
                               $"FROM personal_bills WHERE id = @{nameof(PersonalBillEntity.Id)}";
            
            return await connection.QueryFirstOrDefaultAsync<PersonalBillEntity>(sql, new { Id = id });
        }

        /// <inheritdoc/>
        public async Task<Guid> AddBill(PersonalBillEntity billEntity)
        {
            await using var connection = CreateConnection();
            const string sql = $"INSERT INTO personal_bills VALUES " +
                               $"(@{nameof(PersonalBillEntity.Id)}, " +
                               $"@{nameof(PersonalBillEntity.BillNumber)}, " +
                               $"@{nameof(PersonalBillEntity.OpenDate)}, " +
                               $"@{nameof(PersonalBillEntity.CloseDate)}, " +
                               $"@{nameof(PersonalBillEntity.IsClosed)}, " +
                               $"@{nameof(PersonalBillEntity.Address)}, " +
                               $"@{nameof(PersonalBillEntity.PlaceArea)}) " +
                               $"RETURNING id";
            
            return await connection.QuerySingleAsync<Guid>(sql, billEntity);
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBill(PersonalBillEntity updatedBillEntity)
        {
            await using var connection = CreateConnection();
            const string sql = $"UPDATE personal_bills SET " +
                               $"bill_number = @{nameof(PersonalBillEntity.BillNumber)}, " +
                               $"open_date = @{nameof(PersonalBillEntity.OpenDate)}, " +
                               $"close_date = @{nameof(PersonalBillEntity.CloseDate)}, " +
                               $"is_closed = @{nameof(PersonalBillEntity.IsClosed)}, " +
                               $"address =  @{nameof(PersonalBillEntity.Address)}, " +
                               $"place_area = @{nameof(PersonalBillEntity.PlaceArea)} " +
                               $"WHERE id = @{nameof(PersonalBillEntity.Id)} " +
                               $"RETURNING id";
            
            return await connection.QuerySingleAsync<Guid>(sql, updatedBillEntity);
        }
        
        /// <inheritdoc/>
        public async Task<Guid> DeleteBill(Guid id)
        {
            await using var connection = CreateConnection();
            const string subSqlFirst = "UPDATE residents SET " + 
                                       "personal_bill_number = '' " +
                                       $"WHERE id = @{nameof(ResidentEntity.Id)}";
            
            const string subSqlSecond = "DELETE FROM personal_bills " + 
                                        $"WHERE id = @{nameof(PersonalBillEntity.Id)}" + 
                                        "RETURNING id";
            
            const string sqlTransaction = $"BEGIN; " +
                                          $"{subSqlFirst}; " +
                                          $"{subSqlSecond}; " +
                                          $"COMMIT;";
            
            return await connection.QuerySingleAsync<Guid>(sqlTransaction, new { Id = id });
        }
    }
}
