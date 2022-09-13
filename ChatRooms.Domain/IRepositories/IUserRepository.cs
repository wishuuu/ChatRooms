﻿using ChatRooms.Domain.SearchCriterias;

namespace ChatRooms.Domain;

public interface IUserRepository : IEntityRepository<User>
{
    Task<IEnumerable<User>> ListAsync(UserSearchCriteria spec);
}