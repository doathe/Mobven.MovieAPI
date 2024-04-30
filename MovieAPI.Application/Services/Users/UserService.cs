﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieAPI.Application.DTOs;
using MovieAPI.Application.Interfaces;
using MovieAPI.Domain.Entities;
using MovieAPI.Domain.Repositories;

namespace MovieAPI.Application.Services;

public class UserService : BaseService<User, UserContract>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IGenericRepository<User> repository, IMapper mapper, IUserRepository userRepository, IAuthService authService, IPasswordHasher<User> passwordHasher) : base(repository, mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _authService = authService;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserGetContract> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserGetContract>(user);
    }

    public async Task<UserGetCommentContract> GetUserCommentAsync(int id)
    {
        var user = await _userRepository.GetUserCommentAsync(id);
        return _mapper.Map<UserGetCommentContract>(user);
    }

    public async Task<UserGetRateContract> GetUserRateAsync(int id)
    {
        var user = await _userRepository.GetUserRateAsync(id);
        return _mapper.Map<UserGetRateContract>(user);
    }

    public async Task<List<UserGetContract>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserGetContract>>(users);
    }

    public async Task<string> LoginUserAsync(UserLoginContract requestModel)
    {
        var user = await _userRepository.UserLoginCheckAsync(requestModel.Email, requestModel.Password);
        return _authService.Token(user);

    }

    public async Task<bool> UpdateUserAsync(UserContract requestModel)
    {
        var model = _mapper.Map<User>(requestModel);
        return await _userRepository.UpdateAsync(model);
    }

    public async Task RegisterUserAsync(UserRegisterContract requestModel)
    {
        var entity = _mapper.Map<User>(requestModel);
        await _userRepository.UserRegistration(entity);
    }
}
