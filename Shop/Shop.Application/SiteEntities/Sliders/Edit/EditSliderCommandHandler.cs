﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilites;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.Sliders.Edit;

public class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository sliderRepository, IFileService fileService)
    {
        _sliderRepository = sliderRepository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetTracking(request.Id);
        if (slider == null)
            return OperationResult.NotFound();

        var imageName = slider.ImageName;
        var oldImage = slider.ImageName;
        if (request.ImageName != null)
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageName, Directories.BannerImage);
        slider.Edit(request.Link, imageName, request.Link);
        await _sliderRepository.Save();
        DeleteOldImage(request.ImageName, oldImage);
        return OperationResult.Success();
    }

    private void DeleteOldImage(IFormFile? imageName, string oldImage)
    {
        if (imageName != null)
        {
            _fileService.DeleteFile(Directories.SliderImage, oldImage);
        }
    }
}