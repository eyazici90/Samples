﻿using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity.Domain
{
    public abstract class IdentityUserEntity : IdentityUserEntity<int>, IAggregateRoot, IEntity
    {
    }
}
