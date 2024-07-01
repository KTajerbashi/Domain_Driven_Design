using DDD.Core.Domain.Library.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents.Courses;

public sealed record AdminCreated(Guid BusinessId,string Title,int RoleId) :IDomainEvent;
