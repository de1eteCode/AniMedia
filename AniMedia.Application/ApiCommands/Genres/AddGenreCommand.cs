﻿using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Genres.Dtos;
using AniMedia.Domain.Models.Genres.Requests;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.Genres; 

/// <summary>
/// Добавление жанра аниме сериала
/// </summary>
[ApplicationAuthorize]
public record AddGenreCommand(AddGenreRequest Model) : IRequest<Result<GenreDto>>;

public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, Result<GenreDto>> {

    private readonly IApplicationDbContext _context;

    public AddGenreCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<GenreDto>> Handle(AddGenreCommand request, CancellationToken cancellationToken) {
        var genre = new GenreEntity(request.Model.Name);

        await _context.Genres.AddAsync(genre, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<GenreDto>(new GenreDto(genre));
    }
}

public class AddGenreCommandValidator : AbstractValidator<AddGenreCommand> {

    public AddGenreCommandValidator() {
        RuleFor(e => e.Model).NotNull();
        RuleFor(e => e.Model.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(64);
    }
}