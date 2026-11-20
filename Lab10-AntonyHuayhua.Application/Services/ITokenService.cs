using System;
using System.Collections.Generic;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Application.Port.Services;

public interface ITokenService
{
    string GenerateToken(User user, IEnumerable<string> roles);
}
