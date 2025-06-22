using AWS.Dto.AwsDtos;
using AWS.Services;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingImageCommand
{
      public sealed class AddTrainingImageCommand : IRequest
    {
        public AddTrainingImageCommand(TrainingImage trainingImage, IFormFile imageFile)
        {
            TrainingImage = trainingImage;
            ImageFile = imageFile;
        }

        public TrainingImage TrainingImage { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class AddTrainingImageCommandHandler : IRequestHandler<AddTrainingImageCommand>
    {
        private readonly ITrainingImageRepository _trainingImageRepository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public AddTrainingImageCommandHandler(ITrainingImageRepository trainingImageRepository, IConfiguration config, IStorageService storageService)
        {
            _trainingImageRepository = trainingImageRepository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(AddTrainingImageCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await request.ImageFile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(request.ImageFile.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                    // 




                    if (xresult.Message.Contains("200"))
                    {
                        request.TrainingImage.Url = xresult.Url;
                        request.TrainingImage.Key = xresult.Key;
                    }

                }
                catch (Exception c)
                {

                }
            }

            await _trainingImageRepository.AddAsync(request.TrainingImage);


        }
    }
}
