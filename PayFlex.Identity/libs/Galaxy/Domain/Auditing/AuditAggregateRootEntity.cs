﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{ 
    public abstract class AuditAggregateRootEntity : AuditAggregateRootEntity<int>, IAggregateRoot, IEntity, IAudit
    {
    }
}
