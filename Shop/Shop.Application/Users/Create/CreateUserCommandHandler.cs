﻿using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Create;

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainUserService _domainUserService;
    public CreateUserCommandHandler(IUserRepository userRepository, IDomainUserService domainUserService)
    {
        _userRepository = userRepository;
        _domainUserService = domainUserService;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = Sha256Hasher.Hash(request.Password);
        var user = new User(request.Name, request.Family, request.PhoneNumber, password, request.Email,
            request.Gender, _domainUserService);

        _userRepository.Add(user);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}