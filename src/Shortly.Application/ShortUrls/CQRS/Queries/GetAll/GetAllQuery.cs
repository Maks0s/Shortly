﻿using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Queries.GetAll
{
    public record GetAllQuery() : IQuery<List<ShortUrl>>;
}