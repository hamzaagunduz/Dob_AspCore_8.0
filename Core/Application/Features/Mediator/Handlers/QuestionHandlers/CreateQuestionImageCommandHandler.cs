using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces.IFileStorageService;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.IQuestionRepository;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class CreateQuestionImageCommandHandler : IRequestHandler<CreateQuestionImageCommand, string>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IQuestionRepository _questionRepository;

        public CreateQuestionImageCommandHandler(IFileStorageService fileStorageService, IQuestionRepository questionRepository)
        {
            _fileStorageService = fileStorageService;
            _questionRepository = questionRepository;
        }

        public async Task<string> Handle(CreateQuestionImageCommand request, CancellationToken cancellationToken)
        {
            // Dosyayı kaydet
            var relativeImagePath = await _fileStorageService.SaveFileAsync(request.File, "question-images", cancellationToken);

            // Veritabanına kayıt için QuestionImage nesnesi oluştur
            var questionImage = new QuestionImage
            {
                ImageUrl = relativeImagePath,
                QuestionID = request.QuestionID,
                Type = request.Type
            };

            // Repository aracılığıyla veritabanına ekle
            await _questionRepository.AddImgAsync(questionImage);

            return relativeImagePath;
        }
    }
}
