using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces;
using Application.Interfaces.IFileStorageService;
using Application.Interfaces.IQuestionRepository;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class CreateFullQuestionCommandHandler : IRequestHandler<CreateFullQuestionCommand, int>
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IQuestionRepository _questionImageRepository;
        private readonly IFileStorageService _fileStorageService;

        public CreateFullQuestionCommandHandler(
            IRepository<Question> questionRepository,
            IFileStorageService fileStorageService,
            IQuestionRepository questionImageRepository)
        {
            _questionRepository = questionRepository;
            _fileStorageService = fileStorageService;
            _questionImageRepository = questionImageRepository;
        }

        public async Task<int> Handle(CreateFullQuestionCommand request, CancellationToken cancellationToken)
        {
            // 1. Soru nesnesini oluştur
            var question = new Question
            {
                Text = request.QuestionText,
                OptionA = request.OptionA,
                OptionB = request.OptionB,
                OptionC = request.OptionC,
                OptionD = request.OptionD,
                OptionE = request.OptionE,
                TestID= request.TestId,
                Answer= request.Answer,
            };

            await _questionRepository.CreateAsync(question); // Buradaki metodun AddAsync olduğundan emin olun

            // 2. Görselleri yükle
            async Task SaveImage(IFormFile? file, QuestionImageType type)
            {
                if (file == null) return;

                var path = await _fileStorageService.SaveFileAsync(file, "question-images", cancellationToken);

                var image = new QuestionImage
                {
                    QuestionID = question.QuestionID,
                    ImageUrl = path,
                    Type = type
                };

                await _questionImageRepository.AddImgAsync(image);
            }

            await SaveImage(request.QuestionImage, QuestionImageType.Question);
            await SaveImage(request.OptionAImage, QuestionImageType.OptionA);
            await SaveImage(request.OptionBImage, QuestionImageType.OptionB);
            await SaveImage(request.OptionCImage, QuestionImageType.OptionC);
            await SaveImage(request.OptionDImage, QuestionImageType.OptionD);
            await SaveImage(request.OptionEImage, QuestionImageType.OptionE);

            return question.QuestionID;
        }
    }
}
