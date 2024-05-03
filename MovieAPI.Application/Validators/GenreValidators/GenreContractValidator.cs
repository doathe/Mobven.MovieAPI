﻿using FluentValidation;
using MovieAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.Application.Validators.GenreValidators
{
    public class GenreContractValidator : AbstractValidator<GenreContract>
    {
        public GenreContractValidator() 
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("Name is required!");
            
        }
    }
}