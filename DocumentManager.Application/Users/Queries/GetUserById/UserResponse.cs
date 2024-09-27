using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Application.Users.Queries.GetUserById;
public sealed record UserResponse(int Id, string Email, int OrganizationId);


